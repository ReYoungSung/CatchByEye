using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class AIBehavior : MonoBehaviour
{
    public List<Transform> targets; // Ÿ������ ������ ��ü�� Transform ������Ʈ ����Ʈ
    public float rotationSpeed = 3f; // ȸ�� �ӵ�
    private float speedIncrement = 0.3f; // �ӵ� ������ 

    private NavMeshAgent agent;

    [SerializeField] private GameObject mainCanvas;
    private GameTimer gameTimer;

    [SerializeField] private GameObject mainCamera;
    private CameraMovement cameraMovement;

    private Rigidbody rigid;

    private int startNumofTarget;

    private int currentNumofTarget;

    private bool outOfArea = true;
    private float missingTime = 20;
    private Coroutine FindingRobberCoroutine = null;

    public AudioSource DingSound;
    public AudioSource MissingSound;

    private bool isTriggerEnter = false;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        gameTimer = mainCanvas.GetComponent<GameTimer>();
        cameraMovement = mainCamera.GetComponent<CameraMovement>();

        targets = new List<Transform>();
    }

    private void Start()
    {
        // Targets ������Ʈ�� �ڽ� ������Ʈ���� �ڵ����� �߰�
        Transform targetsObject = GameObject.Find("Targets").transform; 
        foreach (Transform child in targetsObject)
        {
            targets.Add(child);
        }

        startNumofTarget = targets.Count;
        agent = GetComponent<NavMeshAgent>();
    }

    private void FixedUpdate()
    {
        FreezeVelocity();
    }

    void FreezeVelocity()
    {
        rigid.velocity = Vector3.zero;
        rigid.angularVelocity = Vector3.zero;
    }

    private void Update()
    {
        //AI�� �̵� Ƚ���� ���� ī�޶� �̺�Ʈ ����
        if ((startNumofTarget - targets.Count) == 14)
        {
            if (cameraMovement.isMovingCoroutine == null)
            {
                cameraMovement.movementSpeed = 2.6f; 
                cameraMovement.isMovingCoroutine = cameraMovement.StartCoroutine("MoveToTarget");   
            }
        }
        else if ((startNumofTarget - targets.Count) == 33)
        {
            if (cameraMovement.isMovingCoroutine == null)
            {
                cameraMovement.movementSpeed = 5f;
                cameraMovement.isMovingCoroutine = cameraMovement.StartCoroutine("MoveToTarget");   
            }
        }
        else if ((startNumofTarget - targets.Count) == 39)
        {
            if (cameraMovement.isMovingCoroutine == null)
            {
                cameraMovement.movementSpeed = 3f;   
                cameraMovement.isMovingCoroutine = cameraMovement.StartCoroutine("MoveToTarget");   
            }
        }
        else if ((startNumofTarget - targets.Count) == 46)
        {
            if (cameraMovement.isMovingCoroutine == null)
            {
                cameraMovement.movementSpeed = 3f;   
                cameraMovement.isMovingCoroutine = cameraMovement.StartCoroutine("MoveToTarget");   
            }
        }

        //AI �̵�
        if (gameTimer.isPlayingGame == true && targets.Count > 0 && targets[0] != null)
        {
            if (!agent.isActiveAndEnabled)
            {
                return;  
            }

            // ���� Ÿ�� �������� ȸ��
            Vector3 targetDirection = targets[0].position - transform.position;  
            Quaternion targetRotation = Quaternion.LookRotation(-targetDirection);  
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime); 

            // NavMesh Agent�� ���� Ÿ������ �̵� 
            agent.SetDestination(targets[0].position); 
        }

        //20�� ���� ��ġ�� ���� �� ����
        if (outOfArea == true && missingTime > -1f)
        {
            //missingTime -= Time.deltaTime;

            if (missingTime < 0 && FindingRobberCoroutine == null)
            {
                FindingRobberCoroutine = gameTimer.StartCoroutine("FailGame");
            }
        }

        //���� ���� �Ҹ� ����
        if (MissingSound != null && DingSound != null && gameTimer.isPlayingGame == false)
        {
            MissingSound.Stop();
            DingSound.Stop();
        }

        //����ó��
        if(targets[0] == null)
        {
            targets.RemoveAt(0);
        }
    }

    // ���� Ÿ�ٿ� �����ϸ� Ÿ���� �ı��ϰ� ���� Ÿ������ ����
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Target" && isTriggerEnter == false)
        {
            isTriggerEnter = true;
            Invoke("delayToConvert", 0.05f);
            targets.RemoveAt(0);
            Destroy(other.gameObject);

            if (0 >= targets.Count)
            {
                gameTimer.StartCoroutine("ClearGame");  // ������ Ÿ���̸� Ŭ����
            }

            // �ӵ� ����
            agent.speed += speedIncrement;
        }

        if (other.transform.tag == "SearchLightArea")
        {
            outOfArea = false;
            if (DingSound != null && gameTimer.isPlayingGame == true)
            {
                MissingSound.Stop();
                DingSound.Play();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "SearchLightArea")
        {
            outOfArea = true;
            if (MissingSound != null && gameTimer.isPlayingGame == true)
            {
                MissingSound.Play();
            }
        }
    }

    private void delayToConvert()
    {
        isTriggerEnter = false;
    }
}





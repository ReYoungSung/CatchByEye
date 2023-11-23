using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class AIBehavior : MonoBehaviour
{
    public List<Transform> targets; // 타겟으로 지정할 물체의 Transform 컴포넌트 리스트
    public float rotationSpeed = 3f; // 회전 속도
    private float speedIncrement = 0.3f; // 속도 증가량 

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
        // Targets 오브젝트의 자식 오브젝트들을 자동으로 추가
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
        //AI의 이동 횟수에 따라 카메라 이벤트 실행
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

        //AI 이동
        if (gameTimer.isPlayingGame == true && targets.Count > 0 && targets[0] != null)
        {
            if (!agent.isActiveAndEnabled)
            {
                return;  
            }

            // 현재 타겟 방향으로 회전
            Vector3 targetDirection = targets[0].position - transform.position;  
            Quaternion targetRotation = Quaternion.LookRotation(-targetDirection);  
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime); 

            // NavMesh Agent를 통해 타겟으로 이동 
            agent.SetDestination(targets[0].position); 
        }

        //20초 동안 놓치고 있을 때 실패
        if (outOfArea == true && missingTime > -1f)
        {
            //missingTime -= Time.deltaTime;

            if (missingTime < 0 && FindingRobberCoroutine == null)
            {
                FindingRobberCoroutine = gameTimer.StartCoroutine("FailGame");
            }
        }

        //게임 이후 소리 차단
        if (MissingSound != null && DingSound != null && gameTimer.isPlayingGame == false)
        {
            MissingSound.Stop();
            DingSound.Stop();
        }

        //예외처리
        if(targets[0] == null)
        {
            targets.RemoveAt(0);
        }
    }

    // 현재 타겟에 도달하면 타겟을 파괴하고 다음 타겟으로 변경
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
                gameTimer.StartCoroutine("ClearGame");  // 마지막 타겟이면 클리어
            }

            // 속도 증가
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





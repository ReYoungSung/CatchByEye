using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMoving : MonoBehaviour
{
    [SerializeField] private GameObject mainCanvas;
    public GameObject blockPrefab;
    public float cloneTime = 0.6f;
    public Transform targetPos;
    //public GameObject targetObj;
    public float movingSpeed = 4f;
    public GameObject robberPrefab;
    public int randomPer = 5;
    public float gameStartTime = 8f;

    public GameObject[] lifes;

    private float lastCloneTime;
    private GameObject randomPrefab;
    private float curTime;
    private GameTimer gameTimer;
    private Coroutine FindingRobberCoroutine = null;
    
    private int life = 3;  // life 변수

    void Start()
    {
        if(mainCanvas!=null)
            gameTimer = mainCanvas.GetComponent<GameTimer>();
        lastCloneTime = Time.time;
    }

    void Update()
    {
        curTime += Time.deltaTime;
        if(Time.time - lastCloneTime >= cloneTime){
            lastCloneTime = Time.time;

            if(robberPrefab == null){       // 백그라운드 복제
                GameObject newBlock = Instantiate(blockPrefab, transform.position, Quaternion.identity);
                StartCoroutine(MovingAnimation(newBlock));
                //Destroy(mainCanvas);
            }

            else{
                if(Random.Range(0, 10) < randomPer){    // 30%의 확률로 robberPrefab 복제
                    randomPrefab = robberPrefab;
                }
                else{
                    randomPrefab = blockPrefab;
                }

                if(curTime >= gameStartTime){
                    GameObject newBlock = Instantiate(randomPrefab, transform.position, Quaternion.identity);
                    StartCoroutine(MovingAnimation(newBlock));
                }
            }
        }
    }

    IEnumerator MovingAnimation(GameObject block){
        float startTime = Time.time;
        Vector3 startPos = block.transform.position;       // 블록 프리팹의 현재 위치
        Vector3 endPos = targetPos.position;                // target 위치

        while (block != null && block.transform.position.x > endPos.x)
        {
            if (block == null)
                yield break;

            float distance = movingSpeed * Time.deltaTime;
            block.transform.position = Vector3.MoveTowards(block.transform.position, endPos, distance);

            yield return null;
        }

        if (block != null && mainCanvas!=null && lifes != null)      
        {
            if(block.CompareTag("Robber")){         // 얘는 block 태그여서 오류, 얘를 고쳐야 함
                life --;
                Debug.Log(life);
                //this.lifes[life].SetActive(false);
                if(life==0){
                    FindingRobberCoroutine = gameTimer.StartCoroutine("FailGame");

                    Debug.Log("Game Over");
                }
                Debug.Log("lose life TT");
            }

            Destroy(block);
        }
    } 
}
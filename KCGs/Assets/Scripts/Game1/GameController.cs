using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
        [SerializeField] private GameObject ScoreWindow;

    public GameObject[] robbers; // 도둑들을 담을 배열

    public float robberShowInterval; // 초기 도둑이 나오는 시간 간격
    public float intervalDecrement; // 시간 간격 감소량
    public float robberHideTime; // 도둑이 숨는 시간
    public float hideTimeDecrement; // 도둑이 숨는 시간 감소량

    public int robberShowCount; // 도둑이 나오는 횟수
    private int robberCount = 0; // 도둑이 나온 횟수
    private bool gameRunning = true; // 게임 실행 여부
    private int score = 0; // 현재 점수

    public float gameTime = 120f; // 총 게임 시간
    private float timeElapsed = 0f; // 경과 시간

    private int currentRobberIndex = 0; // 현재 나와있는 도둑 인덱스

    // 문 열리는 소리를 재생할 AudioSource
    public AudioSource doorOpenSound;
    // 문 닫히는 소리를 재생할 AudioSource
    public AudioSource doorCloseSound;

    [SerializeField] List<GameObject> Stars = new List<GameObject>();


    void Start()
    {
        // 게임 시작 시 코루틴 실행
        StartCoroutine(RobberSpawner());
    }

    void Update()
    {
        if (gameRunning)
        {
            timeElapsed += Time.deltaTime;
            if (robberCount >= robberShowCount)
            {
                // 게임 시간 종료
                gameRunning = false;

                ActiveStars( Mathf.Ceil(5 * score/robberShowCount) );

                ClearGame();

                Debug.Log("Game Over (Score: " + score + ")");
                return;
            }
        }
    }

    IEnumerator RobberSpawner()
    {
         yield return new WaitForSeconds(10f);
        while (robberCount < robberShowCount) // 30회 반복
        {
            yield return new WaitForSeconds(robberShowInterval);

            // 도둑를 랜덤하게 선택하고 활성화
            int robberIndex = Random.Range(0, robbers.Length);
            robbers[robberIndex].SetActive(true);
            currentRobberIndex = robberIndex;

            // 문 열리는 소리 재생
            doorOpenSound.Play();

            yield return new WaitForSeconds(robberHideTime);

            // 만약 도둑이 활성화된 상태라면 비활성화
            if (robbers[robberIndex].activeSelf){
                // 문 닫히는 소리 재생
                doorCloseSound.Play();

            robbers[robberIndex].SetActive(false);
            }

            robberCount++;

            // 시간 간격 감소
            robberShowInterval -= intervalDecrement;
            robberShowInterval = Mathf.Max(robberShowInterval, 1f); // 최소 간격을 1초로 제한

            // 도둑이 숨는 시간 감소
            robberHideTime -= hideTimeDecrement;
            robberHideTime = Mathf.Max(robberHideTime, 1f); // 최소 시간을 0.5초로 제한
        }
    }

    // 점수 증가 함수
    public void IncreaseScore()
    {
        // 게임 실행 중이면 점수 증가
        if (gameRunning)
        {
            score++;
            // 모든 도둑를 비활성화
            foreach (GameObject robber in robbers)
            {
                robber.SetActive(false);
            }
            Debug.Log("Score: " + score);
        }
    }

    private void ActiveStars(float a)
    {
        for(int i = 0; i < a; i++)
        {
            if (Stars[i].activeSelf == false)
                Stars[i].SetActive(true);
        }
    }

    // 게임 종료 함수
    private void ClearGame()
    {
        // 모든 도둑를 비활성화
        foreach (GameObject robber in robbers)
        {
            robber.SetActive(false);
        }

        // 게임 종료 UI 활성화
        ScoreWindow.SetActive(true);

        // 5초 대기 후 다음 씬으로 이동 <================================= 여기에서 다음 씬으로 이동 대기시간 수정
        Invoke("GoToNextScene", 5f);
    }

    // 다음 씬으로 이동하는 함수
    private void GoToNextScene()
    {
        // 다음 씬으로 이동
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game2Scene"); // <====================== 다음 씬 이름 수정
    }
}

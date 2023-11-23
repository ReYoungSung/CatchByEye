using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public int totalScore = 0; // ����
    private GameTimer gameTimer;

    public float scoreIncreaseInterval = 10f; // ���� ���� ���� 
    public int scoreIncreaseAmount = 500; // ���� ������

    [SerializeField] List<GameObject> Stars = new List<GameObject>();

    [SerializeField] private Text ScoreText;

    public enum ScoreMode
    {
        Mode1_quick,
        Mode2_follow,
        Mode6_twitch
    }

    public ScoreMode currentScoreMode = ScoreMode.Mode1_quick;

    public Coroutine ScoreCoroutine = null;

    void Awake()
    {
        gameTimer = this.GetComponent<GameTimer>();
    }

    void Update()
    {
        ScoreText.text = totalScore.ToString();

        if (gameTimer.isPlayingGame == true)
        {
            if (currentScoreMode == ScoreMode.Mode1_quick || currentScoreMode == ScoreMode.Mode2_follow  || currentScoreMode == ScoreMode.Mode6_twitch) //���� ��Ƽ�� ������ ���̴� ����
            {
                if (Mathf.Ceil(gameTimer.timer) % scoreIncreaseInterval == 0)
                {
                    if (ScoreCoroutine == null)
                    {
                        ScoreCoroutine = StartCoroutine("IncreaseScore");
                    }
                }
            }
        }
        else
        {
            ActiveStars( Mathf.Ceil(totalScore/1000) ); //�ִ� ���� 5000 �������� ���� 1000�� �� �� �ϳ�
        }
    }

    IEnumerator IncreaseScore()
    {
        totalScore += scoreIncreaseAmount;

        yield return new WaitForSecondsRealtime(scoreIncreaseInterval/2);

        ScoreCoroutine = null;
    }

    //�Է��� ���� ���� ���� Ȱ��ȭ ���ִ� �Լ�
    private void ActiveStars(float a)
    {
        if(a >= 5)
        {
            a = 5;
        }

        for(int i = 0; i < a; i++)
        {
            if (Stars[i].activeSelf == false)
                Stars[i].SetActive(true);
        }
    }
}
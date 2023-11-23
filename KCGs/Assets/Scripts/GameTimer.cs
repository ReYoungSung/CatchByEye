using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    public float gameTime = 100f; // ���� ���� �ð� 

    public bool isPlayingGame = false;

    public float timer;

    [SerializeField] private Text timerText;

    public float startTimer = 10;  //���� ���� �� ����â ���� �ð�
    [SerializeField] private Text startTimerText;

    [SerializeField] private GameObject InfoWindow;
    [SerializeField] private GameObject ScoreWindow;
    private ScoreManager scoreManager;

    public AudioSource failSound = null;
    public AudioSource successSound = null;

    void Awake()
    {
        scoreManager = this.GetComponent<ScoreManager>();
    }

    void Start()
    {
        timer = gameTime;
    }

    void Update()
    {
        if (startTimer >= 0)
        {
            startTimer -= Time.deltaTime;
            startTimerText.text = Mathf.Ceil(startTimer).ToString();
        }
        else
        {
            if (InfoWindow.activeSelf == true)
                InfoWindow.SetActive(false);

            //Ÿ�̸� ����
            timer -= Time.deltaTime;

            if (timer > 0)
            { 
                isPlayingGame = true;
            }

            if (timer >= -1)
            {
                //�����ڿ� �ð� ǥ��
                timerText.text = Mathf.Ceil(timer).ToString();
            }

            if(timer <= 0 && scoreManager.currentScoreMode == ScoreManager.ScoreMode.Mode6_twitch){
                StartCoroutine("ClearGame");
            }
        }
    }

    IEnumerator ClearGame()
    {
        isPlayingGame = false;
        if(scoreManager.currentScoreMode != ScoreManager.ScoreMode.Mode6_twitch)
            yield return new WaitForSeconds(3.0f);
        if (successSound != null) successSound.Play();
        ScoreWindow.SetActive(true);
    }

    IEnumerator FailGame()
    {
        timer = -1;
        isPlayingGame = false;
        if(scoreManager.currentScoreMode != ScoreManager.ScoreMode.Mode6_twitch)
            yield return new WaitForSeconds(3.0f);
        if(failSound != null) failSound.Play();
        ScoreWindow.SetActive(true);
    }
}
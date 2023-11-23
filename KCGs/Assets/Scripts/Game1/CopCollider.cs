using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopCollider : MonoBehaviour
{
    // Game Controller Object 변수
    public GameObject gameController;
    // Game Script 변수
    private GameController gameControllerScript;

    // 잡는 소리를 재생할 AudioSource
    public AudioSource catchSound;

    // Start is called before the first frame update
    void Start()
    {
        gameControllerScript = gameController.GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Section에 들어갔을 때
    private void OnTriggerEnter(Collider other)
    {
        // Section에 부딪혔을 때
        if (other.gameObject.CompareTag("Section"))
        {
            // 점수 증가
            gameControllerScript.IncreaseScore();

            // 잡는 소리 재생
            catchSound.Play();

            // 점수 UI 갱신
            // gameController.UpdateScoreText();
        }
    }
}

using UnityEngine;

public class BackGroundAudioManager : MonoBehaviour
{
    public AudioSource[] audioSources; // 오디오 소스 배열
    public bool[] audioIsPlaying; // 각 오디오 소스의 재생 여부

    [SerializeField] private GameObject mainCanvas;
    private GameTimer gameTimer;

    private void Awake()
    {
        gameTimer = mainCanvas.GetComponent<GameTimer>();
        audioIsPlaying = new bool[audioSources.Length];
    }

    private void Update()
    {
        if (gameTimer.isPlayingGame == false)
        {
            for (int a = 0; a < audioSources.Length; a++)
            {
                if (audioIsPlaying[a])
                {
                    StopAudio(a);
                }
            }
        }
    }

    // 오디오를 재생하는 메서드
    public void PlayAudio(int audioIndex)
    {
        if (audioIndex >= 0 && audioIndex < audioSources.Length)
        {
            if (!audioIsPlaying[audioIndex])
            {
                audioSources[audioIndex].Play();
                audioIsPlaying[audioIndex] = true;
            }
        }
        else
        {
            Debug.LogWarning("Invalid audio index: " + audioIndex);
        }
    }

    // 오디오를 중지하는 메서드
    public void StopAudio(int audioIndex)
    {
        if (audioIndex >= 0 && audioIndex < audioSources.Length)
        {
            if (audioIsPlaying[audioIndex])
            {
                audioSources[audioIndex].Stop();
                audioIsPlaying[audioIndex] = false;
            }
        }
        else
        {
            Debug.LogWarning("Invalid audio index: " + audioIndex);
        }
    }
}

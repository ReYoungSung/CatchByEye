using UnityEngine;

public class BackGroundAudioManager : MonoBehaviour
{
    public AudioSource[] audioSources; // ����� �ҽ� �迭
    public bool[] audioIsPlaying; // �� ����� �ҽ��� ��� ����

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

    // ������� ����ϴ� �޼���
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

    // ������� �����ϴ� �޼���
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

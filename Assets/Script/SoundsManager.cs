using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundsManager : MonoBehaviour
{
    public static SoundsManager Instance;
    public AudioSource BackgroundBGM;
    public AudioClip mainBGM;
    public AudioClip battleBGM;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            SceneManager.sceneLoaded += OnSceneLoaded; // �� �ε� �̺�Ʈ ����
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayMainBGM();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MonsterBattle")
        {
            PlayBattleBGM();
        }
        else if (scene.name == "MainMenu" || scene.name == "SelectDifficulty" || scene.name == "MapSelect")
        {
            PlayMainBGM();
        }
        else
        {
            // �ʿ�� �⺻ BGM ���
            PlayMainBGM();
        }
    }

    private void PlayMainBGM()
    {
        if (BackgroundBGM.clip != mainBGM)
        {
            BackgroundBGM.clip = mainBGM;
            BackgroundBGM.Play();
        }
    }

    private void PlayBattleBGM()
    {
        if (BackgroundBGM.clip != battleBGM)
        {
            BackgroundBGM.clip = battleBGM;
            BackgroundBGM.Play();
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
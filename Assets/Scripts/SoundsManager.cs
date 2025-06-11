using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsManager : MonoBehaviour
{
    public static SoundsManager Instance; // �̱��� �ν��Ͻ�
    public AudioSource BackgroundBGM;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �� ������Ʈ�� ���� �ٲ� ����
        }
        else if (Instance != this)
        {
            Destroy(gameObject); // �̹� �����ϴ� �ν��Ͻ��� �ִٸ� �ڽ� �ı�
        }
    }

    void Start()
    {
        if (!BackgroundBGM.isPlaying)
        {
            BackgroundBGM.Play();
        }
    }
}
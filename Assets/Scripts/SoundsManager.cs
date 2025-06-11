using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsManager : MonoBehaviour
{
    public static SoundsManager Instance; // 싱글턴 인스턴스
    public AudioSource BackgroundBGM;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 이 오브젝트는 씬이 바뀌어도 유지
        }
        else if (Instance != this)
        {
            Destroy(gameObject); // 이미 존재하는 인스턴스가 있다면 자신 파괴
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
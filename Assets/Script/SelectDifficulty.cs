using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class SelectDifficulty : MonoBehaviour
{
    public AudioSource ClickButton;

    public GameObject SelectPOPUP;

    public GameObject BackButton;

    public Animation[] animations;  // Normal, Hard, Hell, Ruin 순서대로

    void Start()
    {
        ClickButton.Play();

        // 처음엔 모두 애니메이션 정지
        foreach (var anim in animations)
        {
            anim.Stop();
        }

        StartCoroutine(PlaySequentially());
    }

    private void Update()
    {

    }
    IEnumerator PlaySequentially()
    {
        for (int i = 0; i < animations.Length; i++)
        {
            Animation anim = animations[i];
            AnimationClip clip = anim.clip;

            // 나머지 애니메이션 끄기
            for (int j = 0; j < animations.Length; j++)
            {
                if (j != i)
                {
                    animations[j].Stop();
                }
            }

            // 현재 애니메이션 재생
            anim.Play(clip.name);

            // 애니메이션 길이만큼 대기
            yield return new WaitForSeconds(clip.length);

            // 재생 끝나면 현재 애니메이션도 끄기
            anim.Stop();
        }

        Debug.Log("모든 난이도 애니메이션 재생 완료");
    }

    public void NormalOnClick()
    {
        ClickButton.Play();
        SelectPOPUP.SetActive(true);
        BackButton.SetActive(false);
    }

    public void HardOnClick()
    {

    }

    public void HellOnClick()
    {

    }

    public void RuinOnClick()
    {

    }

    public void SelectYesButton()
    {
        ClickButton.Play();
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameMap");
    }
    public void SelectNoButton()
    {
        ClickButton.Play();
        SelectPOPUP.SetActive(false);
        BackButton.SetActive(true);
    }

    public void OnClickSceneBack() //뒤로가기
    {
        ClickButton.Play();
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
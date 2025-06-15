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

    public Animation[] animations;  // Normal, Hard, Hell, Ruin �������

    void Start()
    {
        ClickButton.Play();

        // ó���� ��� �ִϸ��̼� ����
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

            // ������ �ִϸ��̼� ����
            for (int j = 0; j < animations.Length; j++)
            {
                if (j != i)
                {
                    animations[j].Stop();
                }
            }

            // ���� �ִϸ��̼� ���
            anim.Play(clip.name);

            // �ִϸ��̼� ���̸�ŭ ���
            yield return new WaitForSeconds(clip.length);

            // ��� ������ ���� �ִϸ��̼ǵ� ����
            anim.Stop();
        }

        Debug.Log("��� ���̵� �ִϸ��̼� ��� �Ϸ�");
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

    public void OnClickSceneBack() //�ڷΰ���
    {
        ClickButton.Play();
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
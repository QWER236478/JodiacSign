using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

public class MainMenu : MonoBehaviour
{
    public GameObject OptionPOPUP;
    public GameObject ContinueLock;
    public AudioSource ClickButton;

    public void NextDifficultyScene() //���ο� ���� ��ư
    {
        Debug.Log("���ο� ���� ��ư");   
        SceneManager.LoadScene("SelectDifficulty"); // ���̵� ���� ������ �̵�
    }

    public void OnContinueClick() //�̾��ϱ� ��ư
    {
        ClickButton.Play();
        Debug.Log("�̾��ϱ�");
    }

    public void OnOptionClick() //���� ��ư
    {
        OptionPOPUP.SetActive(true);
        ClickButton.Play();
        Debug.Log("���� â");
    }

    public void OptionCancelClick()
    {
        ClickButton.Play();
        OptionPOPUP.SetActive(false);
    }

    public void ContinueLockClick()
    {
        ClickButton.Play();
        ContinueLock.SetActive(true);
    }

    public void ContinueLockCancel()
    {
        ClickButton.Play();
        ContinueLock.SetActive(false);
    }

    public void OnQuitClick() //�����ϱ� ��ư
    {
        Application.Quit(); //����
        Debug.Log("�����");
    }


}


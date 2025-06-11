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

    public void NextDifficultyScene() //새로운 시작 버튼
    {
        Debug.Log("새로운 시작 버튼");   
        SceneManager.LoadScene("SelectDifficulty"); // 난이도 선택 씬으로 이동
    }

    public void OnContinueClick() //이어하기 버튼
    {
        ClickButton.Play();
        Debug.Log("이어하기");
    }

    public void OnOptionClick() //설정 버튼
    {
        OptionPOPUP.SetActive(true);
        ClickButton.Play();
        Debug.Log("설정 창");
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

    public void OnQuitClick() //종료하기 버튼
    {
        Application.Quit(); //종료
        Debug.Log("종료됨");
    }


}


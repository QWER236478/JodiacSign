using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

public class Sample : MonoBehaviour
{
    public XRSimpleInteractable interactable;

    private void Start()
    {
        interactable.selectEntered.AddListener(OnSelectEntered);
    }

    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        Debug.Log("¿Ã∫•∆Æ");
        SceneManager.LoadScene("SelectDifficulty");
    }

}
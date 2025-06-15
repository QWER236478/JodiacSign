using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUI : MonoBehaviour
{
    public Animator animator;
    public GameObject Info_hint;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
    
    
    void OnEnable()
    { 
        StartCoroutine(WaitAndErase());
    }

    IEnumerator WaitAndErase()
    {
        yield return new WaitForSeconds(1.0f); //1�� ���
        animator.Play("Info");
        yield return new WaitForSeconds(2.5f); // 2�� ���
        animator.SetTrigger("Erase");
        yield return new WaitForSeconds(0.5f); // 0.5�� ���
        Info_hint.SetActive(true);
    }

    public void ShowHint()
    {
        Debug.Log("�����");
        Info_hint.SetActive(true); 
    }

    public void HideHint()
    {
        Debug.Log("���");
        Info_hint.SetActive(false); 
    }
}

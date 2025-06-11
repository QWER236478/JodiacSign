using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class GameMapManager : MonoBehaviour
{
    public AudioSource ClickButton;
    public List<GameObject> EnemyEffect;
    public Animator animator;

    public enum MapOpenState
    {
        Close, Open
    }
    public MapOpenState mapOpenState = MapOpenState.Close;
    public AnimationClip mapAnim;
    private float mapOpenTime;

    // Start is called before the first frame update
    void Start()
    {
        ClickButton.Play();
    }

    // Update is called once per frame
    void Update()
    {
        switch(mapOpenState)
        {
            case MapOpenState.Close:
                {
                    mapOpenTime += Time.deltaTime;
                    if (mapOpenTime >= mapAnim.length)
                    {
                        EnemyEffectOn();
                        mapOpenTime = 0;
                        mapOpenState = MapOpenState.Open;
                    }
                    break;
                }
        }
    }


    public void EnemyEffectOn()
    {
        foreach (GameObject effect in EnemyEffect)
        {
            effect.SetActive(true);
        }
    }

    public void OnClickMonsterBattle()
    {
        ClickButton.Play();
        UnityEngine.SceneManagement.SceneManager.LoadScene("MonsterBattle");
    }    
    public void OnClickSceneBack() //뒤로가기
    {
        ClickButton.Play();
        UnityEngine.SceneManagement.SceneManager.LoadScene("SelectDifficulty");
    }
}

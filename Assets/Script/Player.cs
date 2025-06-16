using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float maxHP;
    public float playerHP;
    public float playerGold;

    public TextMeshProUGUI PlayerHP_Text;
    public TextMeshProUGUI PlayerGold_Text;

    public int attackDamage;

    //public GameObject AttackEffect;
    //public AudioSource AttackSounds;
    //public AudioSource DyingSounds;
    //public GameObject DieEffect;

    public Enemy targetEnemy;
    public TurnManager turnManager;

    void Start()
    {
        maxHP = playerHP;
        PlayerHP_Text.text = playerHP.ToString();
    }

    public void PlayerAttack()
    {
        if (turnManager != null && !turnManager.CanPlayerAct())
        {
            Debug.Log("턴이 끝나 공격할 수 없음");
            return;
        }

        Debug.Log("플레이어 공격");

        //AttackEffect.SetActive(true);
        //AttackSounds.Play();

        if (targetEnemy != null)
        {
            targetEnemy.TakeDamage(attackDamage);
        }

        turnManager.OnPlayerActionDone();
    }

    public void TakeDamage(int Damage)
    {
        if (playerHP > Damage)
        {
            playerHP -= Damage;
        }
        else
        {
            playerHP = 0;
            PlayerDie();
        }

        PlayerHP_Text.text = playerHP.ToString();
    }

    public void PlayerDie()
    {
        Debug.Log("플레이어 사망 상태");
       // DieEffect.SetActive(true);
        Destroy(gameObject);
       // DyingSounds.Play();
    }
}
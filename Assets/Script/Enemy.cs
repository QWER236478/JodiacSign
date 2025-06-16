using System.Collections;
using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float maxHP;
    public float enemyHP;
    public TextMeshProUGUI EnemyHP_Text;

    public Animator animator;
    public AnimationClip attackClip;
    public int attackDamage;

    public GameObject AttackEffect;
    public AudioSource AttackSounds;
    public AudioSource DyingSounds;
    public GameObject DieEffect;

    public int attackCost = 3; // 공격을 위해 필요한 플레이어 행동 수
    private int currentCost = 0;

    public Player player; // 공격 대상 플레이어

    public float dotDuration; // 지속 데미지 시간
    public int dotDamage;    // 지속 데미지 1씩

    void Start()
    {
        maxHP = enemyHP;
        EnemyHP_Text.text = enemyHP.ToString();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log("키 입력");
            TakeDamage(999);
        }
    }

    public void TakeDamage(int Damage)
    {
        if (enemyHP > Damage)
        {
            enemyHP -= Damage;
        }
        else
        {
            enemyHP = 0;
            EnemyDie();
        }

        EnemyHP_Text.text = enemyHP.ToString();
    }

    public void AddPlayerActionCost()
    {
        currentCost++;
        if (currentCost >= attackCost)
        {
            currentCost = 0;
            EnemyTurn();
        }
    }

    public void EnemyTurn()
    {
        Debug.Log("적 공격!");
        EnemyAnimOn();

        if (player != null)
        {
            player.TakeDamage(attackDamage);
        }
    }

    public void EnemyAnimOn()
    {
        animator.SetTrigger("EnemyAttack");
        StartCoroutine(AttackEffectOn());
        AttackSounds.Play();
    }

    IEnumerator AttackEffectOn()
    {
        yield return new WaitForSeconds(1.0f);
        AttackEffect.SetActive(true);
    }

    void AttackEffectOff()
    {
        ParticleSystem.MainModule main = AttackEffect.GetComponent<ParticleSystem>().main;
        main.loop.enabled = false;
    }

    public void EnemyDie()
    {
        Debug.Log("적 사망 상태");
        DieEffect.SetActive(true);
        Destroy(gameObject);
        DyingSounds.Play();
    }
}
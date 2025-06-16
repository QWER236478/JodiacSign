using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static HandEvaluator;

public class Player : MonoBehaviour
{
    public float maxHP;
    public float playerHP;
    public float playerGold;

    public TextMeshProUGUI PlayerHP_Text;
    public TextMeshProUGUI PlayerGold_Text;

    public int attackDamage;

    public List<GameObject> selectedCards = new(); // 선택된 카드들


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
    public void EvaluateAndAttack()
    {
        var selected = CardSelected.Instance.GetSelectedCards();

        if (selected == null || selected.Count == 0)
        {
            Debug.LogWarning("공격 실패: 선택된 카드가 없습니다.");
            return;
        }

        HandRank rank = HandEvaluator.EvaluateHand(selected);
        int attackDamage = HandEvaluator.GetDamageByRank(rank);

        Debug.Log($"공격! 족보: {rank}, 데미지: {attackDamage}");

        // 카드 파괴
        foreach (var card in selected)
        {
            Destroy(card);
        }

        // 선택 초기화
        CardSelected.Instance.ClearSelection();

        // 카드 다시 뽑을 수 있게 하려면 ShuffleCard.DrawCard() 등 호출 가능
    }
}
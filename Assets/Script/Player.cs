using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    public ShuffleCard shuffleCard;    // 카드 뽑기 스크립트 참조
    public CardSelected cardSelected;  // 선택된 카드 관리
    public Enemy enemy;                // 공격 대상 적
    public TextMeshProUGUI PlayerHP_Text;
    public int playerHP;

    // 턴 시작 시 카드 리필 등 처리

    public void OnTurnStart()
    {
        // 선택 초기화
        cardSelected.ClearSelection();

        // 필요한 카드 수 뽑기 (예: 5장)
        int needCount = 5 - shuffleCard.drawList.Count;
        if (needCount > 0)
        {
            shuffleCard.DrawCards(needCount);
        }
    }

    // 플레이어가 공격 버튼 눌렀을 때 호출
    public void Attack()
    {
        var selectedCards = cardSelected.GetSelectedCards();

        if (selectedCards.Count == 0)
        {
            Debug.Log("카드를 선택해주세요!");
            return;
        }

        var rank = HandEvaluator.EvaluateHand(selectedCards);
        int damage = HandEvaluator.GetDamageByRank(rank);

        Debug.Log($"공격 족보: {rank}, 데미지: {damage}");

        // 적 공격
        enemy.TakeDamage(damage);

        // 공격 후 선택한 카드 제거 및 새 카드 뽑기
        shuffleCard.RemoveCardsAndRefill(selectedCards);

        // 선택 초기화
        cardSelected.ClearSelection();

        // 턴 행동 하나 소모 알림 (TurnManager에서 처리할 수 있음)
        FindObjectOfType<TurnManager>().OnPlayerActionDone();
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
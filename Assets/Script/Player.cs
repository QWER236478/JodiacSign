using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    public ShuffleCard shuffleCard;    // ī�� �̱� ��ũ��Ʈ ����
    public CardSelected cardSelected;  // ���õ� ī�� ����
    public Enemy enemy;                // ���� ��� ��
    public TextMeshProUGUI PlayerHP_Text;
    public int playerHP;

    // �� ���� �� ī�� ���� �� ó��

    public void OnTurnStart()
    {
        // ���� �ʱ�ȭ
        cardSelected.ClearSelection();

        // �ʿ��� ī�� �� �̱� (��: 5��)
        int needCount = 5 - shuffleCard.drawList.Count;
        if (needCount > 0)
        {
            shuffleCard.DrawCards(needCount);
        }
    }

    // �÷��̾ ���� ��ư ������ �� ȣ��
    public void Attack()
    {
        var selectedCards = cardSelected.GetSelectedCards();

        if (selectedCards.Count == 0)
        {
            Debug.Log("ī�带 �������ּ���!");
            return;
        }

        var rank = HandEvaluator.EvaluateHand(selectedCards);
        int damage = HandEvaluator.GetDamageByRank(rank);

        Debug.Log($"���� ����: {rank}, ������: {damage}");

        // �� ����
        enemy.TakeDamage(damage);

        // ���� �� ������ ī�� ���� �� �� ī�� �̱�
        shuffleCard.RemoveCardsAndRefill(selectedCards);

        // ���� �ʱ�ȭ
        cardSelected.ClearSelection();

        // �� �ൿ �ϳ� �Ҹ� �˸� (TurnManager���� ó���� �� ����)
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
        Debug.Log("�÷��̾� ��� ����");
        // DieEffect.SetActive(true);
        Destroy(gameObject);
        // DyingSounds.Play();
    }
}
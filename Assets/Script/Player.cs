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

    public List<GameObject> selectedCards = new(); // ���õ� ī���


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
            Debug.Log("���� ���� ������ �� ����");
            return;
        }

        Debug.Log("�÷��̾� ����");

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
        Debug.Log("�÷��̾� ��� ����");
       // DieEffect.SetActive(true);
        Destroy(gameObject);
       // DyingSounds.Play();
    }
    public void EvaluateAndAttack()
    {
        var selected = CardSelected.Instance.GetSelectedCards();

        if (selected == null || selected.Count == 0)
        {
            Debug.LogWarning("���� ����: ���õ� ī�尡 �����ϴ�.");
            return;
        }

        HandRank rank = HandEvaluator.EvaluateHand(selected);
        int attackDamage = HandEvaluator.GetDamageByRank(rank);

        Debug.Log($"����! ����: {rank}, ������: {attackDamage}");

        // ī�� �ı�
        foreach (var card in selected)
        {
            Destroy(card);
        }

        // ���� �ʱ�ȭ
        CardSelected.Instance.ClearSelection();

        // ī�� �ٽ� ���� �� �ְ� �Ϸ��� ShuffleCard.DrawCard() �� ȣ�� ����
    }
}
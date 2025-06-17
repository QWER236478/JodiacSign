using System.Collections;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public Player player;
    public Enemy enemy;

    public int playerMaxActions = 3;
    private int playerActionsUsed = 0;
    private bool isPlayerTurn = true;

    private void Start()
    {
        StartPlayerTurn();
    }

    private void Update()
    {
        // �׽�Ʈ��: �����̽��ٷ� ���� �� ����
        if (Input.GetKeyDown(KeyCode.Space))
        {
            EndPlayerTurn();
        }
    }

    public void OnPlayerActionDone()
    {
        playerActionsUsed++;

        if (enemy != null)
        {
            enemy.AddPlayerActionCost(); // ������ �ൿ �ڽ�Ʈ �˸�
        }

        if (playerActionsUsed >= playerMaxActions)
        {
            EndPlayerTurn();
        }
    }

    private void EndPlayerTurn()
    {
        if (!isPlayerTurn) return;

        isPlayerTurn = false;
        playerActionsUsed = 0;

        Debug.Log("�÷��̾� �� ���� �� �� �� ����");
        StartCoroutine(EnemyTurnCoroutine());
    }

    private IEnumerator EnemyTurnCoroutine()
    {
        enemy.EnemyTurn();

        // �� �� �ִϸ��̼� �� ��� (1�� ����)
        yield return new WaitForSeconds(1f);

        Debug.Log("�� �� ���� �� �÷��̾� �� ����");
        StartPlayerTurn();
    }

    private void StartPlayerTurn()
    {
        isPlayerTurn = true;
        playerActionsUsed = 0;

        Debug.Log("�÷��̾� �� ����");

        // ī�� ���� �� �� �ʱ�ȭ ó��
        player.OnTurnStart();
    }

    public bool CanPlayerAct()
    {
        return isPlayerTurn && playerActionsUsed < playerMaxActions;
    }
}
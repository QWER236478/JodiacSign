using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public Player player;
    public Enemy enemy;

    public int playerMaxActions; // �÷��̾��� �ִ� �ൿ Ƚ�� (���� �ڽ�Ʈ�� ���� ����)
    private int playerActionsUsed = 0;
    private bool isPlayerTurn = true;

    private void Update()
    {
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
            enemy.AddPlayerActionCost(); // Enemy���� �˸�
        }

        if (playerActionsUsed >= playerMaxActions)
        {
            EndPlayerTurn();
        }
    }

    void EndPlayerTurn()
    {
        isPlayerTurn = false;
        playerActionsUsed = 0;

        Debug.Log("�÷��̾� �� ���� �� �� �� ����");
        StartEnemyTurn();
    }

    void StartEnemyTurn()
    {
        // ����� Enemy ������ �ڽ�Ʈ ���� �ÿ��� ����ǹǷ� ����ֵ� ��
        StartPlayerTurn();
    }

    void StartPlayerTurn()
    {
        isPlayerTurn = true;
        playerActionsUsed = 0;

        Debug.Log("�÷��̾� �� ����");
    }

    public bool CanPlayerAct()
    {
        return isPlayerTurn && playerActionsUsed < playerMaxActions;
    }
}
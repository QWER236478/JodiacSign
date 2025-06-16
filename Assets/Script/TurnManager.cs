using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public Player player;
    public Enemy enemy;

    public int playerMaxActions; // 플레이어의 최대 행동 횟수 (적의 코스트에 따라 세팅)
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
            enemy.AddPlayerActionCost(); // Enemy에게 알림
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

        Debug.Log("플레이어 턴 종료 → 적 턴 시작");
        StartEnemyTurn();
    }

    void StartEnemyTurn()
    {
        // 현재는 Enemy 공격은 코스트 누적 시에만 실행되므로 비워둬도 됨
        StartPlayerTurn();
    }

    void StartPlayerTurn()
    {
        isPlayerTurn = true;
        playerActionsUsed = 0;

        Debug.Log("플레이어 턴 시작");
    }

    public bool CanPlayerAct()
    {
        return isPlayerTurn && playerActionsUsed < playerMaxActions;
    }
}
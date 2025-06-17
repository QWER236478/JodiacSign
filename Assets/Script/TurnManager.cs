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
        // 테스트용: 스페이스바로 강제 턴 종료
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
            enemy.AddPlayerActionCost(); // 적에게 행동 코스트 알림
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

        Debug.Log("플레이어 턴 종료 → 적 턴 시작");
        StartCoroutine(EnemyTurnCoroutine());
    }

    private IEnumerator EnemyTurnCoroutine()
    {
        enemy.EnemyTurn();

        // 적 턴 애니메이션 등 대기 (1초 예시)
        yield return new WaitForSeconds(1f);

        Debug.Log("적 턴 종료 → 플레이어 턴 시작");
        StartPlayerTurn();
    }

    private void StartPlayerTurn()
    {
        isPlayerTurn = true;
        playerActionsUsed = 0;

        Debug.Log("플레이어 턴 시작");

        // 카드 리필 등 턴 초기화 처리
        player.OnTurnStart();
    }

    public bool CanPlayerAct()
    {
        return isPlayerTurn && playerActionsUsed < playerMaxActions;
    }
}
using System.Collections.Generic;
using UnityEngine;

public class ShuffleCard : MonoBehaviour
{
    [Header("카드 설정")]
    public List<GameObject> cardPrefabs; // 전체 카드 프리팹 리스트 (변하지 않음)
    private List<GameObject> availableCardPool = new(); // 리롤, 공격 후 남은 카드만 저장

    [Header("카드 생성 위치")]
    public Transform spawnPoint;

    [Header("현재 뽑힌 카드")]
    public List<GameObject> drawList = new();

    void Start()
    {
        // 복제해서 사용: 원본 cardPrefabs 보호
        availableCardPool = new List<GameObject>(cardPrefabs);
        DrawInitialCards();
    }

    // 초기 카드 8장 뽑기
    public void DrawInitialCards()
    {
        DrawCards(8);
    }

    // 특정 개수 카드 뽑기
    public void DrawCards(int count)
    {
        int drawCount = Mathf.Min(count, availableCardPool.Count);
        List<GameObject> selectedPrefabs = new();

        // 중복 없이 랜덤 카드 선택
        for (int i = 0; i < drawCount; i++)
        {
            int randIndex = Random.Range(0, availableCardPool.Count);
            GameObject selected = availableCardPool[randIndex];
            selectedPrefabs.Add(selected);
            availableCardPool.RemoveAt(randIndex);
        }

        // 카드 인스턴스 생성
        for (int i = 0; i < selectedPrefabs.Count; i++)
        {
            GameObject card = Instantiate(
                selectedPrefabs[i],
                spawnPoint.position + Vector3.right * i * 1.5f,
                Quaternion.identity
            );
            card.transform.eulerAngles = new Vector3(32f, 0f, 0f);
            drawList.Add(card);
        }
    }

    // 선택된 카드만 리롤 (선택 안 한 카드는 유지)
    public void RerollCards()
    {
        var selectedCards = CardSelected.Instance.selectedCards;

        for (int i = drawList.Count - 1; i >= 0; i--)
        {
            GameObject card = drawList[i];

            if (selectedCards.Contains(card))
            {
                Destroy(card);
                drawList.RemoveAt(i);

                // 새 카드가 남아 있다면 교체
                if (availableCardPool.Count > 0)
                {
                    int randIndex = Random.Range(0, availableCardPool.Count);
                    GameObject newCardPrefab = availableCardPool[randIndex];
                    availableCardPool.RemoveAt(randIndex);

                    GameObject newCard = Instantiate(
                        newCardPrefab,
                        spawnPoint.position + Vector3.right * i * 1.5f,
                        Quaternion.identity
                    );
                    newCard.transform.eulerAngles = new Vector3(32f, 0f, 0f);
                    drawList.Insert(i, newCard);
                }
            }
        }

        CardSelected.Instance.ClearSelection();
    }

    // 선택한 카드로 공격하고 사라지게 한 뒤, 빈 자리 채우기
    public void UseSelectedCardsForAttack()
    {
        var selectedCards = CardSelected.Instance.selectedCards;

        for (int i = drawList.Count - 1; i >= 0; i--)
        {
            GameObject card = drawList[i];

            if (selectedCards.Contains(card))
            {
                Destroy(card);
                drawList.RemoveAt(i);

                // 카드 소모되므로 다시 채움
                if (availableCardPool.Count > 0)
                {
                    int randIndex = Random.Range(0, availableCardPool.Count);
                    GameObject newCardPrefab = availableCardPool[randIndex];
                    availableCardPool.RemoveAt(randIndex);

                    GameObject newCard = Instantiate(
                        newCardPrefab,
                        spawnPoint.position + Vector3.right * i * 1.5f,
                        Quaternion.identity
                    );
                    newCard.transform.eulerAngles = new Vector3(32f, 0f, 0f);
                    drawList.Insert(i, newCard);
                }
            }
        }

        CardSelected.Instance.ClearSelection();
    }
}
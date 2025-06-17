using System.Collections.Generic;
using UnityEngine;

public class ShuffleCard : MonoBehaviour
{
    public List<GameObject> cardPrefabs;
    private List<GameObject> availableCardPool = new();

    public Transform spawnPoint;
    public List<GameObject> drawList = new();

    public int maxHandSize = 8;
    public CardSelected cardSelected;

    private void Start()
    {
        availableCardPool = new List<GameObject>(cardPrefabs);
        DrawCards(8);
    }

    public void DrawCards(int count)
    {
        int drawCount = Mathf.Min(count, availableCardPool.Count);

        for (int i = 0; i < drawCount; i++)
        {
            int randIndex = Random.Range(0, availableCardPool.Count);
            GameObject prefab = availableCardPool[randIndex];
            availableCardPool.RemoveAt(randIndex);

            Vector3 pos = spawnPoint.position + Vector3.right * (drawList.Count) * 1.5f;

            GameObject card = Instantiate(prefab, pos, Quaternion.Euler(32f, 0f, 0f));
            drawList.Add(card);
        }
        RearrangeCards();
    }

    public void RemoveCardsAndRefill(List<GameObject> cardsToRemove)
    {
        // 카드 제거
        foreach (var card in cardsToRemove)
        {
            if (drawList.Contains(card))
            {
                drawList.Remove(card);
                Destroy(card);
            }
        }

        // 남은 카드 수
        int currentHandSize = drawList.Count;

        // 최대 패 사이즈까지 부족한 만큼만 카드 드로우
        int cardsToDraw = maxHandSize - currentHandSize;
        if (cardsToDraw > 0)
        {
            DrawCards(cardsToDraw);
        }

        RearrangeCards();
    }

    private void RearrangeCards()
    {
        for (int i = 0; i < drawList.Count; i++)
        {
            drawList[i].transform.position = spawnPoint.position + Vector3.right * i * 1.5f;
        }
    }

    public void OnRerollSelected()
    {
        var selectedCards = cardSelected.GetSelectedCards();
        if (selectedCards.Count > 0)
        {
            RemoveCardsAndRefill(selectedCards);
            cardSelected.ClearSelection();
        }
    }
}
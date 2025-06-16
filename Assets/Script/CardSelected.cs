using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardSelected : MonoBehaviour
{
    public static CardSelected Instance;

    public List<GameObject> selectedCards = new();
    public int maxSelectable = 5;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void ToggleCardSelection(GameObject card)
    {
        if (selectedCards.Contains(card))
        {
            // 선택 해제
            card.transform.localPosition -= Vector3.up * 0.2f;
            selectedCards.Remove(card);
        }
        else
        {
            if (selectedCards.Count >= maxSelectable)
            {
                Debug.Log("최대 선택 개수 초과");
                return;
            }

            // 선택
            card.transform.localPosition += Vector3.up * 0.2f;
            selectedCards.Add(card);
        }
    }

    public void ClearSelection()
    {
        foreach (var card in selectedCards)
        {
            if (card != null)
                card.transform.localPosition -= Vector3.up * 0.2f;
        }
        selectedCards.Clear();
    }

    public List<GameObject> GetSelectedCards()
    {
        return new List<GameObject>(selectedCards);
    }
}
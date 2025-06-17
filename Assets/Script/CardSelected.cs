using System.Collections.Generic;
using UnityEngine;

public class CardSelected : MonoBehaviour
{
    public static CardSelected Instance;

    public List<GameObject> selectedCards = new();
    public int maxSelectable = 5;

    public GameObject Attack_Button;
    public GameObject Reroll_Button;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        UpdateButtonVisibility();
    }

    public void ToggleCardSelection(GameObject card)
    {
        if (selectedCards.Contains(card))
        {
            card.transform.localPosition -= Vector3.up * 0.2f;
            selectedCards.Remove(card);
        }
        else
        {
            if (selectedCards.Count >= maxSelectable) return;
            card.transform.localPosition += Vector3.up * 0.2f;
            selectedCards.Add(card);
        }
        UpdateButtonVisibility();
    }

    public void ClearSelection()
    {
        foreach (var card in selectedCards)
        {
            if (card != null)
                card.transform.localPosition -= Vector3.up * 0.2f;
        }
        selectedCards.Clear();

        UpdateButtonVisibility();
    }

    private void UpdateButtonVisibility()
    {
        bool hasSelection = selectedCards.Count > 0;
        if (Attack_Button != null) Attack_Button.SetActive(hasSelection);
        if (Reroll_Button != null) Reroll_Button.SetActive(hasSelection);
    }

    public List<GameObject> GetSelectedCards()
    {
        return new List<GameObject>(selectedCards);
    }
}

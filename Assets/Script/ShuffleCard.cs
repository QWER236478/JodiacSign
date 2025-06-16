using System.Collections.Generic;
using UnityEngine;

public class ShuffleCard : MonoBehaviour
{
    [Header("ī�� ����")]
    public List<GameObject> cardPrefabs; // ��ü ī�� ������ ����Ʈ (������ ����)
    private List<GameObject> availableCardPool = new(); // ����, ���� �� ���� ī�常 ����

    [Header("ī�� ���� ��ġ")]
    public Transform spawnPoint;

    [Header("���� ���� ī��")]
    public List<GameObject> drawList = new();

    void Start()
    {
        // �����ؼ� ���: ���� cardPrefabs ��ȣ
        availableCardPool = new List<GameObject>(cardPrefabs);
        DrawInitialCards();
    }

    // �ʱ� ī�� 8�� �̱�
    public void DrawInitialCards()
    {
        DrawCards(8);
    }

    // Ư�� ���� ī�� �̱�
    public void DrawCards(int count)
    {
        int drawCount = Mathf.Min(count, availableCardPool.Count);
        List<GameObject> selectedPrefabs = new();

        // �ߺ� ���� ���� ī�� ����
        for (int i = 0; i < drawCount; i++)
        {
            int randIndex = Random.Range(0, availableCardPool.Count);
            GameObject selected = availableCardPool[randIndex];
            selectedPrefabs.Add(selected);
            availableCardPool.RemoveAt(randIndex);
        }

        // ī�� �ν��Ͻ� ����
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

    // ���õ� ī�常 ���� (���� �� �� ī��� ����)
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

                // �� ī�尡 ���� �ִٸ� ��ü
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

    // ������ ī��� �����ϰ� ������� �� ��, �� �ڸ� ä���
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

                // ī�� �Ҹ�ǹǷ� �ٽ� ä��
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
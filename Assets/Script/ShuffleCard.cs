using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShuffleCard : MonoBehaviour
{
    public List<GameObject> cardPrefabs;  // ī�� �����յ��� �ִ� ����Ʈ (0~47�� ������)
    public List<GameObject> drawList = new List<GameObject>(); // ���� ī��� ����

    public Transform spawnPoint; // ī����� ������ ��ġ ������ (�� GameObject ���� ����)

    void Start()
    {
        DrawCard();

    }

    public void DrawCard()
    {
        List<int> indexes = new List<int>();
        for (int i = 0; i < cardPrefabs.Count; i++)
        {
            indexes.Add(i);
        }

        // ����Ʈ ���� (Fisher-Yates)
        for (int i = 0; i < indexes.Count; i++)
        {
            int randIndex = Random.Range(i, indexes.Count);
            int temp = indexes[i];
            indexes[i] = indexes[randIndex];
            indexes[randIndex] = temp;
        }

        // �տ��� 8�� �̾Ƽ� ����
        for (int i = 0; i < 8; i++)
        {
            int idx = indexes[i];
            GameObject card = Instantiate(cardPrefabs[idx], spawnPoint.position + Vector3.right * i * 1.5f, Quaternion.identity);
            card.transform.eulerAngles = new Vector3(32f, 0f, 0f); 
            drawList.Add(card);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShuffleCard : MonoBehaviour
{
    public List<GameObject> cardPrefabs;  // 카드 프리팹들을 넣는 리스트 (0~47개 프리팹)
    public List<GameObject> drawList = new List<GameObject>(); // 뽑힌 카드들 저장

    public Transform spawnPoint; // 카드들을 생성할 위치 기준점 (빈 GameObject 만들어서 연결)

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

        // 리스트 섞기 (Fisher-Yates)
        for (int i = 0; i < indexes.Count; i++)
        {
            int randIndex = Random.Range(i, indexes.Count);
            int temp = indexes[i];
            indexes[i] = indexes[randIndex];
            indexes[randIndex] = temp;
        }

        // 앞에서 8개 뽑아서 생성
        for (int i = 0; i < 8; i++)
        {
            int idx = indexes[i];
            GameObject card = Instantiate(cardPrefabs[idx], spawnPoint.position + Vector3.right * i * 1.5f, Quaternion.identity);
            card.transform.eulerAngles = new Vector3(32f, 0f, 0f); 
            drawList.Add(card);
        }
    }
}
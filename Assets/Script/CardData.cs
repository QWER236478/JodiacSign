using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardData : MonoBehaviour
{
    public enum CardClass // �ϴ�/��/�ٴ�/�ʿ�
    {
        Sky, Mountain, Ocean, Grassland, Joker //�ϴ�, ��, �ٴ�, �ʿ�
    }

    public int id;              // 0~47
    public int rank;            // 0~11
    public CardClass cardClass; //Class
    public string zodiacName;   // ������ �̸�
}

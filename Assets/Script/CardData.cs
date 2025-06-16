using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardData : MonoBehaviour
{
    public enum CardClass // 하늘/산/바다/초원
    {
        Sky, Mountain, Ocean, Grassland, Joker //하늘, 산, 바다, 초원
    }

    public int id;              // 0~47
    public int rank;            // 0~11
    public CardClass cardClass; //Class
    public string zodiacName;   // 십이지 이름
}

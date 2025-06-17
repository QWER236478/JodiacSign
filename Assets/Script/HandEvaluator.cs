using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class HandEvaluator
{
    public enum HandRank
    {
        None,
        Pair,
        Triple,
        Straight,
        Flush,
        FullHouse,
        FourOfAKind,
        UltimateCombo,
        CatOnly
    }

    public static HandRank EvaluateHand(List<GameObject> cardObjects)
    {
        List<int> ranks = new();
        List<CardData.CardClass> classes = new();

        foreach (var obj in cardObjects)
        {
            var data = obj.GetComponent<CardData>();
            if (data != null)
            {
                ranks.Add(data.rank);
                classes.Add(data.cardClass);
            }
        }

        // 고양이 카드 여부 확인
        bool hasCat = ranks.Contains(99);

        // 고양이 단독 카드
        if (ranks.Count == 1 && ranks[0] == 99)
            return HandRank.CatOnly;

        // 고양이 제외한 일반 카드만 추출 (ranks 기준으로 인덱스 맞춰서)
        List<int> normalRanks = new();
        List<CardData.CardClass> normalClasses = new();

        for (int i = 0; i < ranks.Count; i++)
        {
            if (ranks[i] != 99)
            {
                normalRanks.Add(ranks[i]);
                normalClasses.Add(classes[i]);
            }
        }

        if (normalRanks.Count == 0)
            return HandRank.None;

        var rankCounts = normalRanks.GroupBy(x => x)
                                    .ToDictionary(g => g.Key, g => g.Count());

        // 수정: Flush, Straight 조건 최소 카드 개수 제한 추가
        bool isFlush = normalClasses.Count >= 4 && normalClasses.All(c => c == normalClasses[0]);
        bool isStraight = normalRanks.Count >= 4 && IsStraight(normalRanks);
        bool isFullHouse = rankCounts.ContainsValue(3) && rankCounts.ContainsValue(2);

        // 족보 판정 순서
        if (IsUltimateCombo(ranks, classes)) return HandRank.UltimateCombo;
        if (rankCounts.ContainsValue(4)) return HandRank.FourOfAKind;
        if (isFullHouse) return HandRank.FullHouse;
        if (isFlush) return HandRank.Flush;
        if (isStraight) return HandRank.Straight;
        if (rankCounts.ContainsValue(3)) return HandRank.Triple;
        if (rankCounts.ContainsValue(2)) return HandRank.Pair;

        return HandRank.None;
    }

    public static int GetDamageByRank(HandRank rank)
    {
        return rank switch
        {
            HandRank.Pair => 20,
            HandRank.Triple => 80,
            HandRank.Straight => 150,
            HandRank.Flush => 175,
            HandRank.FullHouse => 200,
            HandRank.FourOfAKind => 400,
            HandRank.UltimateCombo => 9999,
            HandRank.CatOnly => Random.Range(1, 101),
            _ => 10,
        };
    }

    private static bool IsStraight(List<int> ranks)
    {
        if (ranks.Count < 5) return false;
        var distinctRanks = ranks.Distinct().OrderBy(r => r).ToList();

        for (int i = 0; i <= distinctRanks.Count - 5; i++)
        {
            bool isSeq = true;
            for (int j = 0; j < 4; j++)
            {
                if ((distinctRanks[i + j + 1] - distinctRanks[i + j] + 12) % 12 != 1)
                {
                    isSeq = false;
                    break;
                }
            }
            if (isSeq) return true;
        }
        return false;
    }

    private static bool IsUltimateCombo(List<int> ranks, List<CardData.CardClass> classes)
    {
        List<int> royal = new() { 0, 2, 4, 5, 99 };
        return new HashSet<int>(ranks).SetEquals(royal)
            && classes.All(c => c == classes[0]);
    }
}
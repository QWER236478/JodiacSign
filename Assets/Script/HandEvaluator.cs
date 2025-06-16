using System.Collections.Generic;
using UnityEngine;
using static CardData;

public static class HandEvaluator 
{
    public enum HandRank
    {
        None,
        Pair,
        LuckyPair,
        Triple,
        LuckyTriple,
        Straight,
        Flush,
        FullHouse,
        FourOfAKind,
        LuckyFourOfAKind,
        UltimateCombo,
        CatAlone
    }

    public static HandRank EvaluateHand(List<GameObject> cardObjects)
    {
        List<int> ranks = new();
        List<CardClass> classes = new();

        foreach (var obj in cardObjects)
        {
            var data = obj.GetComponent<CardData>();
            if (data != null)
            {
                ranks.Add(data.rank);
                classes.Add(data.cardClass);
            }
        }

        bool hasCat = ranks.Contains(99);

        if (ranks.Count == 1 && hasCat) return HandRank.CatAlone;
        if (IsUltimateCombo(ranks, classes)) return HandRank.UltimateCombo;
        if (IsFourOfAKind(ranks)) return hasCat ? HandRank.LuckyFourOfAKind : HandRank.FourOfAKind;
        if (IsFullHouse(ranks)) return HandRank.FullHouse;
        if (IsFlush(classes)) return HandRank.Flush;
        if (IsStraight(ranks)) return HandRank.Straight;
        if (IsTriple(ranks)) return hasCat ? HandRank.LuckyTriple : HandRank.Triple;
        if (IsPair(ranks)) return hasCat ? HandRank.LuckyPair : HandRank.Pair;

        return HandRank.None;
    }

    public static int GetDamageByRank(HandRank rank)
    {
        switch (rank)
        {
            case HandRank.Pair: return 20;
            case HandRank.LuckyPair: return 40;

            case HandRank.Triple: return 80;
            case HandRank.LuckyTriple: return 100;

            case HandRank.Straight: return 150;
            case HandRank.Flush: return 175;
            case HandRank.FullHouse: return 200;

            case HandRank.FourOfAKind: return 400;
            case HandRank.LuckyFourOfAKind: return 600;

            case HandRank.UltimateCombo: return 9999;
            case HandRank.CatAlone: return Random.Range(1, 101);
            default: return 10;
        }
    }

    // ===== 내부 로직들 =====

    static Dictionary<int, int> GetRankCount(List<int> ranks)
    {
        Dictionary<int, int> dict = new();
        foreach (int rank in ranks)
        {
            if (!dict.ContainsKey(rank)) dict[rank] = 0;
            dict[rank]++;
        }
        return dict;
    }

    static bool IsPair(List<int> ranks) => GetRankCount(ranks).ContainsValue(2);
    static bool IsTriple(List<int> ranks) => GetRankCount(ranks).ContainsValue(3);
    static bool IsFourOfAKind(List<int> ranks) => GetRankCount(ranks).ContainsValue(4);
    static bool IsFullHouse(List<int> ranks)
    {
        var count = GetRankCount(ranks);
        return count.ContainsValue(3) && count.ContainsValue(2);
    }

    static bool IsFlush(List<CardClass> classes)
    {
        return classes.Count > 0 && classes.TrueForAll(c => c == classes[0]);
    }

    static bool IsStraight(List<int> ranks)
    {
        if (ranks.Count < 5) return false;
        List<int> sorted = new(ranks);
        sorted.Sort();

        for (int i = 0; i <= sorted.Count - 5; i++)
        {
            bool isSequential = true;
            for (int j = 0; j < 4; j++)
            {
                int current = sorted[i + j];
                int next = sorted[i + j + 1];
                if ((next - current + 12) % 12 != 1)
                {
                    isSequential = false;
                    break;
                }
            }
            if (isSequential) return true;
        }

        List<int> royal = new() { 0, 2, 4, 5, 99 };
        return new HashSet<int>(ranks).SetEquals(royal);
    }

    static bool IsUltimateCombo(List<int> ranks, List<CardClass> classes)
    {
        List<int> royal = new() { 0, 2, 4, 5, 99 };
        return new HashSet<int>(ranks).SetEquals(royal) && IsFlush(classes);
    }
}
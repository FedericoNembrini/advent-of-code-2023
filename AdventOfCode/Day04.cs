﻿namespace AdventOfCode;

public class Day04 : BaseDay
{
    private readonly string[] _input;

    public Day04()
    {
        _input = File.ReadAllLines(InputFilePath);
    }

    public override ValueTask<string> Solve_1() => new($"Solution to {ClassPrefix} {CalculateIndex()}, {SolvePart01()}");

    public override ValueTask<string> Solve_2() => new($"Solution to {ClassPrefix} {CalculateIndex()}, {SolvePart02()}");

    private int SolvePart01()
    {
        List<Card> cards = [];

        for (int i = 0; i < _input.Length; i++)
        {
            _input[i] = _input[i].Substring(_input[i].IndexOf(':') + 1).Trim();
            cards.Add(new Card()
            {
                LuckyNumbers = _input[i].Split('|')[0].Split(' ').Where(i => !string.IsNullOrEmpty(i)).Select(i => Convert.ToInt32(i)).ToHashSet(),
                CardNumbers = _input[i].Split('|')[1].Split(' ').Where(i => !string.IsNullOrEmpty(i)).Select(i => Convert.ToInt32(i)).ToHashSet(),
            });
        }

        int result = 0;
        foreach (Card card in cards)
            result += card.GetCardPoints();

        return result;
    }

    private int SolvePart02()
    {
        Dictionary<int, Card> originalCards = [];

        for (int i = 0; i < _input.Length; i++)
        {
            _input[i] = _input[i].Substring(_input[i].IndexOf(':') + 1).Trim();

            Card card = new()
            {
                Id = i + 1,
                LuckyNumbers = _input[i].Split('|')[0].Split(' ').Where(i => !string.IsNullOrEmpty(i)).Select(i => Convert.ToInt32(i)).ToHashSet(),
                CardNumbers = _input[i].Split('|')[1].Split(' ').Where(i => !string.IsNullOrEmpty(i)).Select(i => Convert.ToInt32(i)).ToHashSet(),
            };

            _ = card.GetWinNumberCount();
            originalCards.Add(card.Id, card);
        }

        List<Card> cards = new List<Card>(originalCards.Values);

        for (int x = 0; x < cards.Count; x++)
        {
            for (int y = 0; y < cards[x].GetWinNumberCount(); y++)
            {
                cards.Add(originalCards[cards[x].Id + y + 1]);
            }
        }

        return cards.Count;
    }
}

public class Card
{
    public int Id { get; set; }

    public HashSet<int> LuckyNumbers { get; set; } = [];

    public HashSet<int> CardNumbers { get; set; } = [];

    private int? _winNumberCount = null;

    public int GetCardPoints()
    {
        int numberCount =
            LuckyNumbers.Intersect(CardNumbers).Count();

        if (numberCount == 0) return 0;
        if (numberCount == 1) return 1;

        int result = 1;
        for (int i = 1; i < numberCount; i++)
            result *= 2;

        return result;
    }

    public int GetWinNumberCount()
    {
        if (_winNumberCount.HasValue)
            return _winNumberCount.Value;

        _winNumberCount = LuckyNumbers.Intersect(CardNumbers).Count();

        return
            _winNumberCount.Value;
    }
}
namespace AdventOfCode;

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
                LuckyNumbers = _input[i].Split('|')[0].Split(' ').Where(i => !string.IsNullOrEmpty(i)).Select(i => Convert.ToInt32(i)).ToList(),
                CardNumbers = _input[i].Split('|')[1].Split(' ').Where(i => !string.IsNullOrEmpty(i)).Select(i => Convert.ToInt32(i)).ToList(),
            });
        }

        int result = 0;
        foreach (Card card in cards)
            result += card.GetCardPoints();

        return result;
    }

    private int SolvePart02()
    {
        List<Card> cards = [];

        for (int i = 0; i < _input.Length; i++)
        {
            _input[i] = _input[i].Substring(_input[i].IndexOf(':') + 1).Trim();
            cards.Add(new Card()
            {
                Id = i + 1,
                LuckyNumbers = _input[i].Split('|')[0].Split(' ').Where(i => !string.IsNullOrEmpty(i)).Select(i => Convert.ToInt32(i)).ToList(),
                CardNumbers = _input[i].Split('|')[1].Split(' ').Where(i => !string.IsNullOrEmpty(i)).Select(i => Convert.ToInt32(i)).ToList(),
            });
        }

        for (int x = 0; x < cards.Count; x++)
        {
            for (int y = 0; y < cards[x].GetWinNumberCount(); y++)
            {
                cards.Add(cards.First(c => c.Id == (cards[x].Id + y + 1)));
            }
        }

        return cards.Count;
    }
}

public class Card
{
    public int Id { get; set; }

    public List<int> LuckyNumbers { get; set; } = [];

    public List<int> CardNumbers { get; set; } = [];

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
        return
            LuckyNumbers.Intersect(CardNumbers).Count();
    }
}
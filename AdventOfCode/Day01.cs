namespace AdventOfCode;

public class Day01 : BaseDay
{
    private readonly string[] _input;

    public Day01()
    {
        _input = File.ReadAllLines(InputFilePath);
    }

    public override ValueTask<string> Solve_1() => new($"Solution to {ClassPrefix} {CalculateIndex()}, {SolvePart01()}");

    public override ValueTask<string> Solve_2() => new($"Solution to {ClassPrefix} {CalculateIndex()}, {SolvePart02()}");

    private int SolvePart01()
    {
        int total = 0;

        foreach (var item in _input)
        {
            char firstNumber = item.FirstOrDefault(c => char.IsNumber(c), '0');
            char lastNumber = item.LastOrDefault(c => char.IsNumber(c), '0');

            total += Convert.ToInt32($"{firstNumber}{lastNumber}");
        }

        return total;
    }

    private int SolvePart02()
    {
        Dictionary<string, int> numbers = new Dictionary<string, int>()
        {
            { "one", 1 },
            { "1", 1 },
            { "two", 2 },
            { "2", 2 },
            { "three", 3 },
            { "3", 3 },
            { "four", 4 },
            { "4", 4 },
            { "five", 5 },
            { "5", 5 },
            { "six", 6 },
            { "6", 6 },
            { "seven", 7 },
            { "7", 7 },
            { "eight", 8 },
            { "8", 8 },
            { "nine", 9 },
            { "9", 9 },
        };

        int total = 0;

        foreach (var inputString in _input)
        {
            int lastIndex = -1;
            int currentIndex = 0;
            int currentIndexNumber = 0;
            foreach (var item2 in numbers)
            {
                currentIndex = inputString.IndexOf(item2.Key);
                if (currentIndex != -1 && (lastIndex == -1 || currentIndex < lastIndex))
                {
                    lastIndex = currentIndex;
                    currentIndexNumber = item2.Value;
                }
            }

            string firstNumber = Convert.ToString(currentIndexNumber);

            lastIndex = -1;
            currentIndex = 0;
            currentIndexNumber = 0;
            foreach (var item2 in numbers)
            {
                currentIndex = inputString.LastIndexOf(item2.Key);
                if (currentIndex != -1 && (lastIndex == -1 || currentIndex > lastIndex))
                {
                    lastIndex = currentIndex;
                    currentIndexNumber = item2.Value;
                }
            }

            string lastNumber = Convert.ToString(currentIndexNumber);

            total += Convert.ToInt32($"{firstNumber}{lastNumber}");
        }

        return total;
    }
}

namespace AdventOfCode;

public class Day02 : BaseDay
{
    private readonly string[] _input;

    public Day02()
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
            int gameId = 0;

            string[] firstSplit = item.Split(":");
            gameId = Convert.ToInt32(firstSplit[0].Replace("Game", string.Empty).Trim());

            string[] secondSplit = firstSplit[1].Split(";");
            bool green = true, blue = true, red = true;
            foreach (string secondSplitString in secondSplit)
            {
                string[] dices = secondSplitString.Split(",");

                string greenString = dices.FirstOrDefault(s => s.Contains("green"));
                if (!string.IsNullOrEmpty(greenString))
                    if (Convert.ToInt32(greenString.Replace("green", string.Empty).Trim()) > 13)
                    {
                        green = false;
                        break;
                    }

                string redString = dices.FirstOrDefault(s => s.Contains("red"));
                if (!string.IsNullOrEmpty(redString))
                    if (Convert.ToInt32(redString.Replace("red", string.Empty).Trim()) > 12)
                    {
                        red = false;
                        break;
                    }

                string blueString = dices.FirstOrDefault(s => s.Contains("blue"));
                if (!string.IsNullOrEmpty(blueString))
                    if (Convert.ToInt32(blueString.Replace("blue", string.Empty).Trim()) > 14)
                    {
                        blue = false;
                        break;
                    }
            }

            total += green && blue && red ? gameId : 0;
        }

        return total;
    }

    private int SolvePart02()
    {
        int total = 0;

        foreach (var item in _input)
        {
            int gameId = 0;

            string[] firstSplit = item.Split(":");
            gameId = Convert.ToInt32(firstSplit[0].Replace("Game", string.Empty).Trim());

            string[] secondSplit = firstSplit[1].Split(";");
            int green = 0, blue = 0, red = 0;
            foreach (string secondSplitString in secondSplit)
            {
                string[] dices = secondSplitString.Split(",");

                string greenString = dices.FirstOrDefault(s => s.Contains("green"));
                if (!string.IsNullOrEmpty(greenString))
                    if (green == 0 || Convert.ToInt32(greenString.Replace("green", string.Empty).Trim()) > green)
                    {
                        green = Convert.ToInt32(greenString.Replace("green", string.Empty).Trim());
                    }

                string redString = dices.FirstOrDefault(s => s.Contains("red"));
                if (!string.IsNullOrEmpty(redString))
                    if (red == 0 || Convert.ToInt32(redString.Replace("red", string.Empty).Trim()) > red)
                    {
                        red = Convert.ToInt32(redString.Replace("red", string.Empty).Trim());
                    }

                string blueString = dices.FirstOrDefault(s => s.Contains("blue"));
                if (!string.IsNullOrEmpty(blueString))
                    if (blue == 0 || Convert.ToInt32(blueString.Replace("blue", string.Empty).Trim()) > blue)
                    {
                        blue = Convert.ToInt32(blueString.Replace("blue", string.Empty).Trim());
                    }
            }

            total += (green * red * blue);
        }

        return total;
    }
}

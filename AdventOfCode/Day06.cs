namespace AdventOfCode;

public class Day06 : BaseDay
{
    private readonly string[] _inputLines;

    public Day06()
    {
        _inputLines = File.ReadAllLines(InputFilePath);
    }

    public override ValueTask<string> Solve_1() => new($"Solution to {ClassPrefix} {CalculateIndex()}, {SolvePart01()}");

    public override ValueTask<string> Solve_2() => new($"Solution to {ClassPrefix} {CalculateIndex()}, {SolvePart02()}");

    private long SolvePart01()
    {
        long[] time = _inputLines[0].Replace("Time:", string.Empty).Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(s => Convert.ToInt64(s)).ToArray();
        long[] distance = _inputLines[1].Replace("Distance:", string.Empty).Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(s => Convert.ToInt64(s)).ToArray();

        List<Race> races = [];
        for (int i = 0; i < time.Length; i++)
            races.Add(new Race(time[i], distance[i]));

        long result = 1;
        foreach (Race race in races)
        {
            Boat boat = new();

            long fromWin = -1;
            for (int i = 0; i < race.Time; i++)
            {
                boat.Hold(i);
                if (boat.GetDistance(race.Time - i) > race.RecordDistance)
                {
                    fromWin = i;
                    break;
                }

                boat.Reset();
            }

            boat.Reset();

            long toWin = -1;
            for (long i = race.Time; i >= 0; i--)
            {
                boat.Hold(i);
                if (boat.GetDistance(race.Time - i) > race.RecordDistance)
                {
                    toWin = i;
                    break;
                }

                boat.Reset();
            }

            result *= (toWin - fromWin + 1);
        }

        return result;
    }

    private long SolvePart02()
    {
        long time = Convert.ToInt64(_inputLines[0].Replace("Time:", string.Empty).Replace(" ", string.Empty));
        long distance = Convert.ToInt64(_inputLines[1].Replace("Distance:", string.Empty).Replace(" ", string.Empty));

        List<Race> races = [];
        races.Add(new Race(time, distance));

        long result = 1;
        foreach (Race race in races)
        {
            Boat boat = new();

            long fromWin = -1;
            for (int i = 0; i < race.Time; i++)
            {
                boat.Hold(i);
                if (boat.GetDistance(race.Time - i) > race.RecordDistance)
                {
                    fromWin = i;
                    break;
                }

                boat.Reset();
            }

            boat.Reset();

            long toWin = -1;
            for (long i = race.Time; i >= 0; i--)
            {
                boat.Hold(i);
                if (boat.GetDistance(race.Time - i) > race.RecordDistance)
                {
                    toWin = i;
                    break;
                }

                boat.Reset();
            }

            result *= (toWin - fromWin + 1);
        }

        return result;
    }
}

internal class Race
{
    public long Time { get; set; }
    public long RecordDistance { get; set; }

    public Race(long time, long recordDistance)
    {
        Time = time;
        RecordDistance = recordDistance;
    }
}

internal class Boat
{
    public long Speed { get; private set; }
    public long HoldTime { get; private set; }

    public Boat()
    {

    }

    public void Hold(long holdTime)
    {
        this.HoldTime = holdTime;
        this.Speed = this.HoldTime;
    }

    public void Reset()
    {
        this.HoldTime = 0;
        this.Speed = 0;
    }

    public long GetDistance(long seconds)
    {
        return this.Speed * seconds;
    }
}

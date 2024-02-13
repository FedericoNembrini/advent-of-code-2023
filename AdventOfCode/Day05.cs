namespace AdventOfCode;

public class Day05 : BaseDay
{
    private readonly string _input;

    public Day05()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    public override ValueTask<string> Solve_1() => new($"Solution to {ClassPrefix} {CalculateIndex()}, {SolvePart01()}");

    public override ValueTask<string> Solve_2() => new($"Solution to {ClassPrefix} {CalculateIndex()}, {SolvePart02()}");

    private long SolvePart01()
    {
        string[] inputLines = _input.Split("\n").Where(l => !string.IsNullOrEmpty(l) && l != "\r").ToArray();

        List<SeedMap> seedMaps = inputLines[0].Replace("seeds: ", string.Empty).Split(" ").Select(number => new SeedMap(Convert.ToInt64(number))).ToList();

        Dictionary<string, Mapper> orderedMappers = new Dictionary<string, Mapper>();
        Mapper? mapper = null;

        for (int i = 1; i < inputLines.Length; i++)
        {
            if (inputLines[i].Contains("map:"))
            {
                if (mapper != null)
                    orderedMappers.Add(mapper!.MapName, mapper!);

                mapper = new Mapper(inputLines[i]);
                continue;
            }

            long[] values = inputLines[i].Split(" ").Select(s => Convert.ToInt64(s.Trim('\r'))).ToArray();

            mapper!.AddMap(new Map(values[0], values[1], values[2]));
        }

        if (mapper != null)
            orderedMappers.Add(mapper!.MapName, mapper!);

        foreach (SeedMap seedMap in seedMaps)
        {
            long number = seedMap.SeedNumber;

            foreach (Mapper orderedMapper in orderedMappers.Values)
            {
                number = orderedMapper.GetDestination(number);
            }

            seedMap.Location = number;
        }

        return seedMaps.Min(sm => sm.Location);
    }

    private long SolvePart02()
    {
        throw new NotImplementedException();

        string[] inputLines = _input.Split("\n").Where(l => !string.IsNullOrEmpty(l) && l != "\r").ToArray();

        string[] seedRange = inputLines[0].Replace("seeds: ", string.Empty).Split(" ");

        List<SeedMap> seedMaps = new List<SeedMap>();
        for (int i = 0; i < seedRange.Length; i = i + 2)
        {
            long seedStart = Convert.ToInt64(seedRange[i]);
            long seedRange2 = seedStart + Convert.ToInt64(seedRange[i + 1]);

            for (long x = seedStart; x < seedRange2; x++)
                seedMaps.Add(new SeedMap(x));
        }

        Dictionary<string, Mapper> orderedMappers = new Dictionary<string, Mapper>();
        Mapper? mapper = null;

        for (int i = 1; i < inputLines.Length; i++)
        {
            if (inputLines[i].Contains("map:"))
            {
                if (mapper != null)
                    orderedMappers.Add(mapper!.MapName, mapper!);

                mapper = new Mapper(inputLines[i]);
                continue;
            }

            long[] values = inputLines[i].Split(" ").Select(s => Convert.ToInt64(s.Trim('\r'))).ToArray();

            mapper!.AddMap(new Map(values[0], values[1], values[2]));
        }

        if (mapper != null)
            orderedMappers.Add(mapper!.MapName, mapper!);

        foreach (SeedMap seedMap in seedMaps)
        {
            long number = seedMap.SeedNumber;

            foreach (Mapper orderedMapper in orderedMappers.Values)
            {
                number = orderedMapper.GetDestination(number);
            }

            seedMap.Location = number;
        }

        return seedMaps.Min(sm => sm.Location);
    }
}

internal class SeedMap
{
    public long SeedNumber { get; set; }

    public long SoilNumber { get; set; }

    public long Fertilizer { get; set; }

    public long Water { get; set; }

    public long Light { get; set; }

    public long Temperature { get; set; }

    public long Humidity { get; set; }

    public long Location { get; set; }

    public SeedMap(long seedNumber)
    {
        SeedNumber = seedNumber;
    }
}

internal class Mapper
{
    public string MapName { get; set; }

    private List<Map> _maps { get; set; }

    public Mapper(string mapName) : this(mapName, [])
    {

    }

    public Mapper(string mapName, List<Map> maps)
    {
        MapName = mapName;
        _maps = maps;
    }

    public void AddMap(Map map)
    {
        _maps.Add(map);
    }

    public long GetDestination(long source)
    {
        Map? map = _maps.Where(m => m.SourceRangeStart <= source && m.SourceRangeStart + m.RangeLength > source).SingleOrDefault();

        if (map == null)
            return source;

        return
            map.DestinationRangeStart + (source - map.SourceRangeStart);
    }
}

internal class Map
{
    public long DestinationRangeStart { get; set; }

    public long SourceRangeStart { get; set; }

    public long RangeLength { get; set; }

    public Map(long destinationRangeStart, long sourceRangeStart, long rangeLength)
    {
        DestinationRangeStart = destinationRangeStart;
        SourceRangeStart = sourceRangeStart;
        RangeLength = rangeLength;
    }
}
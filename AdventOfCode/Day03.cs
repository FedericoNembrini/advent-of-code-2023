namespace AdventOfCode;

public class Day03 : BaseDay
{
    private readonly string[] _input;

    public Day03()
    {
        _input = File.ReadAllLines(InputFilePath);
    }

    public override ValueTask<string> Solve_1() => new($"Solution to {ClassPrefix} {CalculateIndex()}, {SolvePart01()}");

    public override ValueTask<string> Solve_2() => new($"Solution to {ClassPrefix} {CalculateIndex()}, {SolvePart02()}");

    private int SolvePart01()
    {
        char[,] inputBArray = new char[_input.Length, _input[0].Length];

        for (int x = 0; x < inputBArray.GetLength(0); x++)
        {
            for (int y = 0; y < inputBArray.GetLength(1); y++)
            {
                inputBArray[x, y] = _input[x][y];
            }
        }

        //for (int x = 0; x < inputBArray.GetLength(0); x++)
        //{
        //    for (int y = 0; y < inputBArray.GetLength(1); y++)
        //    {
        //        Console.Write(inputBArray[x, y]);
        //    }
        //    Console.Write("\n");
        //}

        List<NumberCoordinate> numberCoordinateList = new List<NumberCoordinate>();
        List<SymbolCoordinate> symbolCoordinateList = new List<SymbolCoordinate>();

        for (int x = 0; x < inputBArray.GetLength(0); x++)
        {
            int firstNumberPosition = -1;
            for (int y = 0; y < inputBArray.GetLength(1); y++)
            {
                if (char.IsNumber(inputBArray[x, y]) && firstNumberPosition == -1)
                {
                    firstNumberPosition = y;
                    continue;
                }

                if (!char.IsNumber(inputBArray[x, y]) && firstNumberPosition != -1)
                {
                    string numberString = string.Empty;
                    for (int z = firstNumberPosition; z < y; z++)
                        numberString += inputBArray[x, z];

                    numberCoordinateList.Add(new NumberCoordinate(Convert.ToInt32(numberString), x, firstNumberPosition, y - 1));
                    firstNumberPosition = -1;
                }

                if (y + 1 >= inputBArray.GetLength(1) && firstNumberPosition != -1)
                {
                    string numberString = string.Empty;
                    for (int z = firstNumberPosition; z <= y; z++)
                        numberString += inputBArray[x, z];

                    numberCoordinateList.Add(new NumberCoordinate(Convert.ToInt32(numberString), x, firstNumberPosition, y));
                    firstNumberPosition = -1;

                    continue;
                }

                if (new List<char> { '+', '*', '@', '#', '/', '=', '%', '$', '&', '-' }.Contains(inputBArray[x, y]))
                {
                    symbolCoordinateList.Add(new SymbolCoordinate(x, y));
                    continue;
                }
            }
        }

        int total = 0;

        foreach (NumberCoordinate numberCoordinate in numberCoordinateList)
        {
            if (symbolCoordinateList.Any(sc =>
                (sc.Row == numberCoordinate.Row || sc.Row == numberCoordinate.Row - 1 || sc.Row == numberCoordinate.Row + 1)
                && (sc.Column == numberCoordinate.FirstColumn || sc.Column == numberCoordinate.FirstColumn - 1 || sc.Column == numberCoordinate.FirstColumn + 1
                    || sc.Column == numberCoordinate.LastColumn || sc.Column == numberCoordinate.LastColumn - 1 || sc.Column == numberCoordinate.LastColumn + 1)))
            {
                //Console.WriteLine(numberCoordinate.Number);
                total += numberCoordinate.Number;
            }
        }

        return total;
    }

    private int SolvePart02()
    {
        char[,] inputBArray = new char[_input.Length, _input[0].Length];

        for (int x = 0; x < inputBArray.GetLength(0); x++)
        {
            for (int y = 0; y < inputBArray.GetLength(1); y++)
            {
                inputBArray[x, y] = _input[x][y];
            }
        }

        List<NumberCoordinate> numberCoordinateList = new List<NumberCoordinate>();
        List<SymbolCoordinate> symbolCoordinateList = new List<SymbolCoordinate>();

        for (int x = 0; x < inputBArray.GetLength(0); x++)
        {
            int firstNumberPosition = -1;
            for (int y = 0; y < inputBArray.GetLength(1); y++)
            {
                if (char.IsNumber(inputBArray[x, y]) && firstNumberPosition == -1)
                {
                    firstNumberPosition = y;
                    continue;
                }

                if (!char.IsNumber(inputBArray[x, y]) && firstNumberPosition != -1)
                {
                    string numberString = string.Empty;
                    for (int z = firstNumberPosition; z < y; z++)
                        numberString += inputBArray[x, z];

                    numberCoordinateList.Add(new NumberCoordinate(Convert.ToInt32(numberString), x, firstNumberPosition, y - 1));
                    firstNumberPosition = -1;
                }

                if (y + 1 >= inputBArray.GetLength(1) && firstNumberPosition != -1)
                {
                    string numberString = string.Empty;
                    for (int z = firstNumberPosition; z <= y; z++)
                        numberString += inputBArray[x, z];

                    numberCoordinateList.Add(new NumberCoordinate(Convert.ToInt32(numberString), x, firstNumberPosition, y));
                    firstNumberPosition = -1;

                    continue;
                }

                if (new List<char> { '*' }.Contains(inputBArray[x, y]))
                {
                    symbolCoordinateList.Add(new SymbolCoordinate(x, y));
                    continue;
                }
            }
        }

        int total = 0;

        foreach (SymbolCoordinate symbolCoordinate in symbolCoordinateList)
        {
            var result = numberCoordinateList.Where(numberCoordinate =>
                symbolCoordinate.Row >= numberCoordinate.Row - 1
                && symbolCoordinate.Row <= numberCoordinate.Row + 1
                && ((symbolCoordinate.Column >= numberCoordinate.FirstColumn - 1 && symbolCoordinate.Column <= numberCoordinate.FirstColumn + 1)
                    || (symbolCoordinate.Column >= numberCoordinate.LastColumn - 1 && symbolCoordinate.Column <= numberCoordinate.LastColumn + 1)));

            if (result.Count() == 2)
            {
                total += result.First().Number * result.Last().Number;
            }
        }

        return total;
    }
}

internal struct NumberCoordinate
{
    public int Number { get; }
    public int Row { get; }
    public int FirstColumn { get; }
    public int LastColumn { get; }

    public NumberCoordinate(int number, int row, int firstColumn, int lastColumn)
    {
        Number = number;
        Row = row;
        FirstColumn = firstColumn;
        LastColumn = lastColumn;
    }
}

internal struct SymbolCoordinate
{
    public int Row { get; }
    public int Column { get; }

    public SymbolCoordinate(int row, int column)
    {
        Row = row;
        Column = column;
    }
}

namespace AoC.Day01;

public class Day1Solver : ISolver
{
    public string DayName => "Secret Entrance";

    public long? SolvePart1(string input)
    {
        var dialPointer = 50;
        var numOfZeros = 0;

        foreach (var (dir, amount) in input.ReadLines().Select(line => (Dir: line[0] == 'L' ? -1 : 1, Amount: int.Parse(line[1..]))))
        {
            dialPointer += dir * amount;
            dialPointer %= 100;

            if (dialPointer == 0)
            {
                numOfZeros++;
            }
        }

        return numOfZeros;
    }

    public long? SolvePart2(string input)
    {
        return null;
    }
}

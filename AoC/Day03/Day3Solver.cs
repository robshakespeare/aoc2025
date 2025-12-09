namespace AoC.Day03;

public class Day3Solver : ISolver
{
    public string DayName => "Lobby";

    public long? SolvePart1(string input) => Solve(input, 2);

    public long? SolvePart2(string input) => Solve(input, 12);

    static long Solve(string input, int countToTurnOn)
    {
        var banks = input.ReadLines().Select(bank => bank.Select(bat => byte.Parse($"{bat}")).ToArray()).ToArray();

        return banks.Sum(bank =>
        {
            var position = 0;
            var turnedOn = new List<byte>();

            while (turnedOn.Count < countToTurnOn)
            {
                var remaining = countToTurnOn - turnedOn.Count - 1;
                var candidates = bank[position..^remaining];
                var max = candidates.Max();
                position += Array.IndexOf(candidates, max) + 1;
                turnedOn.Add(max);
            }

            return long.Parse(string.Concat(turnedOn));
        });
    }
}

namespace AoC.Day09;

public class Day9Solver : ISolver
{
    public string DayName => "Movie Theater";

    public long? SolvePart1(string input)
    {
        var points = ParseInput(input);
        return points.SelectMany((p1, i1) => points.Select((p2, i2) =>
        {
            var width = p2.X - p1.X + 1;
            var height = p2.Y - p1.Y + 1;
            return Math.Abs((long)width * (long)height);
        })).Max();
    }

    public long? SolvePart2(string input)
    {
        return null;
    }

    private static Vector2[] ParseInput(string input) =>
        input.ReadLines().Select(line => line.Split(",").Select(float.Parse).ToArray()).Select(c => new Vector2(c[0], c[1])).ToArray();
}

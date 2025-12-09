namespace AoC.Day04;

public class Day4Solver : ISolver
{
    public string DayName => "Printing Department";

    public long? SolvePart1(string input) => GetAdjacentPapers(ParseInput(input)).Count;

    public long? SolvePart2(string input)
    {
        var papers = ParseInput(input);

        var removedPapers = new HashSet<Vector2>();
        IReadOnlyList<Vector2> adjacentPapers;
        do
        {
            adjacentPapers = GetAdjacentPapers(papers);
            removedPapers.UnionWith(adjacentPapers);
            papers.ExceptWith(adjacentPapers);
        } while (adjacentPapers.Count > 0);

        return removedPapers.Count;
    }

    static IReadOnlyList<Vector2> GetAdjacentPapers(HashSet<Vector2> papers) => papers.Where(pos =>
    {
        var adjacentCount = GridUtils.DirectionsIncludingDiagonal.Count(dir => papers.Contains(pos + dir));
        return adjacentCount < 4;
    }).ToArray();

    static HashSet<Vector2> ParseInput(string input) =>
        input.ReadLines().SelectMany((line, y) => line.Select((c, x) => (c, x)).Where(i => i.c == '@').Select(i => new Vector2(i.x, y))).ToHashSet();
}

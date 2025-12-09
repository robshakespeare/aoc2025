namespace AoC.Day05;

public class Day5Solver : ISolver
{
    public string DayName => "Cafeteria";

    public long? SolvePart1(string input)
    {
        var (freshRanges, ingredientIds) = ParseInput(input);

        return ingredientIds.Count(ingredientId => freshRanges.Any(range => range.Contains(ingredientId)));
    }

    public long? SolvePart2(string input)
    {
        var (freshRanges, _) = ParseInput(input);

        int mergedCount;
        IReadOnlyCollection<Range> ranges = freshRanges;
        do
        {
            (ranges, mergedCount) = MergeRanges(ranges);
        }
        while (mergedCount > 0);

        return ranges.Sum(r => r.Size);
    }

    static (IReadOnlyCollection<Range> MergedRange, int MergedCount) MergeRanges(IReadOnlyCollection<Range> ranges)
    {
        var mergedRanges = new HashSet<Range>();
        var mergedCount = 0;
        foreach (var range in ranges)
        {
            var hasIntersection = false;
            foreach (var intersectedRange in ranges.Where(r => r.Intersects(range) && r != range))
            {
                hasIntersection = true;
                var mergedRange = new Range(Math.Min(range.Start, intersectedRange.Start), Math.Max(range.End, intersectedRange.End));
                if (mergedRanges.Add(mergedRange))
                {
                    mergedCount++;
                }
            }

            if (!hasIntersection)
            {
                mergedRanges.Add(range);
            }
        }

        return (mergedRanges, mergedCount);
    }

    record Range(long Start, long End)
    {
        public long Size { get; } = End - Start + 1;

        public bool Contains(long value) => value >= Start && value <= End;

        public bool Intersects(Range other) => (Start >= other.Start && Start <= other.End) || (End >= other.Start && Start <= other.End);
    }

    static (Range[], long[]) ParseInput(string input)
    {
        var parts = input.Split(Environment.NewLine + Environment.NewLine);
        return (
            parts[0].ReadLines().Select(line => line.Split('-').Select(long.Parse).ToArray()).Select(split => new Range(split[0], split[1])).ToArray(),
            parts[1].ReadLines().Select(long.Parse).ToArray()
        );
    }
}

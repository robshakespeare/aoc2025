namespace AoC.Day08;

public class Day8Solver : ISolver
{
    public string DayName => "Playground";

    public long? SolvePart1(string input)
    {
        var result = Solve(input, context =>
        {
            var targetConnections = context.Points.Length == 20 ? 10 : 1000;
            return context.DirectConnections.Count < targetConnections;
        });
        return result.Circuits.OrderByDescending(c => c.Count).Take(3).Aggregate(1, (acc, cur) => acc * cur.Count);
    }

    public long? SolvePart2(string input)
    {
        var result = Solve(input, context =>
        {
            var complete = context.Circuits.Count == 1 && context.Circuits.Single().Count == context.Points.Length;
            return !complete;
        });
        return (long)result.MostRecentPair.PointA.X * (long)result.MostRecentPair.PointB.X;
    }

    record Context(Vector3[] Points, List<HashSet<Vector3>> Circuits, HashSet<(Vector3, Vector3)> DirectConnections, (Vector3 PointA, Vector3 PointB) MostRecentPair)
    {
        public (Vector3 PointA, Vector3 PointB) MostRecentPair { get; set; } = MostRecentPair;
    }

    static Context Solve(string input, Func<Context, bool> shouldKeepConnecting)
    {
        var points = input.ReadLines().Select(line => line.Split(",").Select(float.Parse).ToArray()).Select(c => new Vector3(c[0], c[1], c[2])).ToArray();

        List<HashSet<Vector3>> circuits = [];
        HashSet<(Vector3, Vector3)> directConnections = [];
        Context context = new(points, circuits, directConnections, default);

        while (shouldKeepConnecting(context))
        {
            var (pointA, pointB) = context.MostRecentPair = FindClosestPairBruteForce(points, directConnections);

            directConnections.Add((pointA, pointB));

            var existingCircuits = circuits.Where(circuit => circuit.Contains(pointA) || circuit.Contains(pointB)).ToArray();
            if (existingCircuits.Length > 0)
            {
                circuits.RemoveAll(c => existingCircuits.Contains(c));
                circuits.Add([..existingCircuits.SelectMany(c => c).Concat([pointA, pointB])]);
            }
            else
            {
                circuits.Add([pointA, pointB]);
            }
        }

        return context;
    }

    private static (Vector3 PointA, Vector3 PointB) FindClosestPairBruteForce(IReadOnlyList<Vector3> points, HashSet<(Vector3, Vector3)> directConnections)
    {
        var bestDist = float.PositiveInfinity;
        var bestA = new Vector3();
        var bestB = new Vector3();

        for (var i = 0; i < points.Count; i++)
        {
            for (var j = i + 1; j < points.Count; j++)
            {
                var a = points[i];
                var b = points[j];

                if (directConnections.Contains((a, b)))
                {
                    continue;
                }

                var dist = Vector3.Distance(a, b);
                if (dist < bestDist)
                {
                    bestDist = dist;
                    bestA = a;
                    bestB = b;
                }
            }
        }

        return (bestA, bestB);
    }
}

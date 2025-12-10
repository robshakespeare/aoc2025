namespace AoC.Day06;

public class Day6Solver : ISolver
{
    public string DayName => "Trash Compactor";

    public long? SolvePart1(string input)
    {
        var matrix = input.ReadLines().Select(line => line.Split(' ', StringSplitOptions.RemoveEmptyEntries)).ToArray();

        var height = matrix.Length;
        var width = matrix[0].Length;

        return Enumerable.Range(0, width).Select(x =>
        {
            var op = matrix[^1][x];
            var result = Enumerable.Range(0, height - 1).Select(y => long.Parse(matrix[y][x])).Aggregate(1L, (acc, cur) => op switch
            {
                "*" => acc * cur,
                "+" => acc + cur,
                _ => throw new Exception("Invalid op: " + op),
            });

            return op == "+" ? result - 1 : result;
        }).Sum();
    }

    public long? SolvePart2(string input)
    {
        var lines = input.ReadLines().ToArray();

        var height = lines.Length;
        var width = lines.Select(line => line.Length).Max();

        lines = lines.Select(line => line.PadRight(width)).ToArray();

        List<int> separators = [];

        for (var x = 0; x < width; x++)
        {
            var isSeparator = Enumerable.Range(0, height).All(y => lines[y][x] == ' ');
            if (isSeparator)
            {
                separators.Add(x);
            }
        }

        var mathProblems = Enumerable.Range(0, separators.Count + 1).Select(_ => new MathProblem()).ToArray();

        foreach (var line in lines)
        {
            var x = 0;
            var prevSep = 0;
            foreach (var sep in separators)
            {
                mathProblems[x].Lines.Add(line[prevSep..sep]);
                prevSep = sep + 1;
                x++;
            }
            mathProblems[x].Lines.Add(line[prevSep..]);
        }

        return mathProblems.Reverse().Select(p => p.Calculate()).Sum();
    }

    class MathProblem
    {
        public List<string> Lines { get; } = [];

        public long Calculate()
        {
            var height = Lines.Count - 1;
            var width = Lines[0].Length;
            var op = Lines[^1].Trim();
            var values = Enumerable.Range(0, width).Reverse().Select(x => long.Parse(string.Concat(Enumerable.Range(0, height).Select(y => Lines[y][x]))));
            var result = values.Aggregate(1L, (acc, cur) => op switch
            {
                "*" => acc * cur,
                "+" => acc + cur,
                _ => throw new Exception("Invalid op: " + op),
            });

            return op == "+" ? result - 1 : result;
        }
    }
}

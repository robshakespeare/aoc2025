namespace AoC;

public partial class SolverFactory : ISolverFactory
{
    private const int NumOfPuzzleDays = 12;

    private SolverFactory()
    {
        AddSolver<Day00.Day0Solver>();
        AddSolver<Day01.Day1Solver>();
        AddSolver<Day02.Day2Solver>();
        AddSolver<Day03.Day3Solver>();
        AddSolver<Day04.Day4Solver>();
        AddSolver<Day05.Day5Solver>();
        AddSolver<Day06.Day6Solver>();
        AddSolver<Day07.Day7Solver>();
        AddSolver<Day08.Day8Solver>();
        AddSolver<Day09.Day9Solver>();
        AddSolver<Day10.Day10Solver>();
        AddSolver<Day11.Day11Solver>();
        AddSolver<Day12.Day12Solver>();

        Solvers = _solvers
            .Select(solver => solver.Key)
            .Select(CreateSolver)
            .Select(solver => (solver.GetDayNumber().ToString(), solver.DayName))
            .ToReadOnlyArray();
    }

    public IReadOnlyCollection<(string DayNumber, string DayName)> Solvers { get; }

    public string DefaultDay => GetDefaultDay(DateTime.Now).ToString();

    private static readonly Lazy<SolverFactory> LazyInstance = new(() => new SolverFactory());

    public static ISolverFactory Instance => LazyInstance.Value;

    private readonly Dictionary<string, Type> _solvers = [];

    internal static int GetDefaultDay(DateTime date) =>
        date.Month switch
        {
            1 or 2 => NumOfPuzzleDays,
            12 => Math.Min(date.Day, NumOfPuzzleDays),
            _ => 1
        };

    public ISolverBase? TryCreateSolver(string? dayNumber) => _solvers.TryGetValue(dayNumber ?? "", out var solverType)
        ? (ISolverBase?)Activator.CreateInstance(solverType)
        : null;

    public ISolverBase CreateSolver(string? dayNumber) => TryCreateSolver(dayNumber) ?? throw new InvalidOperationException($"No solver for day {dayNumber}.");

    private void AddSolver<TSolver>() where TSolver : ISolverBase => _solvers.Add(GetDayNumber(typeof(TSolver)).ToString(), typeof(TSolver));

    private static readonly Regex DayNumRegex = BuildDayNumRegex();

    [GeneratedRegex("Day(?<dayNum>\\d+)", RegexOptions.Compiled)]
    private static partial Regex BuildDayNumRegex();

    private static int GetDayNumber(Type solverType)
    {
        var fullName = solverType.FullName;
        Match match;

        if (fullName != null &&
            (match = DayNumRegex.Match(fullName)).Success &&
            match.Groups["dayNum"].Success)
        {
            return int.Parse(match.Groups["dayNum"].Value);
        }

        throw new InvalidOperationException("Unable to get day number from type name: " + fullName);
    }

    internal static int GetDayNumber(ISolverBase solver) => GetDayNumber(solver.GetType());
}

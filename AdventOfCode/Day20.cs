using AdventOfCode.Map;

namespace AdventOfCode;

public class Day20(string input) : IAdventDay
{
	private Map2D<char> Map { get; } = Map2D<char>.FromString(input);
	private Direction[] Directions { get; } = [Direction.Up, Direction.Right, Direction.Down, Direction.Left];

	public string Part1()
	{
		var path = GetPath();
		return GetCheats(path, 2).Where(w => w.Value <= -100).Count().ToString();
	}

	private HashSet<(Position2D position, int cost)> GetPath()
	{
		var current = Map.SearchAll('S').First();
		var end = Map.SearchAll('E').First();
		
		var path = new HashSet<(Position2D, int)>();
		var cost = 0;
		var direction = Direction.UpLeft;
		while (current != end)
		{
			path.Add((current, cost++));
			foreach (var dir in Directions)
			{
				if (direction.IsOpposite(dir))
					continue;

				var next = current.Move(dir);
				if (Map.OutOfBounds(next) || Map[next] == '#')
					continue;

				current = next;
				direction = dir;
				break;
			}
		}
		path.Add((current, cost++));

		return path;
	}

	private static Dictionary<string, int> GetCheats(HashSet<(Position2D position, int cost)> path, int timeout)
	{
		var cheatList = new Dictionary<string, int>();

		foreach (var step in path)
		{
			var close = SearchPath(path, step, timeout).Where(w => w.diff.Item1 - w.diff.Item2 < 0);

			foreach (var (position, cost, (diffDistance, diffCost)) in close)
			{
				cheatList.TryAdd($"{step.position} - {position}", diffDistance - diffCost);
			}
		}

		return cheatList;
	}

	private static IEnumerable<(Position2D position, int cost, (int, int) diff)> SearchPath(HashSet<(Position2D position, int cost)> path, (Position2D position, int cost) target, int distance) 
		=> path
			//only get steps further along the path 
			.Where(w => w.cost > target.cost)
			//only get steps within the desired distance
			.Where(w => (w.position - target.position).Steps <= distance)
			.Select(s => (s.position, s.cost, ((s.position - target.position).Steps, s.cost - target.cost)));

	public string Part2()
	{
		var path = GetPath();
		return GetCheats(path, 20).Where(w => w.Value <= -100).Count().ToString();
	}
}
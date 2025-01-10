using AdventOfCode.Map;

namespace AdventOfCode;

public class Day16(string input) : IAdventDay
{
	private Map2D<char> InputMap { get; } = Map2D<char>.FromString(input);
	private Direction[] Directions { get; } = [Direction.Up, Direction.Right, Direction.Down, Direction.Left];

	public string Part1()
	{
		var start = InputMap.SearchAll('S').First();
		var end = InputMap.SearchAll('E').First();

		var visited = new HashSet<Position2D>();
		var sortedSet = new SortedSet<(int Cost, Position2D Position, Direction Direction)>
		{
			new(0, start, Direction.Right)
		};

		while (sortedSet.Count > 0)
		{
			var (cost, current, direction) = sortedSet.Min;
			if (current == end)
				return cost.ToString();

			sortedSet.Remove(sortedSet.Min);

			if (!visited.Add(current))
				continue; // Skip if already visited

			foreach (var (pos, dir) in GetNext(current, direction))
			{
				var move = direction == dir ? 1 : 1001;

				if (!visited.Contains(pos))
					sortedSet.Add(new(cost + move, pos, dir));
			}
		}
		return "Nothing found";
	}

	private IEnumerable<(Position2D pos, Direction dir)> GetNext(Position2D current, Direction currentDirection)
	{
		foreach (var direction in Directions)
		{
			if (currentDirection.IsOpposite(direction))
				continue;

			var next = current.Move(direction);
			if (InputMap.OutOfBounds(next) || InputMap[next] == '#')
				continue;

			yield return (next, direction);
		}
	}

	public string Part2()
	{
		var start = InputMap.SearchAll('S').First();
		var end = InputMap.SearchAll('E').First();

		var sortedSet = new PriorityQueue<(Position2D Position, Direction Direction, HashSet<Position2D> History), int>();

		sortedSet.Enqueue((start, Direction.Right, []), 0);

		var visited = new Dictionary<Position2D, int>();
		var output = new HashSet<Position2D>();
		var minCost = int.MaxValue;

		while (sortedSet.TryDequeue(out var state, out var cost))
		{
			var (current, direction, history) = state;
			if (current == end)
			{
				if (cost < minCost)
				{
					minCost = cost;
					output = [];
				}

				if (cost == minCost)
					output = [.. output.Union(history), current];

				//skip processing adjacent tiles
				continue;
			}

			visited.TryAdd(current, cost);

			foreach (var (pos, dir) in GetNext(current, direction))
			{
				var move = direction == dir ? 1 : 1001;

				// limit the search to plausible similar length paths
				if (visited.TryGetValue(pos, out var prev) && Math.Abs(prev - cost + move) > 1000)
					continue;

				sortedSet.Enqueue((pos, dir, [.. history, current]), cost + move);
			}
		}
		return output.Count.ToString();
	}
}
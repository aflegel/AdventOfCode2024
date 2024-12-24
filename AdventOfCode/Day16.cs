using AdventOfCode.Map;

namespace AdventOfCode;

public class Day16(string input) : IAdventDay
{
	private Map2D<char> InputMap { get; } = Map2D<char>.FromString(input);
	private Direction[] Directions { get; } = [Direction.Up, Direction.Right, Direction.Down, Direction.Left];

	private record DijkstraNode(int Cost, Position2D Position, Direction Direction) : IComparable
	{
		public int CompareTo(object? obj)
		{
			if (obj is DijkstraNode other)
			{
				var cost = Cost.CompareTo(other.Cost);
				var position = Position.Compare(Position, other.Position);
				var direction = Direction.CompareTo(other.Direction);

				return cost != 0 ? cost : position != 0 ? position : direction;
			};

			return -1;
		}
	}

	public string Part1()
	{
		var start = InputMap.SearchAll('S').First();
		var end = InputMap.SearchAll('E').First();

		var sortedSet = new SortedSet<DijkstraNode>
		{
			new(0, start, Direction.Right)
		};

		while (sortedSet.Count > 0)
		{
			var (cost, current, direction) = sortedSet.Min;
			if (current == end)
				return cost.ToString();

			sortedSet.Remove(sortedSet.Min);

			foreach (var (pos, dir) in GetNext(current, direction))
			{
				var move = (direction, dir) switch
				{
					_ when direction == dir => 1,
					_ when Opposites(direction, dir) => 2001,
					_ when !Opposites(direction, dir) => 1001,
					_ => int.MaxValue
				};

				sortedSet.Add(new(cost + move, pos, dir));
			}
		}
		return "Nothing found";
	}

	private static bool Opposites(Direction a, Direction b) => a switch
	{
		Direction.Up => b == Direction.Down,
		Direction.Down => b == Direction.Up,
		Direction.Left => b == Direction.Right,
		Direction.Right => b == Direction.Left,
		_ => false
	};

	private IEnumerable<(Position2D pos, Direction dir)> GetNext(Position2D current, Direction currentDirection)
	{
		foreach (var direction in Directions)
		{
			if (Opposites(currentDirection, direction))
			{
				continue;
			}
			var next = current.Move(direction);
			if (InputMap.OutOfBounds(next) || InputMap[next] == '#')
			{
				continue;
			}

			yield return (next, direction);
		}
	}

	public string Part2() => throw new NotImplementedException();
}
using AdventOfCode.Map;

namespace AdventOfCode;

public class Day18(string input) : IAdventDay
{
	public int Width { get; init; } = 71;
	public int Height { get; init; } = 71;
	public int Time { get; init; } = 1024;

	private Position2D[] Input { get; } = [.. input.Split("\n").Select(s =>{
		var split = s.Split(',');
		return new Position2D(int.Parse(split[0]),int.Parse(split[1]));
	} )];
	private Direction[] Directions { get; } = [Direction.Up, Direction.Right, Direction.Down, Direction.Left];
	private Map2D<char> Map { get; set; }

	public string Part1()
	{
		Map = new Map2D<char>(Height, Width, '.');
		return Traverse(Time).ToString();
	}

	public int Traverse(int time)
	{
		var history = new HashSet<(int distance, Position2D position)>();
		var destination = new Position2D(Height - 1, Width - 1);
		var set = new SortedSet<(int distance, Position2D position)>()
		{
			(0, new Position2D(0, 0))
		};

		var bits = Input.Take(time).ToList();

		while (set.Count > 0)
		{
			var min = set.Min;

			if (min.position == destination)
				return min.distance;

			set.Remove(min);
			history.Add(min);

			foreach (var dir in Directions)
			{
				var next = min.position.Move(dir);
				if (Map.OutOfBounds(next) || bits.Contains(next))
					continue;

				if (history.Any(w => w.position == next && w.distance <= min.distance + 1))
					continue;

				set.Add((min.distance + 1, next));
			}
		}

		return -1;
	}

	public string Part2()
	{
		Map = new Map2D<char>(Height, Width, '.');

		var result = 0;
		var upper = Input.Length;
		var lower = Time;

		//implement a binary search for performance
		while (upper - lower > 1)
		{
			var index = (upper + lower) / 2;

			if (Traverse(index) > 0)
			{
				lower = index;
				result = index;
			}
			else
			{
				upper = index;
			}
		}

		return Input[result].ToString();
	}
}
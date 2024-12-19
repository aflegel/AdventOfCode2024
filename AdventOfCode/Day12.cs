namespace AdventOfCode;

public class Day12(string input) : IAdventDay
{
	private readonly Direction[] directions = [Direction.Up, Direction.Down, Direction.Left, Direction.Right];
	private readonly Direction[] diagonals = [Direction.UpLeft, Direction.UpRight, Direction.DownLeft, Direction.DownRight];

	private char[,] InputArray { get; } = input.Split("\n").To2DArray();

	public string Part1() => BuildMap().Select(CountEdges).Sum().ToString();

	private int CountEdges(HashSet<Position2D> map)
	{
		var sum = 0;

		foreach (var position in map)
		{

			var target = InputArray[position.X, position.Y];
			foreach (var direction in directions)
			{

				var next = position.Move(direction);
				if (InputArray.OutOfBounds(next))
				{
					sum += map.Count;
				}
				else if (InputArray[next.X, next.Y] != target)
				{
					sum += map.Count;
				}
			}
		}
		return sum;
	}

	private List<HashSet<Position2D>> BuildMap()
	{
		var map = new List<HashSet<Position2D>>();
		foreach (var position in InputArray.GetPositions())
		{
			if (map.Any(a => a.Contains(position)))
				continue;

			map.Add(GetRegion(position));
		}
		return map;
	}
	private HashSet<Position2D> GetRegion(Position2D position)
	{
		var target = InputArray[position.X, position.Y];
		var set = new HashSet<Position2D>
		{
			position
		};

		void Recurse(Position2D pos)
		{
			foreach (var direction in directions)
			{
				var next = pos.Move(direction);
				if (InputArray.OutOfBounds(next))
					continue;

				if (InputArray[next.X, next.Y] == target && !set.Contains(next))
				{
					set.Add(next);
					Recurse(next);
				}
			}
		}

		Recurse(position);

		return set;
	}

	public string Part2()
	{
		var map = BuildMap();

		var sum = map.Select(CountCorners).ToList();

		return sum.Sum().ToString();
	}

	private int CountCorners(HashSet<Position2D> map)
	{
		var sum = 0;
		var test = map.First();
		var target = InputArray[test.X, test.Y];

		foreach (var position in map)
		{
			var sides = EvaluateDirections(directions, target, position);

			if (sides.Count == 1)
			{
				//skip sides == 1
			}
			else if (sides.Count == 2)
			{
				//skip narrow 
				if (sides.Contains(Direction.Up) && !sides.Contains(Direction.Down))
					sum++;
				if (sides.Contains(Direction.Left) && !sides.Contains(Direction.Right))
					sum++;
			}
			else if (sides.Count is 3)
			{
				sum += 2;
			}
			else if (sides.Count is 4)
			{
				sum += 4;
			}
			else
			{
				var corners = EvaluateDirections(diagonals, target, position);
				if (corners.Count == 1)
					sum++;
			}
		}
		Console.WriteLine($"{target}: {map.Count} * {sum} = {sum * map.Count}");

		return sum * map.Count;
	}

	private List<Direction> EvaluateDirections(Direction[] directions, char target, Position2D position)
	{
		var sides = new List<Direction>();
		foreach (var direction in directions)
		{
			var next = position.Move(direction);
			if (InputArray.OutOfBounds(next))
			{
				sides.Add(direction);
			}
			else if (InputArray[next.X, next.Y] != target)
			{
				sides.Add(direction);
			}
		}

		return sides;
	}
}

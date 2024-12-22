using AdventOfCode.Map;
namespace AdventOfCode;

public class Day12(string input) : IAdventDay
{
	private readonly Direction[] directions = [Direction.Up, Direction.Down, Direction.Left, Direction.Right];
	private readonly Direction[] diagonals = [Direction.UpLeft, Direction.UpRight, Direction.DownLeft, Direction.DownRight];

	private Map2D<char> InputArray { get; } = Map2D<char>.FromString(input);

	public string Part1() => BuildMap().Select(CountEdges).Sum().ToString();

	private int CountEdges(HashSet<Position2D> map)
	{
		var sum = 0;

		foreach (var position in map)
		{
			var target = InputArray[position];
			foreach (var direction in directions)
			{
				var next = position.Move(direction);
				if (InputArray.OutOfBounds(next))
					sum += map.Count;
				else if (InputArray[next] != target)
					sum += map.Count;
			}
		}
		return sum;
	}

	private List<HashSet<Position2D>> BuildMap()
	{
		var map = new List<HashSet<Position2D>>();
		foreach (var position in InputArray.Positions())
		{
			if (map.Any(a => a.Contains(position)))
				continue;

			map.Add(GetRegion(position));
		}
		return map;
	}
	private HashSet<Position2D> GetRegion(Position2D position)
	{
		var target = InputArray[position];
		var set = new HashSet<Position2D> { position };

		void Recurse(Position2D pos)
		{
			foreach (var direction in directions)
			{
				var next = pos.Move(direction);
				if (InputArray.OutOfBounds(next))
					continue;

				if (InputArray[next] == target && !set.Contains(next))
				{
					set.Add(next);
					Recurse(next);
				}
			}
		}

		Recurse(position);

		return set;
	}

	public string Part2() => BuildMap().Select(CountCorners).Sum().ToString();

	private int CountCorners(HashSet<Position2D> map)
	{
		var corners = 0;

		foreach (var position in map)
		{
			var target = InputArray[position];
			var sides = EvaluateDirections(directions, target, position).ToList();

			//skip sides == 1
			if (sides.Count == 2 &&
				 ((sides.Contains(Direction.Up) ^ sides.Contains(Direction.Down))
					|| (sides.Contains(Direction.Left) ^ sides.Contains(Direction.Right))))
					corners++;
			else if (sides.Count is 3)
				corners += 2;
			else if (sides.Count is 4)
				corners += 4;

			corners += diagonals.Count(d => CheckCorner(position, d, target));
		}

		return corners * map.Count;
	}
	private record Corner(Position2D Diagonal, Position2D Alt1, Position2D Alt2);
	private bool CheckCorner(Position2D current, Direction direction, char target)
	{
		var corner = GetCorner(current, direction);
		//inside corners cannot have an out of bounds alt position
		if (InputArray.OutOfBounds(corner.Diagonal) || InputArray.OutOfBounds(corner.Alt1) || InputArray.OutOfBounds(corner.Alt2))
			return false;
		return InputArray[corner.Diagonal] != target
			&& InputArray[corner.Alt1] == target
			&& InputArray[corner.Alt2] == target;
	}

	private static Corner GetCorner(Position2D position, Direction direction) => direction switch
	{
		Direction.UpLeft => new(position.Move(Direction.UpLeft), position.Move(Direction.Left), position.Move(Direction.Up)),
		Direction.UpRight => new(position.Move(Direction.UpRight), position.Move(Direction.Right), position.Move(Direction.Up)),
		Direction.DownLeft => new(position.Move(Direction.DownLeft), position.Move(Direction.Left), position.Move(Direction.Down)),
		Direction.DownRight => new(position.Move(Direction.DownRight), position.Move(Direction.Right), position.Move(Direction.Down)),
		_ => throw new NotImplementedException()
	};

	private IEnumerable<Direction> EvaluateDirections(Direction[] directions, char target, Position2D position)
	{
		foreach (var direction in directions)
		{
			var next = position.Move(direction);
			if (InputArray.OutOfBounds(next))
				yield return direction;
			else if (InputArray[next] != target)
				yield return direction;
		}
	}
}

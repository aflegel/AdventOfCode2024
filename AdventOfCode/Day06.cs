
using AdventOfCode.Map;

namespace AdventOfCode;

public class Day06(string input) : IAdventDay
{
	private Map2D<char> InputArray { get; } = Map2D<char>.FromString(input);

	public string Part1()
	{
		var current = InputArray.SearchAll('^').First();
		var currentDirection = Direction.Up;

		var set = Traverse(InputArray, current, currentDirection);
		return set.Select(s => s.Item1).Distinct().Count().ToString();
	}

	private static Direction Rotate(Direction direction) => direction switch
	{
		Direction.Up => Direction.Right,
		Direction.Down => Direction.Left,
		Direction.Left => Direction.Up,
		Direction.Right => Direction.Down,
		_ => throw new NotImplementedException()
	};

	public string Part2()
	{
		var start = InputArray.SearchAll('^').First();
		var current = start;
		var currentDirection = Direction.Up;

		var set = new HashSet<Position2D>();

		while (true)
		{
			var next = current.Move(currentDirection);
			if (InputArray.OutOfBounds(next))
				break;

			for (var i = 0; i < 2; i++)
			{
				if (InputArray[next] == '#')
				{
					currentDirection = Rotate(currentDirection);
					next = current.Move(currentDirection);
				}
				else
					break;
			}
			if (next != start)
			{
				var modified = InputArray.Clone();
				modified[next] = '#';

				if (Traverse(modified, start, Direction.Up).Count == 0)
				{
					set.Add(next);
				}
			}
			current = next;
		}

		return set.Count.ToString();
	}

	private static HashSet<(Position2D, Direction)> Traverse(Map2D<char> input, Position2D current, Direction direction)
	{
		var set = new HashSet<(Position2D, Direction)>();

		while (true)
		{
			if (set.Contains((current, direction)))
				return [];

			set.Add((current, direction));

			var next = current.Move(direction);

			if (input.OutOfBounds(next))
				return set;

			for (var i = 0; i < 2; i++)
			{
				if (input[next] == '#')
				{
					direction = Rotate(direction);
					next = current.Move(direction);
				}
				else
					break;
			}

			current = next;
		}
	}
}
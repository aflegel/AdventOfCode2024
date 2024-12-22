using AdventOfCode.Map;

namespace AdventOfCode;

public class Day15 : IAdventDay
{
	private Map2D<char> InputMap { get; }
	private Direction[] InputInstructions { get; }

	public Day15(string input)
	{
		var split = input.Split("\n\n");

		InputMap = Map2D<char>.FromString(split[0]);
		InputInstructions = [.. split[1].Replace("\n", "").Select(c => c switch
		{
			'^' => Direction.Up,
			'v' => Direction.Down,
			'<' => Direction.Left,
			'>' => Direction.Right,
			_ => throw new InvalidOperationException()
		})];
	}


	public string Part1()
	{
		var robot = InputMap.SearchAll('@').First();
		var walls = InputMap.SearchAll('#').ToList();
		var boxes = InputMap.SearchAll('O').ToList();

		bool Attempt(Position2D next, Direction instruction)
		{
			if (walls.Contains(next))
				return false;

			if (boxes.Contains(next))
			{
				var nextBox = next.Move(instruction);
				if (!Attempt(nextBox, instruction))
					return false;

				boxes.Remove(next);
				boxes.Add(nextBox);
			}

			return true;
		}

		foreach (var instruction in InputInstructions)
		{
			var next = robot.Move(instruction);

			if (Attempt(next, instruction))
				robot = next;
		}

		return boxes.Sum(s => s.X + s.Y * 100).ToString();
	}

	public string Part2() => throw new NotImplementedException();
}


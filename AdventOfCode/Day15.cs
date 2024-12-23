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

	public string Part2()
	{
		var robot = InputMap.SearchAll('@').Select(Double).First();
		var walls = InputMap.SearchAll('#').SelectMany(w => new[] { Double(w), Double(w).Move(Direction.Right) }).ToList();
		var boxesLeft = InputMap.SearchAll('O').Select(Double).ToList();
		var boxesRight = InputMap.SearchAll('O').Select(s => Double(s).Move(Direction.Right)).ToList();

		var boxesLeftSet = new HashSet<Position2D>();
		var boxesRightSet = new HashSet<Position2D>();

		bool Attempt(Position2D next, Direction instruction)
		{
			if (walls.Contains(next))
				return false;

			switch (instruction)
			{
				case Direction.Up:
				case Direction.Down:
					if (boxesLeft.Contains(next))
					{
						//get matching box piece
						var rightBox = next.Move(Direction.Right).Move(instruction);
						var nextBox = next.Move(instruction);

						if (!Attempt(rightBox, instruction) || !Attempt(nextBox, instruction))
							return false;

						boxesLeftSet.Add(next);
						boxesRightSet.Add(next.Move(Direction.Right));
					}

					if (boxesRight.Contains(next))
					{
						//get matching box piece
						var leftBox = next.Move(Direction.Left).Move(instruction);
						var nextBox = next.Move(instruction);

						if (!Attempt(leftBox, instruction) || !Attempt(nextBox, instruction))
							return false;

						boxesRightSet.Add(next);
						boxesLeftSet.Add(next.Move(Direction.Left));
					}

					break;
				case Direction.Left:
					if (boxesRight.Contains(next))
					{
						var nextBox = next.Move(instruction, 2);
						if (!Attempt(nextBox, instruction))
							return false;
						boxesRightSet.Add(next);
						boxesLeftSet.Add(next.Move(instruction));
					}
					break;
				case Direction.Right:
					if (boxesLeft.Contains(next))
					{
						var nextBox = next.Move(instruction, 2);
						if (!Attempt(nextBox, instruction))
							return false;
						boxesLeftSet.Add(next);
						boxesRightSet.Add(next.Move(instruction));
					}
					break;
			}
			return true;
		}

		foreach (var instruction in InputInstructions)
		{
			var next = robot.Move(instruction);
			if (Attempt(next, instruction))
			{
				robot = next;

				foreach (var box in boxesLeftSet)
				{
					boxesLeft.Remove(box);
					boxesLeft.Add(box.Move(instruction));
				}
				foreach (var box in boxesRightSet)
				{
					boxesRight.Remove(box);
					boxesRight.Add(box.Move(instruction));
				}
			}

			boxesLeftSet = [];
			boxesRightSet = [];
		}

		return boxesLeft.Sum(s => s.X + s.Y * 100).ToString();
	}

	public static Position2D Double(Position2D pos) => new(pos.X * 2, pos.Y);
}


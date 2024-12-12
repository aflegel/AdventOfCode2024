namespace AdventOfCode;

public partial class Day04(string input) : IAdventDay
{

	private char[,] InputArray { get; } = input.Split("\n").To2DArray();
	private enum Direction
	{
		Up,
		Down,
		Left,
		Right,
		UpRight,
		UpLeft,
		DownRight,
		DownLeft,
	}

	public string Part1()
	{
		var found = 0;
		for (var i = 0; i < InputArray.GetLength(0); i++)
		{
			for (var j = 0; j < InputArray.GetLength(1); j++)
			{
				if (InputArray[i, j] == 'X')
				{
					foreach (var name in Enum.GetValues<Direction>())
					{
						if (SeekWord((i, j), name))
							found++;
					}
				}
			}
		}

		return found.ToString();
	}

	private bool SeekWord((int, int) position, Direction direction, string word = "XMAS")
	{
		if (word.Length == 0)
			return true;

		if (InputArray.OutOfBounds(position))
			return false;

		return InputArray[position.Item1, position.Item2] == word[0] && SeekWord(Next(position, direction), direction, word[1..]);
	}

	private static (int, int) Next((int x, int y) position, Direction direction) => direction switch
	{
		Direction.Up => (position.x, position.y + 1),
		Direction.Down => (position.x, position.y - 1),
		Direction.Left => (position.x + 1, position.y),
		Direction.Right => (position.x - 1, position.y),
		Direction.UpRight => (position.x - 1, position.y + 1),
		Direction.UpLeft => (position.x + 1, position.y + 1),
		Direction.DownRight => (position.x - 1, position.y - 1),
		Direction.DownLeft => (position.x + 1, position.y - 1),
		_ => throw new NotImplementedException()
	};

	private static readonly Direction[] seeking = [Direction.DownLeft, Direction.DownRight, Direction.Down, Direction.Up];

	public string Part2()
	{
		var found = 0;
		for (var i = 0; i < InputArray.GetLength(0); i++)
		{
			for (var j = 0; j < InputArray.GetLength(1); j++)
			{
				if (InputArray[i, j] == 'M')
				{
					foreach (var name in seeking)
					{
						if (SeekPattern((i, j), name))
							found++;
					}
				}
			}
		}

		return found.ToString();
	}

	private bool SeekPattern((int, int) position, Direction direction)
	{
		var pattern = NextPattern(position, direction);

		if (InputArray.OutOfBounds(pattern.M2) || InputArray.OutOfBounds(pattern.A) || InputArray.OutOfBounds(pattern.S1) || InputArray.OutOfBounds(pattern.S2))
			return false;
		
		if(InputArray[pattern.M2.Item1,pattern.M2.Item2] != 'M') return false;
		if(InputArray[pattern.A.Item1,pattern.A.Item2] != 'A') return false;
		if(InputArray[pattern.S1.Item1,pattern.S1.Item2] != 'S') return false;
		if(InputArray[pattern.S2.Item1,pattern.S2.Item2] != 'S') return false;

		return true;
	}

	private record Pattern((int, int) M1, (int, int) M2, (int, int) A, (int, int) S1, (int, int) S2);

	private static Pattern NextPattern((int x, int y) position, Direction direction) => direction switch
	{
		Direction.Down => new Pattern(position, (position.x, position.y + 2), (position.x + 1, position.y + 1), (position.x + 2, position.y), (position.x + 2, position.y + 2)),
		Direction.Up => new Pattern(position, (position.x, position.y + 2), (position.x - 1, position.y + 1), (position.x - 2, position.y), (position.x - 2, position.y + 2)),
		Direction.DownRight => new Pattern(position, (position.x + 2, position.y), (position.x + 1, position.y + 1), (position.x, position.y + 2), (position.x + 2, position.y + 2)),
		Direction.DownLeft => new Pattern(position, (position.x + 2, position.y), (position.x + 1, position.y - 1), (position.x, position.y - 2), (position.x + 2, position.y - 2)),
		_ => throw new NotImplementedException()
	};
}
using AdventOfCode.Legacy;
namespace AdventOfCode;

public partial class Day04(string input) : IAdventDay
{

	private char[,] InputArray { get; } = input.Split("\n").To2DArray();

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
						if (SeekWord(new(i, j), name))
							found++;
					}
				}
			}
		}

		return found.ToString();
	}

	private bool SeekWord(Position2D position, Direction direction, string word = "XMAS")
	{
		if (word.Length == 0)
			return true;

		if (InputArray.OutOfBounds(position))
			return false;

		return InputArray[position.X, position.Y] == word[0] && SeekWord(position.Move(direction), direction, word[1..]);
	}

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
						if (SeekPattern(new(i, j), name))
							found++;
					}
				}
			}
		}

		return found.ToString();
	}

	private bool SeekPattern(Position2D position, Direction direction)
	{
		var pattern = NextPattern(position, direction);

		if (InputArray.OutOfBounds(pattern.M2) || InputArray.OutOfBounds(pattern.A) || InputArray.OutOfBounds(pattern.S1) || InputArray.OutOfBounds(pattern.S2))
			return false;

		if (InputArray[pattern.M2.X, pattern.M2.Y] != 'M')
			return false;
		if (InputArray[pattern.A.X, pattern.A.Y] != 'A')
			return false;
		if (InputArray[pattern.S1.X, pattern.S1.Y] != 'S')
			return false;
		if (InputArray[pattern.S2.X, pattern.S2.Y] != 'S')
			return false;

		return true;
	}

	private record Pattern(Position2D M1, Position2D M2, Position2D A, Position2D S1, Position2D S2);

	private static Pattern NextPattern(Position2D position, Direction direction) => direction switch
	{
		Direction.Down => new Pattern(position, new(position.X, position.Y + 2), new(position.X + 1, position.Y + 1), new(position.X + 2, position.Y), new(position.X + 2, position.Y + 2)),
		Direction.Up => new Pattern(position, new(position.X, position.Y + 2), new(position.X - 1, position.Y + 1), new(position.X - 2, position.Y), new(position.X - 2, position.Y + 2)),
		Direction.DownRight => new Pattern(position, new(position.X + 2, position.Y), new(position.X + 1, position.Y + 1), new(position.X, position.Y + 2), new(position.X + 2, position.Y + 2)),
		Direction.DownLeft => new Pattern(position, new(position.X + 2, position.Y), new(position.X + 1, position.Y - 1), new(position.X, position.Y - 2), new(position.X + 2, position.Y - 2)),
		_ => throw new NotImplementedException()
	};
}
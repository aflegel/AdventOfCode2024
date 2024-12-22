using AdventOfCode.Map;

namespace AdventOfCode;

public partial class Day04(string input) : IAdventDay
{

	private Map2D<char> InputArray { get; } = Map2D<char>.FromString(input);

	public string Part1()
	{
		var found = 0;
		foreach (var x in InputArray.SearchAll('X'))
		{
			foreach (var name in Enum.GetValues<Direction>())
			{
				if (SeekWord(x, name))
					found++;
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

		return InputArray[position] == word[0] && SeekWord(position.Move(direction), direction, word[1..]);
	}

	private static readonly Direction[] seeking = [Direction.DownLeft, Direction.DownRight, Direction.Down, Direction.Up];

	public string Part2()
	{
		var found = 0;
		foreach (var x in InputArray.SearchAll('M'))
		{
			foreach (var name in seeking)
			{
				if (SeekPattern(x, name))
					found++;
			}
		}
		return found.ToString();
	}

	private bool SeekPattern(Position2D position, Direction direction)
	{
		var pattern = NextPattern(position, direction);

		if (InputArray.OutOfBounds(pattern.M2) || InputArray.OutOfBounds(pattern.A) || InputArray.OutOfBounds(pattern.S1) || InputArray.OutOfBounds(pattern.S2))
			return false;

		return InputArray[pattern.M2] == 'M' 
			&& InputArray[pattern.A] == 'A' 
			&& InputArray[pattern.S1] == 'S' 
			&& InputArray[pattern.S2] == 'S';
	}

	private record Pattern(Position2D M1, Position2D M2, Position2D A, Position2D S1, Position2D S2);

	private static Pattern NextPattern(Position2D position, Direction direction) => direction switch
	{
		Direction.Down => new Pattern(position, new(position.X, position.Y + 2), position.Move(Direction.DownRight), new(position.X + 2, position.Y), new(position.X + 2, position.Y + 2)),
		Direction.Up => new Pattern(position, new(position.X, position.Y + 2), new(position.X - 1, position.Y + 1), new(position.X - 2, position.Y), new(position.X - 2, position.Y + 2)),
		Direction.DownRight => new Pattern(position, new(position.X + 2, position.Y), new(position.X + 1, position.Y + 1), new(position.X, position.Y + 2), new(position.X + 2, position.Y + 2)),
		Direction.DownLeft => new Pattern(position, new(position.X + 2, position.Y), new(position.X + 1, position.Y - 1), new(position.X, position.Y - 2), new(position.X + 2, position.Y - 2)),
		_ => throw new NotImplementedException()
	};
}
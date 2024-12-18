namespace AdventOfCode;

public class Day10(string input) : IAdventDay
{
	private readonly Direction[] directions = [Direction.Up, Direction.Down, Direction.Left, Direction.Right];
	private char[,] InputArray { get; } = input.Split("\n").To2DArray();

	public string Part1() => InputArray.SearchAll('0').Select(w => FindSummits(w, 0).Count).Sum().ToString();

	private HashSet<Position2D> FindSummits(Position2D position, int current = 0)
	{
		if (current == 9)
			return [position];

		var sum = new HashSet<Position2D>();
		foreach (var direction in directions)
		{
			var next = position.Move(direction);
			if (InputArray.OutOfBounds(next))
				continue;

			var nextHeight = InputArray[next.X, next.Y].ToInt();

			if (nextHeight == current + 1)
				sum.UnionWith(FindSummits(next, nextHeight));
		}

		return sum;
	}


	public string Part2() => InputArray.SearchAll('0').Select(w => FindPaths(w, 0)).Sum().ToString();

	private int FindPaths(Position2D position, int current)
	{
		if (current == 9)
			return 1;

		var sum = 0;
		foreach (var direction in directions)
		{
			var next = position.Move(direction);
			if (InputArray.OutOfBounds(next))
				continue;

			var nextHeight = InputArray[next.X, next.Y].ToInt();

			if (nextHeight == current + 1)
				sum += FindPaths(next, nextHeight);
		}

		return sum;
	}
}

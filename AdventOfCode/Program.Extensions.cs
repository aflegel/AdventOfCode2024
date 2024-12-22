using System.Text;
namespace AdventOfCode.Legacy;

public static class PrintExtensions
{
	public static void PrintToConsole(this char[,] array)
	{
		for (var i = 0; i < array.GetLength(0); i++)
		{
			for (var j = 0; j < array.GetLength(1); j++)
			{
				Console.Write(array[i, j]);
			}
			Console.WriteLine();
		}
	}

	public static string MakeString(this char[,] array)
	{
		var result = new StringBuilder();
		for (var i = 0; i < array.GetLength(0); i++)
		{
			for (var j = 0; j < array.GetLength(1); j++)
			{
				result.Append(array[i, j]);
			}
			result.Append('\n');
		}

		return result.ToString();
	}
}
public enum Direction
{
	Up,
	UpRight,
	UpLeft,
	Down,
	DownRight,
	DownLeft,
	Left,
	Right
}

public record Position2D(int X, int Y)
{
	public static Position2D operator -(Position2D a, Position2D b) => new(a.X - b.X, a.Y - b.Y);
	public static Position2D operator +(Position2D a, Position2D b) => new(a.X + b.X, a.Y + b.Y);
	public static Position2D operator %(Position2D a, Position2D b) => new(a.X % b.Y, a.Y % b.Y);

	public static Position2D operator *(Position2D a, int b) => new(a.X * b, a.Y * b);
	public static Position2D operator /(Position2D a, int b) => new(a.X / b, a.Y / b);

	public override string ToString() => $"({X}, {Y})";
}

internal static class GridExtensions
{
	public static bool OutOfBounds<T>(this T[,] array, Position2D position) => position.X < 0 || position.Y < 0 || position.X >= array.GetLength(0) || position.Y >= array.GetLength(1);

	public static Position2D Move(this Position2D current, Direction direction) => direction switch
	{
		Direction.Up => current + new Position2D(-1, 0),
		Direction.Down => current + new Position2D(1, 0),
		Direction.Left => current + new Position2D(0, -1),
		Direction.Right => current + new Position2D(0, 1),
		Direction.UpLeft => current + new Position2D(-1, -1),
		Direction.UpRight => current + new Position2D(-1, 1),
		Direction.DownLeft => current + new Position2D(1, -1),
		Direction.DownRight => current + new Position2D(1, 1),
		_ => throw new NotImplementedException(),
	};
}

internal static class EnumerableExtensions
{
	/**
	Source https://gist.github.com/kekyo/2e0c456f506ec31431f33741608d5230
	*/
	public static T[,] To2DArray<T>(this IEnumerable<IEnumerable<T>> source)
	{
		var data = source
			.Select(x => x.ToArray())
			.ToArray();

		var res = new T[data.Length, data.Max(x => x.Length)];
		for (var i = 0; i < data.Length; ++i)
		{
			for (var j = 0; j < data[i].Length; ++j)
			{
				res[i, j] = data[i][j];
			}
		}

		return res;
	}

	public static IEnumerable<Position2D> GetPositions(this char[,] source)
	{
		for (var i = 0; i < source.GetLength(0); i++)
		{
			for (var j = 0; j < source.GetLength(1); j++)
			{
				yield return new(i, j);
			}
		}
	}

	public static IEnumerable<Position2D> SearchAll(this char[,] source, char target)
		=> source.GetPositions()
			.Where(w => source[w.X, w.Y] == target);
}

internal static class CharExtensions
{
	public static int ToInt(this char c) => c - '0';
}
using System.Text;
namespace AdventOfCode;

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

internal static class GridExtensions
{
	public static bool OutOfBounds<T>(this T[,] array, (int x, int y) position) => position.x < 0 || position.y < 0 || position.x >= array.GetLength(0) || position.y >= array.GetLength(1);
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
}
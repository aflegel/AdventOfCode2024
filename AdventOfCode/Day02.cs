namespace AdventOfCode;

public class Day02(string input) : IAdventDay
{
	private int[][] InputArray { get; } = [.. input.Split("\n").Select(s => s.Split(" ").Select(s => Convert.ToInt32(s)).ToArray())];

	public string Part1() => InputArray.Where(Verify).Count().ToString();

	private static bool Verify(int[] input)
	{
		var increase = input[1] > input[0];
		for (var i = 1; i < input.Length; i++)
		{
			var diff = input[i] - input[i - 1];

			if (increase && diff < 0)
				return false;
			else if (!increase && diff > 0)
				return false;

			if (Math.Abs(diff) > 3)
				return false;
			if (diff == 0)
				return false;
		}

		return true;
	}

	public string Part2() => InputArray.Where(VerifySafeRemove).Count().ToString();

	private static bool VerifySafeRemove(int[] input)
	{
		if (Verify(input))
			return true;

		for (var i = 0; i < input.Length; i++)
		{
			if (Verify([.. input.Where((s, rem) => rem != i)]))
				return true;
		}

		return false;
	}
}

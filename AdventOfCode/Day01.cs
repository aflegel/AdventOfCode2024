namespace AdventOfCode;

public class Day01(string input) : IAdventDay
{
	private (int, int)[] InputArray { get; } = [.. input.Split("\n").Select(s =>
	{
		var split = s.Split(" ").Where(s => !string.IsNullOrWhiteSpace(s)).ToArray();

		return (Convert.ToInt32(split[0]), Convert.ToInt32(split[1]));
	})];

	public string Part1()
	{
		var left = InputArray.Select(s => s.Item1).OrderBy(s => s).ToList();
		var right = InputArray.Select(s => s.Item2).OrderBy(s => s).ToList();

		return left.Select((s, i) => Math.Abs(left[i] - right[i])).Sum().ToString();
	}


	public string Part2()
	{
		var left = InputArray.Select(s => s.Item1).ToList();
		var right = InputArray.Select(s => s.Item2).ToList();

		return left.Select(s => right.Count(c => c == s) * s).Sum().ToString();
	}
}

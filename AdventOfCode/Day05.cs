using System.Data;

namespace AdventOfCode;

public partial class Day05 : IAdventDay
{
	private record PrintingRule(int First, int Second);
	private PrintingRule[] InputRules { get; }
	private int[][] InputArray { get; }

	public Day05(string input)
	{
		var lines = input.Split("\n\n");

		InputRules = [.. lines[0].Split("\n").Select(s =>
		{
			var rule = s.Split("|");
			return new PrintingRule(Convert.ToInt32(rule[0]), Convert.ToInt32(rule[1]));
		})];

		InputArray = [.. lines[1].Split("\n").Select(s => s.Split(",").Select(ss => Convert.ToInt32(ss)).ToArray())];
	}

	public string Part1() => InputArray.Where(Validate).Select(s => s[s.Length / 2]).Sum().ToString();

	private bool Validate(int[] input) =>
		!InputRules.Any(rule => input.Contains(rule.First) && input.Contains(rule.Second) && (Array.IndexOf(input, rule.First) > Array.IndexOf(input, rule.Second)));

	public string Part2() => InputArray.Where(s => !Validate(s)).Select(FixInput).Select(s => s[s.Length / 2]).Sum().ToString();

	private int[] FixInput(int[] input)
	{
		foreach (var rule in InputRules)
		{
			if (input.Contains(rule.First) && input.Contains(rule.Second))
			{
				var i = Array.IndexOf(input, rule.First);
				var j = Array.IndexOf(input, rule.Second);

				if (i > j)
				{
					var fixedIt = input.ToList();
					fixedIt.RemoveAt(j);
					fixedIt.Insert(i, rule.Second);
					return FixInput([.. fixedIt]);
				}
			}
		}
		return input;
	}
}
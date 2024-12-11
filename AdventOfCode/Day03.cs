using System.Text.RegularExpressions;

namespace AdventOfCode;

public partial class Day03(string input) : IAdventDay
{
	[GeneratedRegex(@"mul\((\d+),(\d+)\)")]
	private static partial Regex MultiplierGroups();

	[GeneratedRegex(@"don\'t\(\)")]
	private static partial Regex DontGroups();

	[GeneratedRegex(@"do\(\)")]
	private static partial Regex DoGroups();

	private string InputArray { get; } = input;

	private static int Mul(Match s) => Convert.ToInt32(s.Groups[1].Value) * Convert.ToInt32(s.Groups[2].Value);

	public string Part1() => MultiplierGroups().Matches(InputArray).Sum(Mul).ToString();

	public string Part2()
	{
		var dos = DoGroups().Matches(InputArray).Select(s => s.Index).ToList();
		var donts = DontGroups().Matches(InputArray).Select(s => s.Index).ToList();

		return MultiplierGroups().Matches(InputArray).Sum(m =>
		{
			var should = dos.LastOrDefault(s => s <= m.Index);
			var shouldNot = donts.LastOrDefault(s => s <= m.Index);
			return should >= shouldNot ? Mul(m) : 0;
		}).ToString();
	}
}

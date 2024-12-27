namespace AdventOfCode;

public class Day19(string input) : IAdventDay
{
	private string[] Patterns { get; } = [.. input.Split("\n\n")[0].Split(",").Select(s => s.Trim())];
	private string[] Designs { get; } = [.. input.Split("\n\n")[1].Split("\n")];

	public string Part1() => Designs.Select(Process).Count(w => w is not null).ToString();

	private string? Process(string design)
	{
		if (design.Length == 0)
			return "";
		var available = Patterns.Where(w => w.StartsWith(design[0]));
		foreach (var pattern in available)
		{
			if (design.Length >= pattern.Length && design[..pattern.Length] == pattern)
			{
				var result = Process(design[pattern.Length..]);
				if (result is not null)
					return $"{pattern}{result}";
			}
		}

		return null;
	}

	public string Part2() => Designs.Select(ProcessSet).Sum().ToString();

	private int ProcessSet(string design)
	{
		if (design.Length == 0)
			return 1;

		var available = Patterns.Where(w => w.StartsWith(design[0]));
		var count = 0;
		foreach (var pattern in available)
		{
			if (design.Length >= pattern.Length && design[..pattern.Length] == pattern)
			{
				var results = ProcessSet(design[pattern.Length..]);
				count += results;
			}
		}

		return count;
	}
}
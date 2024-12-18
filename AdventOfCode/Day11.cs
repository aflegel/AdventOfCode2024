namespace AdventOfCode;

public class Day11(string input) : IAdventDay
{
	private long[] InputArray { get; } = [.. input.Split(" ").Select(s => Convert.ToInt32(s))];

	public string Part1() => Process(25).Sum(s => s.Value).ToString();

	private Dictionary<long, long> Process(int length)
	{
		var dictionary = InputArray.GroupBy(g => g).ToDictionary(key => key.Key, val => (long)val.Count());

		for (var i = 0; i < length; i++)
		{
			var iteration = new Dictionary<long, long>();
			foreach (var kvp in dictionary)
			{
				var results = Blink(kvp.Key);

				foreach (var result in results)
				{
					if (!iteration.ContainsKey(result))
						iteration.Add(result, kvp.Value);
					else
						iteration[result] += kvp.Value;
				}
			}
			dictionary = iteration;
		}

		return dictionary;
	}

	private static IEnumerable<long> Blink(long i)
	{
		var order = i.ToString().Length;

		if (i == 0)
			yield return 1;

		else if (order % 2 == 0)
		{
			var math = (int)Math.Pow(10, order / 2);
			yield return i / math;
			yield return i % math;
		}
		else
		{
			yield return i * 2024;
		}
	}

	public string Part2() => Process(75).Sum(s => s.Value).ToString();
}

namespace AdventOfCode;

public class Day22(string input) : IAdventDay
{
	private int[] Codes { get; } = [.. input.Split("\n").Select(int.Parse)];
	public string Part1() => Codes.Select(s => CalculateSecret((ulong)s, 2000)).Aggregate((a, b) => a + b).ToString();

	private static ulong CalculateSecret(ulong id, int depth)
	{
		var secret = id;
		for (var i = 0; i < depth; i++)
			secret = CalculateSecret(secret);
		return secret;
	}

	private static ulong CalculateSecret(ulong id)
	{
		var output = id;
		output ^= output * 64;
		output %= 16777216;
		output ^= output / 32;
		output %= 16777216;
		output ^= output * 2048;
		output %= 16777216;
		return output;
	}

	public string Part2()
	{
		var sequences = Codes.Select(s => CalculateSeries((ulong)s, 2000)).Select(s => s.Skip(4).Select(ss => ss.sequence).Distinct()
			.Select(sequence => (sequence, s.First(f => f.sequence == sequence).price)).ToList()).ToList();

		var combinedSequences = sequences.SelectMany(s => s).GroupBy(g => g.sequence)
			.ToDictionary(key => key.Key, sum => sum.Sum(s => s.price));

		return combinedSequences.Max(m => m.Value).ToString();
	}

	private static IEnumerable<(int price, int change, string sequence)> CalculateSeries(ulong id, int depth)
	{
		var sequence = new List<int>();
		var secret = id;
		for (var i = 0; i < depth; i++)
		{
			var prev = (int)secret % 10;
			secret = CalculateSecret(secret);

			var delta = (int)secret % 10 - prev;

			if (sequence.Count > 3)
				sequence = sequence[1..];
			sequence.Add(delta);
			yield return ((int)secret % 10, delta, string.Join(",", sequence));
		}
	}
}

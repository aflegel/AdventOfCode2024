using AdventOfCode.Map;

namespace AdventOfCode;

public class Day23(string input) : IAdventDay
{
	private string[][] Computers { get; } = [.. input.Split("\n").Select(s => new string[2] { s[..2], s[3..] })];

	public string Part1()
	{
		//var connections = new Dictionary<string, HashSet<string>>();

		var set = Computers.SelectMany(s => s).Distinct()
			.ToDictionary(s => s, _ => new HashSet<string>());

		//build connection map by computer
		foreach (var key in set.Keys)
		{
			var connections = Computers.Where(w => w.Contains(key)).ToList();

			var test = connections.SelectMany(s => s).ToHashSet();
			test.Remove(key);
			set[key] = test;
		}

		var network = new HashSet<string>();

		//find intersections
		foreach (var a in set.Keys)
		{
			foreach (var b in set[a])
			{
				if (a == b)
					continue;

				var intersect = set[a].Intersect(set[b]).ToArray();

				if (intersect.Length != 0)
				{
					foreach (var i in intersect)
						network.Add(string.Join(",", new List<string>() { a, b, i }.OrderBy(s => s)));
				}
			}
		}

		return network.Count(w => w.Split(",").Any(a => a.StartsWith('t'))).ToString();
	}

	public string Part2() => throw new NotImplementedException();
}
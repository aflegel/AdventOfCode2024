using AdventOfCode.Map;

namespace AdventOfCode;

public class Day08(string input) : IAdventDay
{
	private Map2D<char> InputArray { get; } = Map2D<char>.FromString(input);
	private char[] InputAntennas { get; } = [.. input.Where(char.IsLetterOrDigit).Distinct()];

	public string Part1()
	{
		var antinodes = new HashSet<Position2D>();

		foreach (var frequency in InputAntennas)
		{
			var antennas = InputArray.SearchAll(frequency).ToList();

			var antennaPairs = antennas.SelectMany(s => antennas, (a, b) => (a, b))
				.Where(w => w.a != w.b).ToList();

			antinodes.UnionWith(antennaPairs.SelectMany(GetAnnodes));
		}

		return antinodes.Count.ToString();
	}

	private IEnumerable<Position2D> GetAnnodes((Position2D a, Position2D b) pair)
	{
		var dist = pair.a - pair.b;

		var pos = pair.a + dist;
		if (!InputArray.OutOfBounds(pos))
			yield return pos;

		pos = pair.b - dist;
		if (!InputArray.OutOfBounds(pos))
			yield return pos;
	}

	public string Part2()
	{
		var antinodes = new HashSet<Position2D>();

		foreach (var frequency in InputAntennas)
		{
			var antennas = InputArray.SearchAll(frequency).ToList();

			var antennaPairs = antennas.SelectMany(s => antennas, (a, b) => (a, b))
				.Where(w => w.a != w.b).ToList();

			antinodes.UnionWith(antennaPairs.SelectMany(GetAnnodesUnlimited));
			antinodes.UnionWith(antennas);
		}

		return antinodes.Count.ToString();
	}

	private IEnumerable<Position2D> GetAnnodesUnlimited((Position2D a, Position2D b) pair)
	{
		var dist = pair.a - pair.b;

		var pos = pair.a + dist;
		while (!InputArray.OutOfBounds(pos))
		{
			yield return pos;
			pos += dist;
		}

		pos = pair.b - dist;
		while (!InputArray.OutOfBounds(pos))
		{
			yield return pos;
			pos -= dist;
		}
	}
}

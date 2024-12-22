using Xunit;
using FluentAssertions;
using AdventOfCode.Map;

namespace AdventOfCodeTests;

public class MapTests
{
	private readonly string input = @"MMMSXXMASM
MSAMXMSMSA
AMXSXMAAMM
MSAMASMSMX
XMASAMXAMM
XXAMMXXAMA
SMSMSASXSS
SAXAMASAAA
MAMMMXMMMM
MXMXAXMASX".ReplaceLineEndings("\n");

	[Fact]
	public void Part1ShouldMatchExampleCount()
	{
		var map = Map2D<char>.FromString(input);

		var output = map.ToString();
		
		output.Should().Be(input);
	}
}

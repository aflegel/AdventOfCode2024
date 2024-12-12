using Xunit;
using AdventOfCode;
using FluentAssertions;

namespace AdventOfCodeTests;

public class Day04Tests
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
MXMXAXMASX";

	[Fact]
	public void Part1ShouldMatchExampleCount()
	{
		var day = new Day04(input);

		var answer = day.Part1();

		answer.Should().Be("18");
	}

	[Fact]
	public void Part2ShouldMatchExampleCount()
	{
		var day = new Day04(input);

		var answer = day.Part2();

		answer.Should().Be("9");
	}
}

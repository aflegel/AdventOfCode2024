using Xunit;
using AdventOfCode;
using FluentAssertions;

namespace AdventOfCodeTests;

public class Day12Tests
{
	private readonly string input = @"RRRRIICCFF
RRRRIICCCF
VVRRRCCFFF
VVRCCCJFFF
VVVVCJJCFE
VVIVCCJJEE
VVIIICJJEE
MIIIIIJJEE
MIIISIJEEE
MMMISSJEEE".ReplaceLineEndings("\n");

	[Fact]
	public void Part1ShouldMatchExampleCount()
	{
		var day = new Day12(input);

		var answer = day.Part1();

		answer.Should().Be("1930");
	}

	[Fact]
	public void Part2ShouldMatchExampleCount()
	{
		var day = new Day12(input);

		var answer = day.Part2();

		answer.Should().Be("1206");
	}
}

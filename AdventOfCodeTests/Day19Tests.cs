using Xunit;
using AdventOfCode;
using FluentAssertions;

namespace AdventOfCodeTests;

public class Day19Tests
{
	private readonly string input = @"r, wr, b, g, bwu, rb, gb, br

brwrr
bggr
gbbr
rrbgbr
ubwu
bwurrg
brgr
bbrgwb".ReplaceLineEndings("\n");

	[Fact]
	public void Part1ShouldMatchExampleCount()
	{
		var day = new Day19(input);

		var answer = day.Part1();

		answer.Should().Be("6");
	}

	[Fact]
	public void Part2ShouldMatchExampleCount()
	{
		var day = new Day19(input);

		var answer = day.Part2();

		answer.Should().Be("16");
	}
}

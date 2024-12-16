using Xunit;
using AdventOfCode;
using FluentAssertions;

namespace AdventOfCodeTests;

public class Day01Tests
{
	private readonly string input = @"3   4
4   3
2   5
1   3
3   9
3   3";

	[Fact]
	public void Part1ShouldMatchExampleCount()
	{
		var day = new Day01(input);

		var answer = day.Part1();

		answer.Should().Be("11");
	}

	[Fact]
	public void Part2ShouldMatchExampleCount()
	{
		var day = new Day01(input);

		var answer = day.Part2();

		answer.Should().Be("31");
	}
}

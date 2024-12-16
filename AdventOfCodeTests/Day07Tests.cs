using Xunit;
using AdventOfCode;
using FluentAssertions;

namespace AdventOfCodeTests;

public class Day07Tests
{
	private readonly string input = @"190: 10 19
3267: 81 40 27
83: 17 5
156: 15 6
7290: 6 8 6 15
161011: 16 10 13
192: 17 8 14
21037: 9 7 18 13
292: 11 6 16 20";

	[Fact]
	public void Part1ShouldMatchExampleCount()
	{
		var day = new Day07(input);

		var answer = day.Part1();

		answer.Should().Be("41");
	}

	[Fact]
	public void Part2ShouldMatchExampleCount()
	{
		var day = new Day07(input);

		var answer = day.Part2();

		answer.Should().Be("6");
	}
}

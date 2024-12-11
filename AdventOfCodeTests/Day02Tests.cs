using Xunit;
using AdventOfCode;
using FluentAssertions;

namespace AdventOfCodeTests;

public class Day02Tests
{
	private readonly string input = @"7 6 4 2 1
1 2 7 8 9
9 7 6 2 1
1 3 2 4 5
8 6 4 4 1
1 3 6 7 9";

	[Fact]
	public void Part1ShouldMatchExampleCount()
	{
		var day = new Day02(input);

		var answer = day.Part1();

		answer.Should().Be("2");
	}

	[Fact]
	public void Part2ShouldMatchExampleCount()
	{
		var day = new Day02(input);

		var answer = day.Part2();

		answer.Should().Be("4");
	}
}

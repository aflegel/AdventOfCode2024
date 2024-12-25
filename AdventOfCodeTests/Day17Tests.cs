using Xunit;
using AdventOfCode;
using FluentAssertions;

namespace AdventOfCodeTests;

public class Day17Tests
{
	private readonly string input = @"Register A: 729
Register B: 0
Register C: 0

Program: 0,1,5,4,3,0".ReplaceLineEndings("\n");

	[Fact]
	public void Part1ShouldMatchExampleCount()
	{
		var day = new Day17(input);

		var answer = day.Part1();

		answer.Should().Be("4,6,3,5,6,3,5,2,1,0");
	}

	[Fact]
	public void Part2ShouldMatchExampleCount()
	{
		var day = new Day17(input);

		var answer = day.Part2();

		answer.Should().Be("117440");
	}
}

using Xunit;
using AdventOfCode;
using FluentAssertions;

namespace AdventOfCodeTests;

public class Day10Tests
{
	private readonly string input = @"89010123
78121874
87430965
96549874
45678903
32019012
01329801
10456732";

	[Fact]
	public void Part1ShouldMatchExampleCount()
	{
		var day = new Day10(input);

		var answer = day.Part1();

		answer.Should().Be("36");
	}

	[Fact]
	public void Part2ShouldMatchExampleCount()
	{
		var day = new Day10(input);

		var answer = day.Part2();

		answer.Should().Be("81");
	}
}

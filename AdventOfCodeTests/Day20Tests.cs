using Xunit;
using AdventOfCode;
using FluentAssertions;

namespace AdventOfCodeTests;

public class Day20Tests
{
	private readonly string input = @"###############
#...#...#.....#
#.#.#.#.#.###.#
#S#...#.#.#...#
#######.#.#.###
#######.#.#...#
#######.#.###.#
###..E#...#...#
###.#######.###
#...###...#...#
#.#####.#.###.#
#.#...#.#.#...#
#.#.#.#.#.#.###
#...#...#...###
###############".ReplaceLineEndings("\n");

	[Fact]
	public void Part1ShouldMatchExampleCount()
	{
		var day = new Day20(input);

		var answer = day.Part1();

		answer.Should().Be("0");
	}

	[Fact]
	public void Part2ShouldMatchExampleCount()
	{
		var day = new Day20(input);

		var answer = day.Part2();

		answer.Should().Be("0");
	}
}

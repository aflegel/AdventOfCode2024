using Xunit;
using AdventOfCode;
using FluentAssertions;

namespace AdventOfCodeTests;

public class Day16Tests
{
	private readonly string input = @"###############
#.......#....E#
#.#.###.#.###.#
#.....#.#...#.#
#.###.#####.#.#
#.#.#.......#.#
#.#.#####.###.#
#...........#.#
###.#.#####.#.#
#...#.....#.#.#
#.#.#.###.#.#.#
#.....#...#.#.#
#.###.#.#.#.#.#
#S..#.....#...#
###############".ReplaceLineEndings("\n");

	[Fact]
	public void Part1ShouldMatchExampleCount()
	{
		var day = new Day16(input);

		var answer = day.Part1();

		answer.Should().Be("7036");
	}

	[Fact]
	public void Part2ShouldMatchExampleCount()
	{
		var day = new Day16(input);

		var answer = day.Part2();

		answer.Should().Be("45");
	}
}

using Xunit;
using AdventOfCode;
using FluentAssertions;

namespace AdventOfCodeTests;

public class Day08Tests
{
	private readonly string input = @"............
........0...
.....0......
.......0....
....0.......
......A.....
............
............
........A...
.........A..
............
............".ReplaceLineEndings("\n");

	private readonly string input2 = @"T....#....
...T......
.T....#...
.........#
..#.......
..........
...#......
..........
....#.....
..........".ReplaceLineEndings();

	[Fact]
	public void Part1ShouldMatchExampleCount()
	{
		var day = new Day08(input);

		var answer = day.Part1();

		answer.Should().Be("14");
	}

	[Fact]
	public void Part2ShouldMatchExampleCount()
	{
		var day = new Day08(input);

		var answer = day.Part2();

		answer.Should().Be("34");
	}

	[Fact]
	public void Part2ShouldMatchExampleCount2()
	{
		var day = new Day08(input2);

		var answer = day.Part2();

		answer.Should().Be("9");
	}
}

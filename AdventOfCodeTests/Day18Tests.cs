using Xunit;
using AdventOfCode;
using FluentAssertions;

namespace AdventOfCodeTests;

public class Day18Tests
{
	private readonly string input = @"5,4
4,2
4,5
3,0
2,1
6,3
2,4
1,5
0,6
3,3
2,6
5,1
1,2
5,5
2,5
6,5
1,4
0,4
6,4
1,1
6,1
1,0
0,5
1,6
2,0".ReplaceLineEndings("\n");

	[Fact]
	public void Part1ShouldMatchExampleCount()
	{
		var day = new Day18(input)
		{
			Height = 7,
			Width = 7,
			Time = 12
		};

		var answer = day.Part1();

		answer.Should().Be("22");
	}

	[Fact]
	public void Part2ShouldMatchExampleCount()
	{
		var day = new Day18(input)
		{
			Height = 7,
			Width = 7,
			Time = 12
		};

		var answer = day.Part2();

		answer.Should().Be("6,1");
	}
}

using Xunit;
using AdventOfCode;
using FluentAssertions;

namespace AdventOfCodeTests;

public class Day22Tests
{
	private readonly string input = @"1
10
100
2024".ReplaceLineEndings("\n");

	private readonly string input2 = @"1
2
3
2024".ReplaceLineEndings("\n");

	[Fact]
	public void Part1ShouldMatchExampleCount()
	{
		var day = new Day22(input);

		var answer = day.Part1();

		answer.Should().Be("37327623");
	}

	[Fact]
	public void Part2ShouldMatchExampleCount()
	{
		var day = new Day22(input2);

		var answer = day.Part2();

		answer.Should().Be("23");
	}
}

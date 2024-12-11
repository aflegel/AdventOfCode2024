using Xunit;
using AdventOfCode;
using FluentAssertions;

namespace AdventOfCodeTests;

public class Day03Tests
{
	private readonly string input = @"xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))";
	private readonly string input2 = @"xmul(2,4)&mul[3,7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(8,5))";

	[Fact]
	public void Part1ShouldMatchExampleCount()
	{
		var day = new Day03(input);

		var answer = day.Part1();

		answer.Should().Be("161");
	}

	[Fact]
	public void Part2ShouldMatchExampleCount()
	{
		var day = new Day03(input2);

		var answer = day.Part2();

		answer.Should().Be("48");
	}
}

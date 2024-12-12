using Xunit;
using AdventOfCode;
using FluentAssertions;

namespace AdventOfCodeTests;

public class Day05Tests
{
	private readonly string input = @"47|53
97|13
97|61
97|47
75|29
61|13
75|53
29|13
97|29
53|29
61|53
97|53
61|29
47|13
75|47
97|75
47|61
75|61
47|29
75|13
53|13

75,47,61,53,29
97,61,53,29,13
75,29,13
75,97,47,61,53
61,13,29
97,13,75,29,47".ReplaceLineEndings("\n");

	[Fact]
	public void Part1ShouldMatchExampleCount()
	{
		var day = new Day05(input);

		var answer = day.Part1();

		answer.Should().Be("143");
	}

	[Fact]
	public void Part2ShouldMatchExampleCount()
	{
		var day = new Day05(input);

		var answer = day.Part2();

		answer.Should().Be("123");
	}
}

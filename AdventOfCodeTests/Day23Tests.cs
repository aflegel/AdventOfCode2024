using Xunit;
using AdventOfCode;
using FluentAssertions;

namespace AdventOfCodeTests;

public class Day23Tests
{
	private readonly string input = @"kh-tc
qp-kh
de-cg
ka-co
yn-aq
qp-ub
cg-tb
vc-aq
tb-ka
wh-tc
yn-cg
kh-ub
ta-co
de-co
tc-td
tb-wq
wh-td
ta-ka
td-qp
aq-cg
wq-ub
ub-vc
de-ta
wq-aq
wq-vc
wh-yn
ka-de
kh-ta
co-tc
wh-qp
tb-vc
td-yn".ReplaceLineEndings("\n");

	[Fact]
	public void Part1ShouldMatchExampleCount()
	{
		var day = new Day23(input);

		var answer = day.Part1();

		answer.Should().Be("7");
	}

	[Fact]
	public void Part2ShouldMatchExampleCount()
	{
		var day = new Day23(input);

		var answer = day.Part2();

		answer.Should().Be("co,de,ka,ta");
	}
}

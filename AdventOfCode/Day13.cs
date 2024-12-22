using System.Text.RegularExpressions;

namespace AdventOfCode;

public partial class Day13(string input) : IAdventDay
{
	private record Position2(long X, long Y)
	{
		public static Position2 operator +(Position2 a, Position2 b) => new(a.X + b.X, a.Y + b.Y);
		public static Position2 operator *(Position2 a, long b) => new(a.X * b, a.Y * b);
	}
	[GeneratedRegex(@"X\+(\d+), Y\+(\d+)")] public static partial Regex Button();
	[GeneratedRegex(@"X=(\d+), Y=(\d+)")] public static partial Regex Position();

	private record Arcade(Position2 ButtonA, Position2 ButtonB, Position2 Prize);
	private Arcade[] InputArray { get; } = input.Split("\n\n").Select(s =>
	{
		var split = s.Split("\n");
		var buttonA = Button().Match(split[0]).Groups;
		var buttonB = Button().Match(split[1]).Groups;
		var prize = Position().Match(split[2]).Groups;

		return new Arcade(new(int.Parse(buttonA[1].Value), int.Parse(buttonA[2].Value)),
			new(int.Parse(buttonB[1].Value), int.Parse(buttonB[2].Value)),
			new(int.Parse(prize[1].Value), int.Parse(prize[2].Value)));
	}).ToArray();

	public string Part1() => InputArray.Select(s => Calculate(s, 0)).Sum().ToString();
	public string Part2() => InputArray.Select(s => Calculate(s, 10000000000000)).Sum().ToString();

	private static long Calculate(Arcade item, long increase = 0)
	{
		var prize = increase > 0 ? item.Prize + new Position2(increase, increase) : item.Prize;

		// this is linear algebra system of equations
		//aX * a + bX * b = pX
		//aY * a + bY * b = pY

		var b = (prize.Y * item.ButtonA.X - prize.X * item.ButtonA.Y)
			/ (item.ButtonB.Y * item.ButtonA.X - item.ButtonB.X * item.ButtonA.Y);
		var a = (prize.X - b * item.ButtonB.X) / item.ButtonA.X;

		return item.ButtonA * a + item.ButtonB * b == prize ? a * 3 + b : 0;
	}
}

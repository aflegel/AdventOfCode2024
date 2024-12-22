using System.Text.RegularExpressions;
using AdventOfCode.Map;

namespace AdventOfCode;

public partial class Day14(string input) : IAdventDay
{
	public int Width { get; init; } = 101;
	public int Height { get; init; } = 103;
	[GeneratedRegex(@"(\d+),(\d+)")] public static partial Regex Position();
	[GeneratedRegex(@"(-?\d+),(-?\d+)")] public static partial Regex Velocity();

	private record Robot(Position2D Position, Position2D Velocity);
	private Robot[] InputArray { get; } = [.. input.Split("\n").Select(s =>
	{
		var split = s.Split(" ");
		var position = Position().Match(split[0]).Groups;
		var velocity = Velocity().Match(split[1]).Groups;

		return new Robot(new(int.Parse(position[1].Value), int.Parse(position[2].Value)),
			new(int.Parse(velocity[1].Value), int.Parse(velocity[2].Value)));
	})];


	public string Part1()
	{
		var list = InputArray.Select(s => Move(s,100).Position).ToList();

		var q1 = list.Count(w => w.X < Width / 2 && w.Y < Height / 2);
		var q2 = list.Count(w => w.X > Width / 2 && w.Y < Height / 2);
		var q3 = list.Count(w => w.X < Width / 2 && w.Y > Height / 2);
		var q4 = list.Count(w => w.X > Width / 2 && w.Y > Height / 2);

		return (q1 * q2 * q3 * q4).ToString();
	}

	public string Part2()
	{
		//I found this by hand. A pattern appears every 101 iterations after 82 iterations
		var robots = InputArray.Select(s => Move(s, 82)).ToArray();
		var count = 82;

		while (true)
		{
			robots = [.. robots.Select(r => Move(r, 101))];
			count += 101;
			if (Print(robots).Contains("###############################"))
				break;

			if(count > 10000)
				return "0";
		}

		return count.ToString();
	}
	private string Print(Robot[] robots)
	{
		//I hate this but it works
		Map2D<char> set = new(width: Width, height: Height);

		set.Fill(' ');

		foreach (var robot in robots)
		{
			set[robot.Position] = '#';
		}

		return set.ToString();
	}

	private Robot Move(Robot robot, int moves)
	{
		var end = robot.Position + robot.Velocity * moves;

		end = new Position2D(end.X % Width, end.Y % Height);
		if (end.X < 0)
			end += new Position2D(Width, 0);

		if (end.Y < 0)
			end += new Position2D(0, Height);

		return new Robot(end, robot.Velocity);
	}
}


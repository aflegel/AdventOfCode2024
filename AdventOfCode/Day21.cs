using AdventOfCode.Map;

namespace AdventOfCode;

public class Day21(string input) : IAdventDay
{
	private string[] Codes { get; } = input.Split("\n");
	private Map2D<char> NumericPad { get; } = Map2D<char>.FromString("789\n456\n123\n 0A");
	private Map2D<char> DirectionPad { get; } = Map2D<char>.FromString(" ^A\n<v>");
	public string Part1()
	{
		var (numericBot, control1, control2) = Reset();

		foreach (var code in Codes)
		{
			//numeric pad
			foreach (var letter in code)
			{
				var target = NumericPad.SearchAll(letter).First();
				var diff = numericBot - target;

				//control1
				var vert1 = Enumerable.Repeat(diff.X > 0 ? 'v' : '^', Math.Abs(diff.X));
				var horz1 = Enumerable.Repeat(diff.Y > 0 ? '>' : '<', Math.Abs(diff.Y));
				char[] order1 = [];

				if (!vert1.Any() || !horz1.Any())
				{
					order1 = [.. vert1.Union(vert1)];
				}
				else
				{
					//determine which <^v> is closest
					var diffh = control1 - DirectionPad.SearchAll(vert1.First()).First();
					var diffv = control1 - DirectionPad.SearchAll(horz1.First()).First();
				}


			}

			(numericBot, control1, control2) = Reset();
		}


		return "";
	}

	private (Position2D, Position2D, Position2D) Reset() => (NumericPad.SearchAll('A').First(), DirectionPad.SearchAll('A').First(), DirectionPad.SearchAll('A').First());

	public string Part2() => throw new NotImplementedException();
}
using AdventOfCode.Map;

namespace AdventOfCode;

public class Day21(string input) : IAdventDay
{
	private string[] Codes { get; } = input.Split("\n");
	private Map2D<char> NumericPad { get; } = Map2D<char>.FromString("789\n456\n123\n 0A");
	private Map2D<char> DirectionPad { get; } = Map2D<char>.FromString(" ^A\n<v>");

	private Dictionary<(string code, int depth), long> Cache { get; } = [];
	public string Part1()
	{
		var sums = Codes.Select(s => (s, NumpadInput(s, 2))).Select(s => s.Item2 * int.Parse(s.s[..^1]));
		return sums.Sum().ToString();
	}

	private long NumpadInput(string code, int depth)
	{
		var numericBot = NumericPad.SearchAll('A').First();
		var output = 0L;

		foreach (var letter in code)
		{
			var target = NumericPad.SearchAll(letter).First();
			var newOrder = NumpadOrders(target, numericBot).Where(w => w.count > 0).ToList();

			for (var i = 0; i < newOrder.Count; i++)
				output += DpadInput(newOrder[i], i == 0 ? 'A' : newOrder[i - 1].target, depth);

			numericBot = target;
		}

		return output;
	}

	private long DpadInput((char target, int count) order, char previous, int depth)
	{
		if (depth == 0)
			return order.count;

		var output = 0L;

		var control = DirectionPad.SearchAll(previous).First();
		var target = DirectionPad.SearchAll(order.target).First();
		var newOrder = DpadOrders(target, control, order.count).Where(w => w.count > 0).ToList();

		var code = string.Join("", newOrder.Select(s => $"{s.target}{s.count}"));
		if (Cache.TryGetValue((code, depth), out var value))
			return value;

		for (var i = 0; i < newOrder.Count; i++)
			output += DpadInput(newOrder[i], i == 0 ? 'A' : newOrder[i - 1].target, depth - 1);

		Cache.TryAdd((code, depth), output);

		return output;
	}

	private List<(char target, int count)> NumpadOrders(Position2D target, Position2D control)
	{
		var diff = target - control;

		var horizontal = (diff.X > 0 ? '>' : '<', Math.Abs(diff.X));
		var vertical = (diff.Y > 0 ? 'v' : '^', Math.Abs(diff.Y));

		if (target.X == 0 && control.Y == NumericPad.Height - 1)
			return [vertical, horizontal, ('A', 1)];
		else if (control.X == 0 && target.Y == NumericPad.Height - 1)
			return [horizontal, vertical, ('A', 1)];

		return diff.X < 0 ? [horizontal, vertical, ('A', 1)] : [vertical, horizontal, ('A', 1)];
	}
	private static List<(char target, int count)> DpadOrders(Position2D target, Position2D control, int count)
	{
		var diff = target - control;

		var horizontal = (diff.X > 0 ? '>' : '<', Math.Abs(diff.X));
		var vertical = (diff.Y > 0 ? 'v' : '^', Math.Abs(diff.Y));

		if (target.X == 0 && control.Y == 0)
			return [vertical, horizontal, ('A', count)];
		else if (control.X == 0 && target.Y == 0)
			return [horizontal, vertical, ('A', count)];

		return diff.X < 0 ? [horizontal, vertical, ('A', count)] : [vertical, horizontal, ('A', count)];
	}

	public string Part2()
	{
		var sums = Codes.Select(s => (s, NumpadInput(s, 25))).Select(s => s.Item2 * int.Parse(s.s[..^1]));
		return sums.Sum().ToString();
	}
}

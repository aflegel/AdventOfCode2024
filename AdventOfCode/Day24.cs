using System.Text.RegularExpressions;

namespace AdventOfCode;

public partial class Day24(string input) : IAdventDay
{
	[GeneratedRegex(@"(\w{3})\s(AND|OR|XOR)\s(\w{3})\s->\s(\w{3})")]
	private static partial Regex GateRegex();

	private (string gate, bool value)[] Input { get; } = [.. input.Split("\n\n")[0].Split("\n").Select(s => {
		var split = s.Split(": ");
		return (split[0], split[1] == "1");
	})];

	private Gate[] Gates { get; } = [.. input.Split("\n\n")[1].Split("\n").Select(s => {
		var regex = GateRegex();
		var match = regex.Match(s);
		var order = new List<string>{match.Groups[1].Value, match.Groups[3].Value}.Order().ToList();
			return new Gate(
				order[0],
				order[1],
				Enum.Parse<Operation>(match.Groups[2].Value, true),
				match.Groups[4].Value
			);
	}).OrderBy(o => o.GateA).ThenBy(t => t.Operation)];

	private record Gate(string GateA, string GateB, Operation Operation, string Destination);

	private enum Operation { OR, XOR, AND }

	public string Part1() => Calculate().ToString();

	private long Calculate()
	{
		var wireValues = Input.ToDictionary(k => k.gate, v => v.value);
		var targetZ = Gates.Count(c => c.Destination.StartsWith('z'));

		var current = 0;
		while (current < targetZ)
		{
			foreach (var op in Gates)
			{
				if (wireValues.ContainsKey(op.Destination) || !wireValues.ContainsKey(op.GateA) || !wireValues.ContainsKey(op.GateB))
					continue;

				var newValue = Operate(wireValues[op.GateA], wireValues[op.GateB], op.Operation);

				wireValues.Add(op.Destination, newValue);
			}
			current = wireValues.Count(c => c.Key.StartsWith('z'));
		}

		var zGates = wireValues.Where(w => w.Key.StartsWith('z')).OrderBy(o => o.Key).Select(s => s.Value).ToArray();

		return ToLong(zGates);
	}

	private static bool Operate(bool a, bool b, Operation operation)
		=> operation switch
		{
			Operation.XOR => a ^ b,
			Operation.OR => a || b,
			Operation.AND => a && b,
			_ => throw new NotImplementedException(),
		};

	private static long ToLong(bool[] boolArray)
	{
		var result = 0L;
		for (var i = 0; i < boolArray.Length; i++)
		{
			if (boolArray[i])
				result |= 1L << i;
		}
		return result;
	}

	public string Part2() => string.Join(",", FindBrokenWires().Order());

	private HashSet<string> FindBrokenWires()
	{
		var set = new HashSet<string>();
		foreach (var gate in Gates)
		{
			var nexts = Gates.Where(w => w.GateA == gate.Destination || w.GateB == gate.Destination).ToList();
			if (nexts.Count == 0)
			{
				if (gate.Operation != Operation.XOR && (gate.Destination is not "z45"))
					set.Add(gate.Destination);
				continue; //zgate
			}

			foreach (var next in nexts)
			{
				switch (gate.Operation)
				{
					case Operation.AND:
						if (next.Operation != Operation.OR && gate.GateA != "x00" && gate.GateB != "x00")
							set.Add(gate.Destination);
						break;
					case Operation.OR:
						if (next.Operation == Operation.OR)
							set.Add(gate.Destination);
						break;
					case Operation.XOR:

						if (next.Operation == Operation.OR)
							set.Add(gate.Destination);

						if (!gate.Destination.StartsWith('z'))
						{
							var a = Gates.FirstOrDefault(w => w.Destination == gate.GateA);
							var b = Gates.FirstOrDefault(w => w.Destination == gate.GateB);

							if (a is not null && b is not null)
							{
								if (a.Operation == Operation.XOR && b.Operation == Operation.OR)
									set.Add(gate.Destination);
								else if (a.Operation == Operation.OR && b.Operation == Operation.XOR)
									set.Add(gate.Destination);
								else if (b.Operation == Operation.XOR && a.Operation == Operation.OR)
									set.Add(gate.Destination);
								else if (b.Operation == Operation.OR && a.Operation == Operation.XOR)
									set.Add(gate.Destination);
							}
						}
						break;
					default:
						break;
				}
			}
		}

		return set;
	}
}
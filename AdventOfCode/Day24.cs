using System.Text.RegularExpressions;

namespace AdventOfCode;

public partial class Day24(string input) : IAdventDay
{
	[GeneratedRegex(@"(\w{3})\s(AND|OR|XOR)\s(\w{3})\s->\s(\w{3})")]
	private static partial Regex GateRegex();

	private (string gate, bool value)[] Instructions { get; } = [.. input.Split("\n\n")[0].Split("\n").Select(s => {
		var split = s.Split(": ");
		return (split[0], split[1] == "1");
	})];

	private Gate[] Gates { get; } = [.. input.Split("\n\n")[1].Split("\n").Select(s => {
		var regex = GateRegex();
		var match = regex.Match(s);
			return new Gate(
				match.Groups[1].Value,
				match.Groups[3].Value,
				Enum.Parse<Operation>(match.Groups[2].Value, true),
				match.Groups[4].Value
			);
	})];

	private record Gate(string GateA, string GateB, Operation Operation, string Destination);

	private enum Operation { OR, XOR, AND }

	public string Part1()
	{
		var wireValues = Instructions.ToDictionary(k => k.gate, v => v.value);
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

		return ToLong(zGates).ToString();
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

	public string Part2() => throw new NotImplementedException();

}
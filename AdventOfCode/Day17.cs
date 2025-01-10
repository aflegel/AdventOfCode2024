namespace AdventOfCode;

public class Day17(string input) : IAdventDay
{
	private Dictionary<string, long> InputRegisters { get; } = input.Split("\n\n")[0].Split("\n")
		.ToDictionary(s => s[9].ToString(), s => long.Parse(s[11..]));

	private long[] InputInstructions { get; } = [.. input.Split("\n\n")[1][9..].Split(',').Select(long.Parse)];

	public string Part1() => string.Join(",", RunProgram(InputRegisters["A"], InputInstructions));

	private static IEnumerable<long> RunProgram(long a, long[] instructions)
	{
		var b = 0L;
		var c = 0L;
		var index = 0L;

		while (true)
		{
			if (index >= instructions.Length)
				break;

			var opCode = instructions[index];
			var operand = instructions[index + 1];

			switch (opCode)
			{
				case 0:
					a = (long)(a / Math.Pow(2, Combo(operand)));
					break;
				case 1:
					b ^= operand;
					break;
				case 2:
					b = Combo(operand) % 8;
					break;
				case 3:
					long? jump = a == 0 ? null : operand;
					if (jump is not null)
						index = jump.Value - 2;
					break;
				case 4:
					b ^= c;
					break;
				case 5:
					yield return Combo(operand) % 8;
					break;
				case 6:
					b = (long)(a / Math.Pow(2, Combo(operand)));
					break;
				case 7:
					c = (long)(a / Math.Pow(2, Combo(operand)));
					break;
				default:
					throw new InvalidOperationException($"Unknown instruction: {opCode}");
			}

			index += 2;
		}

		long Combo(long instruction) => instruction switch
		{
			<= 3 => instruction,
			4 => a,
			5 => b,
			6 => c,
			_ => throw new ArgumentOutOfRangeException(nameof(instruction))
		};
	}

	public string Part2() => ReverseProgram(0, 0).ToString();

	private long ReverseProgram(long register, int i)
	{
		for (var delta = 0; delta < 8; delta++)
		{
			var newRegister = register * 8L + delta;
			var output = RunProgram(newRegister, InputInstructions).ToArray();

			if (InputInstructions[^(i + 1)] != output[^(i + 1)])
				continue;

			if (InputInstructions.Length == output.Length)
				return newRegister;

			var result = ReverseProgram(newRegister, i + 1);

			if (result != 0)
				return result;
		}

		return 0;
	}
}

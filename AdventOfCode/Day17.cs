namespace AdventOfCode;

public class Day17(string input) : IAdventDay
{
	private Dictionary<char, int> InputRegisters { get; } = input.Split("\n\n")[0].Split("\n")
		.ToDictionary(s => s[9], s => Convert.ToInt32(s[11..]));

	private string[] InputInstructions { get; } = [.. input.Split("\n\n")[1][9..].Split(',')];

	public string Part1() => RunProgram(InputRegisters['A'], InputRegisters['B'], InputRegisters['C'], InputInstructions);

	private static string RunProgram(int a, int b, int c, string[] instructions)
	{
		List<string> output = [];
		var index = 0;

		while (true)
		{
			if (index >= instructions.Length)
				break;

			var opCode = instructions[index];
			var operand = instructions[index + 1];

			switch (opCode)
			{
				case "0":
					a = (int)(a / Math.Pow(2, Combo(operand)));
					break;
				case "1":
					b ^= int.Parse(operand);
					break;
				case "2":
					b = Combo(operand) % 8;
					break;
				case "3":
					int? jump = a == 0 ? null : int.Parse(operand);
					if (jump is not null)
						index = jump.Value - 2;
					break;
				case "4":
					b ^= c;
					break;
				case "5":
					output.Add((Combo(operand) % 8).ToString());
					break;
				case "6":
					b = (int)(a / Math.Pow(2, Combo(operand)));
					break;
				case "7":
					c = (int)(a / Math.Pow(2, Combo(operand)));
					break;
				default:
					throw new InvalidOperationException($"Unknown instruction: {opCode}");
			};

			index += 2;
		}

		int Combo(string instruction) => instruction switch
		{
			"0" => 0,
			"1" => 1,
			"2" => 2,
			"3" => 3,
			"4" => a,
			"5" => b,
			"6" => c,
			_ => throw new InvalidOperationException($"Unknown instruction: {instruction}")
		};

		return string.Join(",", output);
	}

	public string Part2()
	{
		var newA = InputRegisters['A'];
		var result = RunProgram(InputRegisters['A'], InputRegisters['B'], InputRegisters['C'], InputInstructions);
		var target = string.Join(",", InputInstructions);
		while (result != target)
		{
			//if (result.Length > target.Length)
			//	return "Oops";

			newA += 1;
			try
			{

				result = RunProgram(newA, 0, 0, InputInstructions);
			}
			catch { }
		}


		return newA.ToString();
	}
}
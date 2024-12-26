namespace AdventOfCode;

public class Day17(string input) : IAdventDay
{
	private Dictionary<string, int> InputRegisters { get; } = input.Split("\n\n")[0].Split("\n")
		.ToDictionary(s => s[9].ToString(), s => Convert.ToInt32(s[11..]));

	private string[] InputInstructions { get; } = [.. input.Split("\n\n")[1][9..].Split(',')];

	public string Part1()
	{
		List<string> output = [];
		var index = 0;

		while (true)
		{
			if (index >= InputInstructions.Length)
				break;

			var opCode = InputInstructions[index];
			var operand = InputInstructions[index + 1];

			switch (opCode)
			{
				case "0":
					InputRegisters["A"] = (int)(InputRegisters["A"] / Math.Pow(2, Combo(operand)));
					break;
				case "1":
					InputRegisters["B"] = InputRegisters["B"] ^ int.Parse(operand);
					break;
				case "2":
					InputRegisters["B"] = Combo(operand) % 8;
					break;
				case "3":
					int? jump = InputRegisters["A"] == 0 ? null : int.Parse(operand);
					if (jump is not null)
						index = jump.Value - 2;
					break;
				case "4":
					InputRegisters["B"] = InputRegisters["B"] ^ InputRegisters["C"];
					break;
				case "5":
					output.Add((Combo(operand) % 8).ToString());
					break;
				case "6":
					InputRegisters["B"] = (int)(InputRegisters["A"] / Math.Pow(2, Combo(operand)));
					break;
				case "7":
					InputRegisters["C"] = (int)(InputRegisters["A"] / Math.Pow(2, Combo(operand)));
					break;
				default:
					throw new InvalidOperationException($"Unknown instruction: {opCode}");
			};

			index += 2;
		}

		return string.Join(",", output);
	}

	private int Combo(string instruction) => instruction switch
	{
		"0" => 0,
		"1" => 1,
		"2" => 2,
		"3" => 3,
		"4" => InputRegisters["A"],
		"5" => InputRegisters["B"],
		"6" => InputRegisters["C"],
		_ => throw new InvalidOperationException($"Unknown instruction: {instruction}")
	};

	public string Part2() => throw new NotImplementedException();
}
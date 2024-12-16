namespace AdventOfCode;

public class Day07(string input) : IAdventDay
{
	private record Equation(long Total, long[] Values);
	private Equation[] InputArray { get; } = [.. input.Split("\n").Select(s => {
		var split = s.Split(":");
		var data = split[1].Trim().Split(" ").Select(s => Convert.ToInt64(s));
		return new Equation(Convert.ToInt64(split[0]), [.. data]);
	})];

	public string Part1() => InputArray.Select(equation => Validate(equation, ["*", "+"])).Sum().ToString();

	public string Part2() => InputArray.Select(equation => Validate(equation, ["*", "+", "|"])).Sum().ToString();

	private static long Validate(Equation equation, List<string> operations)
	{
		foreach (var operation in operations.GetPermutations(equation.Values.Length - 1).Select(s => string.Join("", s)))
		{
			var total = equation.Values[0];
			for (var i = 1; i < equation.Values.Length; i++)
			{
				var right = equation.Values[i];

				total = operation[i - 1] switch
				{
					'|' => Convert.ToInt64($"{total}{right}"),
					'*' => total * right,
					'+' => total + right,
					_ => throw new NotImplementedException(),
				};
			}

			if (total == equation.Total)
				return total;
		}

		return 0;
	}
}

public static class EquationExtensions
{
	//modified from https://stackoverflow.com/questions/756055/listing-all-permutations-of-a-string-integer
	public static IEnumerable<IEnumerable<T>> GetPermutations<T>(this IEnumerable<T> list, int length)
		=> length == 1
			? list.Select(t => new T[] { t })
			: GetPermutations(list, length - 1)
				.SelectMany(t => list, (t1, t2) => t1.Concat([t2]));
}
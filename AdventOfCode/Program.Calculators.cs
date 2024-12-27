namespace AdventOfCode;

public static class Calculators
{
    public static int GreatestCommonFactor(int[] numbers)
    {
        if (numbers.Length == 0)
            throw new ArgumentException("Array must contain at least one element");

        var result = numbers[0];
        for (var i = 1; i < numbers.Length; i++)
        {
            result = GreatestCommonFactor(result, numbers[i]);
            if (result == 1)
                break; // GCF of 1 means we can stop early
        }
        return result;
    }
    public static int GreatestCommonFactor(int a, int b)
    {
        while (b != 0)
        {
            var temp = b;
            b = a % b;
            a = temp;
        }
        return a;
    }

    public static List<int> GetPrimeFactors(int n)
    {
        var factors = new List<int>();

        // Divide by 2 until n is odd
        while (n % 2 == 0)
        {
            factors.Add(2);
            n /= 2;
        }

        // Check for odd factors from 3 onwards
        for (var i = 3; i <= Math.Sqrt(n); i += 2)
        {
            while (n % i == 0)
            {
                factors.Add(i);
                n /= i;
            }
        }

        // If n is still greater than 2, it is prime
        if (n > 2)
        {
            factors.Add(n);
        }

        return factors;
    }

    	//I googled the LCM function, original sauce is https://www.geeksforgeeks.org/lcm-of-given-array-elements/
	public static long FindLowestCommonMultiple(int[] elements)
	{
		long lcm = 1;
		var divisor = 2;

		while (true)
		{
			var counter = 0;
			var divisible = false;
			for (var i = 0; i < elements.Length; i++)
			{

				// elements (n1, n2, ... 0) = 0.
				// For negative number we convert into
				// positive and calculate elements.
				if (elements[i] == 0)
					return 0;
				else if (elements[i] < 0)
					elements[i] = elements[i] * -1;
				if (elements[i] == 1)
					counter++;

				// Divide element_array by devisor if complete
				// division i.e. without remainder then replace
				// number with quotient; used for find next factor
				if (elements[i] % divisor == 0)
				{
					divisible = true;
					elements[i] = elements[i] / divisor;
				}
			}

			// If divisor able to completely divide any number
			// from array multiply with elements
			// and store into elements and continue
			// to same divisor for next factor finding.
			// else increment divisor
			if (divisible)
			{
				lcm *= divisor;
			}
			else
			{
				divisor++;
			}

			// Check if all element_array is 1 indicate 
			// we found all factors and terminate while loop.
			if (counter == elements.Length)
			{
				return lcm;
			}
		}
	}
}
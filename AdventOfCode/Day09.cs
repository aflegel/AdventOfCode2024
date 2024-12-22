
namespace AdventOfCode;

public class Day09(string input) : IAdventDay
{
	private string InputArray { get; } = input;

	public string Part1()
	{
		var checksum = 0ul;
		var diskIndex = 0ul;
		var head = 0;
		var foot = InputArray.Length - 1;
		var footRemainder = 0;

		while (head < foot)
		{
			if (head % 2 == 0)
			{
				//non-empty block
				for (var i = 0; i < InputArray[head].ToInt(); i++)
				{
					checksum += diskIndex * (ulong)head / 2;
					diskIndex++;
				}
			}
			else
			{
				//empty block
				for (var i = 0; i < InputArray[head].ToInt(); i++)
				{
					//escape if the head and foot meet (head will be odd here)
					if (Math.Abs(head - foot) < 2)
						break;

					if (footRemainder == InputArray[foot].ToInt())
					{

						foot -= 2;
						footRemainder = 0;
					}

					footRemainder++;

					checksum += diskIndex * (ulong)foot / 2;
					diskIndex++;
				}

			}
			head++;
		}

		//check foot remainder
		for (var i = footRemainder; i < InputArray[foot].ToInt(); i++)
		{
			checksum += diskIndex * (ulong)foot / 2;
			diskIndex++;
		}

		return checksum.ToString();
	}

	public string Part2()
	{
		var disk = InputArray.ToCharArray();

		var checksum = 0ul;
		var diskIndex = 0ul;
		var moved = new HashSet<int>();

		for (var i = 0; i < disk.Length - 1; i++)
		{
			if (i % 2 == 0)
			{
				//non-empty block
				if (moved.Contains(i))
				{
					diskIndex += (ulong)disk[i].ToInt();
				}
				else
				{
					for (var j = 0; j < disk[i].ToInt(); j++)
					{
						checksum += diskIndex * (ulong)i / 2;
						diskIndex++;
					}
				}
			}
			else
			{
				//empty block
				var target = disk[i].ToInt();
				var seek = disk.Length - 1;
				while (target > 0 && seek > i)
				{
					if (disk[seek].ToInt() <= target && !moved.Contains(seek))
					{
						//skip any previously moved files

						target -= disk[seek].ToInt();
						for (var j = 0; j < disk[seek].ToInt(); j++)
						{
							checksum += diskIndex * (ulong)seek / 2;
							diskIndex++;
						}

						moved.Add(seek);
					}
					else
					{
						seek -= 2;
					}
				}

				diskIndex += (ulong)target;
			}
		}

		return checksum.ToString();
	}
}

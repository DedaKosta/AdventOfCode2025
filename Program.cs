using System.Linq;

namespace Main;

public static class Program
{
	//DAY3 Constants
	public static int NUMBER_OF_DIGITS = 12;

	public static void Main(string[] args)
	{
		//var testResult = Day4_Part1(false);
		//Console.WriteLine($"Test Result: {testResult}");

		var inputResult = Day4_Part2(false);
		Console.WriteLine($"InputResult: {inputResult}");

		Console.ReadKey();
	}

	internal static int Day4_Part1(bool isTest = false)
	{
		using StreamReader sr = isTest
			? new StreamReader("..\\..\\..\\Day4\\Test.txt")
			: new StreamReader("..\\..\\..\\Day4\\Input.txt");

		List<List<char>> greed = new List<List<char>>();
		int totalScore = 0;

		while(!sr.EndOfStream)
		{
			greed.Add(sr.ReadLine().ToList<char>());
		}

		var columnCount = greed.First().Count;

		for (int i = 0; i < greed.Count; i++)
		{
			for (int j = 0; j < columnCount; j++)
			{
				if (greed[i][j] == '.')
					continue;

				int cellValue = 0;

				// Corners
				if ((i == 0 && j == 0) || (i == greed.Count - 1 && j == columnCount - 1) || (i == greed.Count - 1 && j == 0) || (i == 0 && j == columnCount - 1))
				{
					totalScore++;
					continue;
				}

				// Top row
				if(i == 0)
				{
					cellValue = (
						Day4Helper_GetFieldValue(greed[i][j - 1]) + 
						Day4Helper_GetFieldValue(greed[i][j + 1]) + 
						Day4Helper_GetFieldValue(greed[i + 1][j - 1]) + 
						Day4Helper_GetFieldValue(greed[i + 1][j]) + 
						Day4Helper_GetFieldValue(greed[i + 1][j + 1])
					);

					if (cellValue < 4)
						totalScore++;

					continue;
				}

				// Left column
				if(j == 0)
				{
					cellValue = (
						Day4Helper_GetFieldValue(greed[i + 1][j]) +
						Day4Helper_GetFieldValue(greed[i - 1][j]) +
						Day4Helper_GetFieldValue(greed[i - 1][j + 1]) +
						Day4Helper_GetFieldValue(greed[i][j + 1]) +
						Day4Helper_GetFieldValue(greed[i + 1][j + 1])
					);

					if (cellValue < 4)
						totalScore++;

					continue;
				}

				// Bottom row
				if(i == greed.Count - 1)
				{
					cellValue = (
						Day4Helper_GetFieldValue(greed[i][j - 1]) +
						Day4Helper_GetFieldValue(greed[i][j + 1]) +
						Day4Helper_GetFieldValue(greed[i - 1][j - 1]) +
						Day4Helper_GetFieldValue(greed[i - 1][j]) +
						Day4Helper_GetFieldValue(greed[i - 1][j + 1])
					);

					if (cellValue < 4)
						totalScore++;

					continue;
				}

				// Right column
				if (j == columnCount - 1)
				{
					cellValue = (
						Day4Helper_GetFieldValue(greed[i - 1][j]) +
						Day4Helper_GetFieldValue(greed[i + 1][j]) +
						Day4Helper_GetFieldValue(greed[i + 1][j - 1]) +
						Day4Helper_GetFieldValue(greed[i][j - 1]) +
						Day4Helper_GetFieldValue(greed[i - 1][j - 1])
					);

					if (cellValue < 4)
						totalScore++;

					continue;
				}

				// Middle cells
				cellValue = (
					Day4Helper_GetFieldValue(greed[i - 1][j - 1]) +
					Day4Helper_GetFieldValue(greed[i - 1][j]) +
					Day4Helper_GetFieldValue(greed[i - 1][j + 1]) +
					Day4Helper_GetFieldValue(greed[i][j - 1]) +
					Day4Helper_GetFieldValue(greed[i][j + 1]) +
					Day4Helper_GetFieldValue(greed[i + 1][j - 1]) +
					Day4Helper_GetFieldValue(greed[i + 1][j]) +
					Day4Helper_GetFieldValue(greed[i + 1][j + 1])
				);

				if (cellValue < 4)
					totalScore++;
			}
		}

		return totalScore;
	}

	internal static int Day4_Part2(bool isTest = false)
	{
		using StreamReader sr = isTest
			? new StreamReader("..\\..\\..\\Day4\\Test.txt")
			: new StreamReader("..\\..\\..\\Day4\\Input.txt");

		List<List<char>> greed = new List<List<char>>();
		int totalScore = 0;

		while (!sr.EndOfStream)
		{
			greed.Add(sr.ReadLine().ToList<char>());
		}

		var columnCount = greed.First().Count;

		int cycleCount = 0;

		do
		{
			cycleCount = 0;

			List<Tuple<int, int>> removecCells = new List<Tuple<int, int>>();

			for (int i = 0; i < greed.Count; i++)
			{
				for (int j = 0; j < columnCount; j++)
				{
					if (greed[i][j] == '.')
						continue;

					int cellValue = 0;

					// Corners
					if ((i == 0 && j == 0) || (i == greed.Count - 1 && j == columnCount - 1) || (i == greed.Count - 1 && j == 0) || (i == 0 && j == columnCount - 1))
					{
						cycleCount++;
						removecCells.Add(new Tuple<int, int>(i, j));
						continue;
					}

					// Top row
					if (i == 0)
					{
						cellValue = (
							Day4Helper_GetFieldValue(greed[i][j - 1]) +
							Day4Helper_GetFieldValue(greed[i][j + 1]) +
							Day4Helper_GetFieldValue(greed[i + 1][j - 1]) +
							Day4Helper_GetFieldValue(greed[i + 1][j]) +
							Day4Helper_GetFieldValue(greed[i + 1][j + 1])
						);

						if (cellValue < 4)
						{
							cycleCount++;
							removecCells.Add(new Tuple<int, int>(i, j));
						}

						continue;
					}

					// Left column
					if (j == 0)
					{
						cellValue = (
							Day4Helper_GetFieldValue(greed[i + 1][j]) +
							Day4Helper_GetFieldValue(greed[i - 1][j]) +
							Day4Helper_GetFieldValue(greed[i - 1][j + 1]) +
							Day4Helper_GetFieldValue(greed[i][j + 1]) +
							Day4Helper_GetFieldValue(greed[i + 1][j + 1])
						);

						if (cellValue < 4)
						{
							cycleCount++;
							removecCells.Add(new Tuple<int, int>(i, j));
						}

						continue;
					}

					// Bottom row
					if (i == greed.Count - 1)
					{
						cellValue = (
							Day4Helper_GetFieldValue(greed[i][j - 1]) +
							Day4Helper_GetFieldValue(greed[i][j + 1]) +
							Day4Helper_GetFieldValue(greed[i - 1][j - 1]) +
							Day4Helper_GetFieldValue(greed[i - 1][j]) +
							Day4Helper_GetFieldValue(greed[i - 1][j + 1])
						);

						if (cellValue < 4)
						{
							cycleCount++;
							removecCells.Add(new Tuple<int, int>(i, j));
						}

						continue;
					}

					// Right column
					if (j == columnCount - 1)
					{
						cellValue = (
							Day4Helper_GetFieldValue(greed[i - 1][j]) +
							Day4Helper_GetFieldValue(greed[i + 1][j]) +
							Day4Helper_GetFieldValue(greed[i + 1][j - 1]) +
							Day4Helper_GetFieldValue(greed[i][j - 1]) +
							Day4Helper_GetFieldValue(greed[i - 1][j - 1])
						);

						if (cellValue < 4)
						{
							cycleCount++;
							removecCells.Add(new Tuple<int, int>(i, j));
						}

						continue;
					}

					// Middle cells
					cellValue = (
						Day4Helper_GetFieldValue(greed[i - 1][j - 1]) +
						Day4Helper_GetFieldValue(greed[i - 1][j]) +
						Day4Helper_GetFieldValue(greed[i - 1][j + 1]) +
						Day4Helper_GetFieldValue(greed[i][j - 1]) +
						Day4Helper_GetFieldValue(greed[i][j + 1]) +
						Day4Helper_GetFieldValue(greed[i + 1][j - 1]) +
						Day4Helper_GetFieldValue(greed[i + 1][j]) +
						Day4Helper_GetFieldValue(greed[i + 1][j + 1])
					);

					if (cellValue < 4)
					{
						cycleCount++;
						removecCells.Add(new Tuple<int, int>(i, j));
					}
				}
			}

			totalScore += cycleCount;

			foreach(var removedCell in removecCells)
			{
				greed[removedCell.Item1][removedCell.Item2] = '.';
			}
		}
		while (cycleCount > 0);

		return totalScore;
	}

	internal static int Day4Helper_GetFieldValue(char c)
	{
		return c == '@' ? 1 : 0;
	}

	internal static int Day3_Part1(bool isTest = false)
	{
		using StreamReader sr = isTest
			? new StreamReader("..\\..\\..\\Day3\\Test.txt")
			: new StreamReader("..\\..\\..\\Day3\\Input.txt");

		int totalScore = 0;

		while(!sr.EndOfStream)
		{
			int firstLargest = 0;
			int firstLargestIndex = 0;
			int secondLargest = 0;
			int secondLargestIndex = 0;

			var battery = sr.ReadLine()
				.Select(x => int.Parse(x.ToString()))
				.ToList();

			firstLargest = battery.Max();

			firstLargestIndex = battery.IndexOf(firstLargest);

			if (firstLargestIndex == battery.Count - 1)
			{
				secondLargest = battery
					.Where(x => x < firstLargest)
					.Max();
			}
			else
			{
				secondLargest = battery
					.Skip(firstLargestIndex + 1)
					.Max();
			}

			if (firstLargestIndex == battery.Count - 1)
			{
				secondLargestIndex = battery.IndexOf(secondLargest);
			}
			else
			{
				secondLargestIndex = battery
					.Skip(firstLargestIndex + 1)
					.ToList()
					.IndexOf(secondLargest) + firstLargestIndex + 1;
			}

			if (secondLargestIndex < firstLargestIndex)
			{
				totalScore += int.Parse(string.Format("{0}{1}", secondLargest, firstLargest));
			}
			else
			{
				totalScore += int.Parse(string.Format("{0}{1}", firstLargest, secondLargest));
			}
		}

		return totalScore;
	}

	internal static long Day3_Part2(bool isTest = false)
	{
		using StreamReader sr = isTest
			? new StreamReader("..\\..\\..\\Day3\\Test.txt")
			: new StreamReader("..\\..\\..\\Day3\\Input.txt");

		long totalScore = 0;

		while (!sr.EndOfStream)
		{
			var battery = sr.ReadLine()
				.Select(x => int.Parse(x.ToString()))
				.ToArray();

			Dictionary<int, int> largestDigits = new Dictionary<int, int>();
			int currentStartIndex = 0;

			for (int i = 0; i < NUMBER_OF_DIGITS; i++)
			{
				int currentMaxDigit = 0;
				int currentMaxDigitIndex = 0;

				for (int j = currentStartIndex; j <= battery.Length - NUMBER_OF_DIGITS + i; j++)
				{
					if (battery[j] > currentMaxDigit)
					{
						currentMaxDigit = battery[j];
						currentMaxDigitIndex = j;
					}
				}
				largestDigits.Add(currentMaxDigitIndex, currentMaxDigit);

				currentStartIndex = currentMaxDigitIndex + 1;
			}

			string batteryEnergyString = string.Join("", largestDigits.OrderBy(ld => ld.Key).Select(ld => ld.Value));


			totalScore += long.Parse(batteryEnergyString);
		}

		return totalScore;
	}

	internal static long Day2_Part1(bool isTest = false)
	{
		using StreamReader sr = isTest
			? new StreamReader("..\\..\\..\\Day2\\Test.txt")
			: new StreamReader("..\\..\\..\\Day2\\Input.txt");

		long totalIdSum = 0;

		var segments = sr.ReadToEnd();
		var separateSegments = segments.Split(',');

		foreach(var segment in separateSegments)
		{
			var currentIndex = long.Parse(segment.Split('-')[0]);
			var endIndex = long.Parse(segment.Split('-')[1]);

			while(currentIndex <= endIndex)
			{
				string currentIndexString = currentIndex.ToString();

				for (int j = 1; j <= currentIndexString.Length / 2; j++)
				{
					if (currentIndexString.Length % j != 0)
					{
						continue;
					}

					string sequence1 = currentIndexString.Substring(0, j);

					int multiplier = 1;
					int numberOfRepeats = 1;
					bool isOnlyRepeatedSequence = true;

					while (multiplier < currentIndexString.Length / j)
					{
						string sequence2 = currentIndexString.Substring(multiplier * j, j);

						if (string.Compare(sequence1, sequence2) != 0)
						{
							isOnlyRepeatedSequence = false;
						}
						else
						{
							numberOfRepeats++;
						}

						multiplier++;
					}

					if (isOnlyRepeatedSequence && numberOfRepeats == 2)
					{
						totalIdSum += currentIndex;
						break;
					}
				}

				currentIndex++;
			}
		}

		return totalIdSum;
	}

	internal static long Day2_Part2(bool isTest = false)
	{
		using StreamReader sr = isTest
			? new StreamReader("..\\..\\..\\Day2\\Test.txt")
			: new StreamReader("..\\..\\..\\Day2\\Input.txt");

		long totalIdSum = 0;

		var segments = sr.ReadToEnd();
		var separateSegments = segments.Split(',');

		foreach (var segment in separateSegments)
		{
			var currentIndex = long.Parse(segment.Split('-')[0]);
			var endIndex = long.Parse(segment.Split('-')[1]);

			while (currentIndex <= endIndex)
			{
				string currentIndexString = currentIndex.ToString();

				for (int j = 1; j <= currentIndexString.Length / 2; j++)
				{
					if (currentIndexString.Length % j != 0)
					{
						continue;
					}

					string sequence1 = currentIndexString.Substring(0, j);

					int multiplier = 1;
					bool isOnlyRepeatedSequence = true;

					while (multiplier < currentIndexString.Length / j)
					{
						string sequence2 = currentIndexString.Substring(multiplier * j, j);

						if (string.Compare(sequence1, sequence2) != 0)
						{
							isOnlyRepeatedSequence = false;
						}

						multiplier++;
					}

					if (isOnlyRepeatedSequence)
					{
						totalIdSum += currentIndex;
						break;
					}
				}

				currentIndex++;
			}
		}

		return totalIdSum;
	}

	internal static int Day1_Part1(bool isTest = false)
	{
		using StreamReader sr = isTest
			? new StreamReader("..\\..\\..\\Day1\\Test.txt")
			: new StreamReader("..\\..\\..\\Day1\\Input.txt");

		int currentPosition = 50;
		int totalZeroCount = 0;

		while (!sr.EndOfStream)
		{
			var instruction = sr.ReadLine();

			if (instruction is null)
			{
				continue;
			}

			var direction = instruction[0];
			var rotationCount = int.Parse(new string(instruction.ToArray().Skip(1).ToArray()) ?? "0");

			currentPosition = direction switch
			{
				'L' => currentPosition - rotationCount < 0
						? (100 + ((currentPosition - rotationCount) % 100)) % 100
						: currentPosition - rotationCount,
				'R' => (currentPosition + rotationCount) % 100,
				_ => currentPosition += 0
			};

			if (currentPosition is 0)
			{
				totalZeroCount++;
			}
		}

		return totalZeroCount;
	}

	internal static int Day1_Part2(bool isTest = false)
	{
		using StreamReader sr = isTest
			? new StreamReader("..\\..\\..\\Day1\\Test.txt")
			: new StreamReader("..\\..\\..\\Day1\\Input.txt");

		int currentPosition = 50;
		int totalZeroCount = 0;

		while (!sr.EndOfStream)
		{
			var instruction = sr.ReadLine();

			if (instruction is null)
			{
				continue;
			}

			var direction = instruction[0];
			var rotationCount = int.Parse(new string(instruction.ToArray().Skip(1).ToArray()) ?? "0");

			switch(direction)
			{
				case 'L':
					var cyclesL = rotationCount / 100;

					totalZeroCount += cyclesL;
					rotationCount %= 100;

					if (currentPosition - rotationCount < 0 && currentPosition != 0)
					{
						currentPosition = (100 + ((currentPosition - rotationCount) % 100)) % 100;
						totalZeroCount++;
					}
					else
					{
						currentPosition = (100 + ((currentPosition - rotationCount) % 100)) % 100;
					}

					if (currentPosition == 0)
					{
						totalZeroCount++;
					}

					break;
				case 'R':
					var cyclesR = rotationCount / 100;

					totalZeroCount += cyclesR;
					rotationCount %= 100;

					if (currentPosition + rotationCount > 100 && currentPosition != 0)
					{
						currentPosition = (currentPosition + rotationCount) % 100;
						totalZeroCount++;
					}
					else
					{
						currentPosition = (currentPosition + rotationCount) % 100;
					}

					if (currentPosition == 0)
					{
						totalZeroCount++;
					}

					break;
				default:
					break;
			}
		}

		return totalZeroCount;
	}
}
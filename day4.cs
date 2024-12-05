class Day4
{
  static void Main(string[] args)
  {
    if (args.Length < 1)
    {
      Console.WriteLine("ERROR: No file path");
      System.Environment.Exit(1);
    }

    var fileContent = File.ReadAllLines(args[0]);
    int count = CountWord(fileContent);
    int countX = CountXWord(fileContent);
    Console.WriteLine($"Result Part 1 = {count}");
    Console.WriteLine($"Result Part 2 = {countX}");
  }

  static int CountWord(String[] grid)
  {
    int count = 0;
    int[,] directions = { { -1, 0 }, { -1, 1 }, { 0, 1 }, { 1, 1 }, { 1, 0 }, { 1, -1 }, { 0, -1 }, { -1, -1 } };
    for (int y = 0; y < grid.Length; y++)
    {
      for (int x = 0; x < grid[0].Length; x++)
      {
        for (int d = 0; d < directions.GetLength(0); d++)
        {
          String word = "";
          int ny = y;
          int nx = x;
          for (int _a = 0; _a < 4; _a++)
          {
            if (ny < 0 || ny >= grid.Length || nx < 0 || nx >= grid[0].Length)
            {
              break;
            }
            word += grid[ny][nx];
            ny += directions[d, 0];
            nx += directions[d, 1];
          }
          if (word == "XMAS")
          {
            count++;
          }
        }
      }
    }
    return count;
  }

  static int CountXWord(String[] grid)
  {
    int count = 0;
    int[,] directions = { { -1, 1 }, { 1, 1 }, { 1, -1 }, { -1, -1 } };
    for (int y = 1; y < grid.Length - 1; y++)
    {
      for (int x = 1; x < grid[0].Length - 1; x++)
      {
        if (grid[y][x] == 'A')
        {
          String tr = grid[y + directions[0, 0]][x + directions[0, 1]].ToString();
          String br = grid[y + directions[1, 0]][x + directions[1, 1]].ToString();
          String bl = grid[y + directions[2, 0]][x + directions[2, 1]].ToString();
          String tl = grid[y + directions[3, 0]][x + directions[3, 1]].ToString();
          String line1 = tr + 'A' + bl;
          String line2 = tl + 'A' + br;
          if ((line1 == "MAS" || line1 == "SAM") && (line2 == "SAM" || line2 == "MAS"))
          {
            count++;
          }
        }
        // for (int d = 0; d < directions.GetLength(0); d++)
        // {
        //   String word = "";
        //   int ny = y;
        //   int nx = x;
        //   for (int _a = 0; _a < 2; _a++)
        //   {
        //     if (ny < 0 || ny >= grid.Length || nx < 0 || nx >= grid[0].Length)
        //     {
        //       break;
        //     }
        //     word += grid[ny][nx];
        //     ny += directions[d, 0];
        //     nx += directions[d, 1];
        //   }
        //   if ((word == "AS" || word == "SA" || word == "MA" || word == "AM") && !words.Contains(word))
        //   {
        //     words.Add(word);
        //     countMAS++;
        //   }
        // }
        // if (countMAS == 4)
        // {
        //   Console.WriteLine($"({y},{x})");
        //   Console.WriteLine(string.Join(" ", words));
        //   count++;
        // }
      }
    }
    return count;
  }
}
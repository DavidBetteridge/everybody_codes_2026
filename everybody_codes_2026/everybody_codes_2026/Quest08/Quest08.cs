namespace everybody_codes_2026.Quest08;

public class Quest08 : IQuest
{
    public async Task<double> Part1(string filename)
    {
        // Read file
        var rows = await File.ReadAllLinesAsync(filename);
        var width = rows[0].Length;
        var height = rows.Length;
        
        // Convert into a 2D array
        var grid = new char[width,height];
        foreach (var cell in grid.Iterate())
            grid[cell.column, cell.row] = rows[cell.row][cell.column];

        var total = 0;

        for (var columnNumber = 0; columnNumber < width; columnNumber++)
        {
            if (grid[columnNumber, 0] != '.')
                total += ScoreFrom(grid, 0, columnNumber, height, width, 1,1 );
        }

        for (var rowNumber = 1; rowNumber <= height / 2; rowNumber++)
        {
            // Find the first ant in the cell
            var columnNumber = 0;
            while (grid[columnNumber, rowNumber] == '.')
                columnNumber++;
            
            total += ScoreFrom(grid, rowNumber, columnNumber, height, width,1,1);
        }

        return total;
    }

    private static int ScoreFrom(char[,] grid, int rowNumber, int columnNumber, int height, int width, int xOff, int yOff)
    {
        // First ant gets some energy
        var total = 1;
        var shadowHeight = grid[columnNumber, rowNumber] - '0';
        var r = rowNumber+yOff;
        var c = columnNumber+xOff;

        // Console.Write(shadowHeight);
        
        while (r >= 0 && c >= 0 && r < height && c < width && grid[c, r] != '.')
        {
            shadowHeight--;
            var antHeight = grid[c, r] - '0';
            if (antHeight > shadowHeight)
            {
                total += 1;
                shadowHeight = antHeight;
      //          Console.Write($"-->{shadowHeight}");
            }
                
            // Next ant
            r += yOff;
            c += xOff;
        }
        // Console.WriteLine();
        return total;
    }

    public async Task<double> Part2(string filename)
    {
        // Read file
        var rows = await File.ReadAllLinesAsync(filename);
        var width = rows[0].Length;
        var height = rows.Length;
        
        // Convert into a 2D array
        var grid = new char[width,height];
        foreach (var cell in grid.Iterate())
            grid[cell.column, cell.row] = rows[cell.row][cell.column];

        var total = 0;
        foreach (var (xOff, yOff) in new List<(int,int)> { (1, 1), (-1, 1), (-2, 0), (-1, -1), (1, -1), (2, 0) })
        {
            var roundTotal = 0;
            for (var columnNumber = 0; columnNumber < width; columnNumber++)
            {
                for (var rowNumber = 0; rowNumber < height; rowNumber++)
                {
                    if (grid[columnNumber, rowNumber] != '.')
                    {
                        var x = columnNumber - xOff;
                        var y = rowNumber - yOff;
                        if (x < 0 || x >= width || y < 0 || y >= height || grid[x, y] == '.')
                            roundTotal += ScoreFrom(grid, rowNumber, columnNumber, height, width, xOff, yOff);
                    }
                }
            }

            total += roundTotal;
        }

        return total;
    }
    
    public async Task<double> Part3(string filename)
    {
        // Read file
        var rows = await File.ReadAllLinesAsync(filename);
        var width = rows[0].Length;
        var height = rows.Length;
        
        // Convert into a 2D array
        var grid = new char[width,height];
        foreach (var cell in grid.Iterate())
            grid[cell.column, cell.row] = rows[cell.row][cell.column];

        var total = 0;
        var totalWarriors = 0;

        while (totalWarriors < 100000)
        {
            foreach (var (xOff, yOff) in new List<(int, int)> { (1, 1), (-1, 1), (-2, 0), (-1, -1), (1, -1), (2, 0) })
            {
                var roundTotal = 0;
                for (var columnNumber = 0; columnNumber < width; columnNumber++)
                {
                    for (var rowNumber = 0; rowNumber < height; rowNumber++)
                    {
                        if (grid[columnNumber, rowNumber] != '.')
                        {
                            var x = columnNumber - xOff;
                            var y = rowNumber - yOff;
                            if (x < 0 || x >= width || y < 0 || y >= height || grid[x, y] == '.')
                            {
                                var result = ScoreAndGrow(grid, rowNumber, columnNumber, height, width, xOff, yOff);
                                roundTotal += result.energy;
                                totalWarriors += result.newWarriors;
                            }
                        }
                    }
                }

                total += roundTotal;
                if (totalWarriors >= 100000) break;
            }
        }
        return total;
    }
    
    private static (int energy, int newWarriors) ScoreAndGrow(char[,] grid, int rowNumber, int columnNumber, int height, int width, int xOff, int yOff)
    {
        var newWarriors = 0;
        var total = 0;
        var shadowHeight = 0;
        if (grid[columnNumber, rowNumber] == 'A')
        {
            grid[columnNumber, rowNumber] = 'B';
        }
        else if (grid[columnNumber, rowNumber] == 'B')
        {
            grid[columnNumber, rowNumber] = 'C';
        }
        else if (grid[columnNumber, rowNumber] == 'C')
        {
            grid[columnNumber, rowNumber] = '1';
        }
        else
        {
            // First ant gets some energy
            total++;
            
            shadowHeight = grid[columnNumber, rowNumber] - '0';
            if (shadowHeight == 9)
            {
                grid[columnNumber, rowNumber] = 'A';
                newWarriors++; 
            }
            else
            {
                grid[columnNumber, rowNumber] = (char)('0' + (shadowHeight+1));
            }
            
        }
        
        var r = rowNumber+yOff;
        var c = columnNumber+xOff;
        
        while (r >= 0 && c >= 0 && r < height && c < width && grid[c, r] != '.')
        {
            shadowHeight--;

            if (grid[c, r] == 'A')
            {
                grid[c, r] = 'B';
            }
            else if (grid[c, r] == 'B')
            {
                grid[c, r] = 'C';
            }
            else if (grid[c, r] == 'C')
            {
                grid[c, r] = '1';
            }
            else
            {
                var antHeight = grid[c, r] - '0';
                if (antHeight > shadowHeight)
                {
                    total += 1;

                    if (antHeight == 9)
                    {
                        // Produced a new warrior
                        grid[c, r] = 'A';
                        newWarriors++;
                    }
                    else
                    {
                        // Grows with the energy
                        grid[c, r] = (char)('0' + (antHeight + 1));
                    }

                    shadowHeight = antHeight;
                }
            }

            // Next ant
            r += yOff;
            c += xOff;
        }
        return (total, newWarriors);
    }
    
}

public static class ArrayExtensions
{
    extension(char[,] source)
    {
        public IEnumerable<(int column, int row, char value)> Iterate()
        {
            var width = source.GetUpperBound(0);
            var height = source.GetUpperBound(1);

            for (var rowNumber = 0; rowNumber <= height; rowNumber++)
            {
                for (var columnNumber = 0; columnNumber <= width; columnNumber++)
                {
                    yield return (columnNumber, rowNumber, source[columnNumber, rowNumber]);
                }
            }
        }
        
        public void Display()
        {
            var width = source.GetUpperBound(0);
            var height = source.GetUpperBound(1);
            
            for (var rowNumber = 0; rowNumber <= height; rowNumber++)
            {
                for (var columnNumber = 0; columnNumber <= width; columnNumber++)
                {
                    Console.Write(source[columnNumber, rowNumber]);
                }
                Console.WriteLine();
            } 
            Console.WriteLine();
        }
    }
}
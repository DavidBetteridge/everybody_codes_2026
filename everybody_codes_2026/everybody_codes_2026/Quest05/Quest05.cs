namespace everybody_codes_2026.Quest05;

public class Quest05 : IQuest
{
    public async Task<double> Part1(string filename)
    {
        // Read file
        var rows = await File.ReadAllLinesAsync(filename);
        var width = rows[0].Length;
        var height = rows.Length;
        
        // Convert into a 2D array
        var grid = new char[width,height];
        foreach (var cell in IterateOverGrid(grid))
            grid[cell.column, cell.row] = rows[cell.row][cell.column];

        // Mark the centre of the grid as land
        grid[width/2, height/2] = '#';
        
        var workDone = true;
        while (workDone)
        {
            workDone = false;
            var grid2 = (char[,])grid.Clone();
            foreach (var cell in IterateOverGrid(grid))
            {
                if (grid[cell.column, cell.row] == '#')
                {
                    foreach (var cell2 in NeighboursOf(grid, cell.column, cell.row))
                    {
                        var possibleLand = NeighboursOf(grid, cell2.column, cell2.row).Count(c => c.value is '#' or '1');
                        if (possibleLand >= 5)
                        {
                            if (grid2[cell2.column, cell2.row] != '#')
                            {
                                grid2[cell2.column, cell2.row] = '#';
                                workDone = true;
                            }
                        }
                    }
                } 
            }
            grid = grid2;
        }
        
        var total = IterateOverGrid(grid).Count(g => g.value == '#');

        return total;

        void DisplayGrid(char[,] g)
        {
            for (var rowNumber = 0; rowNumber < height; rowNumber++)
            {
                for (var columnNumber = 0; columnNumber < width; columnNumber++)
                {
                    Console.Write(g[columnNumber, rowNumber]);
                }
                Console.WriteLine();
            } 
            Console.WriteLine();
        }
        
        
        IEnumerable<(int column, int row, char value)> IterateOverGrid(char[,] g)
        {
            for (var rowNumber = 0; rowNumber < height; rowNumber++)
            {
                for (var columnNumber = 0; columnNumber < width; columnNumber++)
                {
                    yield return (columnNumber, rowNumber, g[columnNumber, rowNumber]);
                }
            }
        }

        IEnumerable<(int column, int row, char value)> NeighboursOf(char[,] g, int columnNumber, int rowNumber)
        {
            for (var x = columnNumber-1; x <= columnNumber+1; x++)
            {
                if (x >= 0 && x < width)
                {
                    for (var y = rowNumber - 1; y <= rowNumber + 1; y++)
                    {
                        if (y >= 0 && y < height && ( x != columnNumber || y != rowNumber))
                        {
                            yield return (x, y, g[x, y]);
                        }
                    }
                }
            }
        }
    }



    public async Task<double> Part2(string filename)
    {
        var contents = await File.ReadAllTextAsync(filename);

        var total = 0;
            
        return total;
    }
    
    public async Task<double> Part3(string filename)
    {
        var contents = await File.ReadAllTextAsync(filename);

        var total = 0;
            
        return total;
    }
}
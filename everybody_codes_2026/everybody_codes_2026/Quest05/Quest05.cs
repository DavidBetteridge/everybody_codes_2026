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
        foreach (var cell in grid.Iterate())
            grid[cell.column, cell.row] = rows[cell.row][cell.column];

        // Mark the centre of the grid as land
        grid[width/2, height/2] = '#';
        
        var workDone = true;
        while (workDone)
        {
            workDone = false;
            var grid2 = (char[,])grid.Clone();
            foreach (var cell in grid.Iterate().Where(c => c.value == '#'))
            {
                foreach (var cell2 in grid.NeighboursOf(cell.column, cell.row))
                {
                    var possibleLand = grid.NeighboursOf(cell2.column, cell2.row).Count(c => c.value is '#' or '1');
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
            grid = grid2;
        }
        return grid.Iterate().Count(g => g.value == '#');
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

        // Mark the centre of the grid as land
        grid[width/2, height/2] = '#';
        
        // Find land
        var workDone = true;
        while (workDone)
        {
            workDone = false;
            var grid2 = (char[,])grid.Clone();
            foreach (var cell in grid.Iterate().Where(c => c.value == '#'))
            {
                foreach (var cell2 in grid.NeighboursOf(cell.column, cell.row))
                {
                    var possibleLand = grid.NeighboursOf(cell2.column, cell2.row).Count(c => c.value is '#' or '1');
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
            grid = grid2;
        }
        
        // Find edge of sand
        var grid3 = (char[,])grid.Clone();
        foreach (var cell in grid.Iterate().Where(c => c.value == '#'))
        { 
            //Edges
            if (cell.row == 0 || cell.column == 0 || cell.row == height - 1 || cell.column == width - 1)
                grid3[cell.column, cell.row] = '=';
            else
            {
                if (grid.NeighboursOf(cell.column, cell.row).Any(c => c.value != '#'))
                    grid3[cell.column, cell.row] = '=';
            }
        }
        grid = grid3;

        // Grow sand
         workDone = true;
         while (workDone)
         {
             workDone = false;
             var grid2 = (char[,])grid.Clone();
             foreach (var cell in grid.Iterate().Where(c => c.value == '#'))
             {
                var possibleLand = grid.NeighboursOf(cell.column, cell.row).Count(c => c.value is '=');
                 if (possibleLand >= 5)
                 {
                     grid2[cell.column, cell.row] = '=';
                     workDone = true;
                 }
             }
             grid = grid2;
        }
        
        return grid.Iterate().Count(g => g.value == '=');
    }
    
    public async Task<double> Part3(string filename)
    {
        var contents = await File.ReadAllTextAsync(filename);

        var total = 0;
            
        return total;
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
        
        public IEnumerable<(int column, int row, char value)> NeighboursOf(int columnNumber, int rowNumber)
        {
            var width = source.GetUpperBound(0);
            var height = source.GetUpperBound(1);
            
            for (var x = columnNumber-1; x <= columnNumber+1; x++)
            {
                if (x >= 0 && x <= width)
                {
                    for (var y = rowNumber - 1; y <= rowNumber + 1; y++)
                    {
                        if (y >= 0 && y <= height && ( x != columnNumber || y != rowNumber))
                        {
                            yield return (x, y, source[x, y]);
                        }
                    }
                }
            }
        }
        
        public void DisplayGrid()
        {
            var width = source.GetUpperBound(0);
            var height = source.GetUpperBound(1);
            
            for (var rowNumber = 0; rowNumber <= height; rowNumber++)
            {
                for (var columnNumber = 0; columnNumber <= width; columnNumber++)
                {
                    if (source[columnNumber, rowNumber] == '0')
                        Console.Write('.');
                    else if (source[columnNumber, rowNumber] == '1')
                        Console.Write('.');                    
                    else
                        Console.Write(source[columnNumber, rowNumber]);
                }
                Console.WriteLine();
            } 
            Console.WriteLine();
        }
        
    }
}

namespace everybody_codes_2026.Quest10;

public class Quest10 : IQuest
{
    public async Task<double> Part1(string filename)
    {
        // Read file
        var rows = await File.ReadAllLinesAsync(filename);
        var width = rows[0].Length;
        var height = rows.Length;

        var walls = new HashSet<int>();
        var robots = new List<Robot>();

        for (var rowNumber = 0; rowNumber < height; rowNumber++)
        {
            for (var columnNumber = 0; columnNumber < width; columnNumber++)
            {
                var cellValue = rows[rowNumber][columnNumber];
                var cellIndex = (rowNumber * width) + columnNumber;

                if (cellValue == '#')
                    walls.Add(cellIndex);
                else if (cellValue == '>')
                    robots.Add(new Robot { Index = cellIndex, Direction = 0 });
                else if (cellValue == 'v')
                    robots.Add(new Robot { Index = cellIndex, Direction = 1 });
                else if (cellValue == '<')
                    robots.Add(new Robot { Index = cellIndex, Direction = 2 });
                else if (cellValue == '^')
                    robots.Add(new Robot { Index = cellIndex, Direction = 3 });
            }
        }

        Display();
        
        var collisions = 0;

        for (var i = 0; i < 10; i++)
        {
            // Move robots on one position 
            var nextRobots = new List<Robot>();
            foreach (var robot in robots)
            {
                var next = 0;
                if (robot.Direction == 0)
                    next = robot.Index + 1;

                if (robot.Direction == 1)
                    next = robot.Index + width;

                if (robot.Direction == 2)
                    next = robot.Index - 1;

                if (robot.Direction == 3)
                    next = robot.Index - width;

                if (walls.Contains(next))
                {
                    nextRobots.Add(new Robot { Index = robot.Index, Direction = (robot.Direction + 1) % 4 });
                    collisions++;
                }
                else
                    nextRobots.Add(new Robot { Index = next, Direction = robot.Direction });
            }

            robots = nextRobots;

            // Check for robots sharing cells
            var crashes = robots.GroupBy(r => r.Index).Where(g => g.Count() > 1);
            foreach (var crash in crashes)
            {
                foreach (var robot in crash)
                {
                    robot.Direction = (robot.Direction + 2) % 4;
                    collisions++;
                }
            }


     //       Display();

        }



        return collisions;

        void Display()
        {
            for (var rowNumber = 0; rowNumber < height; rowNumber++)
            {
                for (var columnNumber = 0; columnNumber < width; columnNumber++)
                {
                    var index = (rowNumber * width) + columnNumber;
                    if (walls.Contains(index))
                        Console.Write('#');
                    else
                    {
                        var inLocation = robots.Where(r => r.Index == index).ToList();
                        if (inLocation.Count == 0)
                            Console.Write('.');
                        else if (inLocation.Count == 1)
                        {
                            if (inLocation[0].Direction == 0)
                                Console.Write('>');
                            else if (inLocation[0].Direction == 1)
                                Console.Write('v');
                            else if (inLocation[0].Direction == 2)
                                Console.Write('<');
                            else if (inLocation[0].Direction == 4)
                                Console.Write('^');
                        }
                        else
                            Console.Write(inLocation.Count);
                    }
                }

                Console.WriteLine();
            }

            Console.WriteLine();
        }
    }

    private record Robot
    {
        public int Index { get; set; }
        public int Direction { get; set; }
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

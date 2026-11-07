namespace everybody_codes_2026.Quest09;

public class Quest09 : IQuest
{
    public async Task<double> Part1(string filename)
    {
        var plants = await File.ReadAllLinesAsync(filename);

        var total = 0;

        foreach (var plant in plants)
        {
            var parts = plant.Split(", ");
            var id = int.Parse(parts[0].Split('=')[1]);
            var growth = int.Parse(parts[1].Split('=')[1]);
            var limit = int.Parse(parts[2].Split('=')[1]);

            // [0,1,4] means 0 branches of length 0, 1 of length 1 and 4 of length 2
            var splits = 0;
            var branches = new int[growth];
            var time = 0;
            branches[0] = 1;

            while (branches.Select((i, j) =>i*j).Sum()+splits < limit)
            {
                // All the branches grow by one.
                var final = branches[^1];
                for (var i = growth - 1; i > 0; i--)
                    branches[i] = branches[i - 1];

                branches[0] = 0;

                splits += final;
                branches[1]+=final;
                branches[growth / 2] += final * 2;

                time++;
                
              //  Console.WriteLine(branches.Select((i, j) =>i*j).Sum()+splits);
                
            }
            
            //  Console.WriteLine(time);
            total += time;
        }

        return total;
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
namespace everybody_codes_2026.Quest02;

public class Quest02
{
    public async Task<double> Part1(string filename)
    {
        var lines = await File.ReadAllLinesAsync(filename);
        var total = 0D;
        foreach (var line in lines)
        {
            var comparisions = line.Split(" ");
            var largestCounts = new Dictionary<int, AlloyCount>
            {
                [1] = new AlloyCount { Alloy = 1, Count = 0 },
                [2] = new AlloyCount { Alloy = 2, Count = 0 },
                [3] = new AlloyCount { Alloy = 3, Count = 0 },
                [4] = new AlloyCount { Alloy = 4, Count = 0 },
                [5] = new AlloyCount { Alloy = 5, Count = 0 },
            };
            foreach (var comparision in comparisions)
            {
                if (comparision.Contains('<'))
                {
                    var alloy = int.Parse(comparision[^1..]);
                    largestCounts[alloy].Count++;
                }
                else
                {
                    var alloy = int.Parse(comparision[..1]);
                    largestCounts[alloy].Count++;
                }
            }

            var ordered = largestCounts.Values.OrderByDescending(c => c.Count).Select(c => c.Alloy.ToString());
            var checksum = double.Parse(string.Join("", ordered));
            total += checksum;
        }
        
        

        return total;
    }

    record AlloyCount
    {
        public int Alloy { get; set; }
        public int Count;
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
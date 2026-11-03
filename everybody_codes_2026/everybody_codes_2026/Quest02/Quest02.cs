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
        var lines = await File.ReadAllLinesAsync(filename);
        var total = 0D;
        foreach (var line in lines)
        {
            var equalities = line.Split(" ");
            var hardestFirst = new List<char>();
            foreach (var equality in equalities)
            {
                if (equality.Contains('<'))
                {
                    var smallest = equality[0];
                    var largest = equality[^1];

                    var s = hardestFirst.IndexOf(smallest);
                    var l = hardestFirst.IndexOf(largest);

                    if (s == -1 && l == -1)
                    {
                        hardestFirst.Add(largest);
                        hardestFirst.Add(smallest);
                    }
                    else if (s == -1)
                        hardestFirst.Insert(l+1,smallest);
                    else if (l == -1)
                        hardestFirst.Insert(s,largest);
                    else if (l > s)
                    {
                        hardestFirst.Remove(largest);
                        hardestFirst.Insert(s,largest);
                    }
                }
                else
                {
                    var smallest = equality[^1];
                    var largest = equality[0];
                    
                    var s = hardestFirst.IndexOf(smallest);
                    var l = hardestFirst.IndexOf(largest);

                    if (s == -1 && l == -1)
                    {
                        hardestFirst.Add(largest);
                        hardestFirst.Add(smallest);
                    }
                    else if (s == -1)
                        hardestFirst.Insert(l+1,smallest);
                    else if (l == -1)
                        hardestFirst.Insert(s,largest);
                    else if (l > s)
                    {
                        hardestFirst.Remove(largest);
                        hardestFirst.Insert(s,largest);
                    }
                }
            }
            var checksum = double.Parse(string.Join("", hardestFirst));
            total += checksum;
            
        }
        return total;
    }
    
    public async Task<double> Part3(string filename)
    {
        var lines = await File.ReadAllLinesAsync(filename);


        
        var total = 0D;

        foreach (var line in lines)
        {
            var perms = new List<string>();
            for (var i = 1; i <= 9; i++)
                Build(perms, i.ToString());
            
            var equalities = line.Split(" ");

            foreach (var equality in equalities)
            {
                if (equality.Contains('<'))
                {
                    var smallest = equality[0];
                    var largest = equality[^1];
                    perms = perms.Where(p => p.IndexOf(largest) < p.IndexOf(smallest)).ToList();
                }
                else
                {
                    var smallest = equality[^1];
                    var largest = equality[0];
                    perms = perms.Where(p => p.IndexOf(largest) < p.IndexOf(smallest)).ToList();
                }
            }

            total += double.Parse(perms[0]);
        }
            
        return total;
    }

    private void Build(List<string> perms, string soFar)
    {
        if (soFar.Length == 9)
            perms.Add(soFar);
        else
        {
            for (var i = 1; i <= 9; i++)
            {
                if (!soFar.Contains(i.ToString()))
                    Build(perms, soFar + i);
            }
        }
    }
}
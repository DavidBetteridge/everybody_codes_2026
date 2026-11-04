namespace everybody_codes_2026.Quest04;

public class Quest04 : IQuest
{
    private record Line
    {
        public int Id { get; set; }
        public int Quest { get; set; }
        public int Part { get; set; }
        public int Time { get; set; }
    }
    
    
    public async Task<double> Part1(string filename)
    {
        // id=1 quest=1 part=1 time=187
        var results = await File.ReadAllLinesAsync(filename);

        var lines = new List<Line>();
        foreach (var result in results)
        {
            var segments = result.Split(" ");
            var id = segments[0].Split('=')[1];
            var quest = segments[1].Split('=')[1];
            var part = segments[2].Split('=')[1];
            var time = segments[3].Split('=')[1];
            
            lines.Add(new Line
            {
                Id = int.Parse(id),
                Quest = int.Parse(quest),
                Part = int.Parse(part),
                Time = int.Parse(time),
            });
        }

        var total = lines
            .GroupBy(line => line.Id)
            .Select(grp => grp.Sum(l => l.Time))
            .Order()
            .Select((i, j) => i * (j + 1))
            .Sum();
        
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
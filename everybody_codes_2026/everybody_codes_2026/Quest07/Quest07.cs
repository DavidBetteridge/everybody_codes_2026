namespace everybody_codes_2026.Quest07;

public class Quest07 : IQuest
{
    private record Cloud
    {
        public int Id { get; set; }
        public int Energy { get; set; }
        public List<int> Connections { get; set; } = [];
    }
    
    public async Task<double> Part1(string filename)
    {
        //id=1, energy=5, connections=2,5,7
        var lines = await File.ReadAllLinesAsync(filename);

        var clouds = new Dictionary<int, Cloud>();
        foreach (var line in lines)
        {
            var parts = line.Split(", ");
            var id = int.Parse(parts[0].Split('=')[1]);
            var energy = int.Parse(parts[1].Split('=')[1]);
            var connections = parts[2].Split('=')[1].Split(',').Select(int.Parse).ToList();

            clouds[id] = new Cloud
            {
                Connections = connections,
                Energy = energy,
                Id = id
            };
        }

        // Find the cloud with the most connections
        var start = clouds.MaxBy(c => c.Value.Connections.Count);

        SendImpulse(clouds, start.Key, 5);
        
        
        var total = clouds.Sum(c => c.Value.Energy);

        return total;
    }

    private void SendImpulse(Dictionary<int, Cloud> clouds, int cloud, int impulse)
    {
        clouds[cloud].Energy += impulse;
        if (impulse > 1)
        {
            foreach (var connection in clouds[cloud].Connections)
                SendImpulse(clouds, connection, impulse-1);
        }
    }

    public async Task<double> Part2(string filename)
    {
        //id=1, energy=5, connections=2,5,7
        var lines = await File.ReadAllLinesAsync(filename);

        var clouds = new Dictionary<int, Cloud>();
        foreach (var line in lines)
        {
            var parts = line.Split(", ");
            var id = int.Parse(parts[0].Split('=')[1]);
            var energy = int.Parse(parts[1].Split('=')[1]);
            var connections = parts[2].Split('=')[1].Split(',').Select(int.Parse).ToList();

            clouds[id] = new Cloud
            {
                Connections = connections,
                Energy = energy,
                Id = id
            };
        }

        // Find the cloud with the most connections
        var start = clouds.MaxBy(c => c.Value.Connections.Count);

        SendImpulse(clouds, start.Key, 10);
        
        
        var total = clouds.Sum(c => c.Value.Energy);

        return total;
    }
    
    public async Task<double> Part3(string filename)
    {
        var contents = await File.ReadAllTextAsync(filename);

        var total = 0;
            
        return total;
    }
}
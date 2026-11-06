using System.Text;

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
        if (!clouds.TryGetValue(cloud, out var cloudDetails)) return;
        
        cloudDetails.Energy += impulse;
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
        var limit = filename.Contains("_Sample") ? 100 : 1000000;
        
        //id=1, energy=5, connections=2,5,7
        var lines = await File.ReadAllLinesAsync(filename);

        var cloudList = new List<int>();
        foreach (var line in lines)
        {
            var parts = line.Split(", ");
            var id = int.Parse(parts[0].Split('=')[1]);
            cloudList.Add(id);
        }

        var best = 0;
        foreach (var start in cloudList.Order())
        {
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
            
            var actions = new List<ImpulseAction>
            {
                new ImpulseAction { CloudId = start, Strength = 10, Quantity = 1}
            };

            while (actions.Count > 0)
            {
                var nextActions = new List<ImpulseAction>();
                var cloudsToUpdate = actions.GroupBy(c => c.CloudId);
                foreach (var cloudToUpdate in cloudsToUpdate)
                {
                    var details = clouds[cloudToUpdate.Key];
                    if (details.Energy < limit)
                    {
                        var impulse = cloudToUpdate.GroupBy(c => c.Strength).ToList();
                        var toApply = 0;
                        foreach (var update in impulse)
                        {
                            toApply += update.Sum(u => u.Quantity) * update.Key;
                            if (update.Key > 1)
                            {
                                foreach (var connection in details.Connections)
                                {
                                    if (clouds[connection].Energy < limit)
                                    {
                                        nextActions.Add(new ImpulseAction
                                        {
                                            CloudId = connection, Strength = update.Key - 1,
                                            Quantity = update.Sum(u => u.Quantity)
                                        });
                                    }
                                }
                            }
                        }
                        
                        details.Energy += toApply;
                        if (details.Energy >= limit)
                        {
                            // Just exploded!
                            foreach (var connection in details.Connections)
                            {
                                if (clouds[connection].Energy < limit)
                                {
                                    nextActions.Add(new ImpulseAction
                                        { CloudId = connection, Strength = 10, Quantity = 1});
                                }

                            }
                        }
                    }

                }

                actions = nextActions;
            }

            var total = clouds.Where(c => c.Value.Energy < limit).Sum(c => c.Value.Energy);
            best = Math.Max(total, best);
        }

        return best;
    }

    private sealed record ImpulseAction
    {
        public int CloudId { get; set; }
        public int Strength { get; set; }
        public required int Quantity { get; set; }
    }
    
}
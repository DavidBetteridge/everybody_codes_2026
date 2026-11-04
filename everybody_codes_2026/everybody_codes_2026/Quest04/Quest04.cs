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
            .Select(grp => new { total_time = grp.Sum(l => l.Time), parts = grp.Count() } )
            .OrderByDescending( a => a.parts).ThenByDescending(a => a.total_time)
            .Select((i, j) => i.total_time * (j + 1))
            .Sum();

        return total;
    }
    
    public async Task<double> Part3(string filename)
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

        var quests = lines.Select(l => l.Quest).Distinct().Order().ToList();
        var scores = new Dictionary<int, int>();  //id=>score
        foreach (var quest in quests)
        {
            Console.WriteLine($"Quest {quest}");
            
            // Part 1
            Console.WriteLine($"Part 1:");
            var part1Scores = lines
                .Where(l => l.Quest == quest && l.Part == 1)
                .Select(l => new { person = l.Id, time = l.Time })
                .OrderBy(l => l.time);

            var points = 50;
            var nextPoints = 50;
            var previousTime = -1;
            foreach (var score in part1Scores)
            {
                if (score.time != previousTime)
                    points = nextPoints;
                
                previousTime = score.time;
                
                if (points <= 0) break;
                
                scores[score.person] = scores.GetValueOrDefault(score.person) + points;
                Console.WriteLine($"id={score.person}, time {score.time} : {points} points");
                
                
                nextPoints--;                
            }
            
            
            // Part 2
            Console.WriteLine($"Part 2:");
            var peopleWhoHaveCompletedPart2 = lines.Where(l => l.Quest == quest && l.Part == 2).Select(l => l.Id).ToHashSet();
            var part2Scores = lines
                .Where(l => peopleWhoHaveCompletedPart2.Contains(l.Id))
                .Where(l => l.Quest == quest && l.Part != 3)
                .GroupBy(l => l.Id)
                .Select(l => new { person = l.Key, time = l.Sum(a=>a.Time) })
                .OrderBy(l => l.time);

            points = 100;
            nextPoints = 100;
            previousTime = -1;
            foreach (var score in part2Scores)
            {
                if (score.time != previousTime)
                    points = nextPoints;
                
                previousTime = score.time;
                
                if (points <= 0) break;
                
                scores[score.person] = scores.GetValueOrDefault(score.person) + points;
                Console.WriteLine($"id={score.person}, time {score.time} : {points} points");
                nextPoints--;                
            }
            
            // Part 3
            Console.WriteLine($"Part 3:");
            var peopleWhoHaveCompletedPart3 = lines.Where(l => l.Quest == quest && l.Part == 3).Select(l => l.Id).ToHashSet();
            var part3Scores = lines
                .Where(l => peopleWhoHaveCompletedPart3.Contains(l.Id))
                .Where(l => l.Quest == quest)
                .GroupBy(l => l.Id)
                .Select(l => new { person = l.Key, time = l.Sum(a=>a.Time) })
                .OrderBy(l => l.time);

            points = 150;
            nextPoints = 150;
            previousTime = -1;
            foreach (var score in part3Scores)
            {
                if (score.time != previousTime)
                    points = nextPoints;
                
                previousTime = score.time;
                
                if (points <= 0) break;
                
                scores[score.person] = scores.GetValueOrDefault(score.person) + points;
                Console.WriteLine($"id={score.person}, time {score.time} : {points} points");
                nextPoints--;                
            }
            
            Console.WriteLine($"");
        }
        
        var total = scores.Select(l => l.Value )
                          .OrderDescending()
                          .RankWithTies()
                          .Sum(t => t.score * t.rank);

        return total;
    }


}

public static class EnumerableExtensions
{
    extension<T>(IEnumerable<T> source) where T:notnull
    {
        /// <summary>
        /// Assumes source is already sorted
        /// </summary>
        public IEnumerable<(T score, int rank)> RankWithTies()
        {
            var rank = 1;
            var nextRank = 1;
            var previousScore = default(T);
            foreach (var t in source)
            {
                if (!t.Equals(previousScore))
                    rank = nextRank;

                yield return (t, rank);
            
                previousScore = t;
                nextRank++;
            }
        }
    }
}
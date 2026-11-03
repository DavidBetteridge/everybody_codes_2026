namespace everybody_codes_2026.Quest03;

public class Quest03
{
    public async Task<double> Part1(string filename)
    {
        var votes = await File.ReadAllLinesAsync(filename);

        var total = 0D;

        foreach (var vote in votes)
        {
            var standard = vote.Split(' ')[0][4..].ToCharArray();
            var value = vote.Split(' ')[1].ToCharArray();
            var index = Enumerable.Range(0, 4).Reverse().ToArray();

            foreach (var (s, multiplier, i) in standard.Zip(value, index))
                total += (multiplier - '0') * Math.Pow(s - '0', i);
        }
        
        return total;
    }
    
    public async Task<double> Part2(string filename)
    {
        var votes = await File.ReadAllLinesAsync(filename);

        var total = 0D;

        foreach (var vote in votes)
        {
            var standard = vote.Split(' ')[0][4..].Reverse().ToList();
            
            var value = vote.Split(' ')[1].Reverse().ToList();

            var voteValue = 0D;
            foreach (var (multiplier,s, i) in value.Zip(Repeater(standard), Enumerable.Range(0, 100)))
            {
                voteValue += (multiplier - '0') * Math.Pow(s - '0', i);
            }
            Console.WriteLine(voteValue);
            total += voteValue;
        }
        
        return total;
    }

    public static IEnumerable<char> Repeater(List<char> str)
    {
        while (true)
        {
            foreach(var c in str)
                yield return c;
        }
    }
    
    public async Task<double> Part3(string filename)
    {
        var contents = await File.ReadAllTextAsync(filename);

        var total = 0;
            
        return total;
    }
}
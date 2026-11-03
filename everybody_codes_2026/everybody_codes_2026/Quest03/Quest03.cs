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
        var votes = await File.ReadAllLinesAsync(filename);

        var total = 0D;

        foreach (var vote in votes)
        {
            var standard = vote.Split(' ')[0][5..].Reverse().Select(c => c - '0').ToList();
            var value = vote.Split(' ')[1].Reverse().Select(c => c - '0').ToList();
            var voteValue = CalculateValue(standard, value);
            
            // Now we need to check that there isn't a smaller value which gives the same vote value
            var ok = true;
            var valueAsNumber = int.Parse(vote.Split(' ')[1]);
            for (var valueAsNumberToTry = 0; valueAsNumberToTry < valueAsNumber; valueAsNumberToTry++)
            {
                var valueToTry = valueAsNumberToTry.ToString().Reverse().Select(c => c - '0').ToList();
                var voteValue2 = CalculateValue(standard, valueToTry);
                if (voteValue2 == voteValue)
                {
                    ok = false;
                    break;
                }
            }

            if (ok)
            {
                total += voteValue;
            }
        }
        
        return total;
    }

    private static double CalculateValue(List<int> standard, List<int> value)
    {
        var voteValue = 0D;
        foreach (var (multiplier,s, i) in value.Zip(Repeater(standard), Enumerable.Range(0, 100)))
        {
            voteValue += multiplier * Math.Pow(s, i);
        }

        return voteValue;
    }
    
    public static IEnumerable<int> Repeater(List<int> str)
    {
        while (true)
        {
            foreach(var c in str)
                yield return c;
        }
    }
}
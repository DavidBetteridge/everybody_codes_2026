namespace everybody_codes_2026.Quest03;

public class Quest03 : IQuest
{
    public async Task<double> Part1(string filename)
    {
        var votes = await File.ReadAllLinesAsync(filename);

        var total = 0D;
        foreach (var vote in votes)
        {
            var standard = vote.Split(' ')[0][4..].Select(c => c - '0');
            var value = vote.Split(' ')[1].Select(c => c - '0');

            foreach (var (s, multiplier, power) in standard.Zip(value, CountDownFrom(3)))
                total += multiplier * Math.Pow(s, power);
        }
        
        return total;
    }

    private static IEnumerable<int> CountDownFrom(int startValue)
    {
        while (true)
        {
            yield return startValue--;
        }
    }
    
    private static IEnumerable<int> CountUpFrom(int startValue)
    {
        while (true)
        {
            yield return startValue++;
        }
    }
    
    public async Task<double> Part2(string filename)
    {
        var votes = await File.ReadAllLinesAsync(filename);
        var total = 0D;

        foreach (var vote in votes)
        {
            var standard = vote.Split(' ')[0][4..].Select(c => c - '0').Reverse().ToList();
            var value = vote.Split(' ')[1].Select(c => c - '0').Reverse().ToList();
            total += CalculateValue(standard, value);
        }
        
        return total;
    }

    public static IEnumerable<int> Repeat(List<int> str)
    {
        while (true)
        {
            foreach(var c in str)
                yield return c;
        }
    }
    
    private static double CalculateValue(List<int> standard, List<int> value)
    {
        var voteValue = 0D;
        foreach (var (multiplier,s, i) in value.Zip(Repeat(standard), CountUpFrom(0)))
            voteValue += multiplier * Math.Pow(s, i);

        return voteValue;
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



}
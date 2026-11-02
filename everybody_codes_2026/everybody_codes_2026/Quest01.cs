namespace everybody_codes_2026;

public class Quest01
{
    public async Task<double> Part1(string filename)
    {
        var contents = await File.ReadAllTextAsync(filename);

        var total = contents
            .Split(",")
            .Select(int.Parse)
            .Select(c => new
            {
                currentCones = c,
                currentSize = Math.Floor(Math.Sqrt(c)),
                offset = Math.Sqrt(c) == Math.Floor(Math.Sqrt(c)) ? 0 : 1
            })
            .Sum(c => Math.Pow(c.currentSize + c.offset, 2) - c.currentCones);

        return total;
    }
    
    public async Task<double> Part2(string filename)
    {
        var contents = await File.ReadAllTextAsync(filename);

        // Precompute frame sizes.
        var frameSizes = Enumerable
            .Range(3, 10000)
            .Select(w => Math.Pow(w, 2) - Math.Pow(w - 2, 2))
            .ToArray();

        var total = contents
            .Split(",")
            .Select(int.Parse)
            .Sum(c => frameSizes.First(fs => fs >= c) - c);
            
        return total;
    }
}
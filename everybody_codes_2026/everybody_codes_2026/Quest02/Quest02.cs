namespace everybody_codes_2026.Quest02;

public class Quest02
{
    public async Task<double> Part3(string filename)
    {
        var size = 9;  //1..size
        var lines = await File.ReadAllLinesAsync(filename);
        var total = 0D;

        foreach (var line in lines)
        {
            //if notLargerThan[1,2] is True, then it means that 1 < 2
            var notLargerThan = new bool[size, size];

            var equalities = line.Split(" ");
            foreach (var equality in equalities)
            {
                if (equality.Contains('<'))
                {
                    var smallest = equality[0] - '1';
                    var largest = equality[^1] - '1';
                    notLargerThan[smallest,largest] = true;
                }
                else
                {
                    var smallest = equality[^1] - '1';
                    var largest = equality[0] - '1';
                    notLargerThan[smallest,largest] = true;
                }
            }

            var solution = Find("", notLargerThan);
            var checksum = double.Parse(string.Join("", solution));
            total += checksum;
        }
            
        return total;

        string Find(string soFar, bool[,] notLargerThan)
        {
            if (soFar.Length == size) return soFar;  // This is the solution
            
            for (var nextToTry = 1; nextToTry <= size; nextToTry++)
            {
                // Is this value possible?
                var nextToTryIndex = nextToTry - 1;
                var ok = true;
                foreach (var existing in soFar)
                {
                    // Already used
                    if (existing - '1' == (nextToTryIndex))
                    {
                        ok = false;
                        break;
                    }
                    
                    // Does it break an equality?
                    if (notLargerThan[existing - '1', nextToTryIndex])
                    {
                        ok = false;
                        break;
                    }
                }
                
                if (ok)
                {
                    var result = Find(soFar + nextToTry, notLargerThan);
                    if (result != "") return result;
                }
            }

            return "";
        }
    }
}
namespace everybody_codes_2026.Quest06;

public class Quest06 : IQuest
{
    public async Task<double> Part1(string filename)
    {
        var chains = await File.ReadAllLinesAsync(filename);

        var rules = new Dictionary<string, List<string>>();
        foreach (var chain in chains)
        {
            var parts = chain.Split(':');
            rules[parts[0]] = parts[1].Split(',').ToList();
        }

        var ingredients = new HashSet<string>() { "NULL" };
        AddIngredientsFor("NULL", ingredients, rules);

        return ingredients.Count - 1;
    }

    private void AddIngredientsFor(string identifier, 
        HashSet<string> ingredients,
        Dictionary<string, List<string>> rules)
    {
        if (rules.TryGetValue(identifier, out var list))
        {
            foreach (var item in list)
            {
                if (ingredients.Add(item))
                    AddIngredientsFor(item, ingredients, rules);
            }
        }
    }

    public async Task<double> Part2(string filename)
    {
        var chains = await File.ReadAllLinesAsync(filename);

        var rules = new Dictionary<string, List<string>>();
        foreach (var chain in chains)
        {
            var parts = chain.Split(':');
            rules[parts[0]] = parts[1].Split(',').ToList();
        }

        var usesNull = new HashSet<string>() { "NULL" };
        var examining = new Stack<string>();
        foreach (var root in rules.Keys)
        {
            CheckForNull(root, rules, usesNull, examining);
        }

        return usesNull.Count -1;
    }

    private bool CheckForNull(string identifier, Dictionary<string, List<string>> rules,
        HashSet<string> usesNull, Stack<string> examining)
    {
        examining.Push(identifier);
        
        if (rules.TryGetValue(identifier, out var list))
        {
            if (list.Contains("NULL"))
            {
                // identifier directly uses null
                usesNull.Add(identifier);
                examining.Pop();
                return true;
            }

            foreach (var item in list)
            {
                if ( !examining.Contains(item) && CheckForNull(item, rules, usesNull, examining))
                {
                    usesNull.Add(identifier);
                    examining.Pop();
                    return true;
                }
            }
        }

        examining.Pop();
        return false;
    }

    public async Task<double> Part3(string filename)
    {
        var contents = await File.ReadAllTextAsync(filename);

        var total = 0;
            
        return total;
    }
}
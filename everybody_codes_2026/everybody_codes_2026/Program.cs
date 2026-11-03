using everybody_codes_2026;
using everybody_codes_2026.Quest03;

var runner = new Quest03();
await RunPart1(3, runner, 1478, 15828);
await RunPart2(3, runner, 1161463, 29669548980);
await RunPart3(3, runner, 50987, 455897);


return;
const string rootFolder = "/Users/davidbetteridge/everybody_codes_2026/everybody_codes_2026/everybody_codes_2026";
async Task RunPart1(int questNumber, IQuest quests, double expectedSample, double expected)
{
    var actualSample = await quests.Part1($"{rootFolder}/Quest{questNumber:00}/Part1_Sample.csv");
    if (Math.Abs(actualSample - expectedSample) > double.Epsilon)
        Console.WriteLine($"Quest {questNumber:00} - Part1 - Sample - Got {actualSample} but expected {expectedSample}");
    
    var actual = await quests.Part1($"{rootFolder}/Quest{questNumber:00}/Part1.csv");
    if (Math.Abs(actual - expected) > double.Epsilon)
        Console.WriteLine($"Quest {questNumber:00} - Part1 - Got {actual} but expected {expected}");
    
    Console.WriteLine($"Quest {questNumber:00} - Part1 - Passed");
}

async Task RunPart2(int questNumber, IQuest quests, double expectedSample, double expected)
{
    var actualSample = await quests.Part2($"{rootFolder}/Quest{questNumber:00}/Part2_Sample.csv");
    if (Math.Abs(actualSample - expectedSample) > double.Epsilon)
        Console.WriteLine($"Quest {questNumber:00} - Part2 - Sample - Got {actualSample} but expected {expectedSample}");
    
    var actual = await quests.Part2($"{rootFolder}/Quest{questNumber:00}/Part2.csv");
    if (Math.Abs(actual - expected) > double.Epsilon)
        Console.WriteLine($"Quest {questNumber:00} - Part2 - Got {actual} but expected {expected}");
    
    Console.WriteLine($"Quest {questNumber:00} - Part2 - Passed");
}

async Task RunPart3(int questNumber, IQuest quests, double expectedSample, double expected)
{
    var actualSample = await quests.Part3($"{rootFolder}/Quest{questNumber:00}/Part3_Sample.csv");
    if (Math.Abs(actualSample - expectedSample) > double.Epsilon)
        Console.WriteLine($"Quest {questNumber:00} - Part3 - Sample - Got {actualSample} but expected {expectedSample}");
    
    var actual = await quests.Part3($"{rootFolder}/Quest{questNumber:00}/Part3.csv");
    if (Math.Abs(actual - expected) > double.Epsilon)
        Console.WriteLine($"Quest {questNumber:00} - Part3 - Got {actual} but expected {expected}");
    
    Console.WriteLine($"Quest {questNumber:00} - Part3 - Passed");
}
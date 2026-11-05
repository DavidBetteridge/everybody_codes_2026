using everybody_codes_2026;
using everybody_codes_2026.Quest03;
using everybody_codes_2026.Quest04;
using everybody_codes_2026.Quest05;

// var runner = new Quest03();
// await RunPart1(3, runner, 1478, 15828);
// await RunPart2(3, runner, 1161463, 29669548980);
// await RunPart3(3, runner, 50987, 455897);

// var runner = new Quest04();
// await RunPart1(4, runner, 27598, 194126);
// await RunPart2(4, runner, 6470, 575669565);
// await RunPart3(4, runner, 2998, 6644780);

var runner = new Quest05();
// await RunPart1(5, runner, 137, 1133);
await RunPart2(5, runner, 69, 1433);
// await RunPart3(5, runner, 2998, 6644780);

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
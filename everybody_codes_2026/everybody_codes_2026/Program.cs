using everybody_codes_2026.Quest02;

// var runner = new Quest01();
// var actual = await runner.Part1("/Users/davidbetteridge/everybody_codes_2026/everybody_codes_2026/everybody_codes_2026/Quest01/Part1.csv");
// var actual = await runner.Part3("/Users/davidbetteridge/everybody_codes_2026/everybody_codes_2026/everybody_codes_2026/Quest01/Part3.csv");

var runner = new Quest02();
var actual = await runner.Part1("/Users/davidbetteridge/everybody_codes_2026/everybody_codes_2026/everybody_codes_2026/Quest01/Part1_Sample.csv");
Console.WriteLine(actual);

actual = await runner.Part1("/Users/davidbetteridge/everybody_codes_2026/everybody_codes_2026/everybody_codes_2026/Quest01/Part1.csv");
Console.WriteLine(actual);
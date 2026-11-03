using everybody_codes_2026.Quest02;

// var runner = new Quest01();
// var actual = await runner.Part1("/Users/davidbetteridge/everybody_codes_2026/everybody_codes_2026/everybody_codes_2026/Quest01/Part1.csv");
// var actual = await runner.Part3("/Users/davidbetteridge/everybody_codes_2026/everybody_codes_2026/everybody_codes_2026/Quest01/Part3.csv");

var runner = new Quest02();
// var actual = await runner.Part3("/Users/davidbetteridge/everybody_codes_2026/everybody_codes_2026/everybody_codes_2026/Quest02/Part1_Sample.csv");
// Console.WriteLine(actual);

var actual = await runner.Part3("/Users/davidbetteridge/everybody_codes_2026/everybody_codes_2026/everybody_codes_2026/Quest02/Part3.csv");
var expected = 587204596017;
if (actual == expected)
    Console.WriteLine("OK" + actual);
else
    Console.WriteLine("Wrong " + actual);
namespace everybody_codes_2026;

public interface IQuest
{
    public Task<double> Part1(string filename);
    public Task<double> Part2(string filename);
    public Task<double> Part3(string filename);
}
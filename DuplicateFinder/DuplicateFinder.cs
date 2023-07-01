
using FuzzySharp;

public class DuplicateFinder
{
    private readonly IFileReader _fileReader;
    private static Mutex mut = new Mutex();

    public DuplicateFinder(IFileReader fileReader)
    {
        _fileReader = fileReader;
    }

    public List<ComparisonScore> Execute(string path)
    {
        var words = _fileReader.ReadAllLines(path);

        if(words.Length  == 0)
        {
            throw new ArgumentException("File not found");
        }

        var results = new List<ComparisonScore>();

        var task = Parallel.For(0, words.Length, (index) => { FindAndAddDuplicates(words[index], words, index, results); });
        

        return results.OrderByDescending(x => x.Score).ToList();
    }

    public void FindAndAddDuplicates(string subject, string[] words, int index, List<ComparisonScore> results)
    {
        mut.WaitOne();
        for (int i = 0; i < words.Length; i++)
        {
            if(i == index)
            {
                continue;
            }

            var target = words[i];
            var similarityScore = Fuzz.Ratio(subject, target);
            var result = new ComparisonScore() { Score = similarityScore, Subject = subject, Target = target };
            results.Add(result);
        }
        mut.ReleaseMutex();
    }

}



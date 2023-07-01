public interface IFileReader
{
    public string[] ReadAllLines(string path);
}
public class MyFileReader : IFileReader
{

    public MyFileReader()
    {
    }

    public string[] ReadAllLines(string path)
    {
        return File.ReadAllLines(path);
    }
}



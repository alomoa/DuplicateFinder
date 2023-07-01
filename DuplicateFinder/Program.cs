

var fileReader = new MyFileReader();
var finder = new DuplicateFinder(fileReader);

var result = finder.Execute("Test.txt");

Console.WriteLine(result.Count);

foreach (var item in result)
{
    Console.WriteLine($"{item.Score}: {item.Subject} -> {item.Target}");
}

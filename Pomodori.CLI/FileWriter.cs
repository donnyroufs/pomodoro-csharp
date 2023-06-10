namespace Pomodori.CLI;

public class FileWriter : IFileWriter
{
    private readonly string _path;

    public FileWriter(string path)
    {
        _path = path;
    }
    
    public void Write(string content)
    {
        using (var writer = new StreamWriter(_path))
        {
            writer.WriteLine(content);
        }
    }
}
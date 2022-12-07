namespace AdventOfCode_7;

public class AdventFile
{
    public AdventFile(string fileName, int size)
    {
        FileName = fileName;
        Size = size;
    }

    public string FileName { get; set; }
    public int Size { get; set; }
}
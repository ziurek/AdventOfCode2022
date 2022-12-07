namespace AdventOfCode_7;

public class Folder
{
    public Folder(string name, Folder parent)
    {
        Name = name;
        Parent = parent;
    }

    public string Name { get; set; }
    public List<AdventFile> Files { get; set; } = new List<AdventFile>();
    public List<Folder> Folders { get; set; } = new List<Folder>();
    public Folder Parent { get; set; }
}
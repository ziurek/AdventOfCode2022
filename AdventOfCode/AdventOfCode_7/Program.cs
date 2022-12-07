using AdventOfCode_7;
using System.Text.RegularExpressions;

Folder CreateDataStructure()
{
    Folder rootFolder = new Folder("/", null);
    Folder currentFolder = rootFolder;
    Folder? previousFolder = null;

    foreach (string line in File.ReadLines(@"dane_7.txt"))
    {
        Regex pattern = new Regex(@"(?:\$\scd )(\w+|\/|(..))");
        Match match = pattern.Match(line);
        if (match.Success)
        {
            var commandParameter = match.Groups["1"].Value;
            if (commandParameter == "/")
            {
                currentFolder = rootFolder;
            }
            else if (commandParameter == "..")
            {
                currentFolder = currentFolder.Parent;
            }
            else
            {
                currentFolder = currentFolder.Folders.Find(folder => folder.Name == commandParameter);
            }
        }

        pattern = new Regex(@"(?:dir )(\w+)");
        match = pattern.Match(line);
        if (match.Success) {
            var folderName = match.Groups["1"].Value;
            currentFolder?.Folders.Add(new Folder(folderName, currentFolder));
        }

        pattern = new Regex(@"(\d+)(?: )(\w+)");
        match = pattern.Match(line);
        if (match.Success)
        {
            var fileSize = int.Parse(match.Groups["1"].Value);
            var fileName = match.Groups["2"].Value;
            currentFolder?.Files.Add(new AdventFile(fileName, fileSize));
        }
    }

    return rootFolder;
}

int totalSum = 0;
int GetSizeOfFilesInFolder(Folder folder, int treshold)
{
    var sum = folder.Files.Select(x => x.Size).Sum() + folder.Folders.Select(f => GetSizeOfFilesInFolder(f, treshold)).Sum();
    if (sum < treshold)
    {
        totalSum += sum;
    }
    return sum;
}

var rootFolder = CreateDataStructure();
var folderSize = GetSizeOfFilesInFolder(rootFolder, 100000);

Console.WriteLine($"Task 1 response: {totalSum}");

int GetSizeOfFilesInFolderSimple(Folder folder)
{
    return folder.Files.Select(x => x.Size).Sum() + folder.Folders.Select(f => GetSizeOfFilesInFolderSimple(f)).Sum();
}

int rootSize = GetSizeOfFilesInFolderSimple(rootFolder);
var foldersToRemove = new List<(Folder, int size)>();
void GetSizeOfFilesInFolderTask2(Folder folder)
{
    var freeSpace = 70000000 - rootSize;
    if (freeSpace + GetSizeOfFilesInFolderSimple(folder) > 30000000)
    {
        foldersToRemove.Add(new (folder, GetSizeOfFilesInFolderSimple(folder)));
    }

    foreach (var f in folder.Folders)
    {
        GetSizeOfFilesInFolderTask2(f);
    }
}

GetSizeOfFilesInFolderTask2(rootFolder);
var result = foldersToRemove.OrderBy(x => x.Item2).First().Item2;

Console.WriteLine($"Task 2 response: {result}");
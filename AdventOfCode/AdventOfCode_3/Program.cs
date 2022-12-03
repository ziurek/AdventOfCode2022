// See https://aka.ms/new-console-template for more information

Console.WriteLine($"Task 1: priority sum: {Task1()}");
Console.WriteLine($"Task 2: priority sum: {Task2()}");

int Task1()
{
    var prioritySum = 0;
    foreach (string line in File.ReadLines(@"dane_3.txt"))
    {
        var firstCompartment = line.Substring(0, (int)line.Length / 2).ToArray().Distinct();
        var secondCompartment = line.Substring((int)line.Length / 2, (int)line.Length / 2).ToArray().Distinct();

        var commonItem = firstCompartment.Single(item => secondCompartment.Contains(item));

        prioritySum += ConvertToPriority(commonItem);
    }
    return prioritySum;
}

int Task2()
{
    var lines = new List<string>();
    var prioritySum = 0;

    foreach (string line in File.ReadLines(@"dane_3.txt"))
    {
        lines.Add(line);
    }

    for (int i = 0; i < lines.Count; i += 3)
    {
        var line1 = lines[i].ToArray().Distinct();
        var line2 = lines[i + 1].ToArray().Distinct();
        var line3 = lines[i + 2].ToArray().Distinct();

        var commonItem = line1.Single(item1 => line2.Contains(item1) && line3.Contains(item1));
        prioritySum += ConvertToPriority(commonItem);
    }
    return prioritySum;
}

int ConvertToPriority(char item)
{
    if (char.IsLower(item))
    {
        return (int)item - 96;
    }
    else
    {
        return (int)item - 38;
    }
}
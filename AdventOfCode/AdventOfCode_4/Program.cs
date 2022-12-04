int Task1()
{
    var total = 0;
    foreach (string line in File.ReadLines(@"dane_4.txt"))
    {
        var twoElves = line.Split(',');

        var firstElve = twoElves[0].Split('-').Select(int.Parse).ToArray();
        var firstElveAssignments = Enumerable.Range(firstElve[0], firstElve[1] - firstElve[0] + 1);

        var secondElve = twoElves[1].Split('-').Select(int.Parse).ToArray();
        var secondElveAssignments = Enumerable.Range(secondElve[0], secondElve[1] - secondElve[0] + 1);

        var isFullyCovered = firstElveAssignments.All(x => secondElveAssignments.Contains(x)) || secondElveAssignments.All(x => firstElveAssignments.Contains(x));
        total += isFullyCovered ? 1 : 0;
    }

    return total;
}

Console.WriteLine($"First task result: {Task1()}");

int Task2()
{
    var total = 0;
    foreach (string line in File.ReadLines(@"dane_4.txt"))
    {
        var twoElves = line.Split(',');

        var firstElve = twoElves[0].Split('-').Select(int.Parse).ToArray();
        var firstElveAssignments = Enumerable.Range(firstElve[0], firstElve[1] - firstElve[0] + 1);

        var secondElve = twoElves[1].Split('-').Select(int.Parse).ToArray();
        var secondElveAssignments = Enumerable.Range(secondElve[0], secondElve[1] - secondElve[0] + 1);

        var isFullyCovered = firstElveAssignments.Any(x => secondElveAssignments.Contains(x)) || secondElveAssignments.Any(x => firstElveAssignments.Contains(x));
        total += isFullyCovered ? 1 : 0;
    }

    return total;
}

Console.WriteLine($"Second task result: {Task2()}");
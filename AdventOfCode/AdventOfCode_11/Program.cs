int Task1()
{
    var lines = File.ReadLines(@"dane_11.txt").ToList();
    var monkeyItems = new List<List<int>>();
    var inspectionTimes = new List<int>();

    for (int i = 0; i < lines.Count; i+=7)
    {
        var startingItems = lines[i+1].Remove(0, 18).Split(", ").Select(x => int.Parse(x));

        monkeyItems.Add(new List<int>());
        monkeyItems.Last().AddRange(startingItems);
        inspectionTimes.Add(0);
    }

    for (int x = 0; x < 20; x++)
    {
        for (var idx = 0; idx < monkeyItems.Count; idx++)
        {
            for (int i = 0; monkeyItems[idx].Count > 0; i++)
            {
                var nextItem = monkeyItems[idx].First();
                inspectionTimes[idx] = inspectionTimes[idx] + 1;
                var operation = lines[idx * 7 + 2].Remove(0, 19).Split();

                var first = operation[0] == "old" ? nextItem : int.Parse(operation[0]);
                var second = operation[2] == "old" ? nextItem : int.Parse(operation[2]);
                var newWorrylvl = operation[1] == "*" ? first * second : first + second;
                newWorrylvl = (int)Math.Floor(newWorrylvl / 3.0);

                var divisibleBy = int.Parse(lines[idx * 7 + 3].Remove(0, 21));
                int targetMonkey = int.Parse(lines[idx * 7 + 5].Remove(0, 30));
                if (newWorrylvl % divisibleBy == 0)
                    targetMonkey = int.Parse(lines[idx * 7 + 4].Remove(0, 29));

                monkeyItems[idx].Remove(first);
                monkeyItems[targetMonkey].Add(newWorrylvl);
            }
        }
    }

    return inspectionTimes.OrderByDescending(x => x).Take(2).Aggregate(1, (a, b) => a * b);
}

long Task2()
{
    var lines = File.ReadLines(@"dane_11.txt").ToList();
    var monkeyItems = new List<List<long>>();
    var inspectionTimes = new List<long>();

    for (int i = 0; i < lines.Count; i+=7)
    {
        var startingItems = lines[i+1].Remove(0, 18).Split(", ").Select(x => long.Parse(x));

        monkeyItems.Add(new List<long>());
        monkeyItems.Last().AddRange(startingItems);
        inspectionTimes.Add(0);
    }

    for (int x = 0; x < 10000; x++)
    {
        for (var idx = 0; idx < monkeyItems.Count; idx++)
        {
            for (int i = 0; monkeyItems[idx].Count > 0; i++)
            {
                var nextItem = monkeyItems[idx].First();
                inspectionTimes[idx] = inspectionTimes[idx] + 1;
                var operation = lines[idx * 7 + 2].Remove(0, 19).Split();

                var first = operation[0] == "old" ? nextItem : long.Parse(operation[0]);
                var second = operation[2] == "old" ? nextItem : long.Parse(operation[2]);
                var newWorrylvl = operation[1] == "*" ? first * second : first + second;

                var divisibleBy = long.Parse(lines[idx * 7 + 3].Remove(0, 21));
                int targetMonkey = int.Parse(lines[idx * 7 + 5].Remove(0, 30));
                if (newWorrylvl % divisibleBy == 0)
                    targetMonkey = int.Parse(lines[idx * 7 + 4].Remove(0, 29));

                monkeyItems[idx].Remove(first);

                newWorrylvl %= 9699690;

                monkeyItems[targetMonkey].Add(newWorrylvl);
            }
        }
    }

    return inspectionTimes.OrderByDescending(x => x).Take(2).Aggregate((long)1, (a, b) => a * b);
}

Console.WriteLine($"Task 1 result: {Task1()}");
Console.WriteLine($"Task 2 result: {Task2()}");
/*
            [Q]     [G]     [M]    
            [B] [S] [V]     [P] [R]
    [T]     [C] [F] [L]     [V] [N]
[Q] [P]     [H] [N] [S]     [W] [C]
[F] [G] [B] [J] [B] [N]     [Z] [L]
[L] [Q] [Q] [Z] [M] [Q] [F] [G] [D]
[S] [Z] [M] [G] [H] [C] [C] [H] [Z]
[R] [N] [S] [T] [P] [P] [W] [Q] [G]
 1   2   3   4   5   6   7   8   9 
*/

using System.Text.RegularExpressions;

List<Stack<char>> InitializeStacks()
{
    var stacks = new List<Stack<char>>();
    for (int i = 0; i < 9; i++)
    {
        stacks.Add(new Stack<char> { });
    }

    var storedLines = new List<string>();
    var idx = 0;
    foreach (string line in File.ReadLines(@"dane_5.txt"))
    {
        var splitBySpace = line.Split().Where(x => x != string.Empty);
        var areAllNumbers = splitBySpace.All(x => int.TryParse(x, out var _));
        if (areAllNumbers && line != string.Empty)
        {
            var numbers = splitBySpace.Select(x => (int.Parse(x), line.IndexOf(x))).ToArray();
            foreach (var number in numbers)
            {
                for (int i = idx - 1; i >= 0; i--)
                {
                    var stackValue = storedLines[i][number.Item2];
                    if (stackValue != ' ')
                    {
                        stacks[number.Item1 - 1].Push(stackValue);
                    }
                }
            }
        }
        else
        {
            storedLines.Add(line);
            idx++;
        }
    }

    return stacks;
}

string Task1()
{
    var stacks = InitializeStacks();

    foreach (string line in File.ReadLines(@"dane_5.txt"))
    {
        if (line.StartsWith("move"))
        {
            Regex pattern = new Regex(@"(?:move).(\d+).(?:from).(\d).(?:to).(\d)");
            Match match = pattern.Match(line);

            int moveHowMany = int.Parse(match.Groups["1"].Value);
            int moveFrom = int.Parse(match.Groups["2"].Value);
            int moveTo = int.Parse(match.Groups["3"].Value);

            for (int i = 0; i < moveHowMany; i++)
            {
                var obj = stacks[moveFrom - 1].Pop();
                stacks[moveTo - 1].Push(obj);
            }
        }
    }

    return new string(stacks.Select(s => s.Pop()).ToArray());
}

Console.WriteLine($"Result of first task: {Task1()}");


List<List<char>> InitializeLists()
{
    var lists = new List<List<char>>();
    for (int i = 0; i < 9; i++)
    {
        lists.Add(new List<char> { });
    }

    var storedLines = new List<string>();
    var idx = 0;
    foreach (string line in File.ReadLines(@"dane_5.txt"))
    {
        var splitBySpace = line.Split().Where(x => x != string.Empty);
        var areAllNumbers = splitBySpace.All(x => int.TryParse(x, out var _));
        if (areAllNumbers && line != string.Empty)
        {
            var numbers = splitBySpace.Select(x => (int.Parse(x), line.IndexOf(x))).ToArray();
            foreach (var number in numbers)
            {
                for (int i = idx - 1; i >= 0; i--)
                {
                    var stackValue = storedLines[i][number.Item2];
                    if (stackValue != ' ')
                    {
                        lists[number.Item1 - 1].Add(stackValue);
                    }
                }
            }
        }
        else
        {
            storedLines.Add(line);
            idx++;
        }
    }

    return lists;
}

string Task2()
{
    var lists = InitializeLists();

    foreach (string line in File.ReadLines(@"dane_5.txt"))
    {
        if (line.StartsWith("move"))
        {
            Regex pattern = new Regex(@"(?:move).(\d+).(?:from).(\d).(?:to).(\d)");
            Match match = pattern.Match(line);

            int moveHowMany = int.Parse(match.Groups["1"].Value);
            int moveFrom = int.Parse(match.Groups["2"].Value);
            int moveTo = int.Parse(match.Groups["3"].Value);

            var obj = lists[moveFrom - 1].LastOrDefault();
            var from = lists[moveFrom - 1].Count - moveHowMany;
            lists[moveTo - 1].AddRange(lists[moveFrom - 1].GetRange(from, moveHowMany));
            lists[moveFrom - 1].RemoveRange(from, moveHowMany);
        }
    }

    return new string(lists.Select(s => s.Last()).ToArray());
}

Console.WriteLine($"Result of second task: {Task2()}");
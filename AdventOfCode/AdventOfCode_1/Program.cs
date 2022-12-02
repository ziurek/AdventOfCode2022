string text = File.ReadAllText(@"dane_1.txt");

var elves = text.Split(
    new string[] { "\r\n\r\n" },
    StringSplitOptions.None
);

var max = 0;
var elvesSum = new List<int>();

foreach (var elve in elves)
{
    var sumOfElve = elve.Split(
        new string[] { "\r\n" },
        StringSplitOptions.None
    ).Select(x => int.Parse(x)).Sum();

    elvesSum.Add(sumOfElve);

    max = Math.Max(max, sumOfElve);
}


var sum = elvesSum.OrderByDescending(x => x).Take(3).Sum();
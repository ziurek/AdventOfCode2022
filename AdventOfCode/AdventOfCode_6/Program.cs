int Task1()
{
    string text = File.ReadAllText(@"dane_6.txt");

    for (int i = 3; i < text.Length; i++)
    {
        var subArr = new char[] { text[i-3], text[i-2], text[i-1], text[i] };
        var areAllDifferent = subArr.Distinct().Count() == 4;

        if (areAllDifferent)
        {
            return i + 1;
        }
    }

    return -1;
}

Console.WriteLine($"Task 1 answer: {Task1()}");

int Task2()
{
    string text = File.ReadAllText(@"dane_6.txt");

    for (int i = 13; i < text.Length; i++)
    {
        var subArr = text.Substring(i-13, 14);

        var areAllDifferent = subArr.Distinct().Count() == 14;

        if (areAllDifferent)
        {
            return i + 1;
        }
    }

    return -1;
}

Console.WriteLine($"Task 2 answer: {Task2()}");
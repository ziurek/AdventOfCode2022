
var totalScore = 0;
foreach (string line in File.ReadLines(@"dane_2.txt"))
{
    var choices = line.Split(' ').Select(MapToInt).ToArray();
    var didFirstPlayerWon = FirstPlayerWin(choices[0], choices[1]);

    totalScore += choices[1];
    if (didFirstPlayerWon == false)
    {
        totalScore += 6;
    }
    if (didFirstPlayerWon == null)
    {
        totalScore += 3;
    }
}

Console.WriteLine($"Answer 1 = {totalScore}");

totalScore = 0;
foreach (string line in File.ReadLines(@"dane_2.txt"))
{
    var choices = line.Split(' ').ToArray();
    var youMustWin = YouMustWin(choices[1]);
    var firstPlayerChoice = MapToInt(choices[0]);

    totalScore += GetYourScore(firstPlayerChoice, youMustWin);
}
Console.WriteLine($"Answer 2 = {totalScore}");


int GetYourScore(int firstPlayerChoice, bool? youMustWin)
{
    var totalScore = 0;
    if (youMustWin == true)
    {
        var yourChoice = -1 * (-1 - firstPlayerChoice);
        yourChoice = yourChoice >= 1 && yourChoice <= 3 ? yourChoice : -1 * (2 - firstPlayerChoice);
        totalScore += yourChoice + 6;
    }
    else if (youMustWin == null)
    {
        totalScore += firstPlayerChoice + 3;
    }
    else
    {
        var yourChoice = -1 * (1 - firstPlayerChoice);
        yourChoice = yourChoice > 0 ? yourChoice : -1 * (-2 - firstPlayerChoice);
        totalScore += yourChoice;
    }

    return totalScore;
}

int MapToInt(string value)
{
    if (value == "A" || value == "X")
        return 1;
    else if (value == "B" || value == "Y")
        return 2; 
    else if (value == "C" || value == "Z")
        return 3;

    throw new Exception();
}

bool? YouMustWin(string value)
{
    if (value == "X")
        return false;
    else if (value == "Y")
        return null;
    else if (value == "Z")
        return true;

    throw new Exception();
}

bool? FirstPlayerWin(int first, int second)
{
    var result = first - second;

    if (result == 0)
    {
        return null;
    }
    else if (result == 1)
    {
        return true;
    }
    else if (result == -1)
    {
        return false;
    }
    else if (result == 2)
    {
        return false;
    }
    else if (result == -2)
    {
        return true;
    }

    throw new Exception();
}
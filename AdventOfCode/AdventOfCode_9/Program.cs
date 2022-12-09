int GetAmountOfVistedFields(int size)
{
    var map = new bool[size][];
    for (int i = 0; i < size; i++)
        map[i] = new bool[size];

    var (startX, startY) = (size / 2, size / 2);
    var (headX, headY) = (startX, startY);
    var (tailX, tailY) = (startX, startY);

    foreach (string line in File.ReadLines(@"dane_9.txt"))
    {
        var cmd = line.Split(' ');
        var direction = cmd[0];
        var count = int.Parse(cmd[1]);

        foreach(var i in Enumerable.Range(0, count))
        {
            map[tailX][tailY] = true;

            if (direction == "U")
                headY--;
            else if (direction == "D")
                headY++;
            else if (direction == "R")
                headX++;
            else if (direction == "L")
                headX--;

            (tailX, tailY) = CalculateNextTailPostion(headX, headY, tailX, tailY);
        }
        map[tailX][tailY] = true;
    }
    return map.Select(x => x.Count(x => x == true)).Sum();
}

(int x, int y) CalculateNextTailPostion(int headX, int headY, int tailX, int tailY)
{
    var xDistance = headX - tailX;
    var yDistance = headY - tailY;

    var isClose = Math.Abs(xDistance) <= 1 && Math.Abs(yDistance) <= 1;

    if (isClose) // tail is too close, so don't need to move
        return (tailX, tailY);
    else
    {
        int x = 0, y = 0;
        if (xDistance < 0)
            x = (int)Math.Floor(tailX + xDistance / (double)2);
        if (xDistance >= 0)
            x = (int)Math.Ceiling(tailX + xDistance / (double)2);
        if (yDistance < 0)
            y = (int)Math.Floor(tailY + yDistance / (double)2);
        if (yDistance >= 0)
            y = (int)Math.Ceiling(tailY + yDistance / (double)2);

        return (x, y);
    }
}

void DrawMapTask1((int x, int y) head, (int x, int y) tail, bool[][] map)
{
    Console.Clear();
    for (int y = 0; y < map.Length; y++)
    {
        System.Text.StringBuilder strBuilder = new System.Text.StringBuilder(string.Join("", Enumerable.Range(0, map.Length).Select(x => "*")));
        for (int x = 0; x < map.Length; x++)
        {
            if (x == tail.x && y == tail.y)
                strBuilder[x] = 'T';

            if (x == head.x && y == head.y)
                strBuilder[x] = 'H';
        }
        Console.WriteLine(strBuilder);
    }
}

void DrawMapTask2((int x, int y) head, List<(int x, int y)> knots, bool[][] map)
{
    Console.Clear();
    for (int y = 0; y < map.Length; y++)
    {
        System.Text.StringBuilder strBuilder = new System.Text.StringBuilder(string.Join("", Enumerable.Range(0, map.Length).Select(x => "*")));
        for (int x = 0; x < map.Length; x++)
        {
            for (int c = 0; c < knots.Count; c++)
            {
                if (x == knots[c].x && y == knots[c].y)
                    strBuilder[x] = (char)(c + '0');
            }

            if (x == head.x && y == head.y)
                strBuilder[x] = 'H';
        }
        Console.WriteLine(strBuilder);
    }
}

int GetAmountOfVistedFieldsForRope(int size)
{
    var map = new bool[size][];
    for (int i = 0; i < size; i++)
        map[i] = new bool[size];

    var (startX, startY) = (size / 2, size / 2);
    var (headX, headY) = (startX, startY);
    var ropePositions = new List<(int x, int y)>();
    for (int i = 0; i < 9; i++)
    {
        ropePositions.Add((x: startX, y: startY));
    }

    foreach (string line in File.ReadLines(@"dane_9.txt"))
    {
        var cmd = line.Split(' ');
        var direction = cmd[0];
        var count = int.Parse(cmd[1]);

        foreach (var i in Enumerable.Range(0, count))
        {
            map[ropePositions.Last().x][ropePositions.Last().y] = true;

            if (direction == "U")
                headY--;
            else if (direction == "D")
                headY++;
            else if (direction == "R")
                headX++;
            else if (direction == "L")
                headX--;

            var previousKnot = (x: headX, y: headY);
            for (int x = 0; x < ropePositions.Count(); x++)
            {
                var (newX, newY) = CalculateNextTailPostion(previousKnot.x, previousKnot.y, ropePositions[x].x, ropePositions[x].y);
                ropePositions[x] = (newX, newY);
                previousKnot = (newX, newY);
            }
        }
        map[ropePositions.Last().x][ropePositions.Last().y] = true;
    }
    return map.Select(x => x.Count(x => x == true)).Sum();
}

Console.WriteLine($"Task 1 answer: {GetAmountOfVistedFields(880)}");
Console.WriteLine($"Task 2 answer: {GetAmountOfVistedFieldsForRope(880)}");

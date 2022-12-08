int GetCountOfVisibleFromOutside()
{
    var lines = File.ReadLines(@"dane_8.txt").ToList();

    var forest = lines.Select(x => x.ToCharArray()).Select(x => x.Select(y => (int)char.GetNumericValue(y)).ToArray()).ToArray();

    var visibleCount = 0;
    for (int i = 1; i < forest.Length - 1; i++)
    {
        for (int j = 1; j < forest[i].Length - 1; j++)
        {
            var treeHeight = forest[i][j];
            var isVisbleFromTop = true;
            for (int k = 0; k < i; k++)
            {
                if (forest[k][j] >= treeHeight)
                {
                    isVisbleFromTop = false;
                }
            }
            var isVisbleFromBottom = true;
            for (int k = i + 1; k < forest.Length; k++)
            {
                if (forest[k][j] >= treeHeight)
                {
                    isVisbleFromBottom = false;
                }
            }

            var isVisbleFromLeft = true;
            for (int k = 0; k < j; k++)
            {
                if (forest[i][k] >= treeHeight)
                {
                    isVisbleFromLeft = false;
                }
            }
            var isVisbleFromRight = true;
            for (int k = j + 1; k < forest[i].Length; k++)
            {
                if (forest[i][k] >= treeHeight)
                {
                    isVisbleFromRight = false;
                }
            }

            if (isVisbleFromTop || isVisbleFromBottom || isVisbleFromLeft || isVisbleFromRight)
            {
                visibleCount++;
            }
        }
    }

    return visibleCount + forest.Length * 2 + forest[0].Length * 2 - 4;
}

Console.WriteLine($"Task 1 answer: {GetCountOfVisibleFromOutside()}");

int GetScenicScore()
{
    var scores = new List<int>();
    var lines = File.ReadLines(@"dane_8.txt").ToList();

    var forest = lines.Select(x => x.ToCharArray()).Select(x => x.Select(y => (int)char.GetNumericValue(y)).ToArray()).ToArray();

    for (int i = 1; i < forest.Length - 1; i++)
    {
        for (int j = 1; j < forest[i].Length - 1; j++)
        {
            var treeHeight = forest[i][j];
            var viewingDistanceTop = 0;
            for (int k = i-1; k >= 0; k--)
            {
                viewingDistanceTop++;

                if (forest[k][j] >= treeHeight)
                {
                    break;
                }
            }
            var viewingDistanceBottom = 0;
            for (int k = i + 1; k < forest.Length; k++)
            {
                viewingDistanceBottom++;

                if (forest[k][j] >= treeHeight)
                {
                    break;
                }
            }

            var viewingDistanceLeft = 0;
            for (int k = j-1; k >= 0; k--)
            {
                viewingDistanceLeft++;

                if (forest[i][k] >= treeHeight)
                {
                    break;
                }
            }
            var viewingDistanceRight = 0;
            for (int k = j + 1; k < forest[i].Length; k++)
            {
                viewingDistanceRight++;

                if (forest[i][k] >= treeHeight)
                {
                    break;
                }
            }

            scores.Add(viewingDistanceTop * viewingDistanceBottom * viewingDistanceLeft * viewingDistanceRight);
        }
    }

    return scores.Max();
}

Console.WriteLine($"Task 2 answer: {GetScenicScore()}");
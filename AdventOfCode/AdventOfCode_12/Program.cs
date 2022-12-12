

//using System.Text;

//int Task1()
//{
//    var lines = File.ReadLines(@"dane_12.txt").Select(x => x.ToCharArray().Select(y => (int)y).ToArray()).ToList();
//    var linesCopy = new List<int[]>();
//    for (int i = 0; i < lines.Count; i++)
//    {
//        var arr = new int[lines[i].Length];
//        Array.Copy(lines[i], arr, lines[i].Length);
//        linesCopy.Add(arr);
//    }

//    for (int y = 0; y < lines.Count; y++)
//    {
//        for (int x = 0; x < lines[y].Count(); x++)
//        {
//            if ((char)lines[y][x] == 'S')
//            {
//                return FindRoute(lines, linesCopy, x, y, 1, 10000000);
//            }
//        }
//    }

//    return -1;
//}


//var bestPath = int.MaxValue;
//int FindRoute(List<int[]> originalMap, List<int[]> map, int currentX, int currentY, int currentDistance, int attemptsLeft)
//{
//    var newDistance = currentDistance;

//    if (HasEndAround())
//    {
//        bestPath = currentDistance < bestPath ? currentDistance + 1: bestPath;

//        return currentDistance + 1;
//    }


//    // left
//    if (currentX > 0)
//    {
//        if (isGoodField(currentX-1, currentY))
//        {
//            newDistance = FindRoute(originalMap, map, currentX-1, currentY, currentDistance + 1, attemptsLeft);
//        }
//    }
//    // right
//    if (currentX < map[currentY].Length-1)
//    {
//        if (isGoodField(currentX+1, currentY))
//        {
//            newDistance = FindRoute(originalMap, map, currentX+1, currentY, currentDistance + 1, attemptsLeft);
//        }
//    }
//    // up
//    if (currentY > 0)
//    {
//        if (isGoodField(currentX, currentY-1))
//        {
//            newDistance = FindRoute(originalMap, map, currentX, currentY-1, currentDistance + 1, attemptsLeft);
//        }
//    }
//    // down
//    if (currentY < map.Count-1)
//    {
//        if (isGoodField(currentX, currentY+1))
//        {
//            newDistance = FindRoute(originalMap, map, currentX, currentY+1, currentDistance + 1, attemptsLeft);
//        }
//    }

//    bool isGoodField(int newX, int newY)
//    {
//        var isOk = map[currentY][currentX] == 83 || Math.Abs(map[currentY][currentX] - map[newY][newX]) <= 1;

//        if (isOk)
//            map[currentY][currentX] = 1000;

//        //var isNotTheSameAsPreviousLocation = previousX != currentX || previousY != currentY;

//        return isOk && map[newY][newX] != 1000;// && (map[currentY][currentX] == 83 || isNotTheSameAsPreviousLocation);
//    }

//    bool HasEndAround()
//    {
//        if (currentX < map[currentY].Length - 1 && (char)map[currentY][currentX+1] == 'E')
//            return true;
//        else if (currentX > 0 && (char)map[currentY][currentX-1] == 'E')
//            return true;
//        else if (currentY > 0 && (char)map[currentY-1][currentX] == 'E')
//            return true;
//        else if (currentY < map.Count - 1 && (char)map[currentY+1][currentX] == 'E')
//            return true;
//        return false;
//    }

//    return attemptsLeft > 0 ? FindRoute(originalMap, originalMap, currentX, currentY, 1, attemptsLeft - 1) : currentDistance;
//}




//int Task1_New()
//{
//    var lines = File.ReadLines(@"dane_12.txt").Select(x => x.ToCharArray().Select(y => (int)y).ToArray()).ToList();
//    var linesCopy = new List<int[]>();
//    for (int i = 0; i < lines.Count; i++)
//    {
//        var arr = new int[lines[i].Length];
//        Array.Copy(lines[i], arr, lines[i].Length);
//        linesCopy.Add(arr);
//    }

//    var startPosition = new Position();
//    var endPosition = new Position();

//    for (int y = 0; y < lines.Count; y++)
//    {
//        for (int x = 0; x < lines[y].Count(); x++)
//        {
//            if ((char)lines[y][x] == 'S')
//            {
//                startPosition = new Position { X = x, Y = y };
//            } 
//            else if ((char)lines[y][x] == 'E')
//            {
//                endPosition = new Position { X = x, Y = y };
//            }
//        }
//    }
//    lines[startPosition.Y][startPosition.X] = (int)'a';
//    lines[endPosition.Y][endPosition.X] = (int)'z';

//    return FindRouteNew(lines, startPosition, endPosition, 1000000000);
//}

//int FindRouteNew(List<int[]> map, Position startPosition, Position goal, int maxAttempts)
//{
//    var visited = new Dictionary<string, Position>();
//    var queue = new Queue<(Position pos, Position? lastPos)>();

//    queue.Enqueue((new Position { X = startPosition.X, Y = startPosition.Y }, null));
//    var iters = 0;
//    while (queue.Count > 0)
//    {
//        var cellInfo = queue.Dequeue();

//        if (CheckCell(map, queue, visited, cellInfo.pos, cellInfo.lastPos, goal))
//        {
//            break;
//        }

//        if (iters++ > maxAttempts)
//        {
//            return 0;
//        }
//    }
//    /*
//     while str(cur_pos) in visited and visited[str(cur_pos)] != null:
//		if cur_pos != null:
//			backtraced_path.append(cur_pos)
//		cur_pos = visited[str(cur_pos)]
//     */




//    return PrintVisited(map, startPosition, goal, visited) + 1;
//}

//int PrintVisited(List<int[]> map, Position startPosition, Position goal, Dictionary<string, Position> visited)
//{
//    var backtracedPath = new List<Position>();
//    var currentPos = goal with { };
//    //visited.
//    while (visited.Any(pos => pos.Key == $"{currentPos.X}.{currentPos.Y}") && visited[$"{currentPos.X}.{currentPos.Y}"] != null)
//    {
//        if (currentPos != null)
//            backtracedPath.Add(currentPos);

//        if (currentPos == startPosition)
//            break;

//        currentPos = visited[$"{currentPos.X}.{currentPos.Y}"];
//    }
//    backtracedPath.Add(startPosition);

//    var stringBld = new StringBuilder();
//    for (int i = 0; i < map.Count; i++)
//    {
//        for (int k = 0; k < map[i].Count(); k++)
//        {
//            var found = backtracedPath.FindIndex(x => x.X == k && x.Y == i);
//            if (found != -1)
//            {
//                var item = backtracedPath.ElementAt(found);
//                var itemAfter = backtracedPath.ElementAtOrDefault(found+1);
//                //var itemBefore = backtracedPath.ElementAtOrDefault(found-1);
//                if (itemAfter?.Y > item.Y)
//                {
//                    stringBld.Append("^");

//                }
//                else if (itemAfter?.Y < item.Y)
//                {
//                    stringBld.Append("v");
//                }
//                else if (itemAfter?.X < item.X)
//                {
//                    stringBld.Append(">");
//                }
//                else if (itemAfter != null)
//                {
//                    stringBld.Append("<");
//                }
//                else
//                {
//                    stringBld.Append("S");
//                }
//            }
//            else
//            {
//                stringBld.Append(".");
//            }
//        }
//        Console.WriteLine(stringBld.ToString());
//        stringBld.Clear();
//    }

//    return backtracedPath.Count;
//}

//Console.WriteLine($"Task1 result: {Task1_New()}");

//bool CheckCell(List<int[]> map, Queue<(Position pos, Position? lasPos)> queue, Dictionary<string, Position> visited, Position current, Position? last, Position goal)
//{
//    if (!CanMoveToSpot(map, last, current, goal))
//        return false;
//    if (visited.Any(x => x.Key == $"{current.X}.{current.Y}"))
//        return false;

//    if (last != null)
//    {
//        visited[$"{current.X}.{current.Y}"] = last;
//    }


//    if (current.X == goal.X && current.Y == goal.Y)
//    {
//        //visited[$"{current.X}.{current.Y}"] = last;
//        return true;
//    }

//    queue.Enqueue((new Position { X = current.X, Y = current.Y - 1 }, new Position { X = current.X, Y = current.Y }));
//    queue.Enqueue((new Position { X = current.X, Y = current.Y + 1 }, new Position { X = current.X, Y = current.Y }));
//    queue.Enqueue((new Position { X = current.X - 1, Y = current.Y }, new Position { X = current.X, Y = current.Y }));
//    queue.Enqueue((new Position { X = current.X + 1, Y = current.Y }, new Position { X = current.X, Y = current.Y }));

//    return false;
//}

//bool CanMoveToSpot(List<int[]> map, Position? lastPos, Position currentPos, Position goalPos)
//{
//    if (currentPos.X < 0 || currentPos.Y < 0 || currentPos.Y > map.Count - 1 || currentPos.X > map[currentPos.Y].Length - 1)
//        return false;

//    //if (currentPos.X > 0 || currentPos.Y < 0 || currentPos.Y > map.Count - 1 || currentPos.X > map[currentPos.Y].Length - 1)
//    //    return true

//    //if (currentPos.X == goalPos.X && currentPos.Y == goalPos.Y)
//    //    return true;

//    //|| map[lastPos.Y][lastPos.X] == 83
//    return lastPos == null  || Math.Abs(map[currentPos.Y][currentPos.X] - map[lastPos.Y][lastPos.X]) <= 1;
//}

//int V = 40;

using System.Text;

Task1();

int MinimumDistance(int[] distance, bool[] shortestPathTreeSet, int verticesCount)
{
    int min = int.MaxValue;
    int minIndex = 0;

    for (int v = 0; v < verticesCount; ++v)
    {
        if (shortestPathTreeSet[v] == false && distance[v] <= min)
        {
            min = distance[v];
            minIndex = v;
        }
    }

    return minIndex;
}

void Print(int[][] map, Position goal, int[] distance, int verticesCount)
{
    var idx = GetIdxOfVertexWithPosition(map, goal);
    Console.WriteLine($"Distance from goal to starting location is {distance[idx]}");

    //for (int i = 0; i < verticesCount; ++i)
    //    Console.WriteLine("{0}\t  {1}", i, distance[i]);
}

void Dijkstra(int[][] map, Position goal, int[,] graph, int source, int verticesCount)
{
    List<Position> path = new List<Position>();
    int[] distance = new int[verticesCount];
    bool[] shortestPathTreeSet = new bool[verticesCount];

    for (int i = 0; i < verticesCount; ++i)
    {
        distance[i] = int.MaxValue;
        shortestPathTreeSet[i] = false;
    }

    distance[source] = 0;

    for (int count = 0; count < verticesCount - 1; ++count)
    {
        int u = MinimumDistance(distance, shortestPathTreeSet, verticesCount);
        shortestPathTreeSet[u] = true;

        for (int v = 0; v < verticesCount; ++v)
            if (!shortestPathTreeSet[v] && Convert.ToBoolean(graph[u, v]) && distance[u] != int.MaxValue && distance[u] + graph[u, v] < distance[v])
            {
                path.Add(GetPositionForVertexWithIdx(map, u));
                distance[v] = distance[u] + graph[u, v];
            }
    }

    Print(map, goal, distance, verticesCount);
}

void Task1()
{
    var map = File.ReadLines(@"dane_12.txt").Select(x => x.ToCharArray().Select(y => (int)y).ToArray()).ToArray();

    var startPosition = new Position();
    var endPosition = new Position();

    for (int y = 0; y < map.Length; y++)
    {
        for (int x = 0; x < map[y].Count(); x++)
        {
            if ((char)map[y][x] == 'S')
            {
                startPosition = new Position { x = x, y = y };
            }
            else if ((char)map[y][x] == 'E')
            {
                endPosition = new Position { x = x, y = y };
            }
        }
    }
    map[startPosition.y][startPosition.x] = (int)'a';
    map[endPosition.y][endPosition.x] = (int)'z';

    int[,] graph = new int[map[0].Length * map.Length, map[0].Length * map.Length];
    for (int i = 0; i < graph.GetLength(0); i++)
    {
        for (int k = 0; k < graph.GetLength(1); k++)
        {
            graph[i, k] = 0;
        }
    }

    for (int i = 0; i < map.Length; i++)
    {
        for (int k = 0; k < map[i].Length; k++)
        {
            var currentPosition = new Position { y = i, x = k };
            var vertexIdx = GetIdxOfVertexWithPosition(map, currentPosition);

            var nextPositions = new Position[]
            {
                new Position { x = currentPosition.x, y = currentPosition.y - 1 },
                new Position { x = currentPosition.x, y = currentPosition.y + 1 },
                new Position { x = currentPosition.x + 1, y = currentPosition.y },
                new Position { x = currentPosition.x - 1, y = currentPosition.y }
            };

            foreach (var position in nextPositions)
            {
                int weight = GetWeight(map, currentPosition, position);
                if (weight > 0)
                {
                    var idx = GetIdxOfVertexWithPosition(map, currentPosition);
                    var idxY = GetIdxOfVertexWithPosition(map, position);
                    graph[idx, idxY] = weight;
                }
            }
        }
    }

    //for (int i = 0; i < graph.GetLength(0); i++)
    //{
    //    var strBld = new StringBuilder();
    //    for (int k = 0; k < graph.GetLength(0); k++)
    //    {
    //        strBld.Append(graph[i, k].ToString());
    //    }
    //    Console.WriteLine(strBld.ToString());
    //}

    Dijkstra(map, endPosition, graph, GetIdxOfVertexWithPosition(map, startPosition), map.Length * map[0].Length);

    var _ = 123;
}

int GetWeight(int[][] map, Position current, Position next)
{
    var item = map[current.y][current.x];
    int itemAtNext;
    try
    {
        itemAtNext = map[next.y][next.x];
        var distance = Math.Abs(item - itemAtNext);
        return (current != next && distance <= 1) ? 1 : 1000;
        //return Math.Abs(item - itemAtNext);
    }
    catch
    {
        return -1;
    }
}

Position GetPositionForVertexWithIdx(int[][] map, int idx)
{
    var y = (int)Math.Floor(idx / (double)map[0].Length);
    var x = idx % map[0].Length;
    return new Position { x = x, y = y };
}

int GetIdxOfVertexWithPosition(int[][] map, Position position)
{
    return map[0].Length * position.y + position.x;
}

record Position
{
    public int x { get; set; }
    public int y { get; set; }
}
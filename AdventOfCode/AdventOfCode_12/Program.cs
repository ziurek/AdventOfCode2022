
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

int Print(int[][] map, Position goal, int[] distance, int verticesCount)
{
    var idx = GetIdxOfVertexWithPosition(map, goal);
    //Console.WriteLine($"Distance from goal to starting location is {distance[idx]}");
    return distance[idx];
}

async Task<int> Dijkstra(int[][] map, Position goal, int[,] graph, int source, int verticesCount)
{
    int[] path = new int[verticesCount];
    //List<Position> path = new List<Position>(verticesCount);
    for (int i = 0; i < verticesCount; i++)
    {
        path[i] = -1;
    }

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
                //path.Add(GetPositionForVertexWithIdx(map, u));
                path[v] = u;

                distance[v] = distance[u] + graph[u, v];
            }
    }

    var idxOfGoal = GetIdxOfVertexWithPosition(map, goal);
    var currentIdx = idxOfGoal;
    var pathFull = new List<Position>();
    //pathFull.Add(goal);
    while (path[currentIdx] != -1)
    {
        //var position = 
        pathFull.Add(GetPositionForVertexWithIdx(map, currentIdx));
        currentIdx = path[currentIdx];
    }

    //var lines = new List<string>();
    //for (int i = 0; i < map.Length; i++)
    //{
    //    var strBld = new StringBuilder();
    //    for (int k = 0; k < map[0].Length; k++)
    //    {
    //        var foundVrtx = pathFull.Find(z => z.x == k && z.y == i);
    //        if (foundVrtx != null)
    //        {
    //            strBld.Append('x');
    //        } 
    //        else
    //        {
    //            strBld.Append('.');
    //        }
    //    }
    //    //Console.WriteLine(strBld.ToString());
    //    lines.Add(strBld.ToString());
    //}

    //await File.WriteAllLinesAsync("Result.txt", lines);

    return Print(map, goal, distance, verticesCount);
}

async Task Task1()
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

    var distance = await Dijkstra(map, endPosition, graph, GetIdxOfVertexWithPosition(map, startPosition), map.Length * map[0].Length);

    Console.WriteLine($"Task 1 result: {distance}");
}

async Task Task2()
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

    var listDistances = new List<int>();
    for (int i = 0; i < map.Length; i++)
    {
        for (int k = 0; k < map[0].Length; k++)
        {
            var item = map[i][k];
            if('a' == (char)item)
            {
                var distance = await Dijkstra(map, endPosition, graph, GetIdxOfVertexWithPosition(map, 
                    new Position
                    {
                        x = k,
                        y = i
                    }), map.Length * map[0].Length);

                listDistances.Add(distance);
            }
        }
    }

    Console.WriteLine($"Task 2 result: {listDistances.Min()}");
}


int GetWeight(int[][] map, Position current, Position next)
{
    var item = map[current.y][current.x];
    int itemAtNext;
    try
    {
        itemAtNext = map[next.y][next.x];
        var distance = itemAtNext - item;//Math.Abs(item - itemAtNext);
        return (current != next && distance <= 1) ? 1 : 100000;
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


await Task1();

await Task2();

record Position
{
    public int x { get; set; }
    public int y { get; set; }
}
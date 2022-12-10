using System.Text;

int Task1()
{
    var lines = File.ReadLines(@"dane_10.txt").ToList();
    var processedLineIdx = 0;
    var processingTime = 0;
    var xReg = 1;
    var pendingToAdd = 0;
    var strengths = new List<int>();

    // clock
    for (int i = 1; true; i++)
    {
        if (processedLineIdx >= lines.Count)
            break;

        if (processingTime == 0)
        {
            xReg += pendingToAdd;

            var line = lines[processedLineIdx++];
            var cmds = line.Split(' ');
            var instruction = cmds[0];
            var count = cmds.Length > 1 ? int.Parse(cmds[1]) : 0;
            pendingToAdd = count;
            processingTime = instruction == "noop" ? 1 : 2;
        }

        processingTime--;

        // 20th, 60th, 100th, 140th, 180th, and 220th
        if (i == 20 || i == 60 || i == 100 || i == 140 || i == 180 || i == 220)
            strengths.Add(i * xReg);
    }

    return strengths.Sum();
}

void Task2()
{
    var lines = File.ReadLines(@"dane_10.txt").ToList();
    var processedLineIdx = 0;
    var processingTime = 0;
    var xReg = 1;
    var pendingToAdd = 0;
    var strengths = new List<int>();
    var screen = new StringBuilder();
    var drawIdx = 0;

    // clock
    for (int i = 1; true; i++)
    {
        if (processedLineIdx >= lines.Count)
            break;

        if (processingTime == 0)
        {
            xReg += pendingToAdd;

            var line = lines[processedLineIdx++];
            var cmds = line.Split(' ');
            var instruction = cmds[0];
            var count = cmds.Length > 1 ? int.Parse(cmds[1]) : 0;
            pendingToAdd = count;
            processingTime = instruction == "noop" ? 1 : 2;
        }

        if (drawIdx == xReg || drawIdx == xReg - 1 || drawIdx == xReg + 1)
            screen.Append("X");
        else
            screen.Append(".");
        
        drawIdx++;
        processingTime--;

        // 20th, 60th, 100th, 140th, 180th, and 220th
        if (i == 20 || i == 60 || i == 100 || i == 140 || i == 180 || i == 220)
        {
            strengths.Add(i * xReg);
        }

        if (i % 40 == 0)
        {
            Console.WriteLine(screen);
            drawIdx = 0;
            screen.Clear();
        }
    }
}

Console.WriteLine($"Task 1 response: {Task1()}");
Console.WriteLine($"Task 2:");
Task2();
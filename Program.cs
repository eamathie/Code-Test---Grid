string path = "grid3.txt";
int[,] grid = ReadFile(path);

ulong largestProduct = 0;
(int x, int y)[]? bestCoords = null;

// loop through entire grid
for (int y = 0; y < grid.GetLength(1); y++)
{
    for (int x = 0; x < grid.GetLength(0); x++)
    {
        var h =  CheckHorizontally(x, y, grid);
        var v = CheckVertically(x, y, grid);
        var dDown =  CheckDiagonallyDownRight(x, y, grid);
        var dUp =  CheckDiagonallyUpRight(x, y, grid);
        
        var best = new[] { h, v, dDown, dUp }.MaxBy(t => t.product);
        if (best.product > largestProduct)
        {
            largestProduct = best.product;
            bestCoords = best.coords;
        }
    }
}

// display result
Console.WriteLine($"Largest product: {largestProduct}");
Console.WriteLine("Consists of the following coordinates:");
foreach (var (x, y) in bestCoords)
    Console.WriteLine($"x: {x}, y: {y} = {grid[x, y]}");

int[,] ReadFile(string filePath)
{
    try
    {
        using StreamReader reader = new StreamReader(filePath);
        int[,] grid = new int[20, 20];

        int i = 0;
        while (reader.ReadLine() is { } line)
        {
            int j = 0;
            foreach (string num in line.Split(' '))
            {
                grid [j,i] = int.Parse(num);
                j++;
            }
            i++;
        }

        return grid;
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
        throw;
    }
}

(ulong product, (int x, int y)[] coords) CheckHorizontally(int x, int y, int[,] grid)
{
    if (x > grid.GetLength(0) - 4)
        return (0, Array.Empty<(int, int)>());

    ulong prod = 1;
    var coords = new (int, int)[4];
    for (int i = 0; i < 4; i++)
    {
        prod  *= (ulong)grid[x + i, y];
        coords[i] = (x + i, y);
    }
    
    return (prod, coords);
}

(ulong product, (int x, int y)[] coords) CheckVertically(int x, int y, int[,] grid)
{
    if (y > grid.GetLength(1) - 4)
        return (0,  Array.Empty<(int, int)>());

    ulong prod = 1;
    var coords = new (int, int)[4];
    for (int i = 0; i < 4; i++)
    {
        prod  *= (ulong)grid[x, y + i];
        coords[i] = (x, y + i);
    }
    
    return (prod, coords);
}

(ulong product, (int x, int y)[] coords) CheckDiagonallyDownRight(int x, int y, int[,] grid)
{
    if (x > grid.GetLength(0) - 4 || y > grid.GetLength(1) - 4)
        return (0,  Array.Empty<(int, int)>());

    ulong prod = 1;
    var coords = new (int, int)[4];
    for (int i = 0; i < 4; i++)
    {
        prod *= (ulong)grid[x + i, y + i];
        coords[i] = (x + i, y + i);
    }

    return (prod, coords);
}

(ulong product, (int x, int y)[] coords) CheckDiagonallyUpRight(int x, int y, int[,] grid)
{
    if (x > grid.GetLength(0) - 4 || y < 3)
        return (0, Array.Empty<(int, int)>());

    ulong prod = 1;
    var coords = new (int, int)[4];
    for (int i = 0; i < 4; i++)
    {
        prod *= (ulong)grid[x + i, y - i];
        coords[i] = (x + i, y - i);
    }

    return (prod, coords);
}


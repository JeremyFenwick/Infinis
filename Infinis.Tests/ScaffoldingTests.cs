using System.Drawing;
using System.Drawing.Imaging;
using Infinis.Algorithms;
using Infinis.Scaffolding;

namespace Infinis.Tests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    private Grid MakeGrid()
    {
        return new Grid(4, 4);
    }

    [Test]
    public void GridDimensions()
    {
        var grid = MakeGrid();
        Assert.That(grid.Rows(), Is.EqualTo(4));
        Assert.That(grid.Cols(), Is.EqualTo(4));
    }

    [Test]
    public void GridIndexing()
    {
        var grid = MakeGrid();
        Assert.That(grid[1, 1] != null);
    }

    [Test]
    public void GridEnumeration()
    {
        var grid = MakeGrid();
        foreach (var cell in grid)
        {
            Console.WriteLine(cell);
        }
        Assert.That(true);
    }

    // Visual test. Expected Output
    // +---+---+---+---+
    // |   |   |   |   |
    // +---+---+---+---+
    // |   |   |   |   |
    // +---+---+---+---+
    // |   |   |   |   |
    // +---+---+---+---+
    // |   |   |   |   |
    // +---+---+---+---+
    [Test]
    public void GridToString()
    {
        var grid = MakeGrid();
        Console.WriteLine(grid);
        Assert.That(true);
    }
    
    // Visual test. Sample Output (random generation)
    // +---+---+---+---+
    // |               |
    // +---+   +   +   +
    // |       |   |   |
    // +---+---+---+   +
    // |               |
    // +   +   +---+   +
    // |   |   |       |
    // +---+---+---+---+
    [Test]
    public void MazeToString()
    {
        var grid = MakeGrid();
        MazeGen.BinaryTree(grid);
        Console.WriteLine(grid);
        Assert.That(true);
    }
    
    // Visual test. Sample Output (random generation)
    // +---+---+---+---+
    // |               |
    // +   +   +   +   +
    // |   |   |   |   |
    // +   +   +---+   +
    // |   |       |   |
    // +---+---+   +   +
    // |           |   |
    // +---+---+---+---+
    [Test]
    public void MazeToString2()
    {
        var grid = MakeGrid();
        MazeGen.SideWinder(grid);
        Console.WriteLine(grid);
        Assert.That(true);
    }

    [Test]
    public void GridRow()
    {
        var grid = MakeGrid();
        var row = grid.GetRow(0);
        Assert.That(row[0].Location() == (0, 0));
        Assert.That(row[1].Location() == (0, 1));
    }
    
    [Test]
    public void GridCol()
    {
        var grid = MakeGrid();
        var col = grid.GetColumn(0);
        Assert.That(col[0].Location() == (0, 0));
        Assert.That(col[1].Location() == (1, 0));
    }

    // Visual test. Sample Output (random generation)
    [Test]
    public void GridToImage()
    {
        var grid = MakeGrid();
        MazeGen.BinaryTree(grid);
        grid.CreateImage(40, "C:\\Users\\jerem\\Documents\\sidewinder.jpg", Color.Bisque, Color.DarkGreen);
        Assert.That(true);
    }

    // Visual test. Sample Output (random generation)
    // +---+---+---+---+---+
    // | 0   1   2   3   4 |
    // +---+---+---+---+   +
    // | 9   8   7   6   5 |
    // +---+---+---+   +   +
    // |10   9   8   7 | 6 |
    // +   +---+   +---+   +
    // |11 |10   9 | 8   7 |
    // +   +   +   +   +   +
    // |12 |11 |10 | 9 | 8 |
    // +---+---+---+---+---+
    [Test]
    public void DistanceGrid()
    {
        var grid = new DistanceGrid(20, 20);
        MazeGen.BinaryTree(grid);
        var start = grid[0, 0];
        var distances = start.Distances();
        grid.Distances = distances;
        Console.WriteLine(grid);
        Assert.That(true);
    }
    
    [Test]
    public void SolutionGrid()
    {
        var grid = new DistanceGrid(16, 16);
        MazeGen.SideWinder(grid);
        var start = grid[15, 0];
        var distances = start.Distances();
        grid.Distances = distances;
        grid.Distances = grid.Distances.PathTo(grid[0, 15]);
        Console.WriteLine(grid);
        Assert.That(true);
    }
    
    [Test]
    public void ColourSolutionGrid()
    {
        var grid = new ColourGrid(16, 16);
        MazeGen.SideWinder(grid);
        var start = grid[15, 0];
        var distances = start.Distances();
        grid.SetDistances(distances);
        grid.CreateImage(40, "C:\\Users\\jerem\\Documents\\colourmaze.png", Color.Bisque, Color.DarkSlateGray, 2f, ImageFormat.Png);
        Console.WriteLine(grid);
        Assert.That(true);
    }
}
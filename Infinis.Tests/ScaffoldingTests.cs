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
        var mr = new MazeRunner(grid, new BinaryTree());
        Console.WriteLine(mr);
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
        var mr = new MazeRunner(grid, new SideWinder());
        Console.WriteLine(mr);
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
        var mr = new MazeRunner(grid, new SideWinder());
        mr.CreateImage(40, "C:\\Users\\jerem\\Documents\\sidewinder.jpg", Color.Bisque, Color.DarkGreen);
    }
}
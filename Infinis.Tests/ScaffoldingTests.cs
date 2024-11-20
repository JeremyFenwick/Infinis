using Infinis.Algorithms;
using Infinis.Scaffolding;

namespace Infinis.Tests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    public Grid MakeMaze()
    {
        return new Grid(4, 4);
    }

    [Test]
    public void GridDimensions()
    {
        var grid = MakeMaze();
        Assert.That(grid.Rows(), Is.EqualTo(4));
        Assert.That(grid.Cols(), Is.EqualTo(4));
    }

    [Test]
    public void GridIndexing()
    {
        var grid = MakeMaze();
        Assert.That(grid[1, 1] != null);
    }

    [Test]
    public void GridEnumeration()
    {
        var grid = MakeMaze();
        foreach (var cell in grid)
        {
            Console.WriteLine(cell);
        }
        Assert.That(true);
    }

    [Test]
    public void GridToString()
    {
        var grid = MakeMaze();
        Console.WriteLine(grid);
    }
    
    [Test]
    public void MazeToString()
    {
        var grid = MakeMaze();
        var mr = new MazeRunner(grid, new BinaryTree());
        Console.WriteLine(mr);
    }
}
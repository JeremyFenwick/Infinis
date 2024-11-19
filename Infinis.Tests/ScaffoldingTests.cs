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
        return new Grid(10, 10);
    }

    [Test]
    public void GridDimensions()
    {
        var grid = MakeMaze();
        Assert.That(grid.Rows(), Is.EqualTo(10));
        Assert.That(grid.Cols(), Is.EqualTo(10));
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
            Console.WriteLine(cell.ToString());
        }
    }
}
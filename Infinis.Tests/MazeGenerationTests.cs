using System.Drawing;
using System.Drawing.Imaging;
using Infinis.Algorithms;
using Infinis.Scaffolding;

namespace Infinis.Tests;

public class MazeGenerationTests
{
        [Test]
    public void DistanceGrid()
    {
        var grid = new DistanceMaze(10, 10, MazeGenAlgorithms.BinaryTree);
        // var start = grid[0, 0];
        // var distances = start.Distances();
        // grid.Distances = distances;
        grid.DistanceTo(0, 9);
        Console.WriteLine(grid);
        Assert.That(true);
    }
    
    [Test]
    public void SolutionGrid()
    {
        var grid = new DistanceMaze(16, 16, MazeGenAlgorithms.SideWinder);
        var start = grid[15, 0];
        var distances = start.Distances();
        grid.Distances = distances;
        grid.Distances = grid.Distances.PathTo(grid[0, 15]);
        Console.WriteLine(grid);
        Assert.That(true);
    }
    
    [Test]
    public void GreenSolutionGrid()
    {
        var grid = new ColourMaze(16, 16, MazeGenAlgorithms.SideWinder);
        grid.CellColour = CellColour.Green;
        grid.CreateImage(40, "C:\\Users\\jerem\\Documents\\GreenMaze.png", Color.WhiteSmoke, Color.Black, 2f, ImageFormat.Png);
        Console.WriteLine(grid);
        Assert.That(true);
    }
    
    [Test]
    public void RedSolutionGrid()
    {
        var grid = new ColourMaze(16, 16, MazeGenAlgorithms.SideWinder);
        grid.CellColour = CellColour.Red;
        grid.CreateImage(40, "C:\\Users\\jerem\\Documents\\RedMaze.png", Color.WhiteSmoke, Color.Black, 2f, ImageFormat.Png);
        Console.WriteLine(grid);
        Assert.That(true);
    }
    
    [Test]
    public void BlueSolutionGrid()
    {
        var grid = new ColourMaze(16, 16, MazeGenAlgorithms.BinaryTree);
        grid.CellColour = CellColour.Blue;
        grid.CreateImage(40, "C:\\Users\\jerem\\Documents\\BlueMaze.png", Color.WhiteSmoke, Color.Black, 2f, ImageFormat.Png);
        Console.WriteLine(grid);
        Assert.That(true);
    }
    
    [Test]
    public void SolvedMaze()
    {
        var grid = new SolvedMaze(16, 16, MazeGenAlgorithms.BinaryTree);
        grid.CreateImage(40, "C:\\Users\\jerem\\Documents\\SolvedMaze.png", Color.CornflowerBlue, Color.Black, 2f, ImageFormat.Png);
        Console.WriteLine(grid);
        Assert.That(true);
    }

    [Test]
    public void AldousBroder()
    {
        var grid = new SolvedMaze(16, 16, MazeGenAlgorithms.AldousBroder);
        grid.CreateImage(40, "C:\\Users\\jerem\\Documents\\AldousBroder.png", Color.CornflowerBlue, Color.Black, 2f, ImageFormat.Png);
        Assert.That(true);
    }
    
    [Test]
    public void Wilson()
    {
        var grid = new SolvedMaze(16, 16, MazeGenAlgorithms.Wilson);
        grid.CreateImage(40, "C:\\Users\\jerem\\Documents\\Wilson.png", Color.WhiteSmoke, Color.Black, 2f, ImageFormat.Png);
        Assert.That(true);
    }
}
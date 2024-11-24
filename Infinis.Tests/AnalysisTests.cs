using Infinis.Algorithms;
using Infinis.Analysis;
using Infinis.Scaffolding;

namespace Infinis.Tests;

public class AnalysisTests
{
    [Test]
    public void DeadEndsTest()
    {
        var grid = new ColourMaze(20, 20, MazeGenAlgorithms.HuntAndKill);
        Console.WriteLine(grid);
        Console.WriteLine(grid.DeadEnds().Count);
    }
    
    [Test]
    public void DeadEndAnalysisTest()
    {
        var algos = new List<MazeGenAlgorithms>
        {
            MazeGenAlgorithms.SideWinder, MazeGenAlgorithms.HuntAndKill, MazeGenAlgorithms.BinaryTree,
            MazeGenAlgorithms.AldousBroder, MazeGenAlgorithms.Wilson
        };
        var compare = new DeadEndComparison(algos, 100, 20);
        compare.PrintStatistics();
    }
}

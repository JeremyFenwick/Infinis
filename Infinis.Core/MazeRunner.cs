using Infinis.Interfaces;
using Infinis.Scaffolding;

namespace Infinis;

public class MazeRunner : IFormattable
{
    public Grid Maze { get; }
    private String? _output;

    public MazeRunner(Grid grid, IMazeAlgorithm mazeAlgorithm)
    {
        // We clone the grid to not change the original
        Maze = mazeAlgorithm.Traverse(grid.Clone());
        _output = grid.ToString();
    }

    public string ToString(string? format, IFormatProvider? formatProvider)
    {
        return _output ?? "";
    }
}
using Infinis.Interfaces;
using Infinis.Scaffolding;

namespace Infinis;

public class MazeRunner : IFormattable
{
    public Grid Maze { get; }
    private String? _output;

    public MazeRunner(Grid grid, IAlgorithm algorithm)
    {
        Maze = algorithm.Traverse(grid);
        _output = grid.ToString();
    }

    public string ToString(string? format, IFormatProvider? formatProvider)
    {
        return _output ?? "";
    }
}
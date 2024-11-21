using System.Drawing;
using System.Drawing.Imaging;
using Infinis.Interfaces;
using Infinis.Scaffolding;

namespace Infinis;

public class MazeRunner : IFormattable
{
    public Grid Maze { get; }

    public MazeRunner(Grid grid, IMazeGen mazeGen)
    {
        // We clone the grid to not change the original
        Maze = mazeGen.Traverse(grid.Clone());
    }

    public override string ToString()
    {
        return Maze.ToString();

    }

    public string ToString(string? format, IFormatProvider? formatProvider)
    {
        return ToString();
    }

    public void CreateImage(int cellSize, string directory, Color background, Color walls, float wallWidth = 1f, ImageFormat? format = null)
    {
        format ??= ImageFormat.Jpeg;
        var bitMap = Maze.ToBitmap(cellSize, background, walls, wallWidth);
        bitMap.Save(directory, format);
    }
}
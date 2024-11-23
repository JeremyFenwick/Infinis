using System.Drawing;
using System.Drawing.Imaging;
using Infinis.Algorithms;

namespace Infinis.Scaffolding;

public class ColourMaze : Maze
{
    public Distances Distances { get; private set; }
    public Cell Farthest { get; private set; }
    public int MaxDistance { get; private set; }
    public CellColour CellColour { get; set; }
    
    public ColourMaze(int rows, int columns, MazeGenAlgorithms algo) : base(rows, columns, algo)
    {
        // By default, distances is in the bottom left
        Distances = this[Rows() - 1, 0]!.Distances();
        CellColour = CellColour.Green;
        SetDistances(Distances);
    }

    private void SetDistances(Distances distances)
    {
        Distances = distances;
        var max = Distances.FarthestCell();
        Farthest = max.Key;
        MaxDistance = max.Value;
    }

    public Color CellColor(Cell cell)
    {
        var intensity = (MaxDistance - Distances![cell]) / (float)MaxDistance;
        var dark = (int)(255 * intensity);
        var bright = (int)(128 + (127 * intensity));
        switch (CellColour)
        {
            case CellColour.Red:
                return Color.FromArgb(bright, dark, dark);
            case CellColour.Green:
                return Color.FromArgb(dark, bright, dark);
            case CellColour.Blue:
                return Color.FromArgb(dark, dark, bright);
            default:
                throw new ArgumentOutOfRangeException();
        }
    } 
    
    protected override Bitmap ToBitmap(int cellSize, Color background, Color walls, float wallWidth)
    {
        var width = cellSize * Cols();
        var height = cellSize * Rows();

        var brush = new SolidBrush(background);
        var pen = new Pen(walls, wallWidth);
        
        var bitmap = new Bitmap(width + (cellSize), height + (cellSize));
        using var graphics = Graphics.FromImage(bitmap);
        graphics.FillRectangle(brush, 0, 0, width + (cellSize), height + (cellSize));

        // Draw the cell colours 
        foreach (var cell in this)
        {
            var (row, col) = cell.Location();
            var x1 = col * cellSize + (cellSize / 2);
            var y1 = row * cellSize + (cellSize / 2);
            var colour = CellColor(cell);
            var cellBrush = new SolidBrush((Color)colour);
            graphics.FillRectangle(cellBrush, x1, y1, cellSize, cellSize);
        } 
        

        // Draw the walls over the cell colours
        foreach (var cell in this)
        {
            var (row, col) = cell.Location();
            var x1 = col * cellSize + (cellSize / 2);
            var x2 = (col + 1) * cellSize + (cellSize / 2);
            var y1 = row * cellSize + (cellSize / 2);
            var y2 = (row + 1) * cellSize + (cellSize / 2);
            if (cell.North == null) graphics.DrawLine(pen, x1, y1, x2, y1);
            if (cell.West == null) graphics.DrawLine(pen, x1, y1, x1, y2);
            if (!cell.IsLinked(cell.East)) graphics.DrawLine(pen, x2, y1, x2, y2);
            if (!cell.IsLinked(cell.South)) graphics.DrawLine(pen, x1, y2, x2, y2);
        }
        return bitmap;
    }
}
using System.Drawing;
using Infinis.Algorithms;

namespace Infinis.Scaffolding;

public class SolvedMaze : Maze
{
    public Distances Distances { get; set; }

    public SolvedMaze(int rows, int columns, MazeGenAlgorithms algo) : base(rows, columns, algo)
    {
        Distances = this[Rows() - 1, 0]!.Distances();
        Distances = Distances.PathTo(this[0, Cols() - 1]);
    }
    
    public void DistanceTo(int row, int column)
    {
        Distances = Distances.PathTo(this[row, column]);
    }
    
    private Color? CellColor(Cell cell)
    {
        if (Distances.Contains(cell))
        {
            return Color.Goldenrod;
        }
        return null;
    } 
    
    protected override Bitmap ToBitmap(int cellSize, Color background, Color walls, float wallWidth)
    {
        var width = cellSize * Cols();
        var height = cellSize * Rows();

        var whiteBrush = new SolidBrush(Color.WhiteSmoke);
        var brush = new SolidBrush(background);
        var pen = new Pen(walls, wallWidth);
        
        var bitmap = new Bitmap(width + (cellSize), height + (cellSize));
        using var graphics = Graphics.FromImage(bitmap);
        graphics.FillRectangle(whiteBrush, 0, 0, width + (cellSize), height + (cellSize));
        graphics.FillRectangle(brush, (cellSize/2), (cellSize/2), width, height);
        // Draw the cell colours 
        foreach (var cell in this)
        {
            var (row, col) = cell.Location();
            var x1 = col * cellSize + (cellSize / 2);
            var y1 = row * cellSize + (cellSize / 2);
            var colour = CellColor(cell);
            if (colour != null)
            {
                var cellBrush = new SolidBrush((Color)colour);
                graphics.FillRectangle(cellBrush, x1, y1, cellSize, cellSize);
            }
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
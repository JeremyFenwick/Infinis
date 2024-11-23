using System.Collections;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using Infinis.Algorithms;

namespace Infinis.Scaffolding;

public class Maze : IEnumerable<Cell>, IFormattable
{
    private Cell[,] Cells { get; }
    public MazeGenAlgorithms Algorithm { get; }

    public Maze(int rows, int columns, MazeGenAlgorithms algo)
    {
        Cells = new Cell[rows, columns];
        GenerateCells();
        ConfigureCells();
        Algorithm = algo;
        GenerateMaze();
    }

    private Maze(Cell[,] cells)
    {
        Cells = cells;
    }

    private void GenerateCells()
    {
        for (int row = 0; row < Cells.GetLength(0); row++)
        {
            for (int col = 0; col < Cells.GetLength(1); col++)
            {
                Cells[row, col] = new Cell(row, col);
            }
        }
    }
    
    private void ConfigureCells()
    {
        for (int row = 0; row < Cells.GetLength(0); row++)
        {
            for (int col = 0; col < Cells.GetLength(1); col++)
            {
                var cell = Cells[row, col];
                cell.North = ValidLocation(row - 1, col) ? Cells[row - 1, col] : null;
                cell.South = ValidLocation(row + 1, col) ? Cells[row + 1, col] : null;
                cell.East = ValidLocation(row, col + 1) ? Cells[row, col + 1] : null;
                cell.West = ValidLocation(row, col - 1) ? Cells[row, col - 1] : null;
            }
        }
    }

    private void GenerateMaze()
    {
        if (Algorithm == MazeGenAlgorithms.BinaryTree)
        {
            MazeGen.BinaryTree(this);
        }
        else if (Algorithm == MazeGenAlgorithms.SideWinder)
        {
            MazeGen.SideWinder(this);
        }
        else
        {
            return;
        }
    }
    
    private bool ValidLocation(int row, int col)
    {
        return row >= 0 && row < Cells.GetLength(0) && col >= 0 && col < Cells.GetLength(1);
    }

    public Cell? this[int row, int col] => ValidLocation(row, col) ? Cells[row, col] : null;

    public Cell RandomCell()
    {
        Random rnd = new Random();
        int row = rnd.Next(Cells.GetLength(0));
        int column = rnd.Next(Cells.GetLength(1)); 

        return Cells[row, column];
    }
    
    public int Size()
    {
        return Cells.Length;
    }
    
    public int Rows() => Cells.GetLength(0);
    
    public int Cols() => Cells.GetLength(1);

    public IEnumerator<Cell> GetEnumerator()
    {
        foreach (var cell in Cells)
        {
            yield return cell;
        }        
    }

    public Cell[] GetColumn(int col)
    {
        return Enumerable.Range(0, Cells.GetLength(0)).Select(i => Cells[i, col]).ToArray();
    }
    
    public Cell[] GetRow(int row)
    {
        return Enumerable.Range(0, Cells.GetLength(1)).Select(i => Cells[row, i]).ToArray();
    }

    // https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/interfaces/explicit-interface-implementation
    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
    
    /// <summary>
    /// Performs a deep copy of the grid.
    /// </summary>
    public Maze Clone()
    {
        var newCells = new Cell[Rows(), Cols()];
        for (int row = 0; row < Cells.GetLength(0); row++)
        {
            for (int col = 0; col < Cells.GetLength(1); col++)
            {
                newCells[row, col] = Cells[row, col];
            }
        }
        return new Maze(newCells);
    }

    public override string ToString()
    {
        const string corner = "+";
        // const string body = "   ";
        
        var sb = new StringBuilder();
        sb.Append('+');
        sb.Append(string.Concat(Enumerable.Repeat("---+", Cols())));
        sb.Append('\n');
        
        for (int row = 0; row < Cells.GetLength(0); row++)
        {
            var top = "|";
            var bottom = "+";
            for (int col = 0; col < Cells.GetLength(1); col++)
            {
                var cell = Cells[row, col];
                // East wall
                var eastBoundary = cell.IsLinked(cell.East) ? " " : "|";
                top += CellContents(cell) + eastBoundary;
                // South wall
                var southBoundary = cell.IsLinked(cell.South) ? "   " : "---";
                bottom += southBoundary + corner;
            }
            sb.Append(top);
            sb.Append('\n');
            sb.Append(bottom);
            sb.Append('\n');
        }
        return sb.ToString();
    }

    public virtual string CellContents(Cell cell)
    {
        return cell.ToString();
    }

    public string ToString(string? format, IFormatProvider? formatProvider)
    {
        return ToString();
    }

    protected virtual Bitmap ToBitmap(int cellSize, Color background, Color walls, float wallWidth)
    {
        var width = cellSize * Cols();
        var height = cellSize * Rows();

        var brush = new SolidBrush(background);
        var pen = new Pen(walls, wallWidth);
        
        var bitmap = new Bitmap(width + (cellSize), height + (cellSize));
        using var graphics = Graphics.FromImage(bitmap);
        graphics.FillRectangle(brush, 0, 0, width + (cellSize), height + (cellSize));

        // Draw the walls 
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
    
    public virtual void CreateImage(int cellSize, string directory, Color background, Color walls, float wallWidth = 1f, ImageFormat? format = null)
    {
        format ??= ImageFormat.Jpeg;
        var bitMap = this.ToBitmap(cellSize, background, walls, wallWidth);
        bitMap.Save(directory, format);
    }
}
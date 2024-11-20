using System.Collections;
using System.Text;

namespace Infinis.Scaffolding;

public class Grid : IEnumerable<Cell>, IFormattable
{
    public Cell[,] Cells { get; }

    public Grid(int rows, int columns)
    {
        Cells = new Cell[rows, columns];
        GenerateCells();
        ConfigureCells();
    }

    private Grid(Cell[,] cells)
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

    private bool ValidLocation(int row, int col)
    {
        return row >= 0 && row < Cells.GetLength(0) && col >= 0 && col < Cells.GetLength(1);
    }

    public Object? this[int row, int col] => ValidLocation(row, col) ? Cells[row, col] : null;

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

    // https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/interfaces/explicit-interface-implementation
    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
    
    /// <summary>
    /// Performs a deep copy of the grid.
    /// </summary>
    public Grid Clone()
    {
        var newCells = new Cell[Rows(), Cols()];
        for (int row = 0; row < Cells.GetLength(0); row++)
        {
            for (int col = 0; col < Cells.GetLength(1); col++)
            {
                newCells[row, col] = Cells[row, col];
            }
        }
        return new Grid(newCells);
    }

    public override string ToString()
    {
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
                const string body = "   ";
                var eastBoundary = cell.IsLinked(cell.East) ? " " : "|";
                top += body + eastBoundary;
                // South wall
                var southBoundary = cell.IsLinked(cell.South) ? "   " : "---";
                const string corner = "+";
                bottom += southBoundary + corner;
            }
            sb.Append(top);
            sb.Append('\n');
            sb.Append(bottom);
            sb.Append('\n');
        }
        return sb.ToString();
    }

    public string ToString(string? format, IFormatProvider? formatProvider)
    {
        return ToString();
    }
}
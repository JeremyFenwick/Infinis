namespace Infinis.Scaffolding;

public class Cell : IFormattable 
{
    private readonly int _row;
    private readonly int _column;
    public Cell? North { get; set; }
    public Cell? East { get; set; }
    public Cell? South { get; set; }
    public Cell? West { get; set; }
    private readonly HashSet<Cell> _links; 

    public Cell(int row, int column)
    {
        (_row, _column) = (row, column);
        _links = new HashSet<Cell>();
        
    }

    public (int, int) Location()
    {
        return (_row, _column);
    }

    public void Link(Cell? cell, bool bidirectional = true)
    {
        if (cell == null) return;
        _links.Add(cell);
        if (bidirectional)  cell.Link(this, false);
    }

    public void UnLink(Cell cell, bool bidirectional = true)
    {
        _links.Remove(cell);
        if (bidirectional)
        {
            cell.UnLink(this, false);
        }
    }

    public IEnumerable<Cell> GetLinks()
    {
        return new List<Cell>();
    }

    public bool IsLinked(Cell? cell)
    {
        if (cell == null) return false;
        return _links.Contains(cell);
    }

    public string ToString(string? format, IFormatProvider? formatProvider)
    {
        return $"Row: {_row}: Col: {_column} Links: {_links.Count}";    
    }
}
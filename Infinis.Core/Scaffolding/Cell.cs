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

    public IList<Cell> GetLinks()
    {
        return this._links.ToList();
    }

    public bool IsLinked(Cell? cell)
    {
        if (cell == null) return false;
        return _links.Contains(cell);
    }

    public override string ToString()
    {
        return "   ";

    }
    
    public string ToString(string? format, IFormatProvider? formatProvider)
    {
        return ToString();
    }

    public Distances Distances()
    {
        var distances = new Distances(this);
        var frontier = new List<Cell> {this};
        while (frontier.Count > 0)
        {
            var newFrontier = new List<Cell>();
            foreach (var cell in frontier)
            {
                foreach (var link in cell.GetLinks())
                {
                    if (distances.Contains(link)) continue;
                    distances[link] = distances[cell] + 1;
                    newFrontier.Add(link);
                }
            }
            frontier = newFrontier;
        }
        return distances;
    }

    public IList<Cell> Neighbours()
    {
        var neighbours = new List<Cell>();
        if (North != null) neighbours.Add(North);
        if (East != null) neighbours.Add(East);
        if (South != null) neighbours.Add(South);
        if (West != null) neighbours.Add(West);
        return neighbours;
    }

    public static Cell Sample(IList<Cell> cells)
    {
        var rnd = new Random();
        var rndIndex = rnd.Next(0, cells.Count);
        return cells[rndIndex];
    }
}

public enum CellColour
{
    Red,
    Green,
    Blue,
    Purple
}
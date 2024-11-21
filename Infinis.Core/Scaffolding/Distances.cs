namespace Infinis.Scaffolding;

public class Distances
{
    private Dictionary<Cell, int> _distances;

    public Distances(Cell root)
    {
        _distances = new Dictionary<Cell, int>();
        _distances[root] = 0;
    }
    
    public int this[Cell cell]
    {
        get => _distances.ContainsKey(cell) ? _distances[cell] : -1;
        set => _distances[cell] = value;
    }

    public bool Contains(Cell cell)
    {
        return _distances.ContainsKey(cell);
    }

    public void Add(Cell cell, int dist)
    {
        _distances.Add(cell, dist);
    }

    public IEnumerable<Cell> GetCells()
    {
        return _distances.Keys;
    }

}
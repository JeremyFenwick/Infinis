using System.Collections;

namespace Infinis.Scaffolding;

public class Distances : IEnumerable<KeyValuePair<Cell,int>>
{
    private Dictionary<Cell, int> _distances;
    private Cell _root;

    public Distances(Cell root)
    {
        _distances = new Dictionary<Cell, int>();
        _distances[root] = 0;
        _root = root;
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

    public Distances PathTo(Cell cell)
    {
        var current = cell;
        var breadCrumbs = new Distances(current);
        breadCrumbs[current] = this[current];

        while (current != _root)
        {
            foreach (var neighbour in current.GetLinks())
            {
                if (this[neighbour] < this[current])
                {
                    breadCrumbs[neighbour] = this[neighbour];
                    current = neighbour;
                }
            }
        }
        return breadCrumbs;
    }

    public IEnumerator<KeyValuePair<Cell,int>> GetEnumerator()
    {
        foreach (var cell in _distances)
        {
            yield return cell;
        }      
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }

    public KeyValuePair<Cell, int> FarthestCell()
    {
        var candidate = new KeyValuePair<Cell, int>(_root, 0);
        foreach (var cell in _distances)
        {
            if (cell.Value > candidate.Value)
            {
                candidate = cell;
            }
        }
        return candidate;
    }
}
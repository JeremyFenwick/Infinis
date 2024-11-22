using Infinis.Algorithms;

namespace Infinis.Scaffolding;

public class DistanceGrid : Grid
{
    public Distances? Distances { get; set; }
    
    public DistanceGrid(int rows, int columns, MazeGenAlgorithms algo) : base(rows, columns, algo)
    {
    }

    public override string CellContents(Cell cell)
    {
        if (Distances != null && Distances.Contains(cell))
        {
            if (Distances[cell] < 10)
            {
                return $" {Distances[cell]} ";
            }
            else if (Distances[cell] < 100)
            {
                return $"{Distances[cell]} ";
            }
            else
            {
                return $"{Distances[cell]}";
            }
        }
        else
        {
            return base.CellContents(cell);
        }
    }
}
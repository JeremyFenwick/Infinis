using Infinis.Algorithms;

namespace Infinis.Scaffolding;

public class DistanceMaze : Maze
{
    public Distances Distances { get; set; }
    
    public DistanceMaze(int rows, int columns, MazeGenAlgorithms algo) : base(rows, columns, algo)
    {
        Distances = this[Rows() - 1, 0]!.Distances();
    }

    public void DistanceTo(int row, int column)
    {
        Distances = Distances.PathTo(this[row, column]);
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
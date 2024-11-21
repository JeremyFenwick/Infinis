namespace Infinis.Scaffolding;

public class DistanceGrid : Grid
{
    public Distances? Distances { get; set; }
    
    public DistanceGrid(int rows, int columns) : base(rows, columns)
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
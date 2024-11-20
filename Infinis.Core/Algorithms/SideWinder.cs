using Infinis.Interfaces;
using Infinis.Scaffolding;

namespace Infinis.Algorithms;

public class SideWinder : IMazeAlgorithm
{
    public Grid Traverse(Grid grid)
    {
        for (int row = 0; row < grid.Rows(); row++)
        {
            var rowData = grid.GetRow(row);
            TraverseRow(rowData);
        }
        return grid;
    }

    public void TraverseRow(Cell[] row)
    {
        var rnd = new Random();
        var run = new List<Cell>();
        foreach (var cell in row)
        {
            run.Add(cell);
            var eastBoundary = (cell.East == null);
            var northBoundary = (cell.North == null);

            var shouldCloseOut = eastBoundary || (!northBoundary && rnd.Next(2) == 0);
            if (shouldCloseOut)
            {
                var index = rnd.Next(run.Count);
                var member = run[index];
                if (member.North != null) member.Link(member.North);
                run.Clear();
            }
            else
            {
                cell.Link(cell.East);
            }
        }
    }
}
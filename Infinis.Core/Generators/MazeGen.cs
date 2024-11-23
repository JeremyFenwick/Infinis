using Infinis.Scaffolding;

namespace Infinis.Algorithms;

public static class MazeGen
{
    public static void BinaryTree(Maze maze)
    {
        var rnd = new Random();
        foreach (var cell in maze)
        {
            Cell? neighbour = null;
            var neighbours = new List<Cell?>();
            // Default configuration navigates North and East
            if (cell.North != null) neighbours.Add(cell.North); 
            if (cell.East != null) neighbours.Add(cell.East);
            // Take a random item from the list 
            if (neighbours.Count > 0)
            {
                var index = rnd.Next(neighbours.Count);
                neighbour = neighbours[index];
            }
            if (neighbour != null) cell.Link(neighbour);
        }
    }

    public static void SideWinder(Maze maze)
    {
        for (int row = 0; row < maze.Rows(); row++)
        {
            var rowData = maze.GetRow(row);
            SideWinderHelper(rowData);
        }
    }

    private static void SideWinderHelper(Cell[] row)
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

    public static void AldousBroder(Maze maze)
    {
        var cell = maze.GetRandomCell();
        var unvisited = maze.Size() - 1;

        while (unvisited > 0)
        {
            var neighbours = cell.Neighbours();
            var neighbour = Cell.Sample(neighbours);

            if (!neighbour.GetLinks().Any())
            {
                cell.Link(neighbour);
                unvisited--;
            }
            cell = neighbour;
        }
    }

    public static void Wilson(Maze maze)
    {
        var unvisited = maze.ToList();
        var first = Cell.Sample(unvisited);
        unvisited.Remove(first);

        while (unvisited.Count > 0)
        {
            var cell = Cell.Sample(unvisited);
            var path = new List<Cell>();
            path.Add(cell);

            while (unvisited.Contains(cell))
            {
                var neighbours = cell.Neighbours();
                cell = Cell.Sample(neighbours);
                var position = path.IndexOf(cell);
                
                if (position >= 0) path = path[..(position + 1)];
                else path.Add(cell);
            }

            for (int i = 0; i < path.Count; i++)
            {
                if (i < path.Count - 1)
                {
                    path[i].Link(path[i + 1]);
                    unvisited.Remove(path[i]);
                }
            }
        }
    }
}
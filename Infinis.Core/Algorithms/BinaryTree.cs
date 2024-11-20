using Infinis.Interfaces;
using Infinis.Scaffolding;

namespace Infinis.Algorithms;

public class BinaryTree : IMazeAlgorithm
{
    public Grid Traverse(Grid grid)
    {
        var rnd = new Random();
        foreach (var cell in grid)
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
        return grid;
    }
}
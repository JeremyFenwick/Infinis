using Infinis.Scaffolding;

namespace Infinis.Interfaces;

public interface IMazeGen
{
    public Grid Traverse(Grid grid);
}
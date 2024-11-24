using Infinis.Algorithms;
using Infinis.Scaffolding;

namespace Infinis.Analysis;

public class DeadEndComparison
{
    private readonly IList<MazeGenAlgorithms> _mazeGenAlgorithms;
    private readonly Dictionary<MazeGenAlgorithms, int> _analysisData;
    private readonly int _tries;
    private readonly int _size;

    public DeadEndComparison(IList<MazeGenAlgorithms> mazeGenAlgorithms, int tries, int size)
    {
        _mazeGenAlgorithms = mazeGenAlgorithms;
        _analysisData = new Dictionary<MazeGenAlgorithms, int>();
        _tries = tries;
        _size = size;
        ComparisonRoutine();
    }

    private void ComparisonRoutine()
    {
        foreach (var algorithm in _mazeGenAlgorithms)
        {
            _analysisData[algorithm] = 0;
            for (int run = 0; run < _tries; run++)
            {
                var maze = new Maze(_size, _size, algorithm);
                _analysisData[algorithm] += maze.DeadEnds().Count;
            }
        }
    }

    public void PrintStatistics()
    {
        foreach (var algorithm in _mazeGenAlgorithms)
        {
            var average = _analysisData[algorithm] / _tries;
            var totalSize = _size * _size;
            var percent = 100 * average / totalSize;
            Console.WriteLine($"{algorithm} > An average of {average} Dead Ends against {totalSize} Total Cells. Percentage: {percent}%");
        }
    }
}
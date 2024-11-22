using System.Drawing;
using System.Drawing.Imaging;

namespace Infinis.Scaffolding;

public class ColourGrid : Grid
{
    public Distances? Distances { get; private set; }
    public Cell Farthest { get; private set; }
    public int MaxDistance { get; private set; }
    
    public ColourGrid(int rows, int columns) : base(rows, columns)
    {
    }

    public void SetDistances(Distances distances)
    {
        Distances = distances;
        var max = Distances.FarthestCell();
        Farthest = max.Key;
        MaxDistance = max.Value;
    }

    public override Color? CellColor(Cell cell)
    {
        var intensity = (MaxDistance - Distances![cell]) / (float)MaxDistance;
        var dark = (int)(255 * intensity);
        var bright = (int)(128 + (127 * intensity));
        return Color.FromArgb(dark, bright, dark);
    } 
    
    public override void CreateImage(int cellSize, string directory, Color background, Color walls, float wallWidth = 1f, ImageFormat? format = null)
    {
        format ??= ImageFormat.Jpeg;
        var bitMap = ToBitmap(cellSize, background, walls, wallWidth, true);
        bitMap.Save(directory, format);
    }
}
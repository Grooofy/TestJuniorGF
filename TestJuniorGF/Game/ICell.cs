using System.Windows.Media.Imaging;

namespace TestJuniorGF
{
    public interface ICell
    {
        CellColor Color { get; set; }
        Vector2 Position { get; set; }
        bool IsNullObject { get; }
        void Destroy(ICell[,] list);
        BitmapImage GetBitmapImage();
    }
}

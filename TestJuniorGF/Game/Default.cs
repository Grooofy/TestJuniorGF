using System.Windows.Media.Imaging;

namespace TestJuniorGF
{
    public class Default : ICell
    {
        public CellColor Color { get; set; }
        public Vector2 Position { get; set; }
        public bool IsNullObject { get; private set; }
       
        private const int PointsForDestroying = 100;
       

        public Default(CellColor _color, Vector2 position)
        {
            Color = _color;
            Position = position;
        }

        public void Destroy(ICell[,] list)
        {
            if (IsNullObject)
                return;
            Game.AddScore(PointsForDestroying);
            IsNullObject = true;
        }

        public BitmapImage GetBitmapImage()
        {
            return TypeBitmapImage.GetImage(Color);
        }
    }
}

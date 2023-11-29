using System;
using System.Windows.Media.Imaging;

namespace TestJuniorGF
{
    public class HorizontalLine : ICell
    {
        private const string FileName = "LineH";
        private const int PointsForDestroying = 100;
        public CellColor Color { get; set; }
        public Vector2 Position { get; set; }
        public bool IsNullObject { get; private set; }
        
        public HorizontalLine(ICell figure)
        {
            Position = figure.Position;
            Color = figure.Color;
        }

        public void Destroy(ICell[,] list)
        {
            if (IsNullObject)
                return;
            Game.AddScore(PointsForDestroying);
            IsNullObject = true;
            ActivateBonus(list);
        }
        
        private void ActivateBonus(ICell[,] list)
        {
            for (int i = 0; i < GameWindow.GridSize; i++)
            {
                list[i, Position.Y].Destroy(list);
            }
        }

        public BitmapImage GetBitmapImage()
        {
            return TypeBitmapImage.GetImage(Color, FileName);
        }
    }
}

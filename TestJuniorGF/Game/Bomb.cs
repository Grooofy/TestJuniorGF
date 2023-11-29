using System;
using System.Windows.Media.Imaging;

namespace TestJuniorGF
{
    public class Bomb : ICell
    {
        private const string FileName = "Bomb";
        private const int PointsForDestroying = 100;
        public CellColor Color { get; set; }
        public Vector2 Position { get; set; }
        public bool IsNullObject { get; private set; }
        
        public Bomb(ICell cell)
        {
            Position = cell.Position;
            Color = cell.Color;
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
            if (Position.Y > 0)
            {
                list[Position.X, Position.Y - 1].Destroy(list);
                
                if (Position.X > 0)
                    list[Position.X - 1, Position.Y - 1].Destroy(list);
                
                if (Position.X < GameWindow.GridSize - 1)
                    list[Position.X + 1, Position.Y - 1].Destroy(list);
                
            }
            if (Position.Y < GameWindow.GridSize - 1)
            {
                list[Position.X, Position.Y + 1].Destroy(list);
                
                if (Position.X > 0)
                    list[Position.X - 1, Position.Y + 1].Destroy(list);
                
                if (Position.X < GameWindow.GridSize - 1)
                    list[Position.X + 1, Position.Y + 1].Destroy(list);
                
            }
            if (Position.X > 0)
                list[Position.X - 1, Position.Y].Destroy(list);
            
            if (Position.X < GameWindow.GridSize - 1)
                list[Position.X + 1, Position.Y].Destroy(list);
        }

        public BitmapImage GetBitmapImage()
        {
           return TypeBitmapImage.GetImage(Color, FileName);
        }
    }
}

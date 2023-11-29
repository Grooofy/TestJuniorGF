using System;
using System.Windows.Media.Imaging;

namespace TestJuniorGF
{
    public static class TypeBitmapImage
    {
        private const string Path = @"pack://application:,,,/Sprites/";
        private const string FormatFile = @".png";
        
        

        public static BitmapImage GetImage(CellColor color)
        {
            Uri uriSource;
            switch (color)
            {
                case CellColor.Red:
                    uriSource = new Uri(Path + CellColor.Red + FormatFile);
                    return new BitmapImage(uriSource);
                case CellColor.Blue:
                    uriSource = new Uri(Path + CellColor.Blue + FormatFile);
                    return new BitmapImage(uriSource);
                case CellColor.Green:
                    uriSource = new Uri(Path + CellColor.Green + FormatFile);
                    return new BitmapImage(uriSource);
                case CellColor.Yellow:
                    uriSource = new Uri(Path + CellColor.Yellow + FormatFile);
                    return new BitmapImage(uriSource);
                case CellColor.Pink:
                    uriSource = new Uri(Path + CellColor.Pink + FormatFile);
                    return new BitmapImage(uriSource);
                default:
                    return null;
            }
        }

        public static BitmapImage GetImage(CellColor color, string nameFile)
        {
            Uri uriSource;
            switch (color)
            {
                case CellColor.Red:
                    uriSource = new Uri(Path + CellColor.Red + nameFile + FormatFile);
                    return new BitmapImage(uriSource);
                case CellColor.Blue:
                    uriSource = new Uri(Path + CellColor.Blue + nameFile + FormatFile);
                    return new BitmapImage(uriSource);
                case CellColor.Green:
                    uriSource = new Uri(Path + CellColor.Green + nameFile + FormatFile);
                    return new BitmapImage(uriSource);
                case CellColor.Yellow:
                    uriSource = new Uri(Path + CellColor.Yellow + nameFile + FormatFile);
                    return new BitmapImage(uriSource);
                case CellColor.Pink:
                    uriSource = new Uri(Path + CellColor.Pink + nameFile + FormatFile);
                    return new BitmapImage(uriSource);
                default:
                    return null;
            }
        }
    }
}

using OpenTK;
using System;
using System.Drawing;

namespace Task8
{
    class Canvas
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        public uint[,] Data { get; private set; }

        public Canvas(int width, int height)
        {
            Width = width;
            Height = height;
            Data = new uint[Width, Height];
        }

        public void PutPixel(int x, int y, Vector3 color)
        {
            Data[x, y] = PackRGBFromColor(color);
        }

        private uint PackRGBFromColor(Vector3 color)
        {
            var r = (byte)(color.X * 255);
            var g = (byte)(color.Y * 255);
            var b = (byte)(color.Z * 255);
            return (uint)(r << 24 | g << 16 | b << 8 | 0);
        }

        public Image GetImage()
        {
            var bitmap = new Bitmap(Width, Height);
            for (var x = 0; x < Width; x++)
            {
                for (var y = 0; y < Height; y++)
                {
                    var r = (Data[x, y] & 0xFF000000) >> 24;
                    var g = (Data[x, y] & 0x00FF0000) >> 16;
                    var b = (Data[x, y] & 0x0000FF00) >> 8;
                    var color = Color.FromArgb((int)(r), (int)(g), (int)(b));
                    bitmap.SetPixel(x, y, color);
                }
            }
            return bitmap;
        }
    }
}

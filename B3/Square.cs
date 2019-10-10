using System.Drawing;

namespace B3
{
    class Square
    {
        #region Propertion
        public Color Color { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        #endregion
        #region Processing function
        public Square(int _x,int _y,int _w, Color _c)
        {
            X = _x;
            Y = _y;
            Width = Height = _w;
            Color = _c;
        }

        public Rectangle Rectangle()
        {
            return new Rectangle(X, Y, Width, Height);
        }

        public void Location(int _x,int _y)
        {
            X = _x;
            Y = _y;
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SetServer
{
    public class CardDisplay
    {
        public int rightOffset;
        public int leftOffset;
        public static int shapeWidth = 0;
        public static int shapeHeight = 0;
        public int x = 25;
        public int y = 15;
        public static int thick = 2;

        public void drawCard (Graphics g, Card card)
        {
            int colour = card.Colour;
            int symbol = card.Symbol;
            int number = card.Number;
            int shading = card.Shading;

            if (card.Number == 0)
            {
                Shape shape = new Shape(colour, symbol, shading, x, y, g);
            }
            else if (card.Number == 1)
            {
                rightOffset = 0;
                leftOffset = 0;
                Shape shape = new Shape(colour, symbol, shading, x + rightOffset, y, g);
                Shape shape2 = new Shape(colour, symbol, shading, x - leftOffset, y, g);
            }
            else
            {
                rightOffset = 0;
                leftOffset = 0;
                Shape shape = new Shape(colour, symbol, shading, x + rightOffset, y, g);
                Shape shape2 = new Shape(colour, symbol, shading, x - leftOffset, y, g);
                Shape shape3 = new Shape(colour, symbol, shading, x - leftOffset, y, g);
            }
        }

        public class Shape
        {
            //private int _colour;
            //private int _symbol;
            //private int _shading;
            //private int _x;
            //private int _y;

            public Shape(int colour, int symbol, int shading, int x, int y, Graphics g)
            {

                Color penCol;
                switch (colour)
                {
                    case 0:
                        penCol = Color.Red;
                        break;
                    case 1:
                        penCol = Color.Green;
                        break;
                    case 2:
                        penCol = Color.Purple;
                        break;
                    default:
                        penCol = Color.Black;
                        break;
                }
                Pen p = new Pen(penCol, thick);


                if (symbol == 0)
                {
                    g.DrawRectangle(p, x, y, shapeWidth, shapeHeight);
                }
                else if (symbol == 1)
                {
                    g.DrawEllipse(p, x, y, shapeWidth, shapeHeight);
                }
                else if (symbol == 2)
                {
                    Point p1 = new Point(x, y);
                    Point p2 = new Point(x, y + shapeHeight);
                    Point p3 = new Point(x + shapeWidth, y + shapeHeight / 2);

                    g.DrawLine(p, p1, p2);
                    g.DrawLine(p, p1, p3);
                    g.DrawLine(p, p2, p3);
                }
            }

           
        }
    } 
}

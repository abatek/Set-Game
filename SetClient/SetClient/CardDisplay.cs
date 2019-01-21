using SetClient;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SetClient
{
    public class CardDisplay
    {
        public int rightOffset;
        public int leftOffset;
        public const int pbWidth = 119;
        public const int pbHeight = 71;
        public static int shapeWidth = 25;
        public static int shapeHeight = pbHeight - 20;
        public int x = pbWidth / 2 - shapeWidth / 2;
        public int y = 10;
        public static int thick = 2;

        public void drawCard(Card card, Graphics g)
        {

            int colour = card.Colour;
            int symbol = card.Symbol;
            int number = card.Number;
            int shading = card.Shading;

            g.Clear(Color.White);
            if (card.Number == 0)
            {
                Shape shape = new Shape(colour, symbol, shading, x, y, g);
            }
            else if (card.Number == 1)
            {
                rightOffset = 20;
                leftOffset = 20;
                Shape shape1 = new Shape(colour, symbol, shading, x + rightOffset, y, g);
                Shape shape2 = new Shape(colour, symbol, shading, x - leftOffset, y, g);
            }
            else
            {
                rightOffset = 35;
                leftOffset = 35;
                Shape shape1 = new Shape(colour, symbol, shading, x, y, g);
                Shape shape2 = new Shape(colour, symbol, shading, x + rightOffset, y, g);
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
                if (shading == 0) //solid
                {
                    SolidBrush p = new SolidBrush(penCol);

                    if (symbol == 0)//Rectangle
                    {
                        g.FillRectangle(p, x, y, shapeWidth, shapeHeight);
                    }
                    else if (symbol == 1)//Oval
                    {
                        g.FillEllipse(p, x, y, shapeWidth, shapeHeight);
                    }
                    else if (symbol == 2)//Triangle
                    {
                        Point[] trianglePoints = new Point[3];
                        trianglePoints[0] = new Point(x, y);
                        trianglePoints[1] = new Point(x, y + shapeHeight);
                        trianglePoints[2] = new Point(x + shapeWidth, y + shapeHeight / 2);

                        g.FillPolygon(p, trianglePoints);
                    }
                }
                else if (shading == 1) //open
                {
                    Pen p = new Pen(penCol, thick);

                    if (symbol == 0)//Rectangle
                    {
                        g.DrawRectangle(p, x, y, shapeWidth, shapeHeight);
                    }
                    else if (symbol == 1)//Oval
                    {
                        g.DrawEllipse(p, x, y, shapeWidth, shapeHeight);
                    }
                    else if (symbol == 2)//Triangle
                    {
                        Point[] trianglePoints = new Point[3];
                        trianglePoints[0] = new Point(x, y);
                        trianglePoints[1] = new Point(x, y + shapeHeight);
                        trianglePoints[2] = new Point(x + shapeWidth, y + shapeHeight / 2);

                        g.DrawPolygon(p, trianglePoints);
                    }
                }
                else //transparent
                {
                    Pen p = new Pen(penCol, thick);

                    int red = 0, green = 0, blue = 0;

                    if (colour == 0)
                    {
                        red = 255;
                    }
                    else if (colour == 1)
                    {
                        green = 255;
                    }
                    else
                    {
                        red = 255;
                        blue = 255;
                    }

                    SolidBrush p2 = new SolidBrush(Color.FromArgb(50, red, green, blue));

                    if (symbol == 0)//Rectangle
                    {
                        g.DrawRectangle(p, x, y, shapeWidth, shapeHeight);
                        g.FillRectangle(p2, x, y, shapeWidth, shapeHeight);
                    }
                    else if (symbol == 1)//Oval
                    {
                        g.DrawEllipse(p, x, y, shapeWidth, shapeHeight);
                        g.FillEllipse(p2, x, y, shapeWidth, shapeHeight);
                    }
                    else if (symbol == 2)//Triangle
                    {
                        Point[] trianglePoints = new Point[3];
                        trianglePoints[0] = new Point(x, y);
                        trianglePoints[1] = new Point(x, y + shapeHeight);
                        trianglePoints[2] = new Point(x + shapeWidth, y + shapeHeight / 2);

                        g.DrawPolygon(p, trianglePoints);
                        g.FillPolygon(p2, trianglePoints);
                    }
                }

            }


        }
    }
}


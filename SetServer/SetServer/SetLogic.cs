using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SetServer
{
    class SetLogic
    {
        public void runLogic()
        {
            //a.setFeatures(0, 0, 2, 0);
            //b.setFeatures(1, 2, 1, 0);
            //c.setFeatures(2, 1, 0, 0);

            //Console.WriteLine(isSet(a, b, c));


        }

        public static bool isSet (Card a, Card b, Card c)
        {
            for (int i = 0; i < 4; i++)
            {
                if (a.getFeatures()[i] == b.getFeatures()[i])
                {
                    if (a.getFeatures()[i] != c.getFeatures()[i])
                    {
                        return false;
                    }
                }
                else if (a.getFeatures()[i] != b.getFeatures()[i])
                {
                    if (a.getFeatures()[i] != c.getFeatures()[i])
                    {
                        if (b.getFeatures()[i] == c.getFeatures()[i])
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            return true;
        }
    }
    public class Card
    {
        private int colour; // 0=red, 1=green, 2=purple
        private int symbol; // 0=oval, 1=squiggly, 3=diamond
        private int number; // 0=1, 1=2, 2=3
        private int shading; // 0=solid, 1=open, 2=striped

        public void setFeatures (int colour, int symbol, int number, int shading)
        {
            this.colour = colour;
            this.symbol = symbol;
            this.number = number;
            this.shading = shading;
        }

        public int[] getFeatures()
        {
            int[] features = { colour, symbol, number, shading };
            foreach (int i in features)
            {
                //Console.Write(i);
            }
            //Console.WriteLine();
                return features;
        }

        public void showFeatures()
        {
            string[] features = new string[4];
            switch (this.colour)
            {
                case 0:
                    features[0] = "red";
                    break;
                case 1:
                    features[0] = "green";
                    break;
                case 2:
                    features[0] = "purple";
                    break;
                default:
                    features[0] = "error";
                    break;
            }

            switch (this.symbol)
            {
                case 0:
                    features[1] = "oval";
                    break;
                case 1:
                    features[1] = "squiggly";
                    break;
                case 2:
                    features[1] = "diamond";
                    break;
                default:
                    features[1] = "error";
                    break;
            }
            switch (this.number)
            {
                case 0:
                    features[2] = "1";
                    break;
                case 1:
                    features[2] = "2";
                    break;
                case 2:
                    features[2] = "3";
                    break;
                default:
                    features[2] = "error";
                    break;
            }
            switch (this.shading)
            {
                case 0:
                    features[3] = "solid";
                    break;
                case 1:
                    features[3] = "open";
                    break;
                case 2:
                    features[3] = "striped";
                    break;
                default:
                    features[3] = "error";
                    break;
            }

            Console.WriteLine("Colour: {0}, Symbol: {1}, Number {2}, Shading {3}", features[0], features[1], features[2], features[3]);
            
        }
    }
}



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SetClient
{
    class SetLogic
    {
        public List<Card> convertToCards(string str)
        {
            List<Card> ret = new List<Card>();
            string[] rawCards = str.Split(',');
            foreach (string s in rawCards)
            {
                int[] feats = new int[4];

                for (int i = 0; i < 4; i++)
                {
                    feats[i] = Convert.ToInt32(s.Split(' ')[i]);
                }

                ret.Add(new Card(feats[0], feats[1], feats[2], feats[3]));
            }
            return ret;
        }
    }

    public class Card
    {
        public int Colour { get; set; } // 0=red, 1=green, 2=purple
        public int Symbol { get; set; } // 0=rectangle, 1=oval, 3=triangle
        public int Number { get; set; } // 0=1, 1=2, 2=3
        public int Shading { get; set; } // 0=solid, 1=open, 2=transparent

        public Card(int colour, int symbol, int number, int shading)
        {
            Colour = colour;
            Symbol = symbol;
            Number = number;
            Shading = shading;
        }

        public int[] getFeatures()
        {
            int[] features = { Colour, Symbol, Number, Shading };
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
            switch (Colour)
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

            switch (Symbol)
            {
                case 0:
                    features[1] = "rectangle";
                    break;
                case 1:
                    features[1] = "oval";
                    break;
                case 2:
                    features[1] = "triangle";
                    break;
                default:
                    features[1] = "error";
                    break;
            }
            switch (Number)
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
            switch (Shading)
            {
                case 0:
                    features[3] = "solid";
                    break;
                case 1:
                    features[3] = "open";
                    break;
                case 2:
                    features[3] = "transparent";
                    break;
                default:
                    features[3] = "error";
                    break;
            }

            Console.WriteLine("Colour: {0}, Symbol: {1}, Number: {2}, Shading: {3}", features[0], features[1], features[2], features[3]);
        }
        public string toString()
        {
            return Convert.ToString(Colour + " " + Symbol + " " + Number + " " + Shading);
        }
    }
}

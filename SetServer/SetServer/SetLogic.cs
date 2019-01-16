﻿using System;
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

        public List<Card> convertToCards (string str)
        {
            List <Card> ret = new List<Card>();
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

        public bool isSet (Card a, Card b, Card c)
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
        private int _colour; // 0=red, 1=green, 2=purple
        private int _symbol; // 0=oval, 1=squiggly, 3=diamond
        private int _number; // 0=1, 1=2, 2=3
        private int _shading; // 0=solid, 1=open, 2=striped

        public Card (int colour, int symbol, int number, int shading)
        {
            _colour = colour;
            _symbol = symbol;
            _number = number;
            _shading = shading;
        }

        public void setFeatures (int colour, int symbol, int number, int shading)
        {
            _colour = colour;
            _symbol = symbol;
            _number = number;
            _shading = shading;
        }

        public int[] getFeatures()
        {
            int[] features = { _colour, _symbol, _number, _shading };
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
            switch (_colour)
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

            switch (_symbol)
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
            switch (_number)
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
            switch (_shading)
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

            Console.WriteLine("Colour: {0}, Symbol: {1}, Number: {2}, Shading: {3}", features[0], features[1], features[2], features[3]);
        }
        public string toString()
        {
            return Convert.ToString(_colour + " " + _symbol + " " + _number + " " + _shading);
        }
    }
}



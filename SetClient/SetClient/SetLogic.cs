using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SetClient
{
    class SetLogic
    {
        
    }

    public class Card
    {
        private int _colour; // 0=red, 1=green, 2=purple
        private int _symbol; // 0=oval, 1=squiggly, 3=diamond
        private int _number; // 0=1, 1=2, 2=3
        private int _shading; // 0=solid, 1=open, 2=striped

        public Card(int colour, int symbol, int number, int shading)
        {
            _colour = colour;
            _symbol = symbol;
            _number = number;
            _shading = shading;
        }

        public void setFeatures(int colour, int symbol, int number, int shading)
        {
            _colour = colour;
            _symbol = symbol;
            _number = number;
            _shading = shading;
        }

        public int[] getFeatures()
        {
            int[] features = { _colour, _symbol, _number, _shading };
            /*foreach (int i in features)
            {
                Console.Write(i);
            }
            Console.WriteLine();*/
            return features;
        }

        public string toString()
        {
            return Convert.ToString(_colour + " " + _symbol + " " + _number + " " + _shading);
        }
    }
}

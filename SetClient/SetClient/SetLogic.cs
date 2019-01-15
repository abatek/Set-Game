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
        private int colour; // 0=red, 1=green, 2=purple
        private int symbol; // 0=oval, 1=squiggly, 3=diamond
        private int number; // 0=1, 1=2, 2=3
        private int shading; // 0=solid, 1=open, 2=striped

        public void setFeatures(int colour, int symbol, int number, int shading)
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

        public string toString()
        {
            string str = Convert.ToString(this.colour + " " + this.symbol + " " + this.number + " " + this.shading + "/n");
            return str;
        }
    }
}

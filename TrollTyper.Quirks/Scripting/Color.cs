using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrollTyper.Quirks.Scripting
{
    public struct Color
    {
        public static readonly Color Fuchsia =     new Color(153, 0, 77);
        public static readonly Color Violet =      new Color(106, 0, 106);
        public static readonly Color Purple =      new Color(68, 10, 127);
        public static readonly Color Indigo =      new Color(0, 33, 203);
        public static readonly Color Cerculean =   new Color(0, 65, 130);
        public static readonly Color Teal =        new Color(0, 130, 130);
        public static readonly Color Jade =        new Color(7, 136, 70);
        public static readonly Color Olive =       new Color(65, 102, 0);
        public static readonly Color Lime =        new Color(101, 130, 0);
        public static readonly Color CandyRed =    new Color(255, 0, 0);
        public static readonly Color Yellow =      new Color(161, 161, 0);
        public static readonly Color Bronze =      new Color(162, 82, 3);
        public static readonly Color Burgundy =    new Color(161, 0, 0);

        public static readonly Color Black = new Color(0, 0, 0);
        public static readonly Color White = new Color(255, 255, 255);

        public int R { get; set; }
        public int G { get; set; }
        public int B { get; set; }


        public Color(int R, int G, int B)
        {
            this.R = R;
            this.G = G;
            this.B = B;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrollTyper.TrollQuirks;
using System.Windows.Forms;
using NLua;

namespace TrollTyper
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            TrollTyper t = new TrollTyper(args);
            if (!t.Run())
            {
                Console.Write("\nPress any key to exit...");
                Console.ReadKey();
            }
        }
    }
}

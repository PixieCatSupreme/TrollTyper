using System;

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

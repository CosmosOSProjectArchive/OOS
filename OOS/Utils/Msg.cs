using System;

namespace OOS.Utils
{
    public static class Msg
    {
        public static void ErrorMsg(string msg)
        {
            Console.Write("[ ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("FAIL");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($" ] {msg}");
            Console.WriteLine("");
        }

        public static void OKMsg(string msg)
        {
            Console.WriteLine("");
            Console.Write("[ ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("OK");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($" ] {msg}");
            Console.WriteLine("");
            Console.WriteLine("");
        }
    }
}

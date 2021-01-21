using System;
using Sys = Cosmos.System;

namespace OOS
{
    public class Crash
    {
        public static void StopKernel(Exception ex)
        {
            Kernel.running = false;

            Console.BackgroundColor = ConsoleColor.Red;
            Console.Clear();

            string ex_message = ex.Message;
            string inner_message = "";

            Cosmos.System.PCSpeaker.Beep((uint)Cosmos.System.Notes.GS6, 250);

            Console.WriteLine("An error occured in OOS:");
            Console.WriteLine(ex);
            if (ex.InnerException != null)
            {
                inner_message = ex.InnerException.Message;
                Console.WriteLine(inner_message);
            }
            Console.WriteLine("OOS Version: " + Kernel.version);
            Console.WriteLine("OOS Revision: " + Kernel.revision);
            Console.WriteLine();
            Console.WriteLine("If this is the first time you've seen this error screen, press any key to restart your computer. If this screen appears again, follow these steps:");
            Console.WriteLine();
            Console.WriteLine("Try to reinstall OOS on your computer or Virtual Machine. You can also try to reset the filesystem with a blank .vmdk file if you're on a Virtual Machine and if not by formatting your device.");
            Console.WriteLine();
            Console.WriteLine(@"If problems continue, you can contact us at contact@multiplex.rs");
            Console.WriteLine();
            Console.WriteLine("Press any key to reboot...");

            Console.ReadKey();

            Sys.Power.Reboot();
        }

        public static void StopKernel(string exception, string description, string lastknowaddress, string ctxinterrupt)
        {

            Kernel.running = false;

            Console.BackgroundColor = ConsoleColor.Red;
            Console.Clear();

            Console.WriteLine("CPU Exception x" + ctxinterrupt + " occured in OOS:");
            Console.WriteLine("Exception: " + exception);
            Console.WriteLine("Description: " + description);
            Console.WriteLine("OOS Version: " + Kernel.version);
            Console.WriteLine("OOS Revision: " + Kernel.revision);
            if (lastknowaddress != "")
            {
                Console.WriteLine("Last known address: 0x" + lastknowaddress);
            }
            Console.WriteLine();
            Console.WriteLine("If this is the first time you've seen this error screen, press any key to restart your computer. If this screen appears again, follow these steps:");
            Console.WriteLine();
            Console.WriteLine("Try to reinstall OOS on your computer or Virtual Machine. You can also try to reset the filesystem with a blank .vmdk file if you're on a Virtual Machine and if not by formatting your device.");
            Console.WriteLine();
            Console.WriteLine("If problems continue, you can contact us at contact@multiplex.rs");
            Console.WriteLine();
            Console.WriteLine("Press any key to reboot...");

            Console.ReadKey();

            Sys.Power.Reboot();
        }
    }
}

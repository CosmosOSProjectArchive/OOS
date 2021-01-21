using System;
using System.IO;
using Sys = Cosmos.System;

namespace OOS
{
    public class Kernel : Sys.Kernel
    {
        public static bool running = false;
        public static string current_directory = "0:\\";
        public static string version = "0.4.0";
        public static string revision = "21";
        public static string boottime = Utils.Time.MonthString() + "/" + Utils.Time.DayString() + "/" + Utils.Time.YearString() + ", " + Utils.Time.TimeString(true, true, true);
        public static string file;

        protected override void BeforeRun()
        {
            running = true;
            Cosmos.System.PCSpeaker.Beep((uint)Cosmos.System.Notes.E5, 200);

            FS = new Sys.FileSystem.CosmosVFS();
            Sys.FileSystem.VFS.VFSManager.RegisterVFS(FS);
            FS.Initialize();

            Utils.Msg.OKMsg("OOS(Ostoja's Operating System) booted sucessfully.");
        }

        public Cosmos.System.FileSystem.CosmosVFS FS = null;
        protected override void Run()
        {
            try
            {
                while (running)
                {
                    Console.Write(current_directory + "> ");
                    string input = Console.ReadLine();
                    InterpretCMD(input);
                }
            }
            catch (Exception ex)
            {
                running = false;
                Crash.StopKernel(ex);
            }
        }

        public void InterpretCMD(string input)
        {
            string[] args = input.Split(' ');
            if (args[0] == "shutdown")
            {
                Console.Clear();
                running = false;
                Sys.Power.Shutdown();
            }
            else if (args[0] == "reboot")
            {
                Console.Clear();
                running = false;
                Sys.Power.Reboot();
            }
            else if (args[0] ==  "hex")
            {
                string file = args[1];
                if (File.Exists(current_directory + file))
                {
                    Console.WriteLine("Offset(h)  00 01 02 03 04 05 06 07  08 09 0A 0B 0C 0D 0E 0F");
                    Console.WriteLine();
                    Console.WriteLine(Utils.Conversion.HexDump(File.ReadAllBytes(current_directory + file)));
                }
                else
                {
                    Utils.Msg.ErrorMsg("File doesn't exist.");
                }
            }
            else if (args[0] == "time" || args[0] == "date")
            {
                Console.WriteLine("The current time is:  " + Utils.Time.MonthString() + "/" + Utils.Time.DayString() + "/" + Utils.Time.YearString() + ", " + Utils.Time.TimeString(true, true, true));
            }
            else if (args[0] == "sysinfo")
            {
                Console.WriteLine("Operating system name:     OOS");
                Console.WriteLine("Operating system version:  " + version);
                Console.WriteLine("Operating system revision: " + revision);
                Console.WriteLine("Date and time:             " + Utils.Time.MonthString() + "/" + Utils.Time.DayString() + "/" + Utils.Time.YearString() + ", " + Utils.Time.TimeString(true, true, true));
                Console.WriteLine("System boot time:          " + boottime);
                Console.WriteLine("Total memory:              " + Core.MemoryManager.TotalMemory + "MB");
                Console.WriteLine("Used memory:               " + Core.MemoryManager.GetUsedMemory() + "MB");
                Console.WriteLine("Free memory:               " + Core.MemoryManager.GetFreeMemory() + "MB");
                Console.WriteLine("File system type:          " + FS.GetFileSystemType("0:\\"));
                Console.WriteLine("Available free space:      " + (FS.GetAvailableFreeSpace("0:\\")/1024)/1024 + "MB");
            }
            else if (args[0] == "echo")
            {
                try
                {
                    Console.WriteLine(input.Remove(0, 5));
                }
                catch (Exception ex)
                {
                    Utils.Msg.ErrorMsg(ex.Message);
                }
            }
            else if (args[0] == "miv")
            {
                Tools.MIV.StartMIV();
            }
            else if (args[0] == "dir" || args[0] == "ls")
            {
                Console.WriteLine("Type\tName");
                foreach (var dir in Directory.GetDirectories(current_directory))
                {
                    Console.WriteLine("d\t" + dir);
                }
                foreach (var dir in Directory.GetFiles(current_directory))
                {
                    Console.WriteLine("-\t" + dir);
                }
            }
            else if (args[0] == "mkfil")
            {
                string file = args[1];
                File.Create(current_directory + file);
            }
            else if (args[0] == "mkdir")
            {
                string dir = args[1];
                Directory.CreateDirectory(current_directory + dir);

            }
            else if (args[0] == "cd")
            {
                if (args.Length == 1)
                {
                    current_directory = "0:\\";
                }
                else
                {
                    var newdir = args[1];
                    if (newdir == "..")
                    {
                        if (current_directory != "0:\\")
                        {
                            var dir = FS.GetDirectory(current_directory);
                            string p = dir.mParent.mFullPath;
                            current_directory = p;
                        }
                    }
                    else if (Directory.Exists(current_directory + newdir))
                    {
                        current_directory += newdir + "\\";
                    }
                }
            }
            else if (args[0] == "pwd")
            {
                Console.WriteLine(current_directory);
            }
            else if (args[0] == "beep")
            {
                Cosmos.System.PCSpeaker.Beep((uint)Convert.ToInt64(args[1]), (uint)Convert.ToInt64(args[2]));
            }
            else if (args[0] == "crash")
            {
                int[] myNumbers = { 1, 2, 3 };
                Console.WriteLine(myNumbers[10]);
            }
            else if (args[0] == "cat")
            {
                string file = args[1];
                if (File.Exists(current_directory + file))
                {
                    Console.WriteLine(File.ReadAllText(current_directory + file));
                }
                else
                {
                    Utils.Msg.ErrorMsg("File doesn't exist.");
                }

            }
            else if (args[0] == "lsvol")
            {
                var vols = FS.GetVolumes();
                Console.WriteLine("Name\tSize\tParent");
                foreach (var vol in vols)
                {
                    Console.WriteLine(vol.mName + "\t" + vol.mSize + "\t" + vol.mParent);
                }
            }
            else if (args[0] == "rm")
            {
                string file = args[1];
                if (File.Exists(current_directory + file))
                {
                    File.Delete(current_directory + file);
                }
                else
                {
                    if (Directory.Exists(current_directory + file))
                    {
                        Directory.Delete(current_directory + file);
                    }
                    else
                    {
                        Utils.Msg.ErrorMsg("File or Directory doesn't exist.");
                    }
                }
            }
            else if (args[0] == "cls" || args[0] == "clear")
            {
                Console.Clear();
            }
            else if (args[0] == "help")
            {
                Console.WriteLine("help     -- Displays list of comamnds.");
                Console.WriteLine("shutdown -- Shuts down the system.");
                Console.WriteLine("reboot   -- Reboots the system.");
                Console.WriteLine("hex      -- Displays contents of the file in hex.");
                Console.WriteLine("time     -- Displays currnet time and date.");
                Console.WriteLine("date     -- Displays currnet time and date.");
                Console.WriteLine("sysinfo  -- Displays system information.");
                Console.WriteLine("echo     -- Returns the inputed text.");
                Console.WriteLine("miv      -- Opens MIV text editor.");
                Console.WriteLine("dir      -- Lists all files and directories.");
                Console.WriteLine("ls       -- Lists all files and directories.");
                Console.WriteLine("mkfil    -- Creates a file.");
                Console.WriteLine("mkdir    -- Creates a directory.");
                Console.WriteLine("cat      -- Displays contents of the file.");
                Console.WriteLine("rm       -- Deletes file or directory.");
                Console.WriteLine("lsvol    -- Lists all volumes.");
                Console.WriteLine("cd       -- Use for directory navigation.");
                Console.WriteLine("pwd      -- Displays current path.");
                Console.WriteLine("crash    -- Crashes the OS.");
                Console.WriteLine("clear    -- Clears the terminal.");
                Console.WriteLine("cls      -- Clears the terminal.");
            }
            else
            {
                Utils.Msg.ErrorMsg("Unknown command");
            }
        }
    }
}

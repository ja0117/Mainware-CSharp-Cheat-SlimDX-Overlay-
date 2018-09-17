using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mainware_Full.Security
{
    class GameCheck
    {
        private static string process = "csgo";
        public static int bClient;
        public static int bEngine;

        public static bool Proccess()
        {
            try
            {
                Process[] p = Process.GetProcessesByName(process);
                Console.WriteLine("");
                Console.WriteLine("███╗   ███╗ █████╗ ██╗███╗   ██╗██╗    ██╗ █████╗ ██████╗ ███████╗    ██████╗  ██████╗ ");
                Console.WriteLine("████╗ ████║██╔══██╗██║████╗  ██║██║    ██║██╔══██╗██╔══██╗██╔════╝   ██╔════╝ ██╔═══██╗");
                Console.WriteLine("██╔████╔██║███████║██║██╔██╗ ██║██║ █╗ ██║███████║██████╔╝█████╗     ██║  ███╗██║   ██║");
                Console.WriteLine("██║╚██╔╝██║██╔══██║██║██║╚██╗██║██║███╗██║██╔══██║██╔══██╗██╔══╝     ██║   ██║██║▄▄ ██║");
                Console.WriteLine("██║ ╚═╝ ██║██║  ██║██║██║ ╚████║╚███╔███╔╝██║  ██║██║  ██║███████╗██╗╚██████╔╝╚██████╔╝");
                Console.WriteLine("╚═╝     ╚═╝╚═╝  ╚═╝╚═╝╚═╝  ╚═══╝ ╚══╝╚══╝ ╚═╝  ╚═╝╚═╝  ╚═╝╚══════╝╚═╝ ╚═════╝  ╚══▀▀═╝ ");
                Console.WriteLine("");
                string machinename = Environment.UserName;
                Console.WriteLine("Hey " + machinename + "!", Console.ForegroundColor = ConsoleColor.DarkMagenta);
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("Trying to find CSGO", Console.ForegroundColor = ConsoleColor.DarkRed);
                if (p.Length > 0)
                {
                    Console.WriteLine("Trying to find client_panorama.dll & engine.dll");
                    foreach (ProcessModule m in p[0].Modules)
                    {
                        if (m.ModuleName == "client_panorama.dll")
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("client_panorama.dll Found!");
                            Memory._process = p[0];
                            bClient = (int)m.BaseAddress;
                        }
                        if (m.ModuleName == "engine.dll")
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("engine.dll Found!");
                            Memory._process = p[0];
                            bEngine = (int)m.BaseAddress;
                        }
                    }
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine("");
                    Console.WriteLine("Have FUn!");
                    return true;
                }
                else
                {
                    Console.WriteLine("CSGO Not found! Closing Console Application", Console.ForegroundColor = ConsoleColor.Red);
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}

/**
 * Coded by JoshuaA. Any questions? Contact me on discord: @JoshuaA#6951 
 * Coded by JoshuaA. Any questions? Contact me on discord: @JoshuaA#6951
 * Coded by JoshuaA. Any questions? Contact me on discord: @JoshuaA#6951
 * Coded by JoshuaA. Any questions? Contact me on discord: @JoshuaA#6951
 * Coded by JoshuaA. Any questions? Contact me on discord: @JoshuaA#6951
 * Coded by JoshuaA. Any questions? Contact me on discord: @JoshuaA#6951
 * Coded by JoshuaA. Any questions? Contact me on discord: @JoshuaA#6951
 * Coded by JoshuaA. Any questions? Contact me on discord: @JoshuaA#6951
 * Coded by JoshuaA. Any questions? Contact me on discord: @JoshuaA#6951
 * Coded by JoshuaA. Any questions? Contact me on discord: @JoshuaA#6951
 * Coded by JoshuaA. Any questions? Contact me on discord: @JoshuaA#6951
 * Coded by JoshuaA. Any questions? Contact me on discord: @JoshuaA#6951
 * Coded by JoshuaA. Any questions? Contact me on discord: @JoshuaA#6951
 * Coded by JoshuaA. Any questions? Contact me on discord: @JoshuaA#6951
 * Coded by JoshuaA. Any questions? Contact me on discord: @JoshuaA#6951
 */

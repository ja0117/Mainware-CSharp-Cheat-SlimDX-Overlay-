using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Windows.Forms;
using Mainware_Full.Offset_things;

using System.Media;

namespace Mainware_Full
{
    public partial class Program
    {
        static void Main(string[] args)
        {
            try {
                OffsetGetSet.Offset();
            }
            catch {
                Console.WriteLine("No active internet connection, closing Mainware", Console.BackgroundColor = ConsoleColor.Red, Console.ForegroundColor = ConsoleColor.Black);
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Green;
                Thread.Sleep(5000);
                Environment.Exit(404);
            }
            if (!Security.GameCheck.Proccess())
            {
                Console.WriteLine("Process not found! Exiting Application...");
                Thread.Sleep(5000);
                Environment.Exit(303);
            }
            Application.EnableVisualStyles();
            Application.Run(new FrmSharpDX());
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

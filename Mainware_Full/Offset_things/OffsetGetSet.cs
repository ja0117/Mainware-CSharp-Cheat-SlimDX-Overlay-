using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Mainware_Full.Offset_things
{
    class OffsetGetSet
    {
        public static void Offset()
        {
            Console.WriteLine("Fetching Offsets from https://raw.githubusercontent.com/frk1/hazedumper/master/csgo.cs", Console.ForegroundColor = ConsoleColor.Magenta);
            StringBuilder newFile = new StringBuilder();
            var url = "https://raw.githubusercontent.com/frk1/hazedumper/master/csgo.cs";
            var textFromWeb =  (new WebClient()).DownloadString(url);

            //Console.Write(textFromWeb);

            using (StreamWriter writer = new StreamWriter("../../Offset_things/offsets.cs"))
            {
                writer.Write(textFromWeb);
            }
            string[] file = File.ReadAllLines("../../Offset_things/offsets.cs");
            foreach (string line in file)
            {
                if (line.Contains("namespace hazedumper"))
                {
                    string temp = line.Replace("namespace hazedumper", "namespace Mainware_Full");
                    newFile.Append(temp + "\r\n");
                    continue;
                }
                if(line.Contains("public const Int32 timestamp"))
                {
                    string temp = line.Replace("public const Int32 timestamp", "//");
                    newFile.Append(temp + "\r\n");
                    continue;
                }
                newFile.Append(line + "\r\n");
            }
            File.WriteAllText("../../Offset_things/offsets.cs", newFile.ToString());
            Console.WriteLine("Updated offsets!", Console.ForegroundColor = ConsoleColor.Green);
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace BatchColorReplacer
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] colorDefs;
            try
            {
               colorDefs = System.IO.File.ReadAllLines(args[0]);
            }
            catch(Exception e)
            {
                Console.WriteLine("ERROR>> Cannot read color definitions file, exception: " + e.ToString());
                Console.WriteLine("USAGE: \nBatchColorReplacer colorDefinitions.txt image1.png image2.png ...");
                return;
            }

            ProcessStartInfo converter = new ProcessStartInfo();
            converter.FileName = @"C:\Packages\ImageMagick\convert.exe";
            converter.UseShellExecute = false;
            converter.RedirectStandardOutput = true;
            for(int i = 1; i < args.Length; ++i)
            {
                try
                {
                    string[] files = { args[i] };
                    if (args[i].Contains('*'))
                    {
                        files = Directory.GetFiles(@".\", args[i]);
                    }

                    foreach (string file in files)
                    {
                        Console.WriteLine("Processing " + file);

                        // Want any png files to be encoded at full bit depth because SFML does not process images with 4 bit depth.
                        string png = "";
                        if (file.Contains(".png"))
                            png = "PNG32:";

                        foreach (string colorReplace in colorDefs)
                        {
                            Process p = new Process();

                            converter.Arguments = file + " -fill " + colorReplace.Split(' ')[1] + " -opaque " + colorReplace.Split(' ')[0] + " " + png + file;
                            Console.WriteLine("    convert.exe " + converter.Arguments);

                            p.StartInfo = converter;
                            p.Start();
                            Console.WriteLine(p.StandardOutput.ReadToEnd());
                            p.WaitForExit();
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("ERROR>> " + e.ToString());
                }
            }
        }
    }
}

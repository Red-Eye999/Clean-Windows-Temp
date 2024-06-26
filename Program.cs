using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TempCleanup
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Win_Temp(@"C:\Windows\Temp");
            Win_Temp(Path.GetTempPath());
        }


        public static float DirSize(DirectoryInfo d)
        {
            try
            {
                float size = 0;
                // Add file sizes.
                FileInfo[] fis = d.GetFiles();
                foreach (FileInfo fi in fis)
                {
                    size += (float)fi.Length;
                }
                // Add subdirectory sizes.
                DirectoryInfo[] dis = d.GetDirectories();
                foreach (DirectoryInfo di in dis)
                {
                    size += DirSize(di);
                }
                return size;
            }
            catch
            {
                return 0;
            }
        }

        public static void Win_Temp(string path)
        {
            try
            {
                float sizable = (((DirSize(new DirectoryInfo(path)) / 1024) / 1024) / 1024);
                if (sizable < 1)
                    Console.WriteLine("The size is {0} Megabytes.", sizable * 1024);
                else
                    Console.WriteLine("The size is {0} Gigabyte.", sizable);
                Console.WriteLine("Press any key to continue");
                string deleteSTR = Console.ReadLine();
                Console.WriteLine("Processing....");
                var dir = new DirectoryInfo(path);
                foreach (var file in Directory.GetFiles(dir.ToString()))
                {
                    try
                    {
                        File.Delete(file);
                        Console.WriteLine("Temp files are deleted successfuly!");
                        Console.ReadKey();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

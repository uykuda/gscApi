using System;
using System.IO;
using SevenZipExtractor;
using System.Net;

namespace Api
{
    public class FirstStart
    {
        public void getRuntime()
        {
            try
            {
                using (var client = new WebClient())
                {
                    
                    client.DownloadFile("https://cdn.discordapp.com/attachments/986373626888585267/988219727631499294/runtime.zip", Paths.temp + "runtime.zip");
                    Console.WriteLine("Runtime downloaded successfully.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        public void DecompressRuntime()
        {
            try
            {
                Paths paths = new Paths();
                using (ArchiveFile archiveFile = new ArchiveFile(Paths.temp + "runtime.zip"))
                {
                    archiveFile.Extract(Paths.runtimeFolder + "openjdkjre64"); // extract all
                    Console.WriteLine("Runtime installed successfully.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        public void fs()
        {
            Paths paths = new Paths();
            ngrok ngrok = new ngrok();
            DirectoryInfo di = Directory.CreateDirectory(Paths.AppData + @"\serverCreator");
            Console.WriteLine("default folder created");
            DirectoryInfo runtime = Directory.CreateDirectory(Paths.runtimeFolder);
            Console.WriteLine("runtime folder created");
            DirectoryInfo servers = Directory.CreateDirectory(Paths.AppData + @"\serverCreator\servers");
            Console.WriteLine("servers folder created");
            DirectoryInfo minecraftServers = Directory.CreateDirectory(Paths.minecraftServers);
            Console.WriteLine("minecraft folder created");
            DirectoryInfo temp = Directory.CreateDirectory(Paths.temp);
            Console.WriteLine("default folder created");
            
        }
    }
}

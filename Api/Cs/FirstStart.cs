using SevenZipExtractor;
using System;
using System.IO;
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
                    Console.WriteLine("Runtime downloading...");
                    client.DownloadFile("https://cdn.discordapp.com/attachments/986373626888585267/988219727631499294/runtime.zip", Paths.temp + "runtime.zip");
                    Console.WriteLine("Runtime downloaded successfully!");
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
                    Console.WriteLine("Runtime extracting...");
                    archiveFile.Extract(Paths.runtimeFolder + "openjdkjre64"); // dosyaları çıkart
                    Console.WriteLine("Runtime extracted successfully!");
                }
            }
            catch (Exception error)
            {
                ErrorMessage(error.Message);
            }
        }
        public void fs()
        {
            try
            {
                Paths paths = new Paths();
                ngrok ngrok = new ngrok();
                DirectoryInfo main = Directory.CreateDirectory(Paths.AppData + @"\serverCreator");
                Console.WriteLine("main folder created");
                DirectoryInfo runtime = Directory.CreateDirectory(Paths.runtimeFolder);
                Console.WriteLine("runtime folder created");
                DirectoryInfo servers = Directory.CreateDirectory(Paths.serversFolder);
                Console.WriteLine("servers folder created");
                //DirectoryInfo minecraftServers = Directory.CreateDirectory(Paths.minecraftServers);
                //Console.WriteLine("minecraft folder created");
                DirectoryInfo temp = Directory.CreateDirectory(Paths.temp);
                Console.WriteLine("temp folder created");
            }
            catch (Exception error)
            {
                ErrorMessage(error.Message);
            }
        }
        public static void ErrorMessage(string err)
        {
            Console.WriteLine("\n\nHere is the your error, contact with developer {0}" + err);
            Environment.Exit(0);
        }
    }
}

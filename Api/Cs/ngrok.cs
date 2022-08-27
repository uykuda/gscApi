using SevenZipExtractor;
using System;
using System.Diagnostics;
using System.Net;

namespace Api
{
    public class ngrok
    {
        public void getNgrok()
        {
            try
            {
                using (var client = new WebClient())
                {

                    client.DownloadFile("https://bin.equinox.io/c/bNyj1mQVY4c/ngrok-v3-stable-windows-amd64.zip", Paths.temp + "ngrok.zip");
                    Console.WriteLine("Ngrok downloaded successfully.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        public void decompressNgrok()
        {
            try
            {
                Paths paths = new Paths();
                using (ArchiveFile archiveFile = new ArchiveFile(Paths.temp + "ngrok.zip"))
                {
                    archiveFile.Extract(Paths.runtimeFolder + "ngrok"); // extract all
                    Console.WriteLine("Ngrok installed successfully.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        public void setToken()
        {
            try
            {
                Process process = new Process();
                process.StartInfo.CreateNoWindow = false;
                process.StartInfo.UseShellExecute = true;
                process.StartInfo.FileName = @"cmd.exe";
                process.StartInfo.WorkingDirectory = Paths.minecraftServers + Program.serverName + @"\";
                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                process.StartInfo.Arguments = "/c " + Paths.runtimeFolder + @"ngrok\ngrok.exe config add-authtoken " + Program.token;
                process.Start();
                process.WaitForExit();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        public void startNgrok()
        {
            try
            {
                Process process = new Process();
                process.StartInfo.CreateNoWindow = false;
                process.StartInfo.UseShellExecute = true;
                process.StartInfo.FileName = @"cmd.exe";
                process.StartInfo.WorkingDirectory = Paths.minecraftServers + Program.serverName + @"\";
                process.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                process.StartInfo.Arguments = "/c " + Paths.runtimeFolder + @"ngrok\ngrok.exe" + " tcp " + Program.port;
                process.Start();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}

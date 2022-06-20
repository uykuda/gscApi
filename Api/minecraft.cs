using System;
using System.Net;
using System.IO;
namespace Api
{
    public class minecraft
    {

        public void minecraftCreate()
        {
            Paths paths = new Paths();
            var ini = new ini.IniFile(Paths.minecraftServers + Program.serverName + @"\" + Program.serverName + @".ini");
            ini.Write("serverName", Program.serverName);
            ini.Write("serverSoftware", Program.serverSoftware);
            ini.Write("serverVersion", Program.serverVersion);
            ini.Write("serverPort", Convert.ToString(Program.port));
            if (Program.serverSoftware == "vanilla")
            {
                try
                {
                    using (var client = new WebClient())
                    {
                        DirectoryInfo minecraft = Directory.CreateDirectory(Paths.minecraftServers + Program.serverName);
                        client.DownloadFile(System.Configuration.ConfigurationManager.AppSettings.Get(Program.serverVersion), Paths.minecraftServers + Program.serverName + @"\"+ Program.serverName + ".jar");
                        Console.WriteLine(Program.serverVersion + " file downloaded successfully.");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            if (Program.serverSoftware == "spigot")
            {
                try
                {
                    using (var client = new WebClient())
                    {
                        DirectoryInfo minecraft = Directory.CreateDirectory(Paths.minecraftServers + Program.serverName);
                        var value = System.Configuration.ConfigurationManager.AppSettings[Program.serverVersion + "s"];
                        client.DownloadFile(value, Paths.minecraftServers + Program.serverName + @"\" + Program.serverName + ".jar");
                        Console.WriteLine(Program.serverVersion + " file downloaded successfully.");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }

        }
        public void minecraftLoad()
        {
            Paths paths = new Paths();
            var ini = new ini.IniFile(Paths.minecraftServers + Program.serverName + @"\" + Program.serverName + @".ini");
            string serverName = ini.Read("serverName");
            string serverPort = ini.Read("serverPort");
        }
    }
}

using System;
using System.Diagnostics;
using System.IO;
using System.Net;

namespace Api
{
    public class minecraft

    {
        Paths Paths = new Paths();
        public static string eula;

        public static void checkEula()
        {

            Console.WriteLine(@"By changing the setting below to TRUE you are indicating your agreement to our EULA (https://account.mojang.com/documents/minecraft_eula)." + Environment.NewLine + "You also agree that tacos are tasty, and the best food in the world.");
            Console.WriteLine("Do you accept Eula.txt? Y/N");
            string Enter = Console.ReadLine();
            if (Enter != "Y")
            {
                Environment.Exit(0);
            }
            else
            {
                try
                {
                    using (StreamWriter sw = File.CreateText(eulaLocation))
                    {
                        sw.Write("eula=true");
                    }
                    StreamReader Oku = new StreamReader(eulaLocation);
                    while (!Oku.EndOfStream)
                    {
                        Console.WriteLine(Oku.ReadLine());
                    }
                    Oku.Close();
                    runCommand();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }
        public void minecraftCreate()
        {
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
                        client.DownloadFile(System.Configuration.ConfigurationManager.AppSettings.Get(Program.serverVersion), Paths.minecraftServers + Program.serverName + @"\" + Program.serverName + ".jar");
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
            if (Program.serverSoftware == "purpur")
            {
                try
                {
                    using (var client = new WebClient())
                    {
                        DirectoryInfo minecraft = Directory.CreateDirectory(Paths.minecraftServers + Program.serverName);
                        var value = System.Configuration.ConfigurationManager.AppSettings[Program.serverVersion + "p"];
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
            var ini = new ini.IniFile(@Paths.minecraftServers + Program.serverName + @"\" + Program.serverName + @".ini");
            string serverName = ini.Read("serverName");
            string serverPort = ini.Read("serverPort");
            runCommand();
        }

        public static void runCommand()
        {
            discord discord = new discord();
            ngrok ngrok = new ngrok();

            try
            {
                StreamReader Oku = new StreamReader(eulaLocation);
                while (!Oku.EndOfStream)
                {
                    eula = Oku.ReadLine();
                }
            }
            catch
            {

            }
            if (File.Exists(eulaLocation) && eula == "eula=true")
            {
                goto aer;
            }
            else
            {
                checkEula();
            }
        aer:
            ngrok.startNgrok();
            discord.Initialize();
            Process process = new Process();
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.FileName = @"cmd.exe";
            process.StartInfo.WorkingDirectory = Paths.minecraftServers + Program.serverName + @"\";
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.StartInfo.Arguments = "/c " + Paths.runtimeFolder + @"openjdkjre64\java-runtime-gamma\bin\javaw.exe " + "-jar " + Paths.minecraftServers + Program.serverName + @"\" + Program.serverName + @".jar --nogui";
            process.StartInfo.Verb = "runas";
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.OutputDataReceived += new DataReceivedEventHandler(OutputHandler);
            process.ErrorDataReceived += new DataReceivedEventHandler(OutputHandler);
            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            while (true)
            {
                var sd = Console.ReadLine();
                if (sd != "stop")
                {
                    process.StandardInput.WriteLine(sd);
                    sd = "";
                }
                else
                {
                    Console.WriteLine("Press Ctrl+C");
                    process.StandardInput.WriteLine("stop");
                    process.WaitForExit();
                    discord.Deinitialize();
                    Environment.Exit(0);
                }
            }
        }
        static void OutputHandler(object sendingProcess, DataReceivedEventArgs outLine)
        {
            Console.WriteLine(outLine.Data);
        }
        public static string eulaLocation
        {
            get
            {
                string el = @Paths.minecraftServers + Program.serverName + @"\Eula.txt";
                return el;
            }
            set
            {
                value = eulaLocation;
            }
        }
    }
}
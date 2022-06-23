using System;
using System.Net;
using System.IO;
using System.Diagnostics;

namespace Api
{
    public class minecraft
    {
        public static string eula;
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
        public static void checkEula()
        {
            Console.WriteLine(@"By changing the setting below to TRUE you are indicating your agreement to our EULA (https://account.mojang.com/documents/minecraft_eula)."+Environment.NewLine+"You also agree that tacos are tasty, and the best food in the world.");
            Console.WriteLine("Do you accept Eula.txt? Y/N");
            string sd =Console.ReadLine();
            if (sd !="Y")
            {
                Environment.Exit(0);
            }
            else
            {
                string[] Lines = File.ReadAllLines(Paths.minecraftServers + Program.serverName + @"\" + "Eula.txt");
                File.Delete(Paths.minecraftServers + Program.serverName + @"\" + "Eula.txt");
                using (StreamWriter sw = File.AppendText(Paths.minecraftServers + Program.serverName + @"\" + "Eula.txt"))

                {
                    foreach (string line in Lines)
                    {
                        if (line.IndexOf("eula=false") >= 0)
                        {
                            continue;
                        }
                        else
                        {
                            sw.WriteLine(line);
                            sw.WriteLine("eula=true");
                        }
                    }
                }
               
                runCommand();
            }
        }
        public static void runCommand()
        {
            ngrok ngrok = new ngrok();
            if (File.Exists(Paths.minecraftServers + Program.serverName + @"\" + "Eula.txt"))
            {
                
                using (StreamReader reader = new StreamReader(Paths.minecraftServers + Program.serverName + @"\" + "Eula.txt"))
                {
                    int i = 0;
                    while (reader.ReadLine() != null && i < 2)
                    {
                        i++;
                    }
                    eula = reader.ReadLine();
                    Console.WriteLine(eula);
                }
                if(eula == "eula=true"){
                    goto aer;
                }
                else
                {
                    checkEula();
                }
            }
            else
            {
                using (StreamWriter writer = new StreamWriter(Paths.minecraftServers + Program.serverName + @"\" + "Eula.txt"))
                {
                    writer.WriteLine("#By changing the setting below to TRUE you are indicating your agreement to our EULA (https://account.mojang.com/documents/minecraft_eula).");
                    writer.WriteLine("#You also agree that tacos are tasty, and the best food in the world.");
                    writer.WriteLine("eula=false");
                }
                checkEula();
            }
        aer:
            ngrok.startNgrok();
        
            Process process = new Process();
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.FileName = @"cmd.exe";
            process.StartInfo.WorkingDirectory = Paths.minecraftServers + Program.serverName + @"\";
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.StartInfo.Arguments = "/c "  + Paths.runtimeFolder + @"openjdkjre64\java-runtime-gamma\bin\javaw.exe " + "-jar " + Paths.minecraftServers + Program.serverName + @"\" + Program.serverName + @".jar --nogui";
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
                    process.StandardInput.WriteLine("stop");
                    process.WaitForExit();
                    Environment.Exit(0);
                }
            }
        }
        static void OutputHandler(object sendingProcess, DataReceivedEventArgs outLine)
        {
            Console.WriteLine(outLine.Data);
        }
        public void minecraftLoad()
        {
            Paths paths = new Paths();
            var ini = new ini.IniFile(@Paths.minecraftServers + Program.serverName + @"\" + Program.serverName + @".ini");
            string serverName = ini.Read("serverName");
            string serverPort = ini.Read("serverPort");
            runCommand();
        }
    }
}
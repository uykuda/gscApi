using System;
using System.Net;
using System.IO;
using System.Diagnostics;

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
        
        public static void runCommand()
        {
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
            //* Do your stuff with the output (write to console/log/StringBuilder)
            Console.WriteLine(outLine.Data);
        }
        public void minecraftLoad()
        {
          //Console.WriteLine(Paths.minecraftServers + Program.serverName + @"\");
            Paths paths = new Paths();
            var ini = new ini.IniFile(@Paths.minecraftServers + Program.serverName + @"\" + Program.serverName + @".ini");
            string serverName = ini.Read("serverName");
            string serverPort = ini.Read("serverPort");
            runCommand();
        }
    }
}
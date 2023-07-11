using System;
using System.IO;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;

namespace Api
{
    public class Program
    {

        public static string runLocation = Assembly.GetExecutingAssembly().CodeBase;
        public static string firstArg;
        public static string serverSoftware;
        public static string serverVersion;
        public static string serverName;
        public static string token;
        public static string region;
        public static int port;
        public static string path = Paths.ProgramSettings;

        public static void helpMessage()
        {
            // Console.Write(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)); OUTPUT == C:\Users\USERNAME\AppData\Roaming

            Console.WriteLine(Environment.NewLine + " create a game server." + Environment.NewLine);
            Console.WriteLine(" gsc {gameName | create/load | serverName | serverSoftware | serverVersion | serverPort}" + Environment.NewLine);
            Console.WriteLine(" supported softwares;" + Environment.NewLine + Environment.NewLine + "  -vanilla, spigot, purpur." + Environment.NewLine);
            Console.WriteLine(" supported vanilla versions;" + Environment.NewLine + Environment.NewLine + "  -vanilla 1.18.2 to 1.16" + Environment.NewLine);
            Console.WriteLine(" supported spigot versions;" + Environment.NewLine + Environment.NewLine + "  -spigot 1.18.2" + Environment.NewLine);
            Console.WriteLine(" supported purpur versions;" + Environment.NewLine + Environment.NewLine + "  -purpur 1.18.2" + Environment.NewLine);
            Console.WriteLine(" supported games;" + Environment.NewLine + Environment.NewLine + "  -Minecraft." + Environment.NewLine);
        }
        public static void FirstStartControl()
        {
            if (!File.Exists(path))
            {
                ngrok ngrok = new ngrok();
                FirstStart firstStart = new FirstStart();
                firstStart.fs();
                var settingsini = new ini.IniFile(path);
                settingsini.Write("firstStart", "Yes");
                Console.WriteLine("settings file created");


                if (!File.Exists(Paths.runtimeFolder + "windowsx64"))
                {
                    firstStart.getRuntime();
                    ngrok.getNgrok();
                    firstStart.DecompressRuntime();
                    ngrok.decompressNgrok();
                }
                helpMessage();
                Environment.Exit(0);
            }
            else
            {
                var settingsini = new ini.IniFile(path);
                if (!settingsini.KeyExists("firstStart", "Program"))
                {
                    settingsini.Write("firstStart", "No");
                }
                var DefaultVolume = settingsini.Read("firstStart");
            }
        }
        static void Main(string[] args)
        {
            FirstStartControl();

            minecraft minecraft = new minecraft();
            ngrok ngrok = new ngrok();
            ini ini = new ini();


            Paths paths = new Paths();
            string mod;

            try
            {
                firstArg = args[0];
            }
            catch (Exception error)
            {
                ErrorMessage(error.Message);
            }

            if (firstArg == "discord")
            {
                discord discord = new discord();
                discord.Initialize();
                Console.WriteLine("Press key for cancel the progress.");
                Console.ReadKey();
            }
            else if (firstArg == "minecraft")
            {
                mod = args[1];
                if (mod == "create")
                {
                    serverName = args[2];
                    serverSoftware = args[3];
                    serverVersion = args[4];
                    port = Convert.ToInt32(args[5]);
                    Console.WriteLine("server name: " + serverName + "\n" +
                                      "server software: " + serverSoftware + "\n" +
                                      "server version: " + serverVersion);
                    minecraft.minecraftCreate();
                } else if (mod == "load")
                {
                    serverName = args[2];
                    port = Convert.ToInt32(args[3]);
                    minecraft.minecraftLoad();
                }
            }
            if (firstArg == "ngrok")
            {
                var settingsini = new ini.IniFile(path);
                if (!settingsini.KeyExists("tokenEntered", "Yes"))
                {
                    token = args[1];
                    ngrok.setToken();
                    settingsini.Write("tokenEntered", "Yes");
                }
                else
                {
                    ngrok.startNgrok();
                }
            }
            if (firstArg == "help")
            {
                helpMessage();
            }
            stopGSC();
        }

        public static void stopGSC()
        {
            try
            {
                discord discord = new discord();
                discord.client.Dispose();
                Environment.Exit(0);
            }
            catch (Exception error)
            {
                ErrorMessage(error.Message);
            }
        }
        public static void ErrorMessage(string err)
        {
            helpMessage();

            Console.WriteLine("\n\nHere is the your error, contact with developer {0}" + err);
            Environment.Exit(0);
        }
    }
}

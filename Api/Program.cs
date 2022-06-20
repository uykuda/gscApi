using System;
using System.IO;
using System.IO.Compression;
namespace Api
{
    public class Program
    {
        public static string runLocation = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
        public static string game;
        public static string serverSoftware;
        public static string serverVersion;
        public static string serverName;
        public static string token;
        public static int port;

        public static void helpMessage()
        {
            Console.WriteLine("commands");
            Console.WriteLine("commands");
            Console.WriteLine("commands");
            Console.WriteLine("commands");
        }
        public static void FirstStartControl()
        {
            string path = Paths.ProgramSettings;
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
                    firstStart.DecompressRuntime();
                    ngrok.getNgrok();
                    ngrok.decompressNgrok();
                }
                helpMessage();
                Environment.Exit(0);
            }
            else
            {
                var settingsini = new ini.IniFile(path);
                if (!settingsini.KeyExists("firstStart","Program"))
                {
                    settingsini.Write("firstStart", "No");
                }
                var DefaultVolume = settingsini.Read("firstStart");
            }
        }
        static void Main(string[] args)
        {
            FirstStartControl();
            
            ini ini = new ini();
            minecraft minecraft = new minecraft();
            Paths paths = new Paths();
            string mod;
            game = args[0];
            if (game == "minecraft")
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
                }
                if (mod == "load")
                {
                    serverName = args[2];
                    port = Convert.ToInt32(args[3]);
                    minecraft.minecraftLoad();
                }
            }
            if (game == "ngrok")
            {
                token = args[1];

            }
        }
    }
}

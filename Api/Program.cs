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
            Console.WriteLine(Environment.NewLine + " create a game server." + Environment.NewLine );
            Console.WriteLine(" gsc {gameName | create/load | serverName | serverSoftware | serverVersion | serverPort}" + Environment.NewLine);
            Console.WriteLine(" supported softwares;" + Environment.NewLine + Environment.NewLine +"  -vanilla, spigot, purpur." + Environment.NewLine);
            Console.WriteLine(" supported vanilla versions;" + Environment.NewLine + Environment.NewLine + "  -vanilla 1.18.2 to 1.16" + Environment.NewLine);
            Console.WriteLine(" supported spigot versions;" + Environment.NewLine + Environment.NewLine + "  -spigot 1.18.2" + Environment.NewLine);
            Console.WriteLine(" supported purpur versions;" + Environment.NewLine + Environment.NewLine + "  -purpur 1.18.2" + Environment.NewLine);
            Console.WriteLine(" supported games;" + Environment.NewLine + Environment.NewLine +"  -Minecraft." + Environment.NewLine);
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

            ngrok ngrok = new ngrok();
            ini ini = new ini();
            minecraft minecraft = new minecraft();
            Paths paths = new Paths();
            string mod;
            try
            {
                game = args[0];
            }
            catch
            {
                helpMessage();
            }
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
                ngrok.setToken(); 
            }
            if (game == "help")
            {
                helpMessage();
            }
            stopGSC();
        }
        public static void stopGSC()
        {
            Environment.Exit(0);
        }
    }
}

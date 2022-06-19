using System;
using System.IO;
namespace Api
{
    public class Program
    {
        public static string runLocation = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
        public static string game;
        public static string serverSoftware;
        public static string serverVersion;
        public static string serverName;

        public static void helpMessage()
        {
            Console.WriteLine("commands");
            Console.WriteLine("commands");
            Console.WriteLine("commands");
            Console.WriteLine("commands");
        }
        public static void FirstStartControl()
        {
            Extensions extensions = new Extensions();
            Extensions.readExtensions();
            string path = Paths.ProgramSettings;
            if (!File.Exists(path))
            {
                var settingsini = new ini.IniFile(path);
                settingsini.Write("FirstStart", "Yes");
                helpMessage();
                Environment.Exit(0);
            }
            else
            {
                var settingsini = new ini.IniFile(path);
                if (!settingsini.KeyExists("FirstStart","Program"))
                {
                    settingsini.Write("FirstStart", "No");
                }
                var DefaultVolume = settingsini.Read("FirstStart");
            }
        }
        static void Main(string[] args)
        {
            FirstStartControl();
            ini ini = new ini();
            minecraft minecraft = new minecraft();
            Paths paths = new Paths();
            Extensions extensions = new Extensions();
            string mod;
            int port;
            //args Control&Apply
            //Console.WriteLine(Paths.ProgramSettings);
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
                    serverName = args[1];
                    port = Convert.ToInt32(args[2]);
                    minecraft.minecraftLoad();
                }
            }
        }
    }
}

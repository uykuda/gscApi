using System;
namespace Api
{
    public class Paths
    {

        public static string AppData
        {
            get
            {
                string AppData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                return AppData;
            }
            set
            {
                value = AppData;
            }
        }
        public static string main
        {
            get
            {
                string main = (AppData + @"\serverCreator\");
                return main;
            }
            set
            {
                value = main;
            }
        }
        public static string temp
        {
            get
            {
                string temp = (main + @"\temp\");
                return temp;
            }
            set
            {
                value = temp;
            }
        }
        public static string runtimeFolder
        {
            get
            {
                string runtimeFolder = (main + @"\runtime\");
                return runtimeFolder;
            }
            set
            {
                value = runtimeFolder;
            }
        }
        public static string serversFolder
        {
            get
            {
                string serversFolder = (main + @"\servers\");
                return serversFolder;
            }
            set
            {
                value = serversFolder;
            }
        }
        public static string minecraftServers
        {
            get
            {
                string minecraftServers = (main + @"\servers\minecraft\");
                return minecraftServers;
            }
            set
            {
                value = minecraftServers;
            }
        }
        public static string ProgramSettings
        {
            get
            {
                string ProgramSettings = main + @"\settings.ini";
                return ProgramSettings;
            }
            set
            {
                value = ProgramSettings;
            }
        }
        public static string ngrokFolder
        {
            get
            {
                string minecraftServers = (runtimeFolder + @"\ngrok\");
                return minecraftServers;
            }
            set
            {
                value = ngrokFolder;
            }
        }
    }
}

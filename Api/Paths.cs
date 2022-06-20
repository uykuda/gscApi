using System;
using System.IO;
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
        public static string temp
        {
            get
            {
                string temp = (AppData + @"\serverCreator\temp\");
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
                string runtimeFolder = (AppData + @"\serverCreator\runtime\");
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
                string serversFolder = (AppData + @"\serverCreator\servers\");
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
                string minecraftServers = (AppData + @"\serverCreator\servers\minecraft\");
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
                string ProgramSettings = AppData + @"\serverCreator\settings.ini";
                return ProgramSettings;
            }
            set
            {
                value = ProgramSettings;
            }
        }
    }
}

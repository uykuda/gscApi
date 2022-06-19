using System;
using System.IO;
namespace Api
{
    public class Paths
    {

        public static string AppData {
            get { string AppData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                return AppData;
            } set
            {
                value = AppData;
            }
            
        }
        public static string ProgramSettings
        {
            get
            {
                string ProgramSettings = AppData + @"\serverCreator\Settings.ini";
                return ProgramSettings;
            }
            set
            {
                value = ProgramSettings;
            }

        }

        public void controlExtensionsFolder()
        {
            
        }
        public void createExtensionsFolder()
        {
            Paths paths = new Paths();
            DirectoryInfo di = Directory.CreateDirectory(Paths.AppData + @"\serverCreator\Extensions");
        }
    }
}

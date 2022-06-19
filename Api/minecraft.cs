using System;
using System.Net;

namespace Api
{
    public class minecraft
    {
        Program program = new Program();
        public void minecraftCreate()
        {

            if (Program.serverSoftware == "vanilla")
            {
                try
                {
                    using (var client = new WebClient())
                    {
                        client.DownloadFile(System.Configuration.ConfigurationManager.AppSettings.Get(Program.serverVersion), Paths.AppData + @"\serverCreator\" + Program.serverName + ".jar");
                        Console.WriteLine(Program.serverVersion + " " + "file downloaded successfully.");
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

        }
    }
}

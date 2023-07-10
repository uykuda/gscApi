using DiscordRPC;
using DiscordRPC.Logging;
using System;

namespace Api
{
    class discord
    {
        public DiscordRpcClient client;

        public void Initialize()
        {
            try
            {
                minecraft minecraft = new minecraft();
                Program Program = new Program();

                client = new DiscordRpcClient("937388403517968475");

                //log düzeyi ayarı
                client.Logger = new ConsoleLogger() { Level = LogLevel.None };

                client.OnReady += (sender, e) =>
                {
                    Console.WriteLine("Hazır veren istemci: {0}", e.User.Username);
                };

                client.OnPresenceUpdate += (sender, e) =>
                {
                    Console.WriteLine("Istemci guncellendi! {0}", e.ApplicationID);
                };

                //discord client ile bağlantı
                client.Initialize();
                client.UpdateEndTime();
                client.SetPresence(new RichPresence()
                {
                    Details = "Creating Servers!",
                    State = "Now started " + Program.serverName,
                    Assets = new Assets()
                    {
                        LargeImageKey = "uyku",
                        LargeImageText = "https://github.com/uykuda/gscApi",
                    }
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                try
                {
                    client.Dispose();
                }
                catch (Exception d)
                {
                    Console.WriteLine(d);
                }
            }
        }
        public void Deinitialize()
        {
            try
            {
                client.Dispose();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}

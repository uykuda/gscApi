using DiscordRPC;
using DiscordRPC.Logging;
using System;

namespace Api
{
    class discord
    {
        public DiscordRpcClient client;
        //Called when your application first starts.
        //For example, just before your main loop, on OnEnable for unity.
        public void Initialize()
        {
            minecraft minecraft = new minecraft();
            Program Program = new Program();
            /*
			Create a Discord client
			NOTE: 	If you are using Unity3D, you must use the full constructor and define
					 the pipe connection.
			*/
            client = new DiscordRpcClient("937388403517968475");

            //Set the logger
            client.Logger = new ConsoleLogger() { Level = LogLevel.Warning };

            //Subscribe to events
            client.OnReady += (sender, e) =>
            {
                Console.WriteLine("Hazır veren istemci: {0}", e.User.Username);
            };

            client.OnPresenceUpdate += (sender, e) =>
            {
                Console.WriteLine("Istemci guncellendi! {0}", e.ApplicationID);
            };

            //Connect to the RPC
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
        public void Deinitialize()
        {
            client.Dispose();
        }
    }
}

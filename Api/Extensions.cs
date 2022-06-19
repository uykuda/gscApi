using System.IO;
using System;
using System.Collections;

namespace Api
{
    internal class Extensions
    {
        public static string ExtensionsPath = Paths.AppData + @"\serverCreator\Extensions\";
        public static void readExtensions()
        {
            ArrayList liste = new ArrayList();
            foreach (string s in Directory.GetDirectories(ExtensionsPath))
            {
                Console.Write(s.Remove(0, ExtensionsPath.Length) + "\n");
                liste.Add(s.Remove(0, ExtensionsPath.Length));
                Console.WriteLine(liste[1]);
            }
        }
        public static void loadExtensions()
        {
            if (!Directory.Exists(ExtensionsPath))
            {
                Directory.CreateDirectory(ExtensionsPath);
            }
        }
    }
}

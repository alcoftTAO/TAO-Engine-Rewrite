using System;
using System.Collections.Generic;
using System.IO;

namespace TAO.Engine
{
    public static class DiskData
    {
        public static bool AllowMods = true;

        public static bool CreateFile(string FileName)
        {
            if (!File.Exists(FileName))
            {
                File.Create(FileName).Close();
                return false;
            }

            return true;
        }

        public static bool CreateDirectory(string DirectoryName)
        {
            if (!Directory.Exists(DirectoryName))
            {
                Directory.CreateDirectory(DirectoryName);
                return false;
            }

            return true;
        }

        public static void CheckData()
        {
            if (AllowMods)
            {
                //Mods directories
                CreateDirectory("Mods/");
                CreateDirectory("Mods/Textures/");
                CreateDirectory("Mods/Textures/UChars/");
                CreateDirectory("Mods/Textures/LChars/");
                CreateDirectory("Mods/Textures/NChars/");
                CreateDirectory("Mods/Textures/SChars/");
                CreateDirectory("Mods/Audios/");
                CreateDirectory("Mods/Models/");
                CreateDirectory("Mods/Scenes/");
                CreateDirectory("Mods/OtherData/");
            }

            //Assets directories
            CreateDirectory("Assets/");
            CreateDirectory("Assets/Textures/");
            CreateDirectory("Assets/Textures/UChars/");
            CreateDirectory("Assets/Textures/LChars/");
            CreateDirectory("Assets/Textures/NChars/");
            CreateDirectory("Assets/Textures/SChars/");
            CreateDirectory("Assets/Audios/");
            CreateDirectory("Assets/Models/");
            CreateDirectory("Assets/Scenes/");
            CreateDirectory("Assets/OtherData/");

            //Log directories
            CreateDirectory("Logs/");
            CreateFile("Logs/latest.txt");
        }

        public static string[] GetAssetsFromDirectory(string Directory)
        {
            List<string> assets = new List<string>();
            DirectoryInfo dirInfo = new DirectoryInfo(Directory);
            FileInfo[] files = dirInfo.GetFiles();
            DirectoryInfo[] dirs = dirInfo.GetDirectories();

            for (int i = 0; i < files.Length; i++)
            {
                assets.Add(files[i].Name);
            }

            for (int i = 0; i < dirs.Length; i++)
            {
                string[] dirFiles = GetAssetsFromDirectory(Directory + dirs[i].Name);
                string dirName = dirs[i].Name;

                if (!dirName.EndsWith("/"))
                {
                    dirName += "/";
                }

                for (int a = 0; a < dirFiles.Length; a++)
                {
                    assets.Add(dirName + dirFiles[a]);
                }
            }

            return assets.ToArray();
        }
    }
}
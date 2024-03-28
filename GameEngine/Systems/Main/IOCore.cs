using GameEngine.GameData;
using System.Collections.Generic;
using System.IO;

namespace GameEngine.Systems.Main
{
    public static class IOCore
    {
        public static List<string> logs = new List<string>();

        public static void AddToLog(string log)
        {
            logs.Add($"[{System.DateTime.Now.TimeOfDay.ToString()}] {log}");
        }

        public static void AddToLogError(string log)
        {
            GameSettings.SaveLogs = true;
            logs.Add($"[{System.DateTime.Now.TimeOfDay.ToString()}] [Error] {log}");
        }

        public static void AddToLogWarning(string log)
        {
            GameSettings.SaveLogs = true;
            logs.Add($"[{System.DateTime.Now.TimeOfDay.ToString()}] [Warning] {log}");
        }

        public static void SaveLogs()
        {
            string file = null;

            if (logs.Count > 0)
            {
                AddToLog("Stop engine");
                foreach (string log in logs)
                {
                    file += log + "\r\n";
                }

                file += GameSettings.ToString();

                CreateTextFile("./../logs/Log " + System.DateTime.Now.ToString().Replace(':', '-').Replace('.', '-') + ".txt", file);
            }
        }

        public static string ReadTextFile(string FileName)
        {
            string file = null;
            StreamReader sr = File.OpenText(FileName);
            file = sr.ReadToEnd();
            sr.Close();
            return file;
        }

        public static void CreateTextFile(string FileName, string FileText)
        {
            StreamWriter sw = File.CreateText(FileName);
            sw.Write(FileText);
            sw.Close();
        }

        public static void SaveLocation(string FileName)
        {
            string file = Newtonsoft.Json.JsonConvert.SerializeObject(GameLocation.data);

            //file = CryptographyCore.EncryptString(file, "LOCATION");

            CreateTextFile(FileName, file);
            /*
                DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(Quadtree<Tile>));
                using (FileStream fs = new FileStream("./data/map.json", FileMode.OpenOrCreate))
                {
                    jsonFormatter.WriteObject(fs, GameCore.map.quadtreeFront);
                }
            */
            /*
            if (InputCore.isKeyTap(Keyboard.Key.F3))
            {
                DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(Quadtree<Tile>));
                using (FileStream fs = new FileStream("./data/map.json", FileMode.OpenOrCreate))
                {
                    GameCore.map.quadtreeFront = (Quadtree<Tile>)jsonFormatter.ReadObject(fs);
                }
            }
            */
        }
        /*
        public static void SaveJSONinFile(string FileName, Quadtree<Tile> obj)
        {
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(Quadtree<Tile>));
            using (FileStream fs = new FileStream(FileName, FileMode.OpenOrCreate))
            {
                jsonFormatter.WriteObject(fs, obj);
            }
        }
        */
        /*
        public static GameLocation LoadLocation(string FileName)
        {
            string file = ReadTextFile(FileName);

            //file = CryptographyCore.DecryptString(file, "LOCATION");
            //DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(Quadtree<Tile>));

            //quadtree = (Quadtree<Tile>)jsonFormatter.ReadObject(file);

            //loc = (GameLocation)Newtonsoft.Json.JsonConvert.DeserializeObject(file, typeof(GameLocation));

            //return loc;
        }*/

    }
}

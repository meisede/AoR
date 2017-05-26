using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using System.Xml.Serialization;
using ftpg.AoR.Entity;

namespace ftpg.Aor.Engine
{
    public class Storage
    {
        public static string GetStorageFolder()
        {
            var folder = HttpContext.Current.Server.MapPath("~/SavedGames/") + "Common"; //ConfigurationManager.AppSettings["StorageFolder"];

            if (!Directory.Exists(folder))
            {
                //Directory.CreateDirectory(folder);
            }
            return folder;
        }

        public static List<string> GetGames(string folder)
        {
            if (folder == string.Empty)
            {
                folder = "Common";
            }
            var path = HttpContext.Current.Server.MapPath("~/SavedGames/" + folder);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            var fileNames = Directory.GetFiles(path, "*.xml")
                                     .Select(Path.GetFileName)
                                     .ToList();
            for (var i = 0; i < fileNames.Count; i++)
            {
                fileNames[i] = fileNames[i].Remove(fileNames[i].Length - 4);
            }
            return fileNames;
        }

        public static Game GetGame(string name, string folder)
        {
            var game = new Game();
            try
            {
                var path = GetStorageFolder() + "//" + name + ".xml";

                using (var reader = new StreamReader(path))
                {
                    var serializer = new XmlSerializer(game.GetType());
                    var deserialized = serializer.Deserialize(reader.BaseStream);
                    game = (Game)deserialized;
                }
            }
            catch (Exception)
            {
            }
            return game;
        }

        public static void StoreGame(Game game, string folder)
        {
            Serialize(game, game.Name, GetStorageFolder() + "\\");
            MoveGameToHistory(game, folder);
        }

        private static void MoveGameToHistory(Game game, string folder)
        {
            var storageFolder = GetStorageFolder() + "\\History\\" + game.Name + "\\";
            if (!Directory.Exists(storageFolder))
            {
                Directory.CreateDirectory(storageFolder);
            }
            Serialize(game, DateTime.Now.ToString("yyMMdd_HHmmss") ,storageFolder);
        }

        private static void Serialize(Game game, string fileName, string folder)
        {
            var writer = new XmlSerializer(typeof(Game));
            var path =  folder + fileName + ".xml";

            try
            {
                //var file = new StreamWriter(path);
                var file = new StreamWriter(path);
                writer.Serialize(file, game);
                file.Close();
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            var test = SerializeToXml(game);
            //StoreDb(game);
        }


        public static XDocument SerializeToXml(object input)
        {
            var ser = new XmlSerializer(input.GetType());
            string result = string.Empty;

            using (MemoryStream memStm = new MemoryStream())
            {
                ser.Serialize(memStm, input);

                memStm.Position = 0;
                result = new StreamReader(memStm).ReadToEnd();
            }

            var xd = XDocument.Parse(result);
            return xd;
        } 

        private static bool StoreDb(Game game)
        {
            var strConnString = ConfigurationManager.AppSettings["dbconfstr"];
            var con = new SqlConnection(strConnString);
            var cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "CreateGame";
            cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = game.Name;
            cmd.Parameters.Add("@File", SqlDbType.Xml).Value = SerializeToXml(game).ToString();
            cmd.Connection = con;
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }

            catch (Exception ex)
            {
                throw ex;
            }
                finally
            {
                con.Close();
                con.Dispose();
            }

            return true;
        }
        public static string GetFile(string name, string folder)
        {
            var gameFile = string.Empty;

            try
            {
                var path = GetStorageFolder() + "/" + name + ".xml";
                
                using (var reader = new StreamReader(path))
                {
                    gameFile = reader.ReadToEnd();
                }
            }
            catch (Exception)
            {
            }
            return gameFile;
        }
        public static string SaveFile(string gameFile, string folder)
        {
            Game game;
            var serializer = new XmlSerializer(typeof(Game));
            using (var reader = new StringReader(@gameFile))
            {
                game = (Game)serializer.Deserialize(reader);
            }
            StoreGame(game, folder);
            return game.Name;
        }

        public static void DeleteGame(string name, string folder)
        {
            var path = GetStorageFolder() + "/" + name + ".xml";
            File.Delete(path);
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

//public static class SaveData 
//{
    //Player is for referencing data from progress of the game
   //public static void SavePlayer (Player player)
   //{
        //BinaryFormatter formatter = new BinaryFormatter();
        //string path = Application.persistentDataPath + "/player.ballers";
        //FileStream stream = new FileStream(path, FileMode.Create);

        //PlayerData data = new PlayerData(player);

        //formatter.Serialize(stream, data);
        //stream.Close();
   //}

    //public static PlayerData LoadPlayer ()
    //{
        //string path = Application.persistentDataPath + "/player.ballers";
        //if (File.Exists(path))
        //{
            //BinaryFormatter formatter = new BinaryFormatter();
            //FileStream stream = new Filestream(path, FileMode.Open);

            //PlayerData data = formatter.Deserialize(stream) as PlayerData;
            //stream.Close();

            //return data;
        //}

        //else
        //{
            //Debug.LogError("Save file not found in" + path);
            //return null;
        //}

    //}

//}

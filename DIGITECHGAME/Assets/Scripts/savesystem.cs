using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
public  static class savesystem
{
    public static void saveplayer(FlagPickUp stats)
    {
        Playerdata pdata = loadplayer();
        int totalcoins = pdata.coins;
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/stats.binary";
        FileStream stream = new FileStream(path, FileMode.Create);

        Playerdata data = new Playerdata(stats);
        data.coins += pdata.coins;
        formatter.Serialize(stream, data);
        stream.Close();
    }
    public static Playerdata loadplayer()
    {
        string path = Application.persistentDataPath + "/stats.binary";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            Playerdata data = formatter.Deserialize(stream) as Playerdata;
            stream.Close();
            return data;
                
        }
        else
        {
            Debug.LogError("savefilenotfound" + path);
            return null;
        }
    }
}

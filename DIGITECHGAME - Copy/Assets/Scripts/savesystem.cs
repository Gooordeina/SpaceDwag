using System.IO; //Provides functionalities for file handling
using UnityEngine; //Unity core functionalities
using System.Runtime.Serialization.Formatters.Binary; //For binary serialization

//Static class for saving and loading player and ship data
public static class savesystem
{
    //Method to save player data
    public static void saveplayer(FlagPickUp stats, MainMenu menu)
    {
        int totalcoins = 0; //Variable to hold the total coins

        //Load existing player data to get current coin count
        if (loadplayer() != null)
        {
            Playerdata pdata = loadplayer(); //Load existing player data
            totalcoins = pdata.coins; //Get current coins
        }

        BinaryFormatter formatter = new BinaryFormatter(); //Create a binary formatter
        string path = Application.persistentDataPath + "/stats.binary"; //Set path for the binary file
        FileStream stream = new FileStream(path, FileMode.Create); //Create a file stream for writing

        //Create a new Playerdata instance with the current stats and menu
        Playerdata data = new Playerdata(stats, menu);

        //Add existing coins to the new player data
        data.coins += totalcoins;

        //Serialize the player data to the binary file
        formatter.Serialize(stream, data);
        stream.Close(); //Close the file stream
    }

    //Method to load player data
    public static Playerdata loadplayer()
    {
        string path = Application.persistentDataPath + "/stats.binary"; //Set path for the binary file
        if (File.Exists(path)) //Check if the file exists
        {
            BinaryFormatter formatter = new BinaryFormatter(); //Create a binary formatter
            FileStream stream = new FileStream(path, FileMode.Open); //Open the file for reading
            Playerdata data = formatter.Deserialize(stream) as Playerdata; //Deserialize the player data
            stream.Close(); //Close the file stream
            return data; //Return the loaded player data
        }
        else
        {
            return null; //Return null if the file does not exist
        }
    }

    //Method to save ship data
    public static void saveship(MainMenu ship)
    {
        BinaryFormatter formatter = new BinaryFormatter(); //Create a binary formatter
        string path = Application.persistentDataPath + "/ship.binary"; //Set path for the ship binary file
        FileStream stream = new FileStream(path, FileMode.Create); //Create a file stream for writing

        //Create a new Shipdata instance with the current ship menu data
        Shipdata data = new Shipdata(ship);

        //Serialize the ship data to the binary file
        formatter.Serialize(stream, data);
        stream.Close(); //Close the file stream
    }

    //Method to load ship data
    public static Shipdata loadship()
    {
        string path = Application.persistentDataPath + "/ship.binary"; //Set path for the ship binary file
        if (File.Exists(path)) //Check if the file exists
        {
            BinaryFormatter formatter = new BinaryFormatter(); //Create a binary formatter
            FileStream stream = new FileStream(path, FileMode.Open); //Open the file for reading
            Shipdata data = formatter.Deserialize(stream) as Shipdata; //Deserialize the ship data
            stream.Close(); //Close the file stream
            return data; //Return the loaded ship data
        }
        else
        {
            return null; //Return null if the file does not exist
        }
    }
}
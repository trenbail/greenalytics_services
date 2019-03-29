<<<<<<< HEAD
﻿using System;
using System.IO; //used for file reading
using MySql.Data.MySqlClient;
using System.Collections.Generic; //used for lists
using dabaseAPI.connections;

namespace databaseAPI.plants
{
    class Plants : Connect
    {
        //Constructor call to base case - selecting 'plants' database
        public Plants() : base("plants") { }

        //This function needs to be called after each query function to close the connection
        public new void Close() { base.Close(); }

        //This function can take in a plantsID (which should be a string after being read in) or a plantName 
        //This function determines which type of input was selected and returns the string containing the other.   id->name or name->id
        public string Convert(string old)
        {
            //Seeing if the string is a plantID
            string[] numbers = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
            bool isID = false;
            foreach (string num in numbers)
            {
                if (old.StartsWith(num))
                {
                    isID = true;
                    break;
                }
            }
            //Constructing the correct query depending on input
            string query;
            if (isID == true) { query = "SELECT name FROM masterPlants WHERE plantID=" + old + ";"; }
            else { query = "SELECT plantID FROM masterPlants WHERE name = '" + old + "';"; }

            //Open connection
            Open();

            //Create Command
            MySqlCommand cmd = new MySqlCommand(query, connection);

            //Create a data reader and Execute the command
            MySqlDataReader dataReader = cmd.ExecuteReader();

            List<string> returnList = new List<string>();

            //reading the MySqlDataReader
            if (isID == true) { while (dataReader.Read()) { returnList.Add(dataReader["name"] + ""); } }
            else { while (dataReader.Read()) { returnList.Add(dataReader["plantID"] + ""); } }

            //Close connection
            Close();

            //return the new string
            return returnList[0];
        }

        //mySQL functions

        //public wrapper for 'Select * FROM __' in SelectALL in Connection - return type may need to be changed after talking to Zack
        public MySqlDataReader ShowAll(string tableName) { return SelectAll(tableName); }

        //returns information for 'plant info screen' - return type may need to be changed after talking to Zack
        public MySqlDataReader PlantData(string plantID)
        {
            //constructing query
            string query = "SELECT p.name, d.type, d.rainfall, d.temperature, d.humidity, d.sunlight, d.soil FROM masterPlants p, plantData d WHERE p.plantID = d.plantID AND p.plantID = " + plantID + ";";

            //Open connection
            Open();

            //Create Command
            MySqlCommand cmd = new MySqlCommand(query, connection);

            //Create a data reader and Execute the command
            MySqlDataReader dataReader = cmd.ExecuteReader();

            //return the MySqlDataReader to be used - may need to switch this after talking to zack
            return (dataReader);
        }

        //returns friends and enemies given a plant id - return type may need to be changed after talking to Zack
        public MySqlDataReader FriendsEnemies(string plantID)
        {
            //constructing query
            string query = "SELECT friends, enemies FROM friends_enemies WHERE plantID = " + plantID + ";";

            //Open connection
            Open();

            //Create Command
            MySqlCommand cmd = new MySqlCommand(query, connection);

            //Create a data reader and Execute the command
            MySqlDataReader dataReader = cmd.ExecuteReader();

            //return the MySqlDataReader to be used - may need to switch this after talking to zack
            return (dataReader);
        }

    }
}
=======
﻿using System;
using System.IO; //used for file reading
using MySql.Data.MySqlClient;
using System.Collections.Generic; //used for lists

class Plants : Connect
{
    //Constructor call to base case - selecting 'plants' database
    public Plants() : base("plants") { }

    //This function needs to be called after each query function to close the connection
    public new void Close() { base.Close(); }

    //This function can take in a plantsID (which should be a string after being read in) or a plantName 
    //This function determines which type of input was selected and returns the string containing the other.   id->name or name->id
    public string Convert(string old)
    {
        //Seeing if the string is a plantID
        string[] numbers = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
        bool isID = false;
        foreach (string num in numbers)
        {
            if (old.StartsWith(num))
            {
                isID = true;
                break;
            }
        }
        //Constructing the correct query depending on input
        string query;
        if (isID == true) { query = "SELECT name FROM masterPlants WHERE plantID=" + old + ";"; }
        else { query = "SELECT plantID FROM masterPlants WHERE name = '" + old + "';"; }

        //Open connection
        Open();

        //Create Command
        MySqlCommand cmd = new MySqlCommand(query, connection);

        //Create a data reader and Execute the command
        MySqlDataReader dataReader = cmd.ExecuteReader();

        List<string> returnList = new List<string>();

        //reading the MySqlDataReader
        if (isID == true) { while (dataReader.Read()) { returnList.Add(dataReader["name"] + ""); } }
        else { while (dataReader.Read()) { returnList.Add(dataReader["plantID"] + ""); } }

        //Close connection
        Close();

        //return the new string
        return returnList[0];
    }

    //mySQL functions

    //public wrapper for 'Select * FROM __' in SelectALL in Connection - return type may need to be changed after talking to Zack
    public MySqlDataReader ShowAll(string tableName) { return SelectAll(tableName); }

    //returns information for 'plant info screen' - return type may need to be changed after talking to Zack
    public MySqlDataReader PlantData(string plantID)
    {
        //constructing query
        string query = "SELECT p.name, d.type, d.rainfall, d.temperature, d.humidity, d.sunlight, d.soil FROM masterPlants p, plantData d WHERE p.plantID = d.plantID AND p.plantID = "+plantID+";";

        //Open connection
        Open();

        //Create Command
        MySqlCommand cmd = new MySqlCommand(query, connection);

        //Create a data reader and Execute the command
        MySqlDataReader dataReader = cmd.ExecuteReader();

        //return the MySqlDataReader to be used - may need to switch this after talking to zack
        return (dataReader);
    }

    //returns friends and enemies given a plant id - return type may need to be changed after talking to Zack
    public MySqlDataReader FriendsEnemies(string plantID)
    {
        //constructing query
        string query = "SELECT friends, enemies FROM friends_enemies WHERE plantID = " + plantID + ";";

        //Open connection
        Open();

        //Create Command
        MySqlCommand cmd = new MySqlCommand(query, connection);

        //Create a data reader and Execute the command
        MySqlDataReader dataReader = cmd.ExecuteReader();

        //return the MySqlDataReader to be used - may need to switch this after talking to zack
        return (dataReader);
    }

}
>>>>>>> 60a34fed314fb91e0a4ec77ef676709da83b5bab

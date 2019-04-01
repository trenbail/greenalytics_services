using System;
//using System.IO; //used for file reading
using MySql.Data.MySqlClient;
using System.Collections.Generic; //used for lists
using db.connections;
using System.Linq;

namespace db.plants
{

    public enum temperature_classes { low, med_low, med, med_high, high }
    public enum soil_classes { low, med, high } //This may need to change because I am unsure how we will categorize soil
    public enum sunlight_classes { low, med_low, med, med_high, high }

    public struct PlantInfo
    {
        public string Name;
        public string Type;
        public float Rainfall;
        public sunlight_classes Sunlight;
        public temperature_classes Temperature;
        public soil_classes Soil;
    }

    public class Plants : Connect
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
            //Open();

            //Create Command
            MySqlCommand cmd = new MySqlCommand(query, connection);

            //Create a data reader and Execute the command
            MySqlDataReader dataReader = cmd.ExecuteReader();

            List<string> returnList = new List<string>();

            //reading the MySqlDataReader
            if (isID == true) { while (dataReader.Read()) { returnList.Add(dataReader["name"] + ""); } }
            else { while (dataReader.Read()) { returnList.Add(dataReader["plantID"] + ""); } }

            //Close connection
            //Close();

            //return the new string
            return returnList.FirstOrDefault();
        }


        //mySQL functions

        //public wrapper for 'Select * FROM __' in SelectALL in Connection
        public MySqlDataReader ShowAll(string tableName)
        {
            return SelectAll(tableName); //todo fill out
        }

        //returns plantInfo struct given plantID - this should be changed to accept name
        public PlantInfo PlantData(string plantID)
        {
            //constructing query
            string query = "SELECT p.name, d.type, d.rainfall, d.temperature, d.sunlight, d.soil FROM masterPlants p, plantData d WHERE p.plantID = d.plantID AND p.plantID = " + plantID + ";";

            //Open connection
            Open();

            //Create Command
            MySqlCommand cmd = new MySqlCommand(query, connection);

            //Create a data reader and Execute the command
            MySqlDataReader dataReader = cmd.ExecuteReader();

            PlantInfo returnInfo = new PlantInfo();

            while (dataReader.Read())
            {
                returnInfo.Name = dataReader.GetString("name");
                returnInfo.Type = dataReader.GetString("type");
                returnInfo.Rainfall = dataReader.GetFloat("rainfall");
                Enum.TryParse<temperature_classes>(dataReader.GetString("temperature"), out returnInfo.Temperature);
                Enum.TryParse<sunlight_classes>(dataReader.GetString("sunlight"), out returnInfo.Sunlight);
                Enum.TryParse<soil_classes>(dataReader.GetString("soil"), out returnInfo.Soil);
            }

            //Closes connection
            Close();

            return (returnInfo);
        }

        //returns description for a plant - this should be changed to accept name
        public String Description(string plantID)
        {
            //constructing query
            string query = "SELECT description FROM description WHERE plantID = " + plantID + ";";

            //Open connection
            Open();

            //Create Command
            MySqlCommand cmd = new MySqlCommand(query, connection);

            //Create a data reader and Execute the command
            MySqlDataReader dataReader = cmd.ExecuteReader();

            string returnString = string.Empty;
             
            while (dataReader.Read())
            {
                returnString = dataReader.GetString("description");
            }

            return (returnString);
        }

        //returns friends given a plant id - this should be changed to accept name
        public MySqlDataReader Friends(string plantID)
        {
            //constructing query
            string query = "SELECT friends FROM friends WHERE plantID = " + plantID + ";";

            //Open connection
            Open();

            //Create Command
            MySqlCommand cmd = new MySqlCommand(query, connection);

            //Create a data reader and Execute the command
            MySqlDataReader dataReader = cmd.ExecuteReader();

            //return the MySqlDataReader to be used - may need to switch this after talking to zack
            return (dataReader);
        }

        //returns enemies given a plant id - this should be changed to accept name
        public MySqlDataReader Enemies(string plantID)
        {
            //constructing query
            string query = "SELECT enemies FROM enemies WHERE plantID = " + plantID + ";";

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
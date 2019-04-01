using System;
using db.connections;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using db.plants;
using db.gardens;

namespace db.groups
{

    public class Groups : Connect
    {
        //Constructor call to base case - selecting 'u_gardens' database
        public Groups() : base("plants") { }

        //This function needs to be called after each query function to close the connection
        public new void Close() { base.Close(); }

        //public wrapper for 'Select * FROM __' in SelectALL in Connection - return type may need to be changed after talking to Zack
        public MySqlDataReader ShowAll(string tableName) { return SelectAll(tableName); }

        //fix - include garden ID to so group names can be shared throughout gardens
        //This has a bug in it
        public string Convert(string userID, string name)
        {

            //constructing query
            string query = String.Format("SELECT groupID FROM hasGroups WHERE userID = '{0}' AND groupName = '{1}';", userID, name);

            //Open connection
            Open();

            //Create Command
            MySqlCommand cmd = new MySqlCommand(query, connection);

            //Create a data reader and Execute the command
            MySqlDataReader dataReader = cmd.ExecuteReader();

            string returnString = string.Empty;

            while (dataReader.Read())
            {
                returnString = dataReader.GetString("groupID");
            }

            Close();

            return returnString;
        }

        //Checks if a group name exists for a user and returns a bool 
        public bool Exists(string userID, string groupName)
        {
            //Converting gardenName to gardenID
            string groupID = Convert(userID, groupName);

            string query = string.Format("SELECT * FROM hasGroups WHERE userID = '{0}' AND groupID = '{1}'", userID, groupID);

            //Open connection
            Open();

            //Create Command
            MySqlCommand cmd = new MySqlCommand(query, connection);

            //Create a data reader and Execute the command
            MySqlDataReader dataReader = cmd.ExecuteReader();

            bool returnBool = dataReader.HasRows;

            Close();

            if (returnBool == true)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        //Adds a hardware given the hardware ID and the gardengroup name for it to be added to
        public void AddHardware(string userID, string groupName, string MACaddress)
        {
            string groupID = Convert(userID, groupName);

            string query = String.Format("INSERT INTO hasHardware VALUES('{0}','{1}')", MACaddress, groupID);

            //Open connection
            Open();

            //Create Command
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.ExecuteNonQuery();

            Close();

        }

        //Returns a string hardwareID given a garden groupName
        public string GetHardwareID(string userID, string groupName)
        {
            //convert groupName into groupID
            string groupID = Convert(userID, groupName);

            //construct query
            string query = String.Format("SELECT hardwareID FROM hasHardware WHERE groupID = '{0}';", groupID);

            //Open connection
            Open();

            //Create Command
            MySqlCommand cmd = new MySqlCommand(query, connection);

            //Create a data reader and Execute the command
            MySqlDataReader dataReader = cmd.ExecuteReader();

            string returnString = string.Empty;

            while (dataReader.Read())
            {
                returnString = dataReader.GetString("hardwareID");
            }

            Close();

            return (returnString);
        }

        //Adds a plant group to a user given the userID and gardenName
        public void AddGroup(string userID, string gardenName, Guid groupID_g, string groupName)
        {
            //convert garden name to gardenID
            Gardens tempgarden = new Gardens();
            string gardenID = tempgarden.Convert(userID, gardenName);
            //convert groupID_g to string
            string groupID = groupID_g.ToString();

            //constructing query
            string query = String.Format("INSERT INTO hasGroups VALUES('{0}','{1}','{2}','{3}');", userID, gardenID, groupID, groupName); 

            //Open connection
            Open();

            //Create Command
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.ExecuteNonQuery();

            Close();
        }

        //This function adds the given plant to the given garden group for the given user
        public void AddPlant(string userID, string groupName, string plantName)
        {
            //converting to plantID - doing this to allow adding by plantName
            Plants tempPlant = new Plants();
            string plantID = tempPlant.Convert(plantName);

            //Convert groupName to groupID given the user
            string groupID = Convert(userID, groupName);

            //constructing query - adds the plant to the group or if it is already there increases count by one
            string query = String.Format("INSERT INTO hasPlants(userID, groupID, plantID) VALUES('{0}','{1}',{2}) ON DUPLICATE KEY UPDATE quantity = quantity + 1", userID, groupID, plantID);

            //Open connection
            Open();

            //Create Command
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.ExecuteNonQuery();

            Close();
        }

        //This function returns a list of plant names inside a group
        public List<string> ListPlants(string userID, string groupName)
        {
            //constructing query
            string groupID = Convert(userID, groupName);
            string query = String.Format("SELECT p.name FROM masterPlants p, hasPlants h WHERE p.plantID = h.plantID AND groupID = '{0}';", groupID);

            //Open connection
            Open();

            //Create Command
            MySqlCommand cmd = new MySqlCommand(query, connection);

            //Create a data reader and Execute the command
            MySqlDataReader dataReader = cmd.ExecuteReader();

            List<string> returnList = new List<string>();

            while (dataReader.Read())
            {
                returnList.Add(dataReader.GetString("name"));
            }

            Close();

            return (returnList);
        }

        //This function returns a list of all plants a user has - duplicates removed
        public List<string> ListAllPlants(string userID)
        {
            //constructing query
            string query = String.Format("SELECT p.name FROM masterPlants p, hasPlants h WHERE p.plantID = h.plantID AND userID = '{0}';", userID);

            //Open connection
            Open();

            //Create Command
            MySqlCommand cmd = new MySqlCommand(query, connection);

            //Create a data reader and Execute the command
            MySqlDataReader dataReader = cmd.ExecuteReader();

            List<string> returnList = new List<string>();
            string temp;

            while (dataReader.Read())
            {
                //stores the current plant name
                temp = dataReader.GetString("name");
                //If this plant is not in the return list
                if(!returnList.Contains(temp))
                {
                    returnList.Add(temp);
                }
            }

            Close();

            return (returnList);
        }
    }
}
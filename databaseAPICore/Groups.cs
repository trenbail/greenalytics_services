using System;
using db.connections;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using db.plants;

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


        //This function adds the given plant to the given garden group for the given user
        public void AddPlant(string userID, Guid groupID_g, string plantName)
        {
            string groupID = groupID_g.ToString();
            //converting to plantID - doing this to allow adding by plantName
            Plants tempPlant = new Plants();
            string plantID = tempPlant.Convert(plantName);

            //constructing query - adds the plant to the group or if it is already there increases count by one
            string query = String.Format("INSERT INTO hasPlants(userID, groupID, plantID) VALUES('{0}','{1}',{2}) ON DUPLICATE KEY UPDATE quantity = quantity + 1", userID, groupID, plantID);

            //Open connection
            Open();

            //Create Command
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.ExecuteNonQuery();

            Close();
        }

        public List<string> ListGroup(string userID, Guid groupID_g)
        {
            //constructing query
            string groupID = groupID_g.ToString();
            string query = String.Format("SELECT p.name FROM masterPlants p, hasPlants h WHERE p.plantID = h.plantID AND userID = '{0}';", groupID);

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
    }
}
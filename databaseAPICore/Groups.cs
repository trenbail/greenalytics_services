using System;
using db.connections;
using MySql.Data.MySqlClient;
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


        public void AddPlant(string userID, Guid groupID_g, string plantName)
        {
            string groupID = groupID_g.ToString();
            //converting to plantID - doing this to allow adding by plantName
            Plants tempPlant = new Plants();
            string plantID = tempPlant.Convert(plantName);

            //constructing query
            string query = String.Format("INSERT INTO hasPlants(userID, groupID, plantID) VALUES('{0}','{1}',{2}) ON DUPLICATE KEY UPDATE quantity = quantity + 1", userID, groupID, plantID);

            //Open connection
            Open();

            //Create Command
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.ExecuteNonQuery();

            Close();
        }


    }
}
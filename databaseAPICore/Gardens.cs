using System;
using db.connections;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using db.groups;


namespace db.gardens
{

    public class Gardens : Connect
    {
        //Constructor call to base case - selecting 'u_gardens' database
        public Gardens() : base("plants") { }

        //This function needs to be called after each query function to close the connection
        public new void Close() { base.Close(); }

        //public wrapper for 'Select * FROM __' in SelectALL in Connection - return type may need to be changed after talking to Zack
        public MySqlDataReader ShowAll(string tableName) { return SelectAll(tableName); }

        //returns gardenID given userID and gardenName
        public string Convert(string userID, string name)
        {
       
            //constructing query
            string query = String.Format("SELECT gardenID FROM hasGardens WHERE userID = '{0}' AND gardenName = '{1}';", userID, name);

            //Open connection
            Open();

            //Create Command
            MySqlCommand cmd = new MySqlCommand(query, connection);

            //Create a data reader and Execute the command
            MySqlDataReader dataReader = cmd.ExecuteReader();

            string returnString = string.Empty;

            while (dataReader.Read())
            {
                returnString = dataReader.GetString("gardenID");
            }

            Close();

            return returnString;
        }

        //Checks if a garden name exists for a user and returns a bool 
        public bool Exists(string userID, string gardenName)
        {
            //Converting gardenName to gardenID
            string gardenID = Convert(userID, gardenName);

            string query = string.Format("SELECT * FROM hasGardens WHERE userID = '{0}' AND gardenID = '{1}'",userID, gardenID);

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

        //Adds a garden to a user account
        public void AddGarden(string userID, Guid gardenID_g, string gardenName)
        {

            //convert gardenID_g to string
            string gardenID = gardenID_g.ToString();

            //constructing query
            string query = String.Format("INSERT INTO hasGardens VALUES('{0}','{1}','{2}');", userID, gardenID, gardenName);

            //Open connection
            Open();

            //Create Command
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.ExecuteNonQuery();

            Close();
        }

        //Deletes a garden
        public bool DeleteGarden(string userID, string gardenName)
        {

            //convert gardenName to gardenID
            string gardenID = Convert(userID, gardenName);

            //constructing query
            string collect_groups = String.Format("SELECT groupName FROM hasGroups WHERE userID ='{0}' AND  gardenID = '{1}';", userID, gardenID);
            string query_delete_garden = String.Format("DELETE FROM hasGardens WHERE userID ='{0}' AND  gardenID = '{1}';", userID, gardenID);

            //Open connection
            Open();

            //Create Command
            MySqlCommand cmd = new MySqlCommand(collect_groups, connection);

            //Create a data reader and Execute the command
            MySqlDataReader dataReader = cmd.ExecuteReader();

            List<string> groups = new List<string>();

            while (dataReader.Read())
            {
                groups.Add(dataReader.GetString("groupName"));
            }

            Close();

            var accessgroups = new Groups();
   
            //Deletes each group inside a garden
            foreach (var group in groups)
            {
                accessgroups.DeleteGroup(userID, gardenName, group);
            }

            //Open connection
            Open();

            //Create Command
            MySqlCommand cmd2 = new MySqlCommand(query_delete_garden, connection);
            cmd2.ExecuteNonQuery();

            Close();

            //Delete successful
            return true;
        }

        //List all gardens that a user has
        public List<string> ListGardens(string userID)
        {

            //constructing query
            string query = String.Format("SELECT gardenName FROM hasGardens WHERE userID = '{0}';", userID);

            //Open connection
            Open();

            //Create Command
            MySqlCommand cmd = new MySqlCommand(query, connection);

            //Create a data reader and Execute the command
            MySqlDataReader dataReader = cmd.ExecuteReader();

            List<string> returnList = new List<string>();

            while (dataReader.Read())
            {
                returnList.Add(dataReader.GetString("gardenName"));
            }

            Close();

            return (returnList);
        }

        //List all groups inside a given garden for a user
        public List<string> ListGroups(string userID, string gardenName)
        {

            //Converting gardenName to gardenID
            string gardenID = Convert(userID, gardenName);
            //constructing query
            string query = String.Format("SELECT groupName FROM hasGroups WHERE userID = '{0}' AND gardenID = '{1}';", userID, gardenID);

            //Open connection
            Open();

            //Create Command
            MySqlCommand cmd = new MySqlCommand(query, connection);

            //Create a data reader and Execute the command
            MySqlDataReader dataReader = cmd.ExecuteReader();

            List<string> returnList = new List<string>();

            while (dataReader.Read())
            {
                returnList.Add(dataReader.GetString("groupName"));
            }

            Close();

            return (returnList);
        }
    }
}
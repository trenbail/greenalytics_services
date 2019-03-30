using System;
using db.connections;
using MySql.Data.MySqlClient;

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

    }
}
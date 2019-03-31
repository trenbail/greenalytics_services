using db.connections;
using MySql.Data.MySqlClient;
using System;
namespace db.sensors
{

    public class Sensors : Connect
    {
        //Constructor call to base case - selecting 'u_gardens' database
        public Sensors() : base("sensors") { }

        //This function needs to be called after each query function to close the connection
        public new void Close() { base.Close(); }

        //public wrapper for 'Select * FROM __' in SelectALL in Connection - return type may need to be changed after talking to Zack
        public MySqlDataReader ShowAll(string tableName) { return SelectAll(tableName); }


        //remove certain sensor
        public void RemoveTable(string MACaddress, string type)
        {
            //constructing query
            string query = String.Format("DELETE FROM {0} WHERE Macaddress = '{1}';", type, MACaddress);

            //Open connection
            Open();

            //Create Command
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.ExecuteNonQuery();

            Close();
        }

        // Used for sending data into the database
        public void Insert(string MACaddress, long time, string type, int value)
        {
            //constructing query
            string query = String.Format("INSERT INTO {0} VALUES('{1}',{2},{3});", type, MACaddress, time, value);

            //Open connection
            Open();

            //Create Command
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.ExecuteNonQuery();

            Close();
        }
    }
}
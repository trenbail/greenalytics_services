using dabaseAPI.connections;
using MySql.Data.MySqlClient;
using System;
namespace databaseAPI.sensors
{

    public class Sensors : Connect
    {
        //Constructor call to base case - selecting 'u_gardens' database
        public Sensors() : base("sensors") { }

        //This function needs to be called after each query function to close the connection
        public new void Close() { base.Close(); }

        //public wrapper for 'Select * FROM __' in SelectALL in Connection - return type may need to be changed after talking to Zack
        public MySqlDataReader ShowAll(string tableName) { return SelectAll(tableName); }

        public void CreateTable(string address)
        {
            //constructing query
            string query = "CREATE TABLE `" + address + "`(timeStamp BIGINT(20) NOT NULL,sensorID INT NOT NULL,type VARCHAR(20) NOT NULL,value BIGINT(20), PRIMARY KEY (timeStamp,sensorID));";
            //Open connection
            Open();

            //Create Command
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataReader newTable = cmd.ExecuteReader();

            Close();

            //Create a data reader and Execute the command
            //MySqlDataReader dataReader = cmd.ExecuteReader();

        }
    }
}
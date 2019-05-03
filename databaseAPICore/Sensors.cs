using db.connections;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System;
namespace db.sensors
{
    //TODO: add function to retrieve data from table

    public class Sensors : Connect
    {
        //Constructor call to base case - selecting 'u_gardens' database
        public Sensors() : base("sensors") { }

        //This function needs to be called after each query function to close the connection
        public new void Close() { base.Close(); }

        //public wrapper for 'Select * FROM __' in SelectALL in Connection - return type may need to be changed after talking to Zack
        public MySqlDataReader ShowAll(string tableName) { return SelectAll(tableName); }


        //remove  sensor given the id and type
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

            //Query wont run unless it is in the bounds
            bool runQ = false;

            //So I can commit
            if (type == "temperature")
            {
                if (value < 100 && value > 30)
                {
                    runQ = true;
                }
            }
            if (type == "humidity")
            {
                if (value < 80 && value > 0)
                {
                    runQ = true;
                }
            }

            if(runQ == true)
            {
                //Open connection
                Open();

                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();

                Close();
            }

        }

        public string getMacByAcctANDpgName(string accountID, string pgName)
        {
            var g = new groups.Groups();
            return g.GetHardwareID(accountID, pgName);
        }

        //used for pulling hardware data
        //returns [time][value]
        public List<List<long>> pulldata(string MACaddress, string type, int time)
        {

            //constructing query - This returns all the values from the selected table that match a given MACaddress and are from a time greater than the given time
            string query = String.Format("SELECT time, value FROM {0} WHERE MACaddress = '{1}' AND time > {2};", type, MACaddress, time);

            //Open connection
            Open();

            //Create Command
            MySqlCommand cmd = new MySqlCommand(query, connection);

            //Create a data reader and Execute the command
            MySqlDataReader dataReader = cmd.ExecuteReader();


            List<List<long>> valueList = new List<List<long>>();

            //Read the database
            while (dataReader.Read())
            {
                // array(x: time, y: value)
                valueList.Add(new List<long> { dataReader.GetInt64("time"), dataReader.GetInt64("value") });
            }

            Close();

            return valueList;
        }

        public int getLatestSensor(string userID, string pgName, string type)
        {
            string MAC = getMacByAcctANDpgName(userID, pgName);
            string query = String.Format(
                @"
                SELECT time, value FROM {0} WHERE MACaddress='{1}' ORDER BY time DESC LIMIT 1
                ", type, MAC
                );
            Open();
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            long value = 0;
            dataReader.Read();
            try
            {

                value = dataReader.GetInt64("value");
                long time = dataReader.GetInt64("time");
            } catch
            {

            }
            Close();
            return (int)value;
        }
        public int getLatestTemperature(string userID, string pgName) { return getLatestSensor(userID, pgName, "temperature"); }
        public int getLatestHumidity(string userID, string pgName) { return getLatestSensor(userID, pgName, "humidity"); }
    }
}
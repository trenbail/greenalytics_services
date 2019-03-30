using db.connections;
using MySql.Data.MySqlClient;
using System;

namespace db.users
{

    public class Users : Connect
    {
        //Constructor call to base case - selecting 'users' database
        public Users() : base("users") { }

        //This function needs to be called after each query function to close the connection
        public new void Close() { base.Close(); }

        //public wrapper for 'Select * FROM __' in SelectALL in Connection - return type may need to be changed after talking to Zack
        public MySqlDataReader ShowAll(string tableName) { return SelectAll(tableName); }

        //adds new user
        public void Add(int id, string name, string password)
        {
            //constructing query
            string query = "INSERT INTO userInfo VALUES (" + id + ", \"" + name + "\", \"" + password + "\");";

            //Open connection
            Open();

            //Create Command
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.ExecuteNonQuery();

            Close();
        }

    }

}
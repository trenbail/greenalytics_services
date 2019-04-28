using db.connections;
using MySql.Data.MySqlClient;
using System;

namespace db.users
{

    //TODO clean up, fix add user to check for existence, create login, create edit, create delete

    public class Users : Connect
    {
        //Constructor call to base case - selecting 'users' database
        public Users() : base("users") { }

        //This function needs to be called after each query function to close the connection
        public new void Close() { base.Close(); }

        //public wrapper for 'Select * FROM __' in SelectALL in Connection - return type may need to be changed after talking to Zack
        public MySqlDataReader ShowAll(string tableName) { return SelectAll(tableName); }

        //adds new user
        public string Signup(string name, string password)
        {
            //Trys to login to the system
            string userExists = Login(name, password);

            //If Login was successful, then this user already exists in the system and therfore Login will return a name
            if (userExists.Length > 0)
            {
                return string.Empty;
            }

            //constructing query
            string query = String.Format("INSERT INTO userInfo(userName, password) VALUES ('{0}','{1}');", name, password);

            //Open connection
            Open();

            try
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
            }
            catch
            {
                Close();
                return string.Empty;
            }

            Close();

            //Signup successful 
            return name;
        }

        //Checks if the user is in the database and that the given password matches the user
        public string Login(string name, string password)
        {
            //constructing query
            string query = String.Format("SELECT userName, password FROM userInfo WHERE userName = '{0}' AND password = '{1}';", name, password);

            //Open connection
            Open();

            //Create Command
            MySqlCommand cmd = new MySqlCommand(query, connection);

            //Create a data reader and Execute the command
            MySqlDataReader dataReader = cmd.ExecuteReader();

            //Setting database 'read' string to empty
            string db_userName = string.Empty;
            string db_password = string.Empty;

            //Read the database
            while (dataReader.Read())
            {
                db_userName = dataReader.GetString("userName");
                db_password = dataReader.GetString("password");
            }

            Close();

            //Checking to see if either the username or password is empty (not found in the database)
            if ((db_userName.Length == 0) | (db_password.Length == 0))
            {
                return string.Empty;
            }

            //The username and password were found in the database
            return db_userName;
        }

        //Updates user's token
        public bool SetToken(string name, string token)
        {
            //constructing query
            string query = String.Format("update userInfo set token = '{0}' where userName = '{1}';", token, name);

            //Open connection
            Open();

            try
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
            }
            catch
            {
                //Error with token update
                Close();
                return false;
            }

            Close();

            //Token updated
            return true;

        }

        public string GetToken(string name)
        {
            //constructing query
            string query = String.Format("SELECT token FROM userInfo WHERE userName = '{0}';", name);

            //Open connection
            Open();

            //Setting database 'read' string to empty
            string token = string.Empty;

            try
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);

                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();



                //Read the database
                while (dataReader.Read())
                {
                    token = dataReader.GetString("token");
                }
            }
            catch
            {
                Close();
                return string.Empty;
            }

            Close();

            //Token updated
            return token;

        }

    }

}
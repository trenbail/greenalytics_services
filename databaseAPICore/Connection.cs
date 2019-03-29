using System;
using MySql.Data.MySqlClient;


namespace db.connections
{


    public class Connect
    {

        //Class variables
        protected MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;

        //Constructor - Called from all subclasses - input is the name of the desired database you want to connect to
        protected Connect(string database) { Initialize(database); }

        //Initialize values for connection - this has a field that needs to change upon server restart
        private void Initialize(string inputDatabase)
        {

            server = "greenalytics.ga"; //swithces to local host when on the server  |  Needs to be changed with each server launch
            database = inputDatabase;
            uid = "ubuntu";
            password = "cornisgood";
            string connectionString;

            connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            connection = new MySqlConnection(connectionString);
        }

        //Wrapper for OpenConnection
        protected bool Open() { return OpenConnection(); }

        //open connection to database
        private bool OpenConnection()
        {
            try
            {
                connection.Open();
                //Console.WriteLine("Connection success"); //for testing
                return true;
            }
            catch (MySqlException ex)
            {
                //0: Connection with server can not be found   |   1045: Error with username or password
                Console.WriteLine(ex.Number); //for testing
                return false;
            }
        }

        //Wrapper for CloseConnection
        protected bool Close() { return CloseConnection(); }

        //Close connection to database
        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                //Console.WriteLine("Close success"); //for testing
                return true;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex); //for testing
                return false;
            }
        }



        //Shared mySQL functions

        //Select all statement - avaliable to all subclasses
        protected MySqlDataReader SelectAll(string tableName)
        {
            //constructing query
            string queryTemp = "SELECT * FROM ";
            string query = queryTemp + tableName;

            //Open connection
            Open();

            //Create Command
            MySqlCommand cmd = new MySqlCommand(query, connection);

            //Create a data reader and Execute the command
            MySqlDataReader dataReader = cmd.ExecuteReader();

            //return the MySqlDataReader to be used - may need to switch this after talking to zack
            return (dataReader);
        }
    }
}
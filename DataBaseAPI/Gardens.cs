using System;
using MySql.Data.MySqlClient;

class Gardens : Connect
{
    //Constructor call to base case - selecting 'u_gardens' database
    public Gardens() : base("u_gardens") { }

    //This function needs to be called after each query function to close the connection
    public new void Close() { base.Close(); }

    //public wrapper for 'Select * FROM __' in SelectALL in Connection - return type may need to be changed after talking to Zack
    public MySqlDataReader ShowAll(string tableName) { return SelectAll(tableName); }

}

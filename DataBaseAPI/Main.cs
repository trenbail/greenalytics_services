using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace testfunctions
{
    class Test
    {
        public static void Main(string[] args)
        {
            //Connect testconn = new Connect();
            Plants testPlant = new Plants();
            MySqlDataReader plantlist = testPlant.ShowAll("masterPlants");
            //Console.WriteLine(testPlant.Convert("Corn"));
            //MySqlDataReader plantlist = testPlant.FriendsEnemies(testPlant.Convert("Corn"));

            Sensors testSensor = new Sensors();
            testSensor.CreateTable("00-14-22-01-23-45");

            //interact with plantlist HERE


            //this closes plantlist
            //testPlant.Close();       
            Console.WriteLine("Done");
        }
    }
}

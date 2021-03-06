using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite; //necessary to connect with SQLite

public class DBConnection : MonoBehaviour
{
    protected static string dbShortName = "WannaWanna";
    protected static string dbName = "URI=file:WannaWanna.db";


    public static void CreateDB()
    {
        //Creates the Database connection
        using(var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            //Set up an object called "command" to query into the database
            using(var command = connection.CreateCommand())
            {
                command.CommandText = "CREATE TABLE IF NOT EXISTS flappy_ranking("
                    + "id_flappy_ranking INTEGER PRIMARY KEY, "
                    + "nick VARCHAR(10) NOT NULL, "
                    + "score INTEGER NOT NULL DEFAULT 0, "
                    + "timestamp VARCHAR(20) NOT NULL);"; //SQLite don't have timestamp...
                command.ExecuteNonQuery();
            }

            connection.Close();
        }
    }
}

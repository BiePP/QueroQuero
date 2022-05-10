using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite; //necessary to connect with SQLite
using System.Data; //necessary to bring data from database

public class FlappyQueries : DBConnection
{

    private static string tableName = "flappy_ranking";

    private static string GetDateTime()
    {
        return System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
    }

    public static void AddScore(int totalScore, string nickname)
    {
        string datetime = GetDateTime();
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            using(var command = connection.CreateCommand())
            {
                command.CommandText = "INSERT INTO " + tableName + " "
                    + "(score, nick, timestamp) VALUES ("
                    + totalScore + ", "
                    + "'" + nickname + "', "
                    + "'" + datetime + "'"
                    + ");";
                command.ExecuteNonQuery();
            }

            connection.Close();
        }
    }

    public static List<List<string>> Display10thBestScore()
    {
        List<List<string>> results = new List<List<string>>();
        List<string> score = new List<string>();

        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            using(var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT score, nick FROM " + tableName + " "
                    + "ORDER BY score DESC "
                    + "LIMIT 10"
                    + ";";
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        /*Debug.Log("Score: " + reader["score"] + "\t"
                            + "Nick: " + reader["nick"] + "\t"
                            + "Timestamp: " + reader["timestamp"]);*/
                        results.Add(new List<string> { 
                            reader["nick"].ToString(),
                            reader["score"].ToString()
                        });
                    }
                }
            }
            connection.Close();
            return results;
        }
    }
}

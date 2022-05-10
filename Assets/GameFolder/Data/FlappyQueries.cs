using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite; //necessary to connect with SQLite
using System.Data; //necessary to bring data from database

public class FlappyQueries : DBConnection
{

    private static string tableName = "flappy_ranking";
    private static int maxRows = 10;

    private static string GetDateTime()
    {
        return System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
    }

    public static void AddScore(int totalScore, string nickname)
    {
        string datetime = GetDateTime();
        //stabilish connection
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            //creates the first query (a search for the number of entries into the table)
            using(var commandSearch = connection.CreateCommand())
            {
                commandSearch.CommandText = "SELECT COUNT(1) FROM " + tableName + ";";
                using (IDataReader reader = commandSearch.ExecuteReader())
                {
                    //tests if the number of entries is bigger than the maxRows permitted (default 10)
                    if (int.Parse(reader[0].ToString()) >= maxRows)
                    {
                        //TRUE: update the "last one" with lesser points
                        using (var command = connection.CreateCommand())
                        {
                            command.CommandText = "UPDATE " + tableName + " "
                            + "SET score = '" + totalScore + "', "
                            + "nick = '" + nickname + "', "
                            + "timestamp = '" + datetime + "'"
                            + "WHERE score = (SELECT MIN(score) FROM " + tableName + ");";
                            command.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        //FALSE can entry a new row, insert the new score
                        using (var command = connection.CreateCommand())
                        {
                            command.CommandText = "INSERT INTO " + tableName + " "
                            + "(score, nick, timestamp) VALUES ("
                            + totalScore + ", "
                            + "'" + nickname + "', "
                            + "'" + datetime + "'"
                            + ");";
                            command.ExecuteNonQuery();
                        }
                    }
                    reader.Close();   
                }
            }
            connection.Close();
        }
    }

    public static List<List<string>> Display10thBestScore()
    {
        //Creates to lists: results (list of scores) and score (nick and points)
        List<List<string>> results = new List<List<string>>();
        List<string> score = new List<string>();

        //stabilish connection
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            //uses the command for reading
            using(var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT score, nick FROM " + tableName + " "
                    + "ORDER BY score DESC "
                    + "LIMIT 10"
                    + ";";
                using (IDataReader reader = command.ExecuteReader())
                {
                    //foreach result...
                    while (reader.Read())
                    {
                        results.Add(new List<string> { 
                            reader["nick"].ToString(),
                            reader["score"].ToString()
                        });
                    }
                    reader.Close();
                }
            }
            connection.Close();
            return results;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite; //necessary to connect with SQLite

public class FlappyQueries : DBConnection
{
    private string GetDateTime()
    {
        return System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
    }

    public void AddScore(int totalScore, string nickname)
    {
        string datetime = GetDateTime();
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            using(var command = connection.CreateCommand())
            {
                command.CommandText = "INSERT INTO flappy_ranking "
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
}

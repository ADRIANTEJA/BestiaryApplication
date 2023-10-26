using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BestiaryApplication.GamesModule.Model;
using Microsoft.Data.Sqlite;

namespace BestiaryApplication.GamesModule.Repository
{
    public class GameRespository 
    {
        public static void InsertGame(Game game)
        {
            using (var connection = new SqliteConnection(App.DatabasePath))
            {
                connection.Open();
                var command = connection.CreateCommand(); 

                command.CommandText = @"INSERT INTO game (name, registeredCreatures, dateRegistered, imageIcon)
                                        VALUES ($name, $registeredCreatures, $dateRegistered, $imageIcon)";

                command.Parameters.Add(new SqliteParameter("$name", game.Name));
                command.Parameters.Add(new SqliteParameter("$registeredCreatures", game.RegisteredCreatures));
                command.Parameters.Add(new SqliteParameter("$dateRegistered", game.DateRegistered));
                command.Parameters.Add(new SqliteParameter("$imageIcon", game.ImageIcon));
                command.ExecuteNonQuery();
            }
        }

        public static void UpdateGame(Game game)
        {
            var id = game.Id;
            var name = game.Name;
            var registeredCreatures = game.RegisteredCreatures;
            var dateRegistered = game.DateRegistered;
            var imageIcon = game.ImageIcon;

            using (var connection = new SqliteConnection(App.DatabasePath))
            {
                connection.Open();
                var command = connection.CreateCommand();

                command.CommandText = @"UPDATE game SET name = $name,
                                        registeredCreatures = $registeredCreatures,
                                        dateRegistered = $dateRegistered, 
                                        imageIcon = $imageIcon WHERE id = $id";

                command.Parameters.Add(new SqliteParameter("$id", id));
                command.Parameters.Add(new SqliteParameter("$name", name));
                command.Parameters.Add(new SqliteParameter("$registeredCreatures", registeredCreatures));
                command.Parameters.Add(new SqliteParameter("$dateRegistered", dateRegistered));
                command.Parameters.Add(new SqliteParameter("$imageIcon", imageIcon));
                command.ExecuteNonQuery();
            }
        }

        public static void UpdateGameImageIcon(byte[] imageIcon, int id)
        {
            using (var connection = new SqliteConnection(App.DatabasePath))
            {
                connection.Open();

                var command = connection.CreateCommand();

                command.CommandText = $@"UPDATE game SET imageIcon = $imageIcon
                                         WHERE id = $id";
                command.Parameters.Add(new SqliteParameter("$imageIcon", imageIcon));
                command.Parameters.Add(new SqliteParameter("$id", id));
                command.ExecuteNonQuery();
            }
        }

        public static void DeleteGame(int id)
        {
            using (var connection = new SqliteConnection(App.DatabasePath))
            {
                connection.Open();

                using (var transaction =  connection.BeginTransaction())
                {
                    var command = connection.CreateCommand();

                    command.Transaction = transaction;
                    command.CommandText = "DELETE FROM creature WHERE gameId = $id";
                    command.Parameters.Add(new SqliteParameter("$id", id));
                    command.ExecuteNonQuery();

                    command.CommandText = $"DELETE FROM game WHERE id = $id";
                    command.ExecuteNonQuery();

                    transaction.Commit();
                }   
            }  
        }

        public static List<Game> QueryAllGames()
        {
            List<Game> games = new();

            using (var connection = new SqliteConnection(App.DatabasePath)) 
            {
                connection.Open();

                var command = connection.CreateCommand();

                command.CommandText = "SELECT * FROM game";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var game = new Game
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            RegisteredCreatures = reader.GetInt32(2),
                            DateRegistered = reader.GetInt64(3),
                            ImageIcon = reader.IsDBNull(4) ? null : (byte[])reader["imageIcon"],
                        };

                        games.Add(game);
                    }
                }
            }
            return games;
        }

        public static List<Game> QueryGameByName(string name)
        {
            List<Game> games = new();

            using (var connection = new SqliteConnection(App.DatabasePath))
            {
                connection.Open();

                var command = connection.CreateCommand();

                command.CommandText = $"SELECT * FROM game WHERE name = $name";
                command.Parameters.Add(new SqliteParameter("$name", name));

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Game game = new()
                        {
                            Name = reader.GetString(1),
                            RegisteredCreatures = reader.GetInt32(2),
                            DateRegistered = reader.GetInt64(3),
                            ImageIcon = reader.IsDBNull(4) ? null : (byte[])reader["ImageIcon"],
                        };

                        games.Add(game);
                    }

                    return games;
                }
            }
        }
    }
}

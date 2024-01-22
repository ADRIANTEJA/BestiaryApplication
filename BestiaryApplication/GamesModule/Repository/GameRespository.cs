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

                command.CommandText = @"INSERT INTO games (name, genre, registeredCreatures, dateRegistered, imageIcon)
                                        VALUES ($name, $genre, $registeredCreatures, $dateRegistered, $imageIcon)";

                command.Parameters.Add(new SqliteParameter("$name", game.Name));
                command.Parameters.Add(new SqliteParameter("$genre", game.Genre));
                command.Parameters.Add(new SqliteParameter("$registeredCreatures", game.RegisteredCreatures));
                command.Parameters.Add(new SqliteParameter("$dateRegistered", game.DateRegistered));
                command.Parameters.Add(new SqliteParameter("$imageIcon", game.ImageIcon));
                command.ExecuteNonQuery();
            }
        }

        public static void UpdateGame(Game game)
        {
            using (var connection = new SqliteConnection(App.DatabasePath))
            {
                connection.Open();
                var command = connection.CreateCommand();

                command.CommandText = @"UPDATE games SET name = $name, 
                                        imageIcon = $imageIcon WHERE id = $id";

                command.Parameters.Add(new SqliteParameter("$id", game.Id));
                command.Parameters.Add(new SqliteParameter("$name", game.Name));
                command.Parameters.Add(new SqliteParameter("$imageIcon", game.ImageIcon));
                command.ExecuteNonQuery();
            }
        }

        public static void UpdateGameRegisteredCreatures(int gameId)
        {
            using (var connection = new SqliteConnection(App.DatabasePath))
            {
                connection.Open();

                var command = connection.CreateCommand();

                command.CommandText = @"UPDATE games SET registeredCreatures = registeredCreatures + 1
                                        WHERE id = $gameId";
                command.Parameters.Add(new SqliteParameter("$gameId", gameId));
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
                    command.CommandText = "DELETE FROM creatures WHERE gameId = $id";
                    command.Parameters.Add(new SqliteParameter("$id", id));
                    command.ExecuteNonQuery();

                    command.CommandText = $"DELETE FROM games WHERE id = $id";
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

                command.CommandText = "SELECT * FROM games";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var game = new Game
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Genre = reader.GetString(2),
                            RegisteredCreatures = reader.GetInt32(3),
                            DateRegistered = reader.GetInt64(4),
                            ImageIcon = (byte[])reader["imageIcon"],
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

                command.CommandText = $"SELECT * FROM games WHERE name = $name";
                command.Parameters.Add(new SqliteParameter("$name", name));

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Game game = new()
                        {
                            Name = reader.GetString(1),
                            Genre = reader.GetString(2),
                            RegisteredCreatures = reader.GetInt32(3),
                            DateRegistered = reader.GetInt64(4),
                            ImageIcon = (byte[])reader["ImageIcon"],
                        };

                        games.Add(game);
                    }

                    return games;
                }
            }
        }
    }
}

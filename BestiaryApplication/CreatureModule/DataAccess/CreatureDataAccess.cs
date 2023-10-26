using BestiaryApplication.CreatureModule.Model;
using BestiaryApplication.GamesModule.Model;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

namespace BestiaryApplication.CreatureModule.DataAccess
{
    public class CreatureDataAccess
    {
        public static void InsertCreature(Creature creature)
        {
            using (var connection = new SqliteConnection(App.DatabasePath)) 
            {
                connection.Open();
                var command = connection.CreateCommand();

                command.CommandText = @"INSERT INTO creature(name, type, element, strongPoint, weakPoint, description, imageIcon, gameId)
                                        VALUES ($name, $type, $element, $strongPoint, $weakPoint, $description, $imageIcon, $gameId)";

                command.Parameters.Add(new SqliteParameter("$name", creature.Name));
                command.Parameters.Add(new SqliteParameter("$type", creature.Type));
                command.Parameters.Add(new SqliteParameter("$element", creature.Element.ToString()));
                command.Parameters.Add(new SqliteParameter("$strongPoint", creature.StrongPoint));
                command.Parameters.Add(new SqliteParameter("$weakPoint", creature.WeakPoint));
                command.Parameters.Add(new SqliteParameter("$description", creature.Description));
                command.Parameters.Add(new SqliteParameter("$imageIcon", creature.ImageIcon));
                command.Parameters.Add(new SqliteParameter("$gameId", creature.GameId));
                command.ExecuteNonQuery();
            }
        }

        public static void UpdateCreature(Creature creature) 
        {
            var id = creature.Id;
            var name = creature.Name;
            var type = creature.Type;
            var element = creature.Element;
            var strongPoint = creature.StrongPoint;
            var weakPoint = creature.WeakPoint;
            var description = creature.Description;
            var imageIcon = creature.ImageIcon;

            using (var connection  = new SqliteConnection(App.DatabasePath))
            {
                connection.Open();

                var command = connection.CreateCommand();

                command.CommandText = @"UPDATE creature SET name = $name, type = $type,
                                        element = $element, strongPoint = $strongPoint,
                                        weakPoint = $weakPoint, description = $description,
                                        imageIcon = $imageIcon WHERE id = $id";

                command.Parameters.Add(new SqliteParameter("$id", id));
                command.Parameters.Add(new SqliteParameter("$name", name));
                command.Parameters.Add(new SqliteParameter("$type", type));
                command.Parameters.Add(new SqliteParameter("$element", element.ToString()));
                command.Parameters.Add(new SqliteParameter("$strongPoint", strongPoint));
                command.Parameters.Add(new SqliteParameter("$weakPoint", weakPoint));
                command.Parameters.Add(new SqliteParameter("$description", description));
                command.Parameters.Add(new SqliteParameter("$imageIcon", imageIcon));
                command.ExecuteNonQuery();
            }
        }

        public static void DeleteCreature(int id)
        {
            using (var connection = new SqliteConnection(App.DatabasePath))
            {
                connection.Open();

                var commmand = connection.CreateCommand();

                commmand.CommandText = "DELETE FROM creature WHERE id = $id";
                commmand.Parameters.Add(new SqliteParameter("id", id));
                commmand.ExecuteNonQuery();
            }
        }

        public static List<Creature> QueryAllCreatures()
        {
            List<Creature> creatures = new List<Creature>();

            using(var connection = new SqliteConnection(App.DatabasePath))
            {
                connection.Open();

                var command = connection.CreateCommand();

                command.CommandText = @"SELECT * FROM creature";

                using(var reader = command.ExecuteReader()) 
                {
                    while (reader.Read())
                    {
                        var creature = new Creature
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Type = reader.GetString(2),
                            Element = (Creature.element)Enum.Parse(typeof(Creature.element), reader.GetString(3)),
                            StrongPoint = reader.GetString(4),
                            WeakPoint = reader.GetString(5),
                            Description = reader.GetString(6),
                            ImageIcon = reader.IsDBNull(7) ? null : reader["imageIcon"] as byte[],
                            GameId = reader.GetInt32(8),
                        };

                        creatures.Add(creature);
                    }
                }
            }
            return creatures;
        }

        public static List<Creature> QueryGameCreatures(int gameId)
        {
            List<Creature> creatures = new();

            using(var connection = new SqliteConnection(App.DatabasePath))
            {
                connection.Open();

                var command = connection.CreateCommand();

                command.CommandText = @"SELECT creature.* FROM game 
                                        INNER JOIN creature ON game.id = creature.gameId
                                        WHERE game.id = @gameId";

                command.Parameters.Add(new SqliteParameter("@gameId", gameId));

                using(var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var creature = new Creature
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Type = reader.GetString(2),
                            Element = (Creature.element)Enum.Parse(typeof(Creature.element),
                                       reader.GetString(3)),
                            StrongPoint = reader.GetString(4),
                            WeakPoint = reader.GetString(5),
                            Description = reader.GetString(6),
                            ImageIcon = reader.IsDBNull(7) ? null : reader["imageIcon"] as byte[],
                            GameId = reader.GetInt32(8),
                        };
                        creatures.Add(creature);
                    }
                }
            }
            return creatures;
        }

        public static List<Creature> QueryCreatureByName(string name)
        {
            var creatures = new List<Creature>();

            using (var connection = new SqliteConnection(App.DatabasePath))
            {
                connection.Open();

                var command = connection.CreateCommand();

                command.CommandText = "SELECT * FROM creature WHERE name = $name";
                command.Parameters.Add(new SqliteParameter("$name", name));

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read()) 
                    {
                        var creature = new Creature
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Type = reader.GetString(2),
                            Element = (Creature.element)Enum.Parse(typeof(Creature.element),
                            reader.GetString(3)),
                            StrongPoint = reader.GetString(4),
                            WeakPoint = reader.GetString(5),
                            Description = reader.GetString(6),
                        };
                        creatures.Add(creature);
                    }
                }
            }
            return creatures;
        }
    }
}

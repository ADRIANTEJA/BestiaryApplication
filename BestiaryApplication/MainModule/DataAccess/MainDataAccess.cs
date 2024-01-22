using BestiaryApplication.CreatureModule.Model;
using BestiaryApplication.MainModule.Model;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestiaryApplication.MainModule.DataAccess
{
    public  class MainDataAccess
    {
        public static void InsertLastConsultedCreature(Creature creature)
        {
            using (var connection = new SqliteConnection(App.DatabasePath)) 
            {
                connection.Open();

                var command = connection.CreateCommand();

                command.CommandText = @"INSERT INTO LastConsultedCreature (name, imageIcon, description)
                                        VALUES ($name, $imageIcon, $description)";

                command.Parameters.Add(new SqliteParameter("$name", creature.Name));
                command.Parameters.Add(new SqliteParameter("$imageIcon", creature.ImageIcon));
                command.Parameters.Add(new SqliteParameter("$description", creature.Description));
                command.ExecuteNonQuery();
            }
        }

        public static void DeleteLastConsultedCreature()
        {
            using (var connection = new SqliteConnection(App.DatabasePath))
            {
                connection.Open();

                var command = connection.CreateCommand();

                command.CommandText = "DELETE FROM LastConsultedCreature";

                command.ExecuteNonQuery();
            }
        }

        public static LastConsultedCreature QueryLastConsultedCreature()
        {
            var lastConsultedCreature = new LastConsultedCreature();

            using (var connection = new SqliteConnection(App.DatabasePath))
            {
                connection.Open();

                var command = connection.CreateCommand();

                command.CommandText = "SELECT * FROM LastConsultedCreature";

                var reader = command.ExecuteReader();

                while (reader.Read()) 
                {
                    lastConsultedCreature.Name = reader.GetString(1);
                    lastConsultedCreature.ImageIcon = (byte[])reader["imageIcon"];
                    lastConsultedCreature.Descipttion = reader.GetString(3);
                }
            }
            return lastConsultedCreature;
        }
    }
}

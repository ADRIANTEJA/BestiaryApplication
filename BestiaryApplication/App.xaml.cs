using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using BestiaryApplication.GamesModule.Repository;
using BestiaryApplication.GamesModule.ViewModel;
using Microsoft.Data.Sqlite;

namespace BestiaryApplication
{
    public partial class App : Application
    {
        private static readonly string databasePath = "Data Source=database.db;Foreign Keys=True";

        public static string DatabasePath
        {
            get { return databasePath; }
        }
     
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            
            using (var connection = new SqliteConnection(DatabasePath))
            {
                connection.Open();

                var command = connection.CreateCommand();

                command.CommandText = @"CREATE TABLE IF NOT EXISTS games (
                                        id INTEGER PRIMARY KEY AUTOINCREMENT,
                                        name STRING NOT NULL,
                                        genre STRING NOT NULL,
                                        registeredCreatures INTEGER NOT NULL,
                                        dateRegistered LONG NOT NUll,
                                        imageIcon BLOB NULL)";

                command.ExecuteNonQuery();

                command.CommandText = @"CREATE TABLE IF NOT EXISTS creatures (
                                        id INTEGER PRIMARY KEY AUTOINCREMENT,
                                        name STRING NOT NULL,
                                        type STRING NOT NULL,
                                        element STRING NOT NULL,
                                        strongPoint STRING NOT NULL,
                                        weakPoint STRING NOT NULL,
                                        description STRING NOT NULL,
                                        imageIcon BLOB NULL,
                                        gameId INTEGER NOT NULL,
                                        FOREIGN KEY (gameId) REFERENCES games(id))";

                command.ExecuteNonQuery();

                command.CommandText = @"CREATE TABLE IF NOT EXISTS LastConsultedCreature (
                                        id TINYINT PRIMARY KEY DEFAULT 1,
                                        name STRING NOT NULL,
                                        imageIcon BLOB NOT NULL,
                                        description NOT NULL,
                                        CHECK (id = 1))";

                command.ExecuteNonQuery();
            }
        }
    }
}

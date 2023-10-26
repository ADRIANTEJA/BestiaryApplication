using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using BestiaryApplication.Common.utils;
using BestiaryApplication.CreatureModule.Model;
using BestiaryApplication.CreatureModule.ViewModel;
using BestiaryApplication.GamesModule.Model;
using BestiaryApplication.GamesModule.Repository;
using Microsoft.Data.Sqlite;

namespace BestiaryApplication.GamesModule.ViewModel
{
    public class GameViewModel : INotifyPropertyChanged
    {
        private string[] expanderOptions;

        public string[] ExpanderOptions
        {
            get { return expanderOptions = new string[9] {"RPG", "Action-RPG", "RTS", "MMO",
                                                          "Shooter", "Survival", "Horror",
                                                          "Plattforming" , "Adventure"}; }
        }


        private ObservableCollection<Game>? games;

        public ObservableCollection<Game> Games
        {
            get { return games; }
            set 
            { 
                games = value;
                OnPropertyChanged(nameof(Games));
            }
        }

        private CreatureViewModel creatureViewModel;

        public CreatureViewModel CreatureViewModel
        {
            get { return creatureViewModel; }
            set { creatureViewModel = value; }
        }

        private string gameName;

        public string GameName
        {
            get { return gameName; }
            set 
            { 
                gameName = value;
                OnPropertyChanged(nameof(GameName));
            }
        }

        private string  gameGenre = "RPG";

        public string  GameGenre
        {
            get { return gameGenre; }
            set 
            { 
                gameGenre = value;
                OnPropertyChanged(nameof(GameGenre));
            }
        }

        private string gameImageIconPath;

        public string GameImageIconPath
        {
            get { return gameImageIconPath; }
            set 
            { 
                gameImageIconPath = value;
                OnPropertyChanged(nameof(GameImageIconPath));
            }
        }

        private Game selectedGame;

        public Game SelectedGame
        {
            get { return selectedGame; }
            set 
            { 
                selectedGame = value;
                OnPropertyChanged(nameof(SelectedGame));
                if (selectedGame != null)
                {
                    CreatureViewModel.DisplayGameCreaturesCommand.Execute(selectedGame.Id);
                } 
            }
        }

        private string searchGameName;

        public string SearchGameName
        {
            get { return searchGameName; }
            set 
            { 
                searchGameName = value;
                OnPropertyChanged(nameof(SearchGameName));
            }
        }

        private ICommand _addGameCommand;

        public ICommand AddGameCommand
        {
            get
            {
                if (_addGameCommand == null)
                {
                    _addGameCommand = new DelegateCommand(AddGame, CanAddGame);
                }
                return _addGameCommand;
            }
        }

        private ICommand _addGameImagePathCommand;

        public ICommand AddGameImagePathCommand
        {
            get
            {
                if (_addGameImagePathCommand == null)
                {
                    _addGameImagePathCommand = new DelegateCommand(SetGameImageIcon);
                }
                return _addGameImagePathCommand;
            }
        }

        private ICommand _changeGameImageCommand;

        public ICommand ChangeGameImageCommand
        {
            get 
            { 
                if (_changeGameImageCommand == null)
                {
                    _changeGameImageCommand = new DelegateCommand(ChangeGameImage);
                }
                return _changeGameImageCommand; 
            }
        }

        private ICommand _searchGameByNameCommand;

        public ICommand SearchGameByNameCommand
        {
            get 
            { 
                if (_searchGameByNameCommand == null)
                {
                    _searchGameByNameCommand = new DelegateCommand(SearchGameByName);
                }
                return _searchGameByNameCommand; 
            }   
        }

        private ICommand _saveChangesCommand;

        public ICommand SaveChangesCommand
        {
            get 
            { 
                if (_saveChangesCommand == null)
                {
                    _saveChangesCommand = new DelegateCommand(UpdateCreature, CanUpdateCreature);
                }
                return _saveChangesCommand; 
            }
        }

        private ICommand _deleteGameCommand;

        public ICommand DeleteGameCommand
        {
            get 
            {
                if (_deleteGameCommand == null)
                {
                    _deleteGameCommand = new DelegateCommand(DeleteGame);
                }
                return _deleteGameCommand; 
            }
        }

        public GameViewModel()
        {
            Games = new ObservableCollection<Game>(GameRespository.QueryAllGames());
            CreatureViewModel = new CreatureViewModel("just to overload");
        }

        private void AddGame(object parameter) 
        {
            Game newGame = new()
            {
                Name = GameName,
                Genre = GameGenre,
                RegisteredCreatures = 0,
                DateRegistered = DateTime.Now.Ticks,
                ImageIcon = File.ReadAllBytes(GameImageIconPath) 
            };

            Games.Add(newGame);
            GameRespository.InsertGame(newGame);
        }

        private bool CanAddGame(object parameter)
        {
            if (string.IsNullOrEmpty(gameName) || string.IsNullOrEmpty(gameImageIconPath)) return false;
            else return true;
        }
        
        private void SetGameImageIcon(object parameter)
        {
            try
            {
                GameImageIconPath = GeneralMethods.GetImagePath();
            }
            catch (Exception e) when (e is FileNotFoundException or DirectoryNotFoundException)
            {
                MessageBox.Show("An error has occured try again");
            }
            catch (Exception e) when (e is ArgumentException) {}
        }

        private void ChangeGameImage(object parameter)
        {
            try
            {
                SelectedGame.ImageIcon = File.ReadAllBytes(GeneralMethods.GetImagePath());
            }
            catch (Exception e) when (e is FileNotFoundException or DirectoryNotFoundException
                                      or ArgumentException)
            {
                MessageBox.Show("An error has occured try again");
            }
            catch (Exception e) when (e is ArgumentException) {}
            OnPropertyChanged(nameof(SelectedGame));
        }

        private void SearchGameByName(object parameter)
        {
            if (string.IsNullOrEmpty(searchGameName))
            {
                Games = new ObservableCollection<Game>(GameRespository.QueryAllGames());
                return;
            }
            Games = new ObservableCollection<Game>(GameRespository.QueryGameByName(searchGameName)); 
        }

        private void UpdateCreature(object parameter)
        {
            GameRespository.UpdateGame(SelectedGame);
            Games = new ObservableCollection<Game>(GameRespository.QueryAllGames());    
        }

        private bool CanUpdateCreature(object parameter)
        {
            if (selectedGame == null || string.IsNullOrEmpty(SelectedGame.Name)) return false;
            return true;
        }

        private void DeleteGame(object parameter)
        {
            var gameId = (int)parameter;
            GameRespository.DeleteGame(gameId);

            var gameToDelete = Games.Where(g => g.Id == selectedGame.Id).First();
            Games.Remove(gameToDelete);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

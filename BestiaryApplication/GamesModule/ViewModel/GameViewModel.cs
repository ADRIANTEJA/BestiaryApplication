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
using System.Windows.Media.Imaging;
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
        private string[] genreTypes = new string[9] {"RPG", "Action-RPG", "RTS", "MMO",
                                                          "Shooter", "Survival", "Horror",
                                                          "Plattforming" , "Adventure"};
        public string[] GenreTypes
        {
            get { return genreTypes; }
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

        private string gameGenre;

        public string  GameGenre
        {
            get { return gameGenre; }
            set 
            { 
                gameGenre = value;
                OnPropertyChanged(nameof(GameGenre));
            }
        }

        private BitmapImage? gameImage;

        public BitmapImage? GameImage
        {
            get { return gameImage; }
            set 
            { 
                gameImage = value;
                OnPropertyChanged(nameof(GameImage));
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
                    _changeGameImageCommand = new DelegateCommand(ChangeGameImage, CanChangeGameImage);
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
                    _saveChangesCommand = new DelegateCommand(UpdateGame, CanUpdateGame);
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
                    _deleteGameCommand = new DelegateCommand(DeleteGame, CanDeleteGame);
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
                ImageIcon = GeneralMethods.ConvertToBytes(gameImage)
            };

            Games.Add(newGame);
            GameRespository.InsertGame(newGame);
            GameImage = null;
        }

        private bool CanAddGame(object parameter)
        {
            if (string.IsNullOrEmpty(gameName) || gameImage == null) return false;
            else return true;
        }

        private void SetGameImageIcon(object parameter)
        {
            try
            {
                GameImage = GeneralMethods.LoadImage(GeneralMethods.GetImagePath());
            }
            catch {}
        }
        private void ChangeGameImage(object parameter)
        {
            try
            {
                SelectedGame.ImageIcon = File.ReadAllBytes(GeneralMethods.GetImagePath());
            }
            catch {}
            OnPropertyChanged(nameof(SelectedGame));
        }

        private bool CanChangeGameImage(object parameter)
        {
            return selectedGame == null ? false : true;
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

        private void UpdateGame(object parameter)
        {
            GameRespository.UpdateGame(SelectedGame);
            Games = new ObservableCollection<Game>(GameRespository.QueryAllGames());    
        }

        private bool CanUpdateGame(object parameter)
        {
            return SelectedGame != null && !string.IsNullOrEmpty(SelectedGame.Name);
        }

        private void DeleteGame(object parameter)
        {
            GameRespository.DeleteGame(selectedGame.Id);

            var gameToDelete = Games.Where(g => g.Id == selectedGame.Id).First();
            Games.Remove(gameToDelete);
        }

        private bool CanDeleteGame(object parameter)
        {
            return selectedGame != null;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

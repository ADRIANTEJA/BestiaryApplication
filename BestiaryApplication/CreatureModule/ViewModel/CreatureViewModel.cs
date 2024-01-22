using BestiaryApplication.Common.utils;
using BestiaryApplication.CreatureModule.DataAccess;
using BestiaryApplication.CreatureModule.Model;
using BestiaryApplication.GamesModule.Model;
using BestiaryApplication.GamesModule.Repository;
using BestiaryApplication.GamesModule.ViewModel;
using BestiaryApplication.MainModule.DataAccess;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BestiaryApplication.CreatureModule.ViewModel
{
    public class CreatureViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private readonly string[] elements = new string[6] { "Water", "Earth", "Fire", "Air", "Light", "Darkness" };

        public string[] Elements
        {
            get { return elements; }
        }

        private readonly string[] editElements = new string[6] { "Water", "Earth", "Fire", "Air", "Light", "Darkness" };

        public string[] EditElements
        {
            get { return editElements; }
        }

        private ObservableCollection<Creature> creatures;

        public ObservableCollection<Creature> Creatures 
        {   
            get { return creatures; }
            set
            {
                creatures = value;
                OnPropertyChanged(nameof(Creatures));
            }
        }

        public GameViewModel? GameViewModel { get; set; }

        private string creatureName;

        public string CreatureName
        {
            get { return creatureName; }
            set 
            {
                creatureName = value;
                OnPropertyChanged(nameof(CreatureName));
            }
        }

        private string creatureType;

        public string CreatureType
        {
            get { return creatureType; }
            set 
            { 
                creatureType = value;
                OnPropertyChanged(nameof(CreatureType));
            }
        }

        private string creatureElement;

        public string CreatureElement
        {
            get { return creatureElement; }
            set 
            { 
                creatureElement = value;
                OnPropertyChanged(nameof(CreatureElement));
            }
        }

        private string strongPoint;

        public string StrongPoint
        {
            get { return strongPoint; }
            set 
            { 
                strongPoint = value;
                OnPropertyChanged(nameof(StrongPoint));
            }
        }

        private string weakPoint;

        public string WeakPoint
        {
            get { return weakPoint; }
            set 
            { 
                weakPoint = value;
                OnPropertyChanged(nameof(WeakPoint));
            }
        }

        private string description;

        public string Description
        {
            get { return description; }
            set 
            { 
                description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        private string creatureImageIconPath;

        public string CreatureImageIconPath
        {
            get { return creatureImageIconPath; }
            set 
            { 
                creatureImageIconPath = value;
                OnPropertyChanged(nameof(CreatureImageIconPath));
            }
        }

        private string searchCreatureName;

        public string SearchCreatureName
        {
            get { return searchCreatureName; }
            set 
            { 
                searchCreatureName = value;
                OnPropertyChanged(nameof(SearchCreatureName));
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
            }
        }

        private Creature selectedCreature;

        public Creature SelectedCreature
        {
            get { return selectedCreature; }
            set
            {
                selectedCreature = value;
                OnPropertyChanged(nameof(SelectedCreature));
            }
        }

        private ICommand _addCreatureImagePathCommand;

        public ICommand AddCreatureImagePathCommand
        {
            get 
            {   
                if(_addCreatureImagePathCommand == null)
                {
                    _addCreatureImagePathCommand = new DelegateCommand(SetCreatureImageIcon);
                }
                return _addCreatureImagePathCommand; 
            }
        }

        private ICommand _changeCreatureImageCommand;

        public ICommand ChangeCreatureImageIconCommand
        {
            get 
            { 
                if (_changeCreatureImageCommand == null)
                {
                    _changeCreatureImageCommand = new DelegateCommand(ChangeCreatureImage);
                }
                return _changeCreatureImageCommand; 
            }
        }

        private ICommand _addNewCreatureCommand;

        public ICommand AddNewCreatureCommand
        {
            get 
            {   
                if(_addNewCreatureCommand == null)
                {
                    _addNewCreatureCommand = new DelegateCommand(AddNewCreature, CanAddNewCreature);
                }
                return _addNewCreatureCommand; 
            }
        }

        private ICommand _displayGameCreaturesCommand;

        public ICommand DisplayGameCreaturesCommand
        {
            get
            {
                if(_displayGameCreaturesCommand == null)
                {
                    _displayGameCreaturesCommand = new DelegateCommand(SearchGameCreatures);
                }
                return _displayGameCreaturesCommand;    
            }
        }

        private ICommand _deleteCreatureCommand;

        public ICommand DeleteCreatureCommand
        {
            get 
            { 
                if (_deleteCreatureCommand == null)
                {
                    _deleteCreatureCommand = new DelegateCommand(DeleteCreature);
                }
                return _deleteCreatureCommand; 
            }
        }

        private ICommand _searchCreatureByNameCommand;

        public ICommand SearchCreatureByNameCommand
        {
            get 
            { 
                if (_searchCreatureByNameCommand == null)
                {
                    _searchCreatureByNameCommand = new DelegateCommand(SearchCreatureByName);
                }
                return _searchCreatureByNameCommand; 
            }
        }

        private ICommand _saveChangesCommand;

        public ICommand SaveChangesCommand
        {
            get 
            { 
                if ( _saveChangesCommand == null)
                {
                    _saveChangesCommand = new DelegateCommand(UpdateCreature, CanUpdateCreature);
                }
                return _saveChangesCommand; 
            }
        }

        private ICommand _insertLastConsultedCreatureCommand;

        public ICommand InsertLastConsultedCreatureCommand
        {
            get
            {
                if (_insertLastConsultedCreatureCommand == null)
                {
                    _insertLastConsultedCreatureCommand = new DelegateCommand(InsertLastConsultedCreature);
                }
                return _insertLastConsultedCreatureCommand;
            }
        }

        public CreatureViewModel() 
        {
            Creatures = new ObservableCollection<Creature>(CreatureDataAccess.QueryAllCreatures());
            GameViewModel = new GameViewModel();
        }

        public CreatureViewModel(string constructorNumber = "2") {}

        private void SetCreatureImageIcon(object parameter)
        {
            try
            {
                CreatureImageIconPath = GeneralMethods.GetImagePath();
            }
            catch {}
        }

        private void ChangeCreatureImage(object parameter)
        {
            try
            {
                SelectedCreature.ImageIcon = File.ReadAllBytes(GeneralMethods.GetImagePath());
            }
            catch {}
            OnPropertyChanged(nameof(SelectedCreature));
        }

        private void AddNewCreature(object parameter)
        {
            Creature creature = new()
            {
                Name = CreatureName,
                Type = CreatureType,
                Element = CreatureElement,
                StrongPoint = StrongPoint,
                WeakPoint = WeakPoint,
                Description = description,
                ImageIcon = File.ReadAllBytes(CreatureImageIconPath),
                GameId = SelectedGame.Id,
            };
            
            creatures.Add(creature);
            CreatureDataAccess.InsertCreature(creature);
            GameRespository.UpdateGameRegisteredCreatures(creature.GameId);
        }

        private bool CanAddNewCreature(object parameter)
        {
            if (string.IsNullOrEmpty(CreatureName)
                || string.IsNullOrEmpty(CreatureType)
                || string.IsNullOrEmpty(StrongPoint)
                || string.IsNullOrEmpty(WeakPoint)
                || string.IsNullOrEmpty(Description)
                || GameViewModel!.Games.Count == 0) { return false; }

            return true;
        }

        private void SearchGameCreatures(object parameter)
        {
            int gameId = (int)parameter;
            Creatures = new ObservableCollection<Creature>(CreatureDataAccess.QueryGameCreatures(gameId));
        }

        private void DeleteCreature(object parameter)
        {
            var id = (int)parameter;
            CreatureDataAccess.DeleteCreature(id);

            var creatureToDelete = Creatures.Where(c => c.Id == SelectedCreature.Id).First();
            Creatures.Remove(creatureToDelete);
        }

        private void SearchCreatureByName(object parameter)
        {
            var creatureName = (string)parameter;

            if (string.IsNullOrEmpty(creatureName))
            {
                Creatures = new ObservableCollection<Creature>(CreatureDataAccess.QueryAllCreatures());
                return;
            }
            Creatures = new ObservableCollection<Creature>(CreatureDataAccess.QueryCreatureByName(creatureName));
        }

        private void UpdateCreature(object parameter)
        {
            CreatureDataAccess.UpdateCreature(SelectedCreature);
            Creatures = new ObservableCollection<Creature>(CreatureDataAccess.QueryAllCreatures());
        }

        private bool CanUpdateCreature(object parameter)
        {
            if (SelectedCreature == null 
                || string.IsNullOrEmpty(SelectedCreature.Name)
                || string.IsNullOrEmpty(SelectedCreature.Element.ToString())
                || string.IsNullOrEmpty(SelectedCreature.StrongPoint)
                || string.IsNullOrEmpty(SelectedCreature.WeakPoint)
                || string.IsNullOrEmpty(SelectedCreature.Description)) return false; 

            return true;
        }

        private void InsertLastConsultedCreature(object parameter)
        {
            var creature = (Creature)parameter;
            MainDataAccess.DeleteLastConsultedCreature();
            MainDataAccess.InsertLastConsultedCreature(creature);
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

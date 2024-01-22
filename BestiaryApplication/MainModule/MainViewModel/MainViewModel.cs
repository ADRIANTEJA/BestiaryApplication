using BestiaryApplication.Common.utils;
using BestiaryApplication.CreatureModule.Model;
using BestiaryApplication.MainModule.DataAccess;
using BestiaryApplication.MainModule.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BestiaryApplication.MainModule.MainViewModel
{
    public class MainViewModel
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private LastConsultedCreature lastConsultedCreature;

        public LastConsultedCreature LastConsultedCreature 
        {   
            get { return lastConsultedCreature; }
            set 
            { 
                lastConsultedCreature = value;
                OnPropertyChanged(nameof(LastConsultedCreature));
            }
        }

        public MainViewModel() { LastConsultedCreature = MainDataAccess.QueryLastConsultedCreature(); } 

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

using BestiaryApplication.CreatureModule.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestiaryApplication.GamesModule.Model
{
    public class Game
    {
        public int Id { get; set; }

        public int RegisteredCreatures { get; set; }

        public long DateRegistered { get; set; }

        public string Name { get; set; }

        public byte[]? ImageIcon { get; set; }

        public string Genre { get; set; }
    }
}

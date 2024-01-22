using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestiaryApplication.CreatureModule.Model
{
    public class Creature
    {
        public int Id { get; set; }

        public string Name { set; get; }

        public int GameId { set; get; }

        public string Type { set; get; }

        public string StrongPoint { set; get; }

        public string WeakPoint { set; get; }

        public string Description { set; get; }

        public string Element { set; get; }

        public byte[]? ImageIcon { set; get; } 
    }
}

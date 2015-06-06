using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextGame
{
    public class Enemy
    {
        public int health { get; set; }

        public int damage { get; set; }

        public string name { get; set; }
    }

    public class Player
    {
        public int health { get; set; }
    }
}

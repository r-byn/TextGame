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
        public int damage { get; set; }
        public List<Item> inventory { get; set; }  
        public int money { get; set; }
        public bool isAlive { get; set; }

    }


    
      public class MapPiece
    {
        public int x { get; set; }

        public int y { get; set; }

        public Enemy enemy { get; set; }

        public Shop shop { get; set; }

        public bool isAccessible { get; set; }

        public string whyNotAccessible { get; set; }

        public String message { get; set; }

        public bool isMountain { get; set; }
      }

    public class Shop
    {
        public string name { get; set; }

        public List<Item> shopItems { get; set; }
    }

    public class Item
    {
        public decimal price { get; set; }

        public string name { get; set; }

        public int damage { get; set; }

        //public int quantity { get; set; }


    }

    
}

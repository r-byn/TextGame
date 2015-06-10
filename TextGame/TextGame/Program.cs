using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextGame
{
    class Program
    {
        public static Player player = new Player();

        public static List<item> inventory = new List<item>();     

        public static List<Enemy> enemies = new List<Enemy>();

        public static Random rnd = new Random();

        public static List<MapPiece> map = new List<MapPiece>();

        public static int currentX;

        public static int currentY;

        public static int caveEnterance;

        static void Main(string[] args)
        {
            map = generateMap(32, 32);

            pathBuilder();

            currentX = 1;
            currentY = 1;
            caveEnterance = 0;

            player.health = 50;
            player.damage = 8;
            player.isAlive = true;
          /*  enemies = createEnemies(rnd.Next(1, 5));

            foreach(Enemy enemy in enemies)
            {
                System.Console.Write("Enemy name: " + enemy.name + " Enemy health: " + enemy.health.ToString() + " Enemy damage: " + enemy.damage.ToString());
            } */

            int last = 0;

            foreach (var entry in map)
            {
                int x = entry.x;

                if (x != last)
                {

                    Console.WriteLine("\n");
                }

                Console.Write(rendermap(entry));

                last = x;
            }

            while (gameRunning())
            {
                checkEvents();

                System.Console.WriteLine("Which way should I go?");

                String dir = System.Console.ReadLine();

                moveLocation(dir);

                checkLife();
            }           

            
        }

        public static bool gameRunning ()
        {
            if(player.isAlive == false)
            {
                return false;
            }
            return true;
        }

        
        public static List<Enemy> createEnemies(int number)
        {
            List<Enemy> toreturn = new List<Enemy>();

            while(number > 0)
            {
                Enemy toadd = new Enemy();
                toadd.health = rnd.Next(10, 20);
                toadd.damage = rnd.Next(1, 5);
                toadd.name = "an enemy" + number.ToString();

                toreturn.Add(toadd);

                number = number - 1;
            }

            return toreturn;
        }


      public static List<MapPiece> generateMap (int x, int y)
    {
            List<int> xs = new List<int>();
            List<int> ys = new List<int>();
            List<decimal> positions = new List<decimal>();
            List<MapPiece> toreturn = new List<MapPiece>();
            
            
            while(x > 0)
            {
                xs.Add(x);
                x--;
            }

            while(y > 0)
            {
                ys.Add(y);
                y--;
            }

            //creates a map reference for each piece. i.e if 32x32 1.1, 2.1, 2.2 32.32, etc etc
            foreach(int axis in xs)
            {
                foreach(int verts in ys)
                {
                        MapPiece piece = new MapPiece();

                        piece.x = axis;
                        piece.y = verts;

                        piece.isAccessible = chanceAccessible();

                        if (!piece.isAccessible)
                        {
                            piece.whyNotAccessible = accessibleReason();
                            piece.isMountain = false;
                            piece.isSpawn = false;
                        }

                       

                        if (piece.x >= 32)
                        {
                            piece.isAccessible = false;
                            piece.isMountain = true;
                            piece.isSpawn = false;

                        }
                        if (piece.x <= 1)
                        {
                            if (piece.y <=1)
                            { piece.isAccessible = true;
                              piece.isSpawn = true;
                              piece.spawn = generateSpawn();
                            }
                        }
                                                

                        piece.enemy = chanceEnemy();
                        piece.shop = chanceShop();
                         
                    
                        try
                        {
                            toreturn.Add(piece);
                        }
                        catch
                        {

                    }
                }
            }

            return toreturn;

        }

        public static Shop chanceShop()
        {
            if (rnd.Next(0, 10) > 8)
            {
                Shop shop = new Shop();
                shop.name = "Shop";
                            

                return shop;
            }

            return null;
        }

        public static Enemy chanceEnemy()
        {
            

            if (rnd.Next(0, 10) > 5)
            {
                    Enemy enemy = new Enemy();
                    enemy.name = "enemy";
                    enemy.health = rnd.Next(5, 25);
                    enemy.damage = rnd.Next(1, 8);

                    return enemy;
                
            }
            return null;
        }

        public static Spawn generateSpawn()
        {
         
                Spawn spawn = new Spawn();
                spawn.name = "Castle";
                

                return spawn;
            }

              
        




        public static void checkLife() {

            if (player.health <= 0)
            {
                player.isAlive = false;
                Console.WriteLine("YOURE DEAD!");
                
                
               
            }

        }

        public static bool chanceAccessible()
        {
            if (rnd.Next(0, 60) > 15)
            {
                return true;
                
            }

            return false;
        }

        public static string accessibleReason()
        {
            List<string> reasons = new List<string>();

            reasons.Add("Two elves are sleeping, I don't want to disturb them!");
            reasons.Add("The forest is far too dense to go this way!");
            reasons.Add("I'm not even sure what that animal is, but it doesnt look friendly, best not to disburb it!");
            reasons.Add("I'm not going to walk through that thicket, those branches look thorny!");
            reasons.Add("A dilapidated golem blocks your path. There's no getting around it.");
            reasons.Add("A strange mushroom discharges spores into the air. Best not to walk through them.");
            reasons.Add("A large web blocks the path. I'm not going to stick around to find out what made it.");
            reasons.Add("A pair of sprites are having a domestic dispute. I'll leave them be.");
            reasons.Add("*You hear a low growl as a wolf warns you away from her pups*");
            reasons.Add("*A rune is carved into the nearby trees. You recognize it as 'danger this way.'* I better not get sidetracked.");

                   

            return reasons.OrderBy(s => Guid.NewGuid()).First();
        }

        public static string generalMessage()
        {
            List<string> message = new List<string>();

            message.Add("Two birds are preparing a nest up in that tree!");
            message.Add("I hope this is the right way.");
            message.Add("Great, I stepped in a puddle.");
            message.Add("I hope this Lich isn't as fearsome as they say!");
            message.Add("At least the pay is good, that's if I live of course...");
            message.Add("*Rabbits dash past your feet, away into the dense forest*");
            message.Add("These flowers are looking worse for ware, thats not a good sign.");
          

            return message.OrderBy(s => Guid.NewGuid()).First();
        }

        public static string rendermap(MapPiece piece)
        {

            if (piece.isAccessible == false)
            {
                if (piece.isMountain == true)
                {
                    return "M";
                }
            }

            if (piece.isAccessible == true)
            {
                if (piece.shop != null)
                {
                    return "S";
                }
                if (piece.spawn != null)
                {
                    return "#";
                }
                return "X";
            }
            else
            {
                return "O";
            }

            
        }

        public static void pathBuilder()
        {
            foreach(MapPiece piece in map)
            {
                if(!checkAround(piece.x, piece.y))
                {
                    int dir = rnd.Next(1, 4);
                    if(piece.y == 1)
                    {
                        while(dir == 1)
                        {
                            dir = rnd.Next(1, 4);
                        }
                    }
                    if (piece.y == 32)
                    {
                        while (dir == 3)
                        {
                            dir = rnd.Next(1, 4);
                        }
                    }
                    if (piece.x == 1)
                    {
                        while (dir == 4)
                        {
                            dir = rnd.Next(1, 4);
                        }
                    }
                    if (piece.x == 32)
                    {
                        while (dir == 2)
                        {
                            dir = rnd.Next(1, 4);
                        }
                    }
                    switch(dir)
                    {
                        case 1:
                            pathFixer("N", piece.x, piece.y);
                            break;
                        case 2:
                            pathFixer("E", piece.x, piece.y);
                            break;
                        case 3:
                            pathFixer("S", piece.x, piece.y);
                            break;
                        case 4:
                            pathFixer("W", piece.x, piece.y);
                            break;


                    }
                }
            }
        }

        public static void pathFixer(string dir, int x, int y)
        {
            switch(dir)
            {
                case "N":
                    y = y - 1;
                    break;

                case "E":
                    x = x + 1;
                    break;

                case "S":
                    y = y + 1;
                    break;

                case "W":
                    x = x - 1;
                    break;
            }

            // map.Where(d => d.x == x && d.y == y).First().isAccessible = true; 
                
            }
           
        

        public static bool moveLocation(string dir)
        {
            try
            {
                switch (dir)
                {
                    case "N":
                        MapPiece goingToN = getPiece(currentX, currentY - 1);
                        if (goingToN.isAccessible && goingToN != null)
                        {
                            currentY = currentY - 1;
                            return true;
                        }
                        else
                        {
                            Console.WriteLine(goingToN.whyNotAccessible);
                            return false;
                        }
                        break;
                    case "S":
                        MapPiece goingToS = getPiece(currentX, currentY + 1);
                        if (goingToS.isAccessible && goingToS != null)
                        {
                            currentY = currentY + 1;
                            return true;
                        }
                        else
                        {
                            Console.WriteLine(goingToS.whyNotAccessible);
                            return false;
                        }
                        break;
                    case "W":
                        MapPiece goingToW = getPiece(currentX - 1, currentY);
                        if (goingToW.isAccessible && goingToW != null)
                        {
                            currentX = currentX - 1;
                            return true;
                        }
                        else
                        {
                            Console.WriteLine(goingToW.whyNotAccessible);
                            return false;
                        }
                        break;
                    case "E":
                        MapPiece goingToE = getPiece(currentX + 1, currentY);
                        if (goingToE.isAccessible && goingToE != null)
                        {
                            currentX = currentX + 1;
                            return true;
                        }
                        else
                        {
                            Console.WriteLine(goingToE.whyNotAccessible);
                            return false;
                        }
                        break;
                    default:
                        Console.WriteLine("N, E, S, or W");
                        return false;
                }
            }

            catch(NullReferenceException)
            {
                Console.WriteLine("The world ends, don't want to fall off a cliff");
                return false;
            }
           
        }

        public static MapPiece getPiece(int x, int y)
        {
            foreach(MapPiece piece in map)
            {
                if(piece.x == x)
                {
                    if(piece.y == y)
                    {
                        return piece;
                    }
                }
            }

            return null;
        }

        public static bool checkAround(int x, int y)
        {
            if(!checkDirectional(x,y,"N"))
            {
                if(!checkDirectional(x,y,"E"))
                {
                    if(!checkDirectional(x,y,"S"))
                    {
                        if(!checkDirectional(x,y,"W"))
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        public static bool checkDirectional(int x, int y, string dir)
        {
            try
            {
                switch (dir)
                {
                    case "N":
                        MapPiece goingToN = getPiece(x, y - 1);
                        if (goingToN.isAccessible && goingToN != null)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                        break;
                    case "S":
                        MapPiece goingToS = getPiece(x, y + 1);
                        if (goingToS.isAccessible && goingToS != null)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                        break;
                    case "W":
                        MapPiece goingToW = getPiece(x - 1, y);
                        if (goingToW.isAccessible && goingToW != null)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                        break;
                    case "E":
                        MapPiece goingToE = getPiece(x + 1, y);
                        if (goingToE.isAccessible && goingToE != null)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                        break;
                    default:
                        Console.WriteLine("n, e, s or w ?");
                        return false;
                }
            }

            catch (NullReferenceException)
            {
                return false;
            }
        }

        public static void checkEvents()
        {
            MapPiece piece = getPiece(currentX, currentY);

            if (piece.shop != null)
            {
                Console.WriteLine("A shop");

            }

            if (piece.spawn != null) { 
                Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n\n\n QUEEN: Oh, Adventurer! Please help us. There is an evil Lich living in a cave in the mountains to the South. \n It sends it's minions to terrorise the town and has taken over the forest, \n Beast have started living there, the branches of trees have grown horns, and it's getting worse and worse by the day! \n Soon the Lich's taint will break down the city walls and our people will be endangered! \n I understand this is a trumendous task and your live will undoubtly be at risk, but if succesfull you will be greatly rewarded!");
                Console.WriteLine("\n YOU: Yes of course your majesty! I will try my best to destroy the Lich and undo the evil it has tainted you forest with!");
                Console.WriteLine("\n QUEEN: Please be careful not to get lost! I'll show you to the forest.\n The forest begins just beyong this gate, remember, youre going to need to head South! May the God's bless your path.");
                Console.WriteLine("\n *The masaive gate door slowly opens as you try to scope out what lies ahead in the dense forest, but its far too dark. \n You begin to walk towards the forest and the door is closed behind you.");
                Console.WriteLine("\n A deep boom vibrates the floor as the gate doors slam together.\n\n\n");
                Console.WriteLine("----------------------------------------------------\n Input N, E, S, or W to chose direction\n Attack by inputting ATTACK\n----------------------------------------------------\n\n\n");
            }


            if(piece.enemy != null)
            {
                Console.WriteLine("An " + piece.enemy.name + " wishes to fight!");
                Console.WriteLine("It has " + piece.enemy.health + " health and does " + piece.enemy.damage + "damage!");
                while(piece.enemy.health > 0)
                {
                    checkLife();
                    Console.WriteLine("What should I do?");
                    String todo = Console.ReadLine();

                    switch(todo)
                    {
                        case "ATTACK":
                            //sumin
                            piece.enemy.health = piece.enemy.health - player.damage;
                            if(piece.enemy.health > 0)
                            {
                                Console.WriteLine("Enemy health is now " + piece.enemy.health.ToString());
                            }

                            if (piece.enemy.health <= 0)
                            {
                                Console.WriteLine("Enemy is now dead!");


                                if (rnd.Next(0, 10) > 5)
                                {
                                    player.money++;
                                    Console.WriteLine("GOLD = " +player.money);
                                }
                            
                                item someitem = new item();

                                someitem.name = "Coin";
                                someitem.damage = 0;
                                someitem.price = 0;
                                someitem.quantity = 1;
//player.inventory.Add(someitem);

                                if (player.inventory != null)
                                {

                                    foreach (item item in player.inventory)
                                    {
                                        Console.WriteLine(item.name);
                                    }
                                }
                            }
                                
                             
                            break;
                        default:
                            Console.WriteLine("That is not an option");
                            break;
                    }
                    if (piece.enemy.health > 0)
                    {
                        
                        Console.WriteLine("The enemy strikes you for " + piece.enemy.damage.ToString());
                        player.health = player.health - piece.enemy.damage;

                        Console.WriteLine("Your health is now " + player.health.ToString());
                        
                    }
                }
                piece.enemy = null;
            }
        }
    }
}

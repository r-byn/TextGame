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

        public static List<Enemy> enemies = new List<Enemy>();

        public static Random rnd = new Random();

        public static List<MapPiece> map = new List<MapPiece>();

        public static int currentX;

        public static int currentY;

        static void Main(string[] args)
        {
            map = generateMap(32, 32);

            currentX = 1;
            currentY = 1;

            player.health = 10;
            enemies = createEnemies(rnd.Next(1, 5));

            foreach(Enemy enemy in enemies)
            {
                System.Console.Write("Enemy name: " + enemy.name + " Enemy health: " + enemy.health.ToString() + " Enemy damage: " + enemy.damage.ToString());
            }

            int last = 0;

            foreach(var entry in map)
            {
                int x = entry.x;

                if (x != last)
                {
                    Console.WriteLine("\n");
                }

                Console.Write(rendermap(entry));

                last = x;
            }        

            while(gameover())
            {
                checkEvents();

                System.Console.WriteLine("Where shall I go, my liege?");

                String dir = System.Console.ReadLine();

                moveLocation(dir);
            }           

            
        }

        public static bool gameover ()
        {
            if(player.health <= 0)
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
                toadd.health = rnd.Next(1, 20);
                toadd.damage = rnd.Next(1, 5);
                toadd.name = "Some prick" + number.ToString();

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

                    if(!piece.isAccessible)
                    {
                        piece.whyNotAccessible = accessibleReason();
                    }

                    piece.enemy = chanceEnemy();

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

        public static Enemy chanceEnemy()
        {
            if(rnd.Next(0, 10) > 3)
            {
                Enemy enemy = new Enemy();
                enemy.name = "dickbutt";
                enemy.health = rnd.Next(5, 25);
                enemy.damage = rnd.Next(1, 8);

                return enemy;
            }

            return null;
        }

        public static bool chanceAccessible()
        {
            if (rnd.Next(0, 60) > 20)
            {
                return true;
            }

            return false;
        }

        public static string accessibleReason()
        {
            List<string> reasons = new List<string>();

            reasons.Add("Two elves busy masturbating, don't want to disturb!");
            reasons.Add("A rogue invisible wall blocks your way");
            reasons.Add("A brothel appears to be this way, do not want to be led into temptation!");
            reasons.Add("Some other thing, to do with something blocks your way");
            

            return reasons.OrderBy(s => Guid.NewGuid()).First();
        }

        public static string rendermap(MapPiece piece)
        {
            if(piece.isAccessible == true)
            {
                return "X";
            }
            else
            {
                return "O";
            }
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
                        Console.WriteLine("That's not a real direction, pillock");
                        return false;
                }
            }

            catch(NullReferenceException e)
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

        public static void checkEvents()
        {
            MapPiece piece = getPiece(currentX, currentY);

            if(piece.enemy != null)
            {
                Console.WriteLine("A " + piece.enemy.name + " wishes to fight!");
                Console.WriteLine("It has " + piece.enemy.health + " health and does " + piece.enemy.damage + "damage!");
                while(piece.enemy.health > 0)
                {

                    Console.WriteLine("How much damage should we do?");
                    piece.enemy.health = piece.enemy.health - Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("The best strikes you!");
                    player.health = player.health - piece.enemy.damage;

                }
            }
        }
    }
}

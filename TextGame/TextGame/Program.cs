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

        static void Main(string[] args)
        {
            map = generateMap(32, 32);

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
                String test = System.Console.ReadLine() + ".. yes it is";

                System.Console.WriteLine(test);

                System.Console.WriteLine("How much damage shall I do?");
                int damage = Convert.ToInt32(System.Console.ReadLine());

                player.health = player.health - damage;
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

                    if (rnd.Next(0, 60) > 20)
                    {
                        piece.isAccessible = true;
                    }

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
    }
}

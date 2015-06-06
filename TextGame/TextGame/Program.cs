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

        static void Main(string[] args)
        {

            player.health = 10;
            enemies = createEnemies(rnd.Next(1, 1000));

            foreach(Enemy enemy in enemies)
            {
                System.Console.Write("Enemy name: " + enemy.name + " Enemy health: " + enemy.health.ToString() + " Enemy damage: " + enemy.damage.ToString());
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
    }
}

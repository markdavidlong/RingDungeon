using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Engine;
using Engine.Enums;

#pragma warning disable IDE0079
#pragma warning disable IDE0060
#pragma warning disable IDE0059 
namespace RDSimpleIface
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Ring Dungeon!");
            var game = new Game();

            bool done = false;
            bool first = true;
            int roomnum = 0;
            while (!done)
            {
                
                var room = game.Dungeon.Rooms[roomnum];
                Console.WriteLine(room.ToString());

                var read = Console.ReadKey();
                var keyvalue = read.KeyChar;

                if (!first)
                {
                    Console.Clear();
                }
                else
                {
                    first = false;
                }

                if (keyvalue.ToString().Contains('Q', StringComparison.CurrentCultureIgnoreCase))
                {
                    done = true;
                }
                else if (keyvalue.ToString().Contains('+', StringComparison.CurrentCultureIgnoreCase))
                {
                    roomnum = (roomnum + 1) % (game.Dungeon.Rooms.Count);
                }
                else if (keyvalue.ToString().Contains('-', StringComparison.CurrentCultureIgnoreCase))
                {
                    roomnum = (game.Dungeon.Rooms.Count + roomnum - 1) % (game.Dungeon.Rooms.Count);
                }
                else if (keyvalue.ToString().Contains('W', StringComparison.CurrentCultureIgnoreCase))
                {
                    room.MovePlayer(Game.CurrentPlayer!, Direction.North);
                }
                else if (keyvalue.ToString().Contains('A', StringComparison.CurrentCultureIgnoreCase))
                {
                    room.MovePlayer(Game.CurrentPlayer!, Direction.West);
                }
                else if (keyvalue.ToString().Contains('S', StringComparison.CurrentCultureIgnoreCase))
                {
                    room.MovePlayer(Game.CurrentPlayer!, Direction.South);
                }
                else if (keyvalue.ToString().Contains('D', StringComparison.CurrentCultureIgnoreCase))
                {
                    room.MovePlayer(Game.CurrentPlayer!, Direction.East);
                }

                Console.WriteLine(keyvalue.ToString() + " (" + read.Key + ") Done: " + done);
                
            }


            // Additional game logic and testing here
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}       


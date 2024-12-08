using System;
using System.Collections.Generic;
using System.Linq;

using Engine.Enums;
using Engine.Structs; 

namespace Engine
{
    public class Game
    {
        public Dungeon Dungeon { get; private set; }
        public static List<Player> Players { get; private set; } = [];

        public static List<Player> LivingPlayers { 
            get => Players.Where(p => !p.Dead).ToList(); 
        }

        public static Player? CurrentPlayer { get; set; }

        public static Player? NextPlayer
        {
            get
            {
                if (CurrentPlayer == null || RemainingPlayers == 0)
                {
                    return null;
                }
                int currentPlayerIndex = Players.IndexOf(CurrentPlayer);
                int nextPlayerIndex = (currentPlayerIndex + 1) % LivingPlayers.Count;
                return LivingPlayers[nextPlayerIndex];
            }
        }

        public static int RemainingPlayers
        {
            get => LivingPlayers.Count;
        }

        public Game()
        {
            Dungeon = new Dungeon();

            // Initialize rooms, players, and monsters
            InitializeGame();
        }

        private void InitializeGame()
        {
            Player p = new("Player 1")
            {
                Location = new EntityLocation(Dungeon.Rooms[0],
                Constants.RoomXCenter, Constants.RoomMaxY, Direction.North)
            };
            AddPlayer(p);
        }


        public void AddPlayer(Player player)
        {
            Players.Add(player);
            // If this is the first player added, make it the current player
            if (Players.Count == 1)
            {
                CurrentPlayer = player;
                Console.WriteLine("Setting current player to " + player.Name);
            }
        }


    }

}

using System.Collections.Generic;
using System.Threading;

namespace Engine
{
    public class Game
    {
        public Dungeon Dungeon { get; private set; }
        public Player? CurrentPlayer { get; private set; }
        public List<Player> Players { get; private set; }

        public Game()
        {
            Dungeon = new Dungeon();
            Players = [];

            // Initialize rooms, players, and monsters
            InitializeGame();
        }

        private void InitializeGame()
        {

        }

        public void AddPlayer(Player player)
        {
            Players.Add(player);
        }


    }

}

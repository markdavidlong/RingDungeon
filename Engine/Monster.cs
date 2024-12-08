using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine;

namespace Engine
{
    public class Monster
    {
        public string Name { get; set; }
        public int Health { get; set; }
        public int AttackPower { get; set; }
        public string Description { get; set; }

        // Constructor
        public Monster(string name, int health, int attackPower, string description ="")
        {
            Name = name;
            Health = health;
            AttackPower = attackPower;
            Description = description;
        }

        // Method for attacking a player
        public void Attack(Player player)
        {
            if (player.Health > 0)
            {
                player.Health -= AttackPower;
                Console.WriteLine($"{Name} attacks {player.Name} for {AttackPower} damage!");
            }
        }

        // Method to take damage
        public void TakeDamage(int damage)
        {
            Health -= damage;
            if (Health <= 0)
            {
                Health = 0;
                Console.WriteLine($"{Name} has been defeated!");
            }
        }
    }
}



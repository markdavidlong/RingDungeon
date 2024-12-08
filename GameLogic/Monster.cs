using System;
using Engine.Interfaces;
using Engine.PatternBases;
using Engine.Structs;

namespace Engine
{
    public interface IMonsterObserver
    {
        void OnHealthChanged(Monster monster, int newHealth);
        void OnMonsterDefeated(Monster monster);
    }

    public class Monster(string name, int health, int attackPower, 
        EntityLocation location, string description = "") : Observable<IMonsterObserver>, ILocatableEntity
    {
        public string Name { get; set; } = name;
        public int Health { get; set; } = health;
        public int AttackPower { get; set; } = attackPower;
        public string Description { get; set; } = description;
        public EntityLocation Location { get; set; } = location;


        // Method for attacking a player
        public void Attack(Player player)
        {
            if (player.Health > 0)
            {
                player.Health -= AttackPower;
            }
        }

        // Method to take damage
        public void TakeDamage(int damage)
        {
            Health -= damage;
            NotifyObservers(observer => observer.OnHealthChanged(this, Health));

            if (Health <= 0)
            {
                Health = 0;
                NotifyObservers(observer => observer.OnMonsterDefeated(this));
            }
        }
    }
}



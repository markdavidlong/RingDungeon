using System;
using System.ComponentModel;
using Engine.Enums;
using Engine.Interfaces;
using Engine.PatternBases;
using Engine.Structs;

namespace Engine
{
    public interface IPlayerObserver
    {
        void OnLocationChanged(Player player, EntityLocation newLocation);
        void OnHealthChanged(Player player, int newHealth);
        void OnPlayerDefeated(Player player);
    }

    public class Player(string name) : Observable<IPlayerObserver>, ILocatableEntity
    {
        public string Name { get; set; } = name;
        public EntityLocation Location { get; set; }
        public int MaxHealth { get; set; }
        public int Health { get; set; }
        public Direction Facing { get; set; } = Direction.North;

        public bool Dead { get => Health <= 0; }



    }
}

using System;
using System.Collections.Generic;

namespace Engine.PatternBases
{
    public abstract class Observable<TObserver>
    {
        private readonly List<TObserver> _observers = [];

        public void AddObserver(TObserver observer)
        {
            _observers.Add(observer);
        }

        public void RemoveObserver(TObserver observer)
        {
            _observers.Remove(observer);
        }

        protected void NotifyObservers(Action<TObserver> notification)
        {
            foreach (var observer in _observers)
            {
                notification(observer);
            }
        }
    }
}
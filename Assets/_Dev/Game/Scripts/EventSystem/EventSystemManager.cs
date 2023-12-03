using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Dev.Game.Scripts.EventSystem
{
    public static class EventSystemManager
    {
        private static readonly Dictionary<EventId, List<Action<EventArgs>>> _listenersDictionary = new();

        public static void AddListener(EventId id, Action<EventArgs> observer)
        {
            if (!_listenersDictionary.ContainsKey(id))
            {
                _listenersDictionary.Add(id, new List<Action<EventArgs>>());
            }
            
            if (!_listenersDictionary[id].Contains(observer)) 
            {
                _listenersDictionary[id].Add(observer); 
            }
        }
        
        public static void RemoveListener(EventId id, Action<EventArgs> observer)
        {
            if (!_listenersDictionary.ContainsKey(id)) return;
            
            if (_listenersDictionary[id].Contains(observer))
            {
                _listenersDictionary[id].Remove(observer);
            }
        }

        public static void InvokeEvent(EventId id, EventArgs args = default)
        {
            if (!_listenersDictionary.ContainsKey(id)) return;
            
            foreach (var observer in _listenersDictionary[id])
            {
                observer?.Invoke(args);
            }
        }
    }
}

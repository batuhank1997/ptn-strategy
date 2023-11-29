using UnityEngine;

namespace _Dev.Utilities.Singleton
{
    public class ScriptableSingleton<T> : ScriptableObject where T : ScriptableSingleton<T>
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    T[] assets = Resources.LoadAll<T>("");

                    if (assets == null || assets.Length < 1)
                        throw new System.Exception($"No instance of { typeof(T) } found in Resources folder");
                    if (assets.Length > 1)
                        Debug.LogWarning($"Multiple instances of { typeof(T)} found in Resources folder");
                    
                    _instance = assets[0];
                }
                return _instance;
            }
        }
    }
}
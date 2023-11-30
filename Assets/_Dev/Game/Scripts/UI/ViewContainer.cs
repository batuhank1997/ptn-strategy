using System.Linq;
using _Dev.Game.Scripts.UI.Views.Base;
using _Dev.Utilities.Singleton;
using UnityEngine;

namespace _Dev.Game.Scripts.UI
{
    [CreateAssetMenu(fileName = "ViewContainer", menuName = "ScriptableObjects/ViewContainer", order = 1)]
    public class ViewContainer : ScriptableSingleton<ViewContainer>
    {
        [SerializeField] private View[] m_views;
        
        public View GetViewPrefab<T>() where T : View
        {
            return m_views.OfType<T>().FirstOrDefault();
        }
    }
}
using _Dev.Utilities.Singleton;
using UnityEngine;

namespace _Dev.Game.Scripts.Managers
{
    public class CameraManager : Singleton<CameraManager>
    {
        private Vector2 _resolution;
        private Camera _mainCamera;
        
        public void Initilize()
        {
            _mainCamera = Camera.main;
            _resolution = new Vector2(Screen.width, Screen.height);
        }
        
        public Vector2 GetResolution()
        {
            return _resolution;
        }
        
        public Vector2 GetLeftCornerCoordinates()
        {
            return _mainCamera.ViewportToWorldPoint(new Vector2(0,1));   
        }
    }
}

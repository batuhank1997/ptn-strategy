using _Dev.Utilities.Singleton;
using UnityEngine;

namespace _Dev.Game.Scripts.Managers
{
    public class CameraManager : Singleton<CameraManager>
    {
        private Camera _mainCamera;
        
        private const int BASE_SIZE = 5;
        private const float BASE_ASPECT_RATIO = 1.6f;
        
        public void Initilize()
        {
            _mainCamera = Camera.main;
        }

        private void SetCameraSize(float targetAspectRatio)
        {
            _mainCamera.orthographicSize = BASE_SIZE * (targetAspectRatio / BASE_ASPECT_RATIO);
        }
    }
}

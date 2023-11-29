using System;
using UnityEngine;

namespace _Dev.Game.Scripts.Managers
{
    public class GameManager : MonoBehaviour
    {
        private void Awake()
        {
            InitilizeGame();
        }
        
        private void InitilizeGame()
        {
            try
            {
                CameraManager.Instance.Initilize();
                GridManager.Instance.Initilize();
                GridInputManager.Instance.Initilize();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Game Manager Initilization Failed: {e}");
                throw;
            }
        }
    }
}
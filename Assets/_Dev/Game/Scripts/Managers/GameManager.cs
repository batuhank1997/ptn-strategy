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
                InputManager.Instance.Initilize();
                GridManager.Instance.Initilize();
                CameraManager.Instance.Initilize();
                UnitManager.Instance.Initilize();
                PlacingManager.Instance.Initilize();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Game Manager Initilization Failed: {e}");
                throw;
            }
        }
    }
}
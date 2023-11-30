using UnityEngine;

namespace _Dev.Game.Scripts.Entities.Buildings
{
    public class Barrack : Building, IProducer
    {
        public Barrack()
        {
            _buildingSo = Resources.Load<BuildingSo>("Buildings/Barrack");
        }
        
        public void Produce()
        {
            
        }
    }
}

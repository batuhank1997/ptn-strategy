using _Dev.Game.Scripts.Components;
using _Dev.Game.Scripts.Entities.Units;

namespace _Dev.Game.Scripts.Entities.Buildings
{
    public abstract class Building : BoardProduct
    {
        protected Building()
        {
            Health = new Health();
        }
        
        public Health Health { get; set; }
    }
}

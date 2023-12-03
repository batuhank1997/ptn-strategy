using System.Collections.Generic;
using System.Linq;
using _Dev.Game.Scripts.Entities;
using _Dev.Game.Scripts.Entities.Units;

namespace _Dev.Game.Scripts.Components
{
    public class UnitMover
    {
        public void MoveToAlongPath(List<Cell> path, Unit unitToMove)
        {
            var target = path.Last();
            target.PlaceUnits(new List<Unit>() { unitToMove });
        }
    }
}

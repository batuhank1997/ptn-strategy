using System;
using _Dev.Game.Scripts.Components;

namespace _Dev.Game.Scripts.Entities.Units
{
    public abstract class Unit : BoardProduct
    {
        public UnitMover Mover { get; set; }
        public Type GetProductType() => GetType();

    }
}

using _Dev.Game.Scripts.Components;
using UnityEngine;

namespace _Dev.Game.Scripts.Entities.Units
{
    public abstract class Unit : MonoBehaviour, IProduct
    {
        protected Health _health;
        protected UnitMover _unitMover;
    }
}

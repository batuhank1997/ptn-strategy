namespace _Dev.Game.Scripts.Entities.Units
{
    public class UnitFactory
    {
        public Unit Create<T>() where T : Unit , new()
        {
            return new T();
        }
    }
}
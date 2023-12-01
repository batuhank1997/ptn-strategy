namespace _Dev.Game.Scripts.Components
{
    public class Health
    {
        private int _value;
        
        public Health(int value = 100)
        {
            _value = value;
        }

        public void Damage(int amount)
        {
            _value -= amount;
        }
        
        public void Heal(int amount)
        {
            _value += amount;
        }

        public int GetValue()
        {
            return _value;
        }
    }
}

namespace Assets.Scripts.PartThree.Characteristics
{
    public class CharacteristicWeaponBase : CharacteristicBase
    {
        private int _damage;
        private int _reload;
        
        public int Damage => _damage;
        public int Reload => _reload;

        public CharacteristicWeaponBase(CharacteristicType characteristicType, int damage, int reload) : base(characteristicType)
        {
            _damage = damage;
            _reload = reload;
        }
    }
}
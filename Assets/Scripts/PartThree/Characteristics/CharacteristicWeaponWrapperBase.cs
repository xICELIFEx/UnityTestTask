using System;

namespace Assets.Scripts.PartThree.Characteristics
{
    public class CharacteristicWeaponWrapperBase : CharacteristicWrapper<CharacteristicWeaponBase>
    {
        public event Action<CharacteristicWeaponWrapperBase, int> OnDamageChanged;
        public event Action<CharacteristicWeaponWrapperBase, int> OnReloadChanged; 
        
        private int _damage;
        private int _reload;

        public int Damage
        {
            get { return _damage; }
            set
            {
                _damage = value;
                OnDamageChanged?.Invoke(this, _damage);
            }
        }

        public int Reload
        {
            get { return _reload; }
            set
            {
                _reload = value;
                OnReloadChanged?.Invoke(this, _reload);
            }
        }

        public CharacteristicWeaponWrapperBase(CharacteristicWeaponBase characteristicBase) : base(characteristicBase)
        {
            _damage = characteristicBase.Damage;
            _reload = characteristicBase.Reload;
        }
    }
}
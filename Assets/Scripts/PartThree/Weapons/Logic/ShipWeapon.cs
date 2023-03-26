using Assets.Scripts.PartThree.Characteristics;

namespace Assets.Scripts.PartThree.Weapons
{
    public class ShipWeapon : IShipWeapon
    {
        private string _id;
        private CharacteristicWeaponWrapperBase _characteristic;

        public string Id => _id;
        public CharacteristicWeaponWrapperBase Characteristic => _characteristic;
        
        public ShipWeapon(int damage, int reload)
        {
            _id = this.GetHashCode().ToString();
            _characteristic = new CharacteristicWeaponWrapperBase(new CharacteristicWeaponBase(CharacteristicType.Weapon, damage, reload));
        }
    }
}
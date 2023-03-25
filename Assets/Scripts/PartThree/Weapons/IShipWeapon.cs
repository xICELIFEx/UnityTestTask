using Assets.Scripts.PartThree.Characteristics;

namespace Assets.Scripts.PartThree.Weapons
{
    public interface IShipWeapon
    {
        string Id { get; }
        CharacteristicWeaponWrapperBase Characteristic { get; }
    }
}
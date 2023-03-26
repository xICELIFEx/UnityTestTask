namespace Assets.Scripts.PartThree.Weapons
{
    public class WeaponFactory
    {
        public IShipWeapon Create(WeaponConfig weaponConfig)
        {
            IShipWeapon result = null; 
            if (weaponConfig == null) { return result; }
            
            result = new ShipWeapon(weaponConfig.Damage, (int)weaponConfig.ReloadTime);

            return result;
        }
    }
}
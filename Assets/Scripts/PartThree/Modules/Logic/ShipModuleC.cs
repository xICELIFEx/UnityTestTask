using System.Collections.Generic;
using Assets.Scripts.PartThree.Characteristics;

namespace Assets.Scripts.PartThree.Modules
{
    public class ShipModuleC : ShipModuleBase
    {
        private List<CharacteristicWeaponWrapperBase> proccessedWeapon = new List<CharacteristicWeaponWrapperBase>();
        
        public ShipModuleC(int value) : base(new CharacteristicModuleWrapperBase(new CharacteristicModuleBase(CharacteristicType.Module, ModuleType.WeaponReload, value))) { }
        
        public override void OnAdd(CharacteristicContainer characteristicContainer)
        {
            List<CharacteristicWeaponWrapperBase> weaponCharacteristics = characteristicContainer.GetAll<CharacteristicWeaponWrapperBase>(CharacteristicType.Weapon);
            for (int i = 0; i < weaponCharacteristics.Count; i++)
            {
                weaponCharacteristics[i].Reload -= (int)(weaponCharacteristics[i].CharacteristicBase.Reload * MultipliedValue);
                proccessedWeapon.Add(weaponCharacteristics[i]);
            }
        }

        public override void OnRemove(CharacteristicContainer characteristicContainer)
        {
            List<CharacteristicWeaponWrapperBase> weaponCharacteristics = characteristicContainer.GetAll<CharacteristicWeaponWrapperBase>(CharacteristicType.Weapon);
            for (int i = 0; i < weaponCharacteristics.Count; i++)
            {
                if (proccessedWeapon.Contains(weaponCharacteristics[i]))
                {
                    weaponCharacteristics[i].Reload += (int)(weaponCharacteristics[i].CharacteristicBase.Reload * MultipliedValue);
                }
            }

            proccessedWeapon.Clear();
        }
        
        public override void OnUpdate(CharacteristicContainer characteristicContainer)
        {
            OnRemove(characteristicContainer);
            OnAdd(characteristicContainer);
        }
    }
}
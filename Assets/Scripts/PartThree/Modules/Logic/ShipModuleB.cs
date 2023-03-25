using Assets.Scripts.PartThree.Characteristics;

namespace Assets.Scripts.PartThree.Modules
{
    public class ShipModuleB : ShipModuleBase
    {
        public ShipModuleB(int value) : base(new CharacteristicModuleWrapperBase(
            new CharacteristicModuleBase(CharacteristicType.Module, ModuleType.HpAmount, value)))
        { }
        
        public override void OnAdd(CharacteristicContainer characteristicContainer)
        {
            CharacteristicShipWrapper characteristicShipWrapper = characteristicContainer.GetFirst<CharacteristicShipWrapper>(CharacteristicType.Ship); 
            if (characteristicShipWrapper == null) { return; }
            characteristicShipWrapper.HpAmount += _characteristic.CharacteristicBase.Value;
        }

        public override void OnRemove(CharacteristicContainer characteristicContainer)
        {
            CharacteristicShipWrapper characteristicShipWrapper = characteristicContainer.GetFirst<CharacteristicShipWrapper>(CharacteristicType.Ship); 
            if (characteristicShipWrapper == null) { return; }
            characteristicShipWrapper.HpAmount -= _characteristic.CharacteristicBase.Value;
        }
    }
}
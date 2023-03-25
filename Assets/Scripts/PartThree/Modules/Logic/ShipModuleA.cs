using Assets.Scripts.PartThree.Characteristics;

namespace Assets.Scripts.PartThree.Modules
{
    public class ShipModuleA : ShipModuleBase
    {
        public ShipModuleA(int value) : base(new CharacteristicModuleWrapperBase(
            new CharacteristicModuleBase(CharacteristicType.Module, ModuleType.SheeldAmount, value)))
        { }
        
        public override void OnAdd(CharacteristicContainer characteristicContainer)
        {
            CharacteristicShipWrapper characteristicShipWrapper = characteristicContainer.GetFirst<CharacteristicShipWrapper>(CharacteristicType.Ship); 
            if (characteristicShipWrapper == null) { return; }
            characteristicShipWrapper.SheeldAmount += _characteristic.CharacteristicBase.Value;
        }

        public override void OnRemove(CharacteristicContainer characteristicContainer)
        {
            CharacteristicShipWrapper characteristicShipWrapper = characteristicContainer.GetFirst<CharacteristicShipWrapper>(CharacteristicType.Ship); 
            if (characteristicShipWrapper == null) { return; }
            characteristicShipWrapper.SheeldAmount -= _characteristic.CharacteristicBase.Value;
        }
    }
}
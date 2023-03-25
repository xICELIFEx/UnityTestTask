using Assets.Scripts.PartThree.Characteristics;

namespace Assets.Scripts.PartThree.Modules
{
    public class ShipModuleD : ShipModuleBase
    {
        public ShipModuleD(int value) : base(
            new CharacteristicModuleWrapperBase(new CharacteristicModuleBase(CharacteristicType.Module, ModuleType.SheeldRestore, value)))
        { }
        
        public override void OnAdd(CharacteristicContainer characteristicContainer)
        {
            CharacteristicShipWrapper characteristicShipWrapper = characteristicContainer.GetFirst<CharacteristicShipWrapper>(CharacteristicType.Ship); 
            if (characteristicShipWrapper == null) { return; }
            characteristicShipWrapper.SheeldRestore += (int)(characteristicShipWrapper.CharacteristicBase.SheeldRestore * MultipliedValue);
        }

        public override void OnRemove(CharacteristicContainer characteristicContainer)
        {
            CharacteristicShipWrapper characteristicShipWrapper = characteristicContainer.GetFirst<CharacteristicShipWrapper>(CharacteristicType.Ship); 
            if (characteristicShipWrapper == null) { return; }
            characteristicShipWrapper.SheeldRestore -= (int)(characteristicShipWrapper.CharacteristicBase.SheeldRestore * MultipliedValue);
        }
    }
}
using Assets.Scripts.PartThree.Modules;

namespace Assets.Scripts.PartThree.Characteristics
{
    public class CharacteristicModuleBase : CharacteristicBase
    {
        private ModuleType _moduleType; 
        
        private int _value;
        
        public ModuleType ModuleType => _moduleType;
        
        public int Value => _value;
        
        public CharacteristicModuleBase(
            CharacteristicType characteristicType,
            ModuleType moduleType,
            int value) : base(characteristicType)
        {
            _moduleType = moduleType;
            _value = value;
        }
    }
}
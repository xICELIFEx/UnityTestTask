using Assets.Scripts.PartThree.Modules;

namespace Assets.Scripts.PartThree.Characteristics
{
    public class CharacteristicModuleWrapperBase : CharacteristicWrapper<CharacteristicModuleBase>
    {
        public ModuleType ModuleType => _characteristicBase.ModuleType;
        
        public CharacteristicModuleWrapperBase(CharacteristicModuleBase characteristicBase) : base(characteristicBase)
        { }
    }
}
using Assets.Scripts.PartThree.Characteristics;

namespace Assets.Scripts.PartThree.Modules
{
    public interface IShipModule
    {
        string Id { get; }
        CharacteristicModuleWrapperBase Characteristic { get; } 
        
        void OnAdd(CharacteristicContainer characteristicContainer);
        void OnRemove(CharacteristicContainer characteristicContainer);
        void OnUpdate(CharacteristicContainer characteristicContainer);
    }
}
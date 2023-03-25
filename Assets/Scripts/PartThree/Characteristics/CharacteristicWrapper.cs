namespace Assets.Scripts.PartThree.Characteristics
{
    public class CharacteristicWrapper<T> : CharacteristicWrapperBase where T : CharacteristicBase
    {
        protected T _characteristicBase;
        
        public T CharacteristicBase => _characteristicBase;
        public override CharacteristicType CharacteristicType => CharacteristicBase.CharacteristicType;

        public CharacteristicWrapper(T characteristicBase)
        {
            _characteristicBase = characteristicBase;
        }
    }
}
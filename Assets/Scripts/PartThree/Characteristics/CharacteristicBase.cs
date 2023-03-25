namespace Assets.Scripts.PartThree.Characteristics
{
    public class CharacteristicBase
    {
        protected CharacteristicType _characteristicType;
        
        public CharacteristicType CharacteristicType => _characteristicType;

        public CharacteristicBase(CharacteristicType characteristicType)
        {
            _characteristicType = characteristicType;
        }
    }
}
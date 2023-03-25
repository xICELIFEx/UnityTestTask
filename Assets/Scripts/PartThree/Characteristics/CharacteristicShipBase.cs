namespace Assets.Scripts.PartThree.Characteristics
{
    public class CharacteristicShipBase : CharacteristicBase
    {
        private int _hpAmount;
        private int _sheeldAmount;
        private int _sheeldRestore;
        private int _moduleCount;
        private int _weaponCount;

        public int HpAmount => _hpAmount;
        public int SheeldAmount => _sheeldAmount;
        public int SheeldRestore => _sheeldRestore;
        public int ModuleCount => _moduleCount;
        public int WeaponCount => _weaponCount;

        public CharacteristicShipBase(
            int hpAmount,
            int sheeldAmount,
            int sheeldRestore,
            int moduleCount,
            int weaponCount)
            : base(CharacteristicType.Ship)
        {
            _hpAmount = hpAmount;
            _sheeldAmount = sheeldAmount;
            _sheeldRestore = sheeldRestore;
            _moduleCount = moduleCount;
            _weaponCount = weaponCount;
        }
    }
}
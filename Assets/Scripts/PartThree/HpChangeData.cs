namespace Assets.Scripts.PartThree
{
    public class HpChangeData
    {
        private string _sourceId;
        private int _hpChangeValue;
        private bool _isDamage;

        public string SourceId => _sourceId;
        public int HpChangeValue => _hpChangeValue;
        public bool IsDamage => _isDamage;

        public HpChangeData(int hpChangeValue, bool isDamage, string sourceId)
        {
            _sourceId = sourceId;
            _hpChangeValue = hpChangeValue;
            _isDamage = isDamage;
        }
    }
}
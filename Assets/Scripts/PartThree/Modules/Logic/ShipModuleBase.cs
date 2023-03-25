using System;
using Assets.Scripts.PartThree.Characteristics;

namespace Assets.Scripts.PartThree.Modules
{
    public abstract class ShipModuleBase : IShipModule
    {
        protected string _id;
        protected CharacteristicModuleWrapperBase _characteristic;
        protected float _multipliedValue = Single.NaN;
        
        public string Id => _id;
        public CharacteristicModuleWrapperBase Characteristic => _characteristic;

        public float MultipliedValue
        {
            get
            {
                if (_multipliedValue.Equals(Single.NaN))
                {
                    _multipliedValue = (float) _characteristic.CharacteristicBase.Value / 100f;
                }

                return _multipliedValue;
            }
        }
        
        public ShipModuleBase(CharacteristicModuleWrapperBase characteristic)
        {
            _id = this.GetHashCode().ToString();
            _characteristic = characteristic;
        }

        public abstract void OnAdd(CharacteristicContainer characteristicContainer);
        public abstract void OnRemove(CharacteristicContainer characteristicContainer);

        public virtual void OnUpdate(CharacteristicContainer characteristicContainer) { /*do nothing*/ }
    }
}
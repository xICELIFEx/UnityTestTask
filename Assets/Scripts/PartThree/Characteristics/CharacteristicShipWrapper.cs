﻿using System;

namespace Assets.Scripts.PartThree.Characteristics
{
    public class CharacteristicShipWrapper : CharacteristicWrapper<CharacteristicShipBase>
    {
        public event Action<int, int> OnHpAmountChanged;
        public event Action<int, int> OnSheeldAmountChanged;
        public event Action<int, int> OnSheeldRestoreChanged;
        public event Action<int, int> OnModuleCountChanged;
        public event Action<int, int> OnWeaponCountChanged;
        
        private int _hpAmount;
        private int _sheeldAmount;
        private int _sheeldRestore;
        private int _moduleCount;
        private int _weaponCount;
        
        public int HpAmount
        {
            get { return _hpAmount; }
            set
            {
                int oldValue = _hpAmount;
                _hpAmount = value;
                OnHpAmountChanged?.Invoke(oldValue, _hpAmount);
            }
        }
        
        public int SheeldAmount
        {
            get { return _sheeldAmount; }
            set
            {
                int oldValue = _sheeldAmount;
                _sheeldAmount = value;
                OnSheeldAmountChanged?.Invoke(oldValue, _sheeldAmount);
            }
        }
        
        public int SheeldRestore
        {
            get { return _sheeldRestore; }
            set
            {
                int oldValue = _sheeldRestore;
                _sheeldRestore = value;
                OnSheeldRestoreChanged?.Invoke(oldValue, _sheeldRestore);
            }
        }
        
        public int ModuleCount
        {
            get { return _moduleCount; }
            set
            {
                int oldValue = _moduleCount;
                _moduleCount = value;
                OnModuleCountChanged?.Invoke(oldValue, _moduleCount);
            }
        }
        
        public int WeaponCount
        {
            get { return _weaponCount; }
            set
            {
                int oldValue = _weaponCount;
                _weaponCount = value;
                OnWeaponCountChanged?.Invoke(oldValue, _weaponCount);
            }
        }

        public CharacteristicShipWrapper(CharacteristicShipBase characteristicShipBase)
        : base(characteristicShipBase)
        {
            _hpAmount = _characteristicBase.HpAmount;
            _sheeldAmount = _characteristicBase.SheeldAmount;
            _sheeldRestore = _characteristicBase.SheeldRestore;
            _moduleCount = _characteristicBase.ModuleCount;
            _weaponCount = _characteristicBase.WeaponCount;
        }
    }
}
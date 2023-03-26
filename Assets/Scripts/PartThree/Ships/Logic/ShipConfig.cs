using System;
using UnityEngine;

namespace Assets.Scripts.PartThree.Ships
{
    [Serializable]
    public class ShipConfig
    {
        [SerializeField] private string _description;
        [SerializeField] private int _hpAmount;
        [SerializeField] private int _sheeldAmout;
        [SerializeField] private int _sheeldRestore;
        [SerializeField] private int _moduleCount;
        [SerializeField] private int _weaponCount;

        public string Description => _description;
        public int HpAmount => _hpAmount;
        public int SheeldAmout => _sheeldAmout;
        public int SheeldRestore => _sheeldRestore;
        public int ModuleCount => _moduleCount;
        public int WeaponCount => _weaponCount;
    }
}
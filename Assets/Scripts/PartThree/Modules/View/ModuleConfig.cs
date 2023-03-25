using System;
using UnityEngine;

namespace Assets.Scripts.PartThree.Modules
{
    [Serializable]
    public class ModuleConfig
    {
        [SerializeField] private ModuleType _moduleType;
        [SerializeField] private string _description;
        [SerializeField] private int _value;
        [SerializeField] private bool _isPercentValue;

        public ModuleType ModuleType => _moduleType;
        public string Description => _description;
        public int Value => _value;
        public bool IsPercentValue => _isPercentValue;
    }
}
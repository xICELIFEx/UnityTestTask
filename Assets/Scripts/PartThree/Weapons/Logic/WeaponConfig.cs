using System;
using UnityEngine;

namespace Assets.Scripts.PartThree.Weapons
{
    [Serializable]
    public class WeaponConfig
    {
        [SerializeField] private string _description;
        [SerializeField] private int _damage;
        [SerializeField] [Tooltip("in milliseconds")]private float _reloadTime;

        public string Description => _description;
        public int Damage => _damage;
        public float ReloadTime => _reloadTime;
    }
}
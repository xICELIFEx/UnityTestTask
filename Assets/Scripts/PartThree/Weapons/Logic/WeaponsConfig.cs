using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.PartThree.Weapons
{
    [CreateAssetMenu(fileName = "WeaponsConfig", menuName = "ScriptableObjects/CreateWeaponsConfig", order = 1)]
    public class WeaponsConfig : ScriptableObject
    {
        [SerializeField] private List<WeaponConfig> _weaponsData = new List<WeaponConfig>();
        
        public IReadOnlyList<WeaponConfig> WeaponsData => _weaponsData;
    }
}
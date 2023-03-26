using System;
using UnityEngine;

namespace Assets.Scripts.PartThree.Weapons
{
    public class ChooseWeaponsPanel : MonoBehaviour
    {
        public event Action<WeaponConfig> OnWeaponClicked;
        
        [SerializeField] private WeaponsConfig _weaponsConfig;
        [SerializeField] private WeaponListView _weaponListView;
        
        private void Awake()
        {
            _weaponListView.OnWeaponClickedEvent += OnWeaponClickedEventHandler;
            _weaponListView.AddWeaponToList(null);
            
            for (int i = 0; i < _weaponsConfig.WeaponsData.Count; i++)
            {
                _weaponListView.AddWeaponToList(_weaponsConfig.WeaponsData[i]);
            }
        }

        public void OnDestroy()
        {
            _weaponListView.OnWeaponClickedEvent -= OnWeaponClickedEventHandler;
        }

        private void OnWeaponClickedEventHandler(WeaponConfig weaponConfig)
        {
            OnWeaponClicked?.Invoke(weaponConfig);
        }
    }
}
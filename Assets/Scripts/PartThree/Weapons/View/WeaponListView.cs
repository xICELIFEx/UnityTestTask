using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.PartThree.Weapons
{
    //TODO: merge with ModuleListView
    public class WeaponListView : MonoBehaviour
    {
        public event Action<WeaponConfig> OnWeaponClickedEvent;
        
        [SerializeField] private WeaponView _weaponPrefab;
        [SerializeField] private ScrollRect _weaponList;
        
        private List<WeaponView> _weaponViews = new List<WeaponView>();
        
        public void AddWeaponToList(WeaponConfig weaponConfig)
        {
            WeaponView weaponView = GameObject.Instantiate(_weaponPrefab, _weaponList.content);
            weaponView.OnWeaponButtonClickedEvent += OnWeaponButtonClickedEventHandler;
            weaponView.Setup(weaponConfig);
            _weaponViews.Add(weaponView);
        }
        
        public void RemoveWeaponFromList(WeaponConfig weaponConfig)
        {
            WeaponView weaponView = null;
            for (int i = 0; i < _weaponViews.Count; i++)
            {
                if (_weaponViews[i].WeaponConfig == weaponConfig)
                {
                    weaponView = _weaponViews[i];
                    break;
                }
            }
        
            if (weaponView != null)
            {
                RemoveWeaponView(weaponView);
            }
        }
        
        public void Clear()
        {
            while (_weaponViews.Count > 0) { RemoveWeaponView(_weaponViews[0]); }
        }
        
        private void RemoveWeaponView(WeaponView weaponView)
        {
            _weaponViews.Remove(weaponView);
            weaponView.OnWeaponButtonClickedEvent -= OnWeaponButtonClickedEventHandler;
            Destroy(weaponView.gameObject);
        }
        
        private void OnWeaponButtonClickedEventHandler(WeaponView weaponView)
        {
            OnWeaponClickedEvent?.Invoke(weaponView.WeaponConfig);
        }
    }
}
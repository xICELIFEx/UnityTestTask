using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.PartThree.Weapons
{
    public class WeaponView : MonoBehaviour
    {
        public event Action<WeaponView> OnWeaponButtonClickedEvent;
        
        [SerializeField] private TMP_Text _weaponDescription;
        [SerializeField] private Button _weaponButton;

        private WeaponConfig _weaponConfig;
        
        public WeaponConfig WeaponConfig => _weaponConfig;
        
        private void Awake()
        {
            _weaponButton.onClick.AddListener(OnWeaponButtonClicked);
        }

        private void OnDestroy()
        {
            _weaponButton.onClick.RemoveAllListeners();
        }

        public void Setup(WeaponConfig weaponConfig)
        {
            _weaponConfig = weaponConfig;
            if (weaponConfig == null)
            {
                _weaponDescription.text = "NONE";
                return;
            }

            _weaponDescription.text = String.Format(weaponConfig.Description, weaponConfig.Damage, weaponConfig.ReloadTime);
        }
        
        private void OnWeaponButtonClicked()
        {
            OnWeaponButtonClickedEvent?.Invoke(this);
        }
    }
}
using System;
using System.Collections.Generic;
using Assets.Scripts.PartThree.Characteristics;
using Assets.Scripts.PartThree.Modules;
using Assets.Scripts.PartThree.Weapons;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.PartThree.Ships
{
    public class ShipViewPanel : MonoBehaviour
    {
        public event Action<ShipViewPanel> OnSelectModule;
        public event Action<ShipViewPanel> OnSelectWeapon;
        
        [SerializeField] private ShipsConfig _shipsConfig;
        [SerializeField] private TMP_Dropdown _shipTypeDropdown;
        [SerializeField] private ModuleListView _modulesList;
        [SerializeField] private WeaponListView _weaponsList;
        [SerializeField] private TMP_Text _shipHpAmount;
        [SerializeField] private TMP_Text _shipSheeldAmount;
        [SerializeField] private TMP_Text _shipSheeldRestore;
        [SerializeField] private TMP_Text _shipDPS;
        
        private Ship _ship;
        private ModuleFactory _moduleFactory = new ModuleFactory();
        private ModuleConfig _selectedModuleConfig;
        
        private List<KeyValuePair<ModuleConfig, IShipModule>> _setupModules =
            new List<KeyValuePair<ModuleConfig, IShipModule>>();
        
        private WeaponFactory _weaponFactory = new WeaponFactory();
        private WeaponConfig _selectedWeaponConfig;
        
        private List<KeyValuePair<WeaponConfig, IShipWeapon>> _setupWeapons =
            new List<KeyValuePair<WeaponConfig, IShipWeapon>>();
        
        public Ship Ship => _ship;
        
        private void Awake()
        {
            _shipTypeDropdown.ClearOptions();
            List<string> shipTypes = new List<string>();
            for (int i = 0; i < _shipsConfig.ShipsData.Count; i++)
            {
                shipTypes.Add(_shipsConfig.ShipsData[i].Description);
            }
            _shipTypeDropdown.AddOptions(shipTypes);
            _shipTypeDropdown.onValueChanged.AddListener(OnShipDropdownValueChanged);
            
            _modulesList.OnModuleClickedEvent += OnModuleClickedEventHandler;
            _weaponsList.OnWeaponClickedEvent += OnWeaponClickedEventHandler;

            OnShipDropdownValueChanged(_shipTypeDropdown.value);
        }
        
        private void OnDestroy()
        {
            _weaponsList.OnWeaponClickedEvent -= OnWeaponClickedEventHandler;
            _modulesList.OnModuleClickedEvent -= OnModuleClickedEventHandler;

            if (_ship != null)
            {
                _ship.OnSheeldRestoreChanged -= OnSheeldRestoreChangedHandler;
                _ship.OnCurrentSheeldChanged -= OnCurrentSheeldChangedHandler;
                _ship.OnCurrentHpChanged -= OnCurrentHpChangedHandler;
            }

            _shipTypeDropdown.onValueChanged.RemoveAllListeners();
        }

        private void OnShipDropdownValueChanged(int typeIndex)
        {
            ShipConfig shipConfig = null; 
            for (int i = 0; i < _shipsConfig.ShipsData.Count; i++)
            {
                if (_shipsConfig.ShipsData[i].Description == _shipTypeDropdown.options[_shipTypeDropdown.value].text)
                {
                    shipConfig = _shipsConfig.ShipsData[i];
                    break;
                }
            }

            if (shipConfig == null) { return; }

            RemoveShip();
            SetupShip(shipConfig);
        }

        private void SetupShip(ShipConfig shipConfig)
        {
            // CharacteristicShipBase characteristicShipA = new CharacteristicShipBase(hpAmount:100, sheeldAmount:80, sheeldRestore:100, moduleCount:2, weaponCount:2);
            CharacteristicShipBase characteristicShip = new CharacteristicShipBase(shipConfig.HpAmount, shipConfig.SheeldAmout, shipConfig.SheeldRestore, shipConfig.ModuleCount, shipConfig.WeaponCount);
            _shipHpAmount.text = characteristicShip.HpAmount.ToString();
            _shipSheeldAmount.text = characteristicShip.SheeldAmount.ToString();
            //TODO: rework Sheeld Restore property
            _shipSheeldRestore.text = ((float)characteristicShip.SheeldRestore/100f).ToString();
            _shipDPS.text = "0";
            _ship = new Ship(characteristicShip);
            _ship.IsPaused = true;

            _ship.OnCurrentHpChanged += OnCurrentHpChangedHandler;
            _ship.OnCurrentSheeldChanged += OnCurrentSheeldChangedHandler;
            _ship.OnSheeldRestoreChanged += OnSheeldRestoreChangedHandler;
            _ship.OnWeaponsChanged += OnWeaponsChangedHandler;
            _ship.OnModulesChanged += OnModulesChangedHandler;

            CharacteristicShipWrapper characteristicShipWrapper = _ship.CharacteristicContainer.GetFirst<CharacteristicShipWrapper>(CharacteristicType.Ship); 
            for (int i = 0; i < characteristicShipWrapper.ModuleCount; i++)
            {
                _modulesList.AddModuleToList(null);
            }
            
            for (int i = 0; i < characteristicShipWrapper.WeaponCount; i++)
            {
                _weaponsList.AddWeaponToList(null);
            }
        }

        private void RemoveShip()
        {
            if (_ship == null) { return; }
            
            _ship.OnModulesChanged -= OnModulesChangedHandler;
            _ship.OnWeaponsChanged -= OnWeaponsChangedHandler;
            _ship.OnSheeldRestoreChanged -= OnSheeldRestoreChangedHandler;
            _ship.OnCurrentSheeldChanged -= OnCurrentSheeldChangedHandler;
            _ship.OnCurrentHpChanged -= OnCurrentHpChangedHandler;
            
            _modulesList.Clear();
            _setupModules.Clear();
            
            _weaponsList.Clear();
            _setupWeapons.Clear();
        }
        
        private void OnCurrentHpChangedHandler(string shipId, int oldValue, int newValue)
        {
            _shipHpAmount.text = newValue.ToString();
        }
        
        private void OnCurrentSheeldChangedHandler(string shipId, int oldValue, int newValue)
        {
            _shipSheeldAmount.text = newValue.ToString();
        }
        
        private void OnSheeldRestoreChangedHandler(string shipId, int oldValue, int newValue)
        {
            _shipSheeldRestore.text = (_ship.CharacteristicContainer.GetFirst<CharacteristicShipWrapper>(CharacteristicType.Ship).SheeldRestore/100f).ToString();
        }
        
        private void OnWeaponsChangedHandler()
        {
            float currentDPS = 0f;
            for (int i = 0; i < _ship.Weapons.Count; i++)
            {
                currentDPS += (float) _ship.Weapons[i].Characteristic.Damage / _ship.Weapons[i].Characteristic.ReloadTimer;
            }

            _shipDPS.text = currentDPS.ToString();
        }
        
        private void OnModulesChangedHandler()
        {
            //For recalculate dps if we add or remove module with change weapon reload time
            OnWeaponsChangedHandler();
        }

        private void OnModuleClickedEventHandler(ModuleConfig moduleConfig)
        {
            _selectedModuleConfig = moduleConfig;
            OnSelectModule?.Invoke(this);
        }

        private void OnWeaponClickedEventHandler(WeaponConfig weaponConfig)
        {
            _selectedWeaponConfig = weaponConfig;
            OnSelectWeapon?.Invoke(this);
        }
        
        public void SetModule(ModuleConfig moduleConfig)
        {
            _modulesList.RemoveModuleFromList(_selectedModuleConfig);
            _modulesList.AddModuleToList(moduleConfig);
            
            if (_selectedModuleConfig != null)
            {
                KeyValuePair<ModuleConfig, IShipModule> targetPair;
                bool Found = false;
                for (int i = 0; i < _setupModules.Count; i++)
                {
                    if (_setupModules[i].Key.Equals(_selectedModuleConfig))
                    {
                        targetPair = _setupModules[i];
                        Found = true;
                        break;
                    }
                }

                if (Found)
                {
                    _setupModules.Remove(targetPair);
                    _ship.RemoveModule(targetPair.Value.Id);
                }
                
                _selectedModuleConfig = null;
            }
            
            if (moduleConfig != null)
            {
                IShipModule shipModule = _moduleFactory.Create(moduleConfig);
                _setupModules.Add(new KeyValuePair<ModuleConfig, IShipModule>(moduleConfig, shipModule));
                _ship.AddModule(shipModule);
            }
        }
        
        public void SetWeapon(WeaponConfig weaponConfig)
        {
            _weaponsList.RemoveWeaponFromList(_selectedWeaponConfig);
            _weaponsList.AddWeaponToList(weaponConfig);
            
            if (_selectedWeaponConfig != null)
            {
                KeyValuePair<WeaponConfig, IShipWeapon> targetPair;
                bool Found = false;
                for (int i = 0; i < _setupWeapons.Count; i++)
                {
                    if (_setupWeapons[i].Key.Equals(_selectedWeaponConfig))
                    {
                        targetPair = _setupWeapons[i];
                        Found = true;
                        break;
                    }
                }

                if (Found)
                {
                    _setupWeapons.Remove(targetPair);
                    _ship.RemoveWeapon(targetPair.Value.Id);
                }
                
                _selectedWeaponConfig = null;
            }
            
            if (weaponConfig != null)
            {
                IShipWeapon shipWeapon = _weaponFactory.Create(weaponConfig);
                _setupWeapons.Add(new KeyValuePair<WeaponConfig, IShipWeapon>(weaponConfig, shipWeapon));
                _ship.AddWeapon(shipWeapon);
            }
        }
    }
}
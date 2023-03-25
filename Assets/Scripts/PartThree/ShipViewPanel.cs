using System;
using System.Collections.Generic;
using Assets.Scripts.PartThree.Characteristics;
using Assets.Scripts.PartThree.Modules;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.PartThree
{
    public class ShipViewPanel : MonoBehaviour
    {
        public event Action<ShipViewPanel> OnSelectModule;
        
        [SerializeField] private ModuleListView _modulesList;
        [SerializeField] private TMP_Text _shipHpAmount;
        [SerializeField] private TMP_Text _shipSheeldAmount;
        [SerializeField] private TMP_Text _shipSheeldRestore;
        
        private Ship _ship;
        private ModuleFactory _moduleFactory = new ModuleFactory();
        private ModuleConfig _selectedModuleConfig;
        
        private List<KeyValuePair<ModuleConfig, IShipModule>> _setupModules =
            new List<KeyValuePair<ModuleConfig, IShipModule>>();
        
        private void Awake()
        {
            CharacteristicShipBase characteristicShipA = new CharacteristicShipBase(hpAmount:100, sheeldAmount:80, sheeldRestore:100, moduleCount:2, weaponCount:2);
            _shipHpAmount.text = characteristicShipA.HpAmount.ToString();
            _shipSheeldAmount.text = characteristicShipA.SheeldAmount.ToString();
            _shipSheeldRestore.text = ((float)characteristicShipA.SheeldRestore/100f).ToString();
            _ship = new Ship(characteristicShipA);
            _ship.IsPaused = true;

            _ship.OnCurrentHpChanged += OnCurrentHpChangedHandler;
            _ship.OnCurrentSheeldChanged += OnCurrentSheeldChangedHandler;
            _ship.OnSheeldRestoreChanged += OnSheeldRestoreChangedHandler;
            
            _modulesList.OnModuleClickedEvent += OnModuleClickedEventHandler; 
            for (int i = 0; i < _ship.CharacteristicContainer.GetFirst<CharacteristicShipWrapper>(CharacteristicType.Ship).ModuleCount; i++)
            {
                _modulesList.AddModuleToList(null);
            }
        }

        private void OnDestroy()
        {
            _modulesList.OnModuleClickedEvent -= OnModuleClickedEventHandler;
            
            _ship.OnSheeldRestoreChanged -= OnSheeldRestoreChangedHandler;
            _ship.OnCurrentSheeldChanged -= OnCurrentSheeldChangedHandler;
            _ship.OnCurrentHpChanged -= OnCurrentHpChangedHandler;
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
            _shipSheeldRestore.text = ((float)newValue/100f).ToString();
        }
        
        private void OnModuleClickedEventHandler(ModuleConfig moduleConfig)
        {
            _selectedModuleConfig = moduleConfig;
            OnSelectModule?.Invoke(this);
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
    }
}
﻿using Assets.Scripts.PartThree.Characteristics;
using Assets.Scripts.PartThree.Modules;
using Assets.Scripts.PartThree.Ships;
using Assets.Scripts.PartThree.Weapons;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.PartThree
{
    public class PartThreeController : PanelControllerBase
    {
        // [SerializeField] private TMP_Text _shipAHp;
        // [SerializeField] private TMP_Text _shipASheeld;
        // [SerializeField] private TMP_Text _shipASheeldRestore;

        [SerializeField] private ShipViewPanel _shipAViewPanel;
        [SerializeField] private ShipViewPanel _shipBViewPanel;
        [SerializeField] private ChooseModulesPanel _chooseModulesPanel;
        [SerializeField] private ChooseWeaponsPanel _chooseWeaponsPanel;
        [SerializeField] private Button _startFightButton;
        
        private ShipViewPanel _activeShipViewPanel = null;
        
        private Ship _a;
        private Ship _b;
        
        protected override void Awake()
        {
            base.Awake();
            _shipAViewPanel.OnSelectModule += OnSelectModuleHandler;
            _shipAViewPanel.OnSelectWeapon += OnSelectWeaponHandler;
            
            _shipBViewPanel.OnSelectModule += OnSelectModuleHandler;
            _shipBViewPanel.OnSelectWeapon += OnSelectWeaponHandler;
            
            _chooseModulesPanel.OnModuleClicked += OnModuleClickedHandler;
            _chooseWeaponsPanel.OnWeaponClicked += OnWeaponClickedHandler;

            _startFightButton.onClick.AddListener(OnStartFightButtonClicked);
        }

        protected override void OnDestroy()
        {
            _startFightButton.onClick.RemoveAllListeners();
            
            _chooseWeaponsPanel.OnWeaponClicked -= OnWeaponClickedHandler;
            _chooseModulesPanel.OnModuleClicked -= OnModuleClickedHandler;
            
            _shipBViewPanel.OnSelectWeapon -= OnSelectWeaponHandler;
            _shipBViewPanel.OnSelectModule -= OnSelectModuleHandler;
            
            _shipAViewPanel.OnSelectWeapon -= OnSelectWeaponHandler;
            _shipAViewPanel.OnSelectModule -= OnSelectModuleHandler;
        }
        
        private void OnStartFightButtonClicked()
        {
            SimpleBattleController simpleBattleController = new SimpleBattleController();
            simpleBattleController.StartBattle(_shipAViewPanel.Ship, _shipBViewPanel.Ship);
            simpleBattleController.OnEndBattle += OnEndBattleHandler;
        }
        
        private void OnSelectModuleHandler(ShipViewPanel shipViewPanel)
        {
            //hide ship panels
            _shipAViewPanel.gameObject.SetActive(false);
            _shipBViewPanel.gameObject.SetActive(false);
            _chooseModulesPanel.gameObject.SetActive(true);
            _activeShipViewPanel = shipViewPanel;
        }
        
        private void OnSelectWeaponHandler(ShipViewPanel shipViewPanel)
        {
            //hide ship panels
            _shipAViewPanel.gameObject.SetActive(false);
            _shipBViewPanel.gameObject.SetActive(false);
            _chooseWeaponsPanel.gameObject.SetActive(true);
            _activeShipViewPanel = shipViewPanel;
        }
            
        private void OnModuleClickedHandler(ModuleConfig moduleConfig)
        {
            _activeShipViewPanel.SetModule(moduleConfig);
            _activeShipViewPanel = null;
            _shipAViewPanel.gameObject.SetActive(true);
            _shipBViewPanel.gameObject.SetActive(true);
            _chooseModulesPanel.gameObject.SetActive(false);
            //show ship panels
        }
        
        private void OnWeaponClickedHandler(WeaponConfig weaponConfig)
        {
            _activeShipViewPanel.SetWeapon(weaponConfig);
            _activeShipViewPanel = null;
            _shipAViewPanel.gameObject.SetActive(true);
            _shipBViewPanel.gameObject.SetActive(true);
            _chooseWeaponsPanel.gameObject.SetActive(false);
            //show ship panels
        }
        
        private void TestStart()
        {
            CharacteristicShipBase characteristicShipA = new CharacteristicShipBase(hpAmount:100, sheeldAmount:80, sheeldRestore:1, moduleCount:2, weaponCount:2);
            CharacteristicShipBase characteristicShipB = new CharacteristicShipBase(hpAmount:60, sheeldAmount:120, sheeldRestore:1, moduleCount:3, weaponCount:2);
            _a = new Ship(characteristicShipA);
            _a.IsPaused = true;
            
            // _a.AddModule(new ShipModuleA("module_a_1"));
            // _a.AddModule(new ShipModuleB("module_a_2"));
            
            // _a.AddWeapon(new ShipWeapon("weapon_a_1", 5, 3));
            // _a.AddWeapon(new ShipWeapon("weapon_a_2", 4, 2));

            _a.OnCurrentHpChanged += OnCurrentHpChangedHandler;
            _a.OnCurrentSheeldChanged += OnCurrentSheeldChangedHandler;
            
            _b = new Ship(characteristicShipB);
            _b.IsPaused = true;
            
            // _b.AddModule(new ShipModuleA("module_b_1"));
            // _b.AddModule(new ShipModuleB("module_b_2"));
            // _b.AddModule(new ShipModuleC("module_b_3"));
            
            // _b.AddWeapon(new ShipWeapon("weapon_a_1", 5, 3));
            // _b.AddWeapon(new ShipWeapon("weapon_a_2", 4, 2));

            _b.OnCurrentHpChanged += OnCurrentHpChangedHandler;
            _b.OnCurrentSheeldChanged += OnCurrentSheeldChangedHandler;
            
            SimpleBattleController simpleBattleController = new SimpleBattleController();
            simpleBattleController.StartBattle(_a, _b);
            simpleBattleController.OnEndBattle += OnEndBattleHandler;
        } 
        
        private void OnCurrentHpChangedHandler(string arg1, int arg2, int arg3)
        {
            if (arg1.Equals(_a.Id))
            {
                // _shipAHp.text = arg3.ToString();
            }
            Debug.LogError($"OnCurrentHpChangedHandler id {arg1} old hp {arg2} new hp {arg3}");
        }

        private void OnCurrentSheeldChangedHandler(string arg1, int arg2, int arg3)
        {
            if (arg1.Equals(_a.Id))
            {
                // _shipASheeld.text = arg3.ToString();
            }
            Debug.LogError($"OnCurrentSheeldChangedHandler id {arg1} old hp {arg2} new hp {arg3}");
        }
        
        private void OnEndBattleHandler(Ship obj)
        {
            Debug.LogError($"OnEndBattleHandler winner id {obj.Id}");
        }
        
        // private void OnCurrentHpChangedHandler()
        // {
        //     
        // }
    }
}
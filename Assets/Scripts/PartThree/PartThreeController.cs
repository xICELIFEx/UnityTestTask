using Assets.Scripts.PartThree.Modules;
using Assets.Scripts.PartThree.Ships;
using Assets.Scripts.PartThree.Weapons;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.PartThree
{
    public class PartThreeController : PanelControllerBase
    {
        [SerializeField] private ShipViewPanel _shipAViewPanel;
        [SerializeField] private ShipViewPanel _shipBViewPanel;
        [SerializeField] private ChooseModulesPanel _chooseModulesPanel;
        [SerializeField] private ChooseWeaponsPanel _chooseWeaponsPanel;
        [SerializeField] private Button _startFightButton;
        
        private ShipViewPanel _activeShipViewPanel = null;
        
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
            HidePanels();
            _chooseModulesPanel.gameObject.SetActive(true);
            _activeShipViewPanel = shipViewPanel;
        }
        
        private void OnSelectWeaponHandler(ShipViewPanel shipViewPanel)
        {
            //hide ship panels
            HidePanels();
            _chooseWeaponsPanel.gameObject.SetActive(true);
            _activeShipViewPanel = shipViewPanel;
        }
            
        private void OnModuleClickedHandler(ModuleConfig moduleConfig)
        {
            _activeShipViewPanel.SetModule(moduleConfig);
            _activeShipViewPanel = null;
            ShowPanels();
            _chooseModulesPanel.gameObject.SetActive(false);
        }
        
        private void OnWeaponClickedHandler(WeaponConfig weaponConfig)
        {
            _activeShipViewPanel.SetWeapon(weaponConfig);
            _activeShipViewPanel = null;
            ShowPanels();
            _chooseWeaponsPanel.gameObject.SetActive(false);
        }

        private void ShowPanels()
        {
            _shipAViewPanel.gameObject.SetActive(true);
            _shipBViewPanel.gameObject.SetActive(true);
            _startFightButton.gameObject.SetActive(true);
            _cancelButton.gameObject.SetActive(true);
        }
        
        private void HidePanels()
        {
            _shipAViewPanel.gameObject.SetActive(false);
            _shipBViewPanel.gameObject.SetActive(false);
            _startFightButton.gameObject.SetActive(false);
            _cancelButton.gameObject.SetActive(false);
        }
        
        private void OnEndBattleHandler(Ship obj)
        {
            Debug.LogError($"OnEndBattleHandler winner id {obj.Id}");
        }
    }
}
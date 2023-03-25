using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.PartThree.Modules
{
    public class ModuleView : MonoBehaviour
    {
        public event Action<ModuleView> OnModuleButtonClickedEvent;
        
        [SerializeField] private TMP_Text _moduleDescription;
        [SerializeField] private Button _moduleButton;

        private ModuleConfig _moduleConfig;
        
        public ModuleConfig ModuleConfig => _moduleConfig;
        
        private void Awake()
        {
            _moduleButton.onClick.AddListener(OnModuleButtonClicked);
        }

        private void OnDestroy()
        {
            _moduleButton.onClick.RemoveAllListeners();
        }

        public void Setup(ModuleConfig moduleConfig)
        {
            _moduleConfig = moduleConfig;
            if (moduleConfig == null)
            {
                _moduleDescription.text = "NONE";
                return;
            }

            _moduleDescription.text = String.Format(moduleConfig.Description, moduleConfig.Value, moduleConfig.IsPercentValue ? "%" : "points");
        }
        
        private void OnModuleButtonClicked()
        {
            OnModuleButtonClickedEvent?.Invoke(this);
        }
    }
}
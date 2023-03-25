using System;
using UnityEngine;

namespace Assets.Scripts.PartThree.Modules
{
    public class ChooseModulesPanel : MonoBehaviour
    {
        public event Action<ModuleConfig> OnModuleClicked;
        
        [SerializeField] private ModulesConfig _modulesConfig;
        [SerializeField] private ModuleListView _moduleListView;
        
        private void Awake()
        {
            _moduleListView.OnModuleClickedEvent += OnModuleClickedEventHandler;
            _moduleListView.AddModuleToList(null);
            
            for (int i = 0; i < _modulesConfig.ModulesData.Count; i++)
            {
                _moduleListView.AddModuleToList(_modulesConfig.ModulesData[i]);
            }
        }

        public void OnDestroy()
        {
            _moduleListView.OnModuleClickedEvent -= OnModuleClickedEventHandler;
        }

        private void OnModuleClickedEventHandler(ModuleConfig moduleConfig)
        {
            OnModuleClicked?.Invoke(moduleConfig);
        }
    }
}
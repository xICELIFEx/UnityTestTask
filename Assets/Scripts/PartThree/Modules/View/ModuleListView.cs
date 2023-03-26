using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.PartThree.Modules
{
    public class ModuleListView : MonoBehaviour
    {
        public event Action<ModuleConfig> OnModuleClickedEvent;
        
        [SerializeField] private ModuleView _modulePrefab;
        [SerializeField] private ScrollRect _modulesList;
        
        private List<ModuleView> _moduleViews = new List<ModuleView>();
        
        public void AddModuleToList(ModuleConfig moduleConfig)
        {
            ModuleView moduleView = GameObject.Instantiate(_modulePrefab, _modulesList.content);
            moduleView.OnModuleButtonClickedEvent += OnModuleButtonClickedEventHandler;
            moduleView.Setup(moduleConfig);
            _moduleViews.Add(moduleView);
        }

        public void RemoveModuleFromList(ModuleConfig moduleConfig)
        {
            ModuleView moduleView = null;
            for (int i = 0; i < _moduleViews.Count; i++)
            {
                if (_moduleViews[i].ModuleConfig == moduleConfig)
                {
                    moduleView = _moduleViews[i];
                    break;
                }
            }

            if (moduleView != null) { RemoveModuleView(moduleView); }
        }
        
        public void Clear()
        {
            while (_moduleViews.Count > 0) { RemoveModuleView(_moduleViews[0]); }
        }

        private void RemoveModuleView(ModuleView moduleView)
        {
            _moduleViews.Remove(moduleView);
            moduleView.OnModuleButtonClickedEvent -= OnModuleButtonClickedEventHandler;
            Destroy(moduleView.gameObject);
        } 
        
        private void OnModuleButtonClickedEventHandler(ModuleView moduleView)
        {
            OnModuleClickedEvent?.Invoke(moduleView.ModuleConfig);
        }
    }
}
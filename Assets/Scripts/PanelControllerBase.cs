using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class PanelControllerBase : MonoBehaviour
    {
        public event Action OnCancelClicked;
        
        [SerializeField] private GameObject _partPanel;
        [SerializeField] protected Button _cancelButton;

        protected virtual void Awake()
        {
            _cancelButton.onClick.AddListener(OnCancelButtonClicked);
        }
        
        protected virtual void OnDestroy()
        {
            _cancelButton.onClick.RemoveAllListeners();
        }
        
        public virtual void TurnOn()
        {
            _partPanel.SetActive(true);
        }

        public virtual void TurnOff()
        {
            _partPanel.SetActive(false);
        }
        
        protected void OnCancelButtonClicked()
        {
            OnCancelClicked?.Invoke();
        }
    }
}
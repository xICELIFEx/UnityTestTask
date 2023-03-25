using System;
using Assets.Scripts.PartOne;
using Assets.Scripts.PartTwo;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField] private Button _partOneButton;
        [SerializeField] private Button _partTwoButton;
        [SerializeField] private Button _partThreeButton;

        [SerializeField] private GameObject _mainMenuPanel;
        [SerializeField] private PanelControllerBase _partOneController;
        [SerializeField] private PanelControllerBase _partTwoController;
        [SerializeField] private PanelControllerBase _partThreeController;
        
        private void Awake()
        {
            _partOneButton.onClick.AddListener(OnPartOneClicked);
            _partTwoButton.onClick.AddListener(OnPartTwoClicked);
            _partThreeButton.onClick.AddListener(OnPartThreeClicked);

            _partOneController.OnCancelClicked += OnCancelClickedHandler;
            _partTwoController.OnCancelClicked += OnCancelClickedHandler;
            _partThreeController.OnCancelClicked += OnCancelClickedHandler;
        }

        private void OnDestroy()
        {
            _partOneButton?.onClick.RemoveAllListeners();
            _partTwoButton?.onClick.RemoveAllListeners();
            _partThreeButton?.onClick.RemoveAllListeners();
            
            _partThreeController.OnCancelClicked -= OnCancelClickedHandler;
            _partTwoController.OnCancelClicked -= OnCancelClickedHandler;
            _partOneController.OnCancelClicked -= OnCancelClickedHandler;
        }

        private void OnPartOneClicked()
        {
            _mainMenuPanel.SetActive(false);
            _partOneController.TurnOn();
        }
        
        private void OnPartTwoClicked()
        {
            _mainMenuPanel.SetActive(false);
            _partTwoController.TurnOn();
        }
        
        private void OnPartThreeClicked()
        {
            _mainMenuPanel.SetActive(false);
            _partThreeController.TurnOn();
        }
        
        private void OnCancelClickedHandler()
        {
            _mainMenuPanel.SetActive(true);
            _partOneController.TurnOff();
            _partTwoController.TurnOff();
            _partThreeController.TurnOff();
        }
    }
}
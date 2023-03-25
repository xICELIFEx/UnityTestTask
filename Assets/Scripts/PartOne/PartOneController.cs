using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.PartOne
{
    public class PartOneController : PanelControllerBase
    {
        private const int DEFAULT_LENGTH = 10;
        private const int DEFAULT_HEIGHT = 10;
        private const float DEFAULT_STEP = 1f;
        
        [SerializeField] private TMP_InputField _lengthInputField;
        [SerializeField] private TMP_InputField _heigthInputField;
        [SerializeField] private TMP_InputField _stepInputField;
        [SerializeField] private Button _generateButton;
        [SerializeField] private Button _playScrollCPUAnimationButton;
        [SerializeField] private Button _playScrollGPUAnimationButton;
        [SerializeField] private Button _playWaveCPUAnimationButton;
        [SerializeField] private Button _playWaveGPUAnimationButton;
        [SerializeField] private MeshView _meshViewPrefeb;
        
        private MeshView _meshView; 
        
        protected override void Awake()
        {
            base.Awake();
            
            _generateButton.onClick.AddListener(OnGenerateButtonClicked);
            _playScrollCPUAnimationButton.onClick.AddListener(OnPlayScrollCPUAnimationButtonClicked);
            _playScrollGPUAnimationButton.onClick.AddListener(OnPlayScrollGPUAnimationButtonClicked);
            _playWaveCPUAnimationButton.onClick.AddListener(OnPlayWaveCPUAnimationButtonClicked);
            _playWaveGPUAnimationButton.onClick.AddListener(OnPlayWaveGPUAnimationButtonClicked);
        }

        private void OnDestroy()
        {
            _playWaveGPUAnimationButton.onClick.RemoveAllListeners();
            _playWaveCPUAnimationButton.onClick.RemoveAllListeners();
            _playScrollGPUAnimationButton.onClick.RemoveAllListeners();
            _playScrollCPUAnimationButton.onClick.RemoveAllListeners();
            _generateButton.onClick.RemoveAllListeners();
        }

        public override void TurnOff()
        {
            base.TurnOff();
            
            HideMeshView();
        }

        private void HideMeshView()
        {
            if (_meshView != null)
            {
                _meshView.HideMesh();
                Destroy(_meshView.gameObject);
                _meshView = null;
            }
        }
        
        private void OnGenerateButtonClicked()
        {
            HideMeshView();
            _meshView = Instantiate(_meshViewPrefeb);

            int length;
            if (!int.TryParse(_lengthInputField.text, out length))
            {
                length = DEFAULT_LENGTH;
            }
            
            int height;
            if (!int.TryParse(_heigthInputField.text, out height))
            {
                height = DEFAULT_HEIGHT;
            }
            
            float step;
            if (!float.TryParse(_stepInputField.text, out step))
            {
                step = DEFAULT_STEP;
            }
            
            _meshView.ShowMesh(length, height, step);
        }
        
        private void OnPlayScrollCPUAnimationButtonClicked()
        {
            if (_meshView == null) { return; }
            
            _meshView.PlayScrollCPUAnimation();
        }
        
        private void OnPlayScrollGPUAnimationButtonClicked()
        {
            if (_meshView == null) { return; }
            
            _meshView.PlayScrollGPUAnimation();
        }
        
        private void OnPlayWaveCPUAnimationButtonClicked()
        {
            if (_meshView == null) { return; }
            
            _meshView.PlayWaveCPUAnimation();
        }
            
        private void OnPlayWaveGPUAnimationButtonClicked()
        {
            if (_meshView == null) { return; }
            
            _meshView.PlayWaveGPUAnimation();
        }
    }
}
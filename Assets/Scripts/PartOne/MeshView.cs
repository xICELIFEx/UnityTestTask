using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.PartOne
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class MeshView : MonoBehaviour
    {
        [SerializeField] private MeshFilter _meshFilter;
        [SerializeField] private MeshRenderer _meshRenderer;
        
        [SerializeField] private Material _defaultMaterial;
        [SerializeField] private Material _scrollMaterial;
        [SerializeField] private Material _waveMaterial;
        
        [SerializeField] private float _scrollSpeed;
        
        [SerializeField] private float _waveSpeed;
        [SerializeField] private float _waveLength;
        [SerializeField] private float _waveAmplitude;
        
        private MeshGenerator _meshGenerator;
        private Coroutine _animationCoroutine;

        private Mesh _mesh;
        
        private void Awake()
        {
            _meshGenerator = new MeshGenerator();

            if (_meshFilter == null) { _meshFilter = GetComponent<MeshFilter>(); }
            if (_meshRenderer == null) { _meshRenderer = GetComponent<MeshRenderer>(); }
        }

        private void OnDestroy()
        {
            if (_animationCoroutine != null) { StopCoroutine(_animationCoroutine); }
        }

        public void ShowMesh(int length, int height, float step)
        {
            _mesh = _meshGenerator.GenerateMesh(length, height, step, "flag");
            _meshRenderer.material = _defaultMaterial;
            _meshFilter.mesh = _mesh;
        }
        
        public void HideMesh()
        {
            if (_animationCoroutine != null) { StopCoroutine(_animationCoroutine); }
            
            _mesh = null;
            _meshFilter.mesh = null;
        }

        public void PlayScrollCPUAnimation()
        {
            if (_mesh == null) { return; }

            if (_animationCoroutine != null) { StopCoroutine(_animationCoroutine); }
            
            _animationCoroutine = StartCoroutine(CoScrollAnimation());
        }

        public void PlayScrollGPUAnimation()
        {
            if (_mesh == null) { return; }
            
            _meshRenderer.material = _scrollMaterial;
        }
        
        public void PlayWaveCPUAnimation()
        {
            if (_mesh == null) { return; }
            
            if (_animationCoroutine != null) { StopCoroutine(_animationCoroutine); }
            
            _animationCoroutine = StartCoroutine(CoWaveAnimation());
        }

        public void PlayWaveGPUAnimation()
        {
            if (_mesh == null) { return; }
            
            _meshRenderer.material = _waveMaterial;
        }
        
        private IEnumerator CoScrollAnimation()
        {
            while (true)
            {
                yield return null;
                _meshRenderer.material.mainTextureOffset = new Vector2(Time.time * _scrollSpeed, 0);    
            }
        }
        
        private IEnumerator CoWaveAnimation()
        {
            List<Vector3> defaultVertices = new List<Vector3>();
            _mesh.GetVertices(defaultVertices);
            List<Vector3> tempVertices = new List<Vector3>();
            Vector3 tempVertex;
            while (true)
            {
                yield return null;

                for (int i = 0; i < defaultVertices.Count; i++)
                {
                    tempVertex = defaultVertices[i];
                    tempVertex.y += Mathf.Sin((Time.time * _waveSpeed + tempVertex.x) / _waveLength) * _waveAmplitude;
                    tempVertices.Add(tempVertex);
                }
                _mesh.SetVertices(tempVertices);
                tempVertices.Clear();
            }
            
        }
    }
}
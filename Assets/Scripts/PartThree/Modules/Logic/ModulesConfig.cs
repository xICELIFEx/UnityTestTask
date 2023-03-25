using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.PartThree.Modules
{
    [CreateAssetMenu(fileName = "ModulesConfig", menuName = "ScriptableObjects/CreateModulesConfig", order = 1)]
    public class ModulesConfig : ScriptableObject
    {
        [SerializeField] private List<ModuleConfig> _modulesData = new List<ModuleConfig>();
        
        public IReadOnlyList<ModuleConfig> ModulesData => _modulesData;
    }
}
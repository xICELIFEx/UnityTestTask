using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.PartThree.Ships
{
    [CreateAssetMenu(fileName = "ShipsConfig", menuName = "ScriptableObjects/CreateShipsConfig", order = 1)]
    public class ShipsConfig : ScriptableObject
    {
        [SerializeField] private List<ShipConfig> _shipsData = new List<ShipConfig>();
        
        public IReadOnlyList<ShipConfig> ShipsData => _shipsData;
    }
}
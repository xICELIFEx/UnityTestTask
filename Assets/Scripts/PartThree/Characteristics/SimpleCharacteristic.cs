// using System;
//
// namespace Assets.Scripts.PartThree.Characteristics
// {
//     public enum SimpleCharacteristicType
//     {
//         Integer = 0,
//         Float = 1,
//     }
//
//     // public class CharacteristicUpdateData
//     // {
//     //     private string _name
//     //     private SimpleCharacteristicType _characteristicType;
//     // }
//     
//     public class SimpleCharacteristic
//     {
//         public event Action<string> OnValueChanged;
//         // public event Action<string> OnMultiplierChanged;
//
//         private string _name;
//         private SimpleCharacteristicType _characteristicType; 
//         
//         private int _baseValue;
//         private float _multiplier;
//
//         public string Name => _name;
//         public SimpleCharacteristicType CharacteristicType => _characteristicType;
//         
//         public int BaseValue
//         {
//             get { return _baseValue; }
//             set
//             {
//                 
//                 _baseValue = value;
//                 OnBaseValueChanged?.Invoke(_name);
//             }
//         }
//
//         public float Multiplier
//         {
//             get { return _multiplier; }
//             set
//             {
//                 _multiplier = value;
//                 OnMultiplierChanged?.Invoke(_name);
//             }
//         }
//         
//         public int IntValue
//         {
//             get { return (int)(_baseValue * _multiplier); }
//         }
//
//         private float FloatValue
//         {
//             get { return _baseValue * _multiplier; }
//         }
//
//         public SimpleCharacteristic(string name, SimpleCharacteristicType characteristicType, int baseValue, float multiplier)
//         {
//             _name = name;
//             _characteristicType = characteristicType;
//             _baseValue = baseValue;
//             _multiplier = multiplier;
//         }
//     }
// }
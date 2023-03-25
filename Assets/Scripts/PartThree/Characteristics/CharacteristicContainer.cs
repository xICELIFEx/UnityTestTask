using System.Collections.Generic;

namespace Assets.Scripts.PartThree.Characteristics
{
    //TODO: implement enumerable
    public class CharacteristicContainer
    {
        private List<CharacteristicWrapperBase> _characteristics = new List<CharacteristicWrapperBase>();

        public void Add(CharacteristicWrapperBase characteristicBase)
        {
            if (characteristicBase == null || _characteristics.Contains(characteristicBase)) { return; }

            _characteristics.Add(characteristicBase);
        }
        
        public void Remove(CharacteristicWrapperBase characteristicBase)
        {
            if (characteristicBase == null || !_characteristics.Contains(characteristicBase)) { return; }
            
            _characteristics.Remove(characteristicBase);
        }

        public T GetFirst<T>(CharacteristicType characteristicType) where T : CharacteristicWrapperBase
        {
            for (int i = 0; i < _characteristics.Count; i++)
            {
                if (_characteristics[i].CharacteristicType == characteristicType)
                {
                    return _characteristics[i] as T;
                }
            }

            return null;
        }

        public List<T> GetAll<T>(CharacteristicType characteristicType) where T : CharacteristicWrapperBase
        {
            List<T> resultList = new List<T>();
            for (int i = 0; i < _characteristics.Count; i++)
            {
                if (_characteristics[i].CharacteristicType == characteristicType)
                {
                    if (_characteristics[i] is T)
                    {
                        resultList.Add(_characteristics[i] as T);
                    }
                }
            }

            return resultList;
        }
        
        public List<CharacteristicWrapperBase> GetAll(CharacteristicType characteristicType)
        {
            List<CharacteristicWrapperBase> resultList = new List<CharacteristicWrapperBase>();
            for (int i = 0; i < _characteristics.Count; i++)
            {
                if (_characteristics[i].CharacteristicType == characteristicType)
                {
                    resultList.Add(_characteristics[i]);
                }
            }

            return resultList;
        }
    }
}
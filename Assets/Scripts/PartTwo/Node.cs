using System;
using System.Collections.Generic;

namespace Assets.Scripts.PartTwo
{
    public class Node
    {
        private string _id;
        private Dictionary<string, object> _additionalData = new Dictionary<string, object>();
        
        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public void AddData(string id, object data)
        {
            if (String.IsNullOrEmpty(id) || data == null) { return; }
            _additionalData[id] = data;
        }
        
        public void RemoveData(string id)
        {
            if (String.IsNullOrEmpty(id)) { return; }
            
            _additionalData.Remove(id);
        }
        
        public bool IsContainDataForKey(string id)
        {
            if (String.IsNullOrEmpty(id)) { return false; }
            
            return _additionalData.ContainsKey(id);
        }

        public T GetDataForKey<T>(string id)
        {
            return (T)GetDataForKey(id);
        }
        
        public object GetDataForKey(string id)
        {
            if (String.IsNullOrEmpty(id)) { return null; }

            if (_additionalData.ContainsKey(id)) { return _additionalData[id]; }
            else { return null; }
        }
    }
}
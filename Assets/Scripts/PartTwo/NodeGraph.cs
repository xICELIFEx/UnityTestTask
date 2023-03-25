using System;
using System.Collections.Generic;

namespace Assets.Scripts.PartTwo
{
    public class NodeGraph
    {
        private Dictionary<string, Node> _nodes = new Dictionary<string, Node>();
        private List<Link> _links = new List<Link>();
        
        public void AddNode(string id)
        {
            AddNode(new Node() {Id = id});
        }

        public void AddNode(Node node)
        {
            if (!_nodes.ContainsKey(node.Id))
            {
                _nodes.Add(node.Id, node);
            }
        }

        public Node GetNode(string id)
        {
            Node result = null;
            if (String.IsNullOrEmpty(id))
            {
                return result;
            }

            _nodes.TryGetValue(id, out result);

            return result;
        }

        public void AddLink(string first, string second)
        {
            if (String.IsNullOrEmpty(first) || String.IsNullOrEmpty(second) || first == second) { return; }

            Node firstNode = GetNode(first);
            Node secondNode = GetNode(second);

            if (firstNode == null || secondNode == null) { return; }

            _links.Add(new Link() {First = firstNode, Second = secondNode});
        }

        public List<Link> GetLinks(string id)
        {
            List<Link> result = new List<Link>();

            Node node = GetNode(id);
            if (node == null)
            {
                return result;
            }

            for (int i = 0; i < _links.Count; i++)
            {
                if (_links[i].First == node || _links[i].Second == node)
                {
                    result.Add(_links[i]);
                }
            }

            return result;
        }
    }
}
using System.Collections.Generic;

namespace Assets.Scripts.PartTwo
{
    public class Path
    {
        private readonly List<Link> _links;
        private readonly List<Node> _nodes;

        public List<Link> Links => _links;
        public List<Node> Nodes => _nodes;
        
        public Path()
        {
            _links = new List<Link>();
            _nodes = new List<Node>();
        }
            
        public Path(Path path)
        {
            _links = new List<Link>(path._links);
            _nodes = new List<Node>(path._nodes);
        }
    }
}
namespace Assets.Scripts.PartTwo
{
    public class Link
    {
        private Node _first;
        private Node _second;

        public Node First
        {
            get { return _first; }
            set { _first = value; }
        }

        public Node Second
        {
            get { return _second; }
            set { _second = value; }
        }
    }
}
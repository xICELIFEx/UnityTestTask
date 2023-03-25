namespace Assets.Scripts.PartTwo
{
    public interface IPathFinder
    {
        Path FindPath(NodeGraph graph, string startId, string endId);
    }
}
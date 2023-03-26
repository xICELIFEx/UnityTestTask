using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.PartTwo
{
    public class PathFinderShortPath : IPathFinder
    {
        //TODO:optimize
        public Path FindPath(NodeGraph nodeGraph, string startId, string endId)
        {
            if (nodeGraph == null)
            {
                Debug.LogError("Graph is null");
                return null;
            }

            Node startNode = nodeGraph.GetNode(startId);
            Node endNode = nodeGraph.GetNode(endId);
            if (startNode == null || endNode == null)
            {
                Debug.LogError("Can not find nodes");
                return null;
            }
            
            if (startNode == endNode)
            {
                Debug.LogError("Already at destination point");
                return null;
            }

            bool reached = false;
            List<Path> paths = new List<Path>();
            
            List<Link> links = nodeGraph.GetLinks(startNode.Id);
            for (int i = 0; i < links.Count; i++)
            {
                Path path = new Path();
                path.Nodes.Add(startNode);
                path.Nodes.Add(startNode.Id == links[i].First.Id ? links[i].Second : links[i].First);
                path.Links.Add(links[i]);
                
                if (links[i].First == endNode ||
                    links[i].Second == endNode)
                { reached = true; }
                
                paths.Add(path);
            }
            
            List<Path> newPaths = new List<Path>();

            if (!reached)
            {
                do
                {
                    newPaths.Clear();

                    for (int i = 0; i < paths.Count; i++)
                    {
                        // ProcessPath(nodeGraph, paths, paths[i], endNode, ref reached);
                        
                        links = nodeGraph.GetLinks(paths[i].Nodes.Last().Id);
                        for (int j = 0; j < links.Count; j++)
                        {
                            bool found = false;
                            for (int k = 0; k < paths.Count; k++)
                            {
                                if (paths[k].Nodes.Contains(links[j].Second) &&
                                    paths[k].Nodes.Contains(links[j].First))
                                {
                                    found = true;
                                    break;
                                }
                            }

                            if (found) { continue; }

                            if (links[j].First == endNode ||
                                links[j].Second == endNode)
                            { reached = true; }

                            Path path = new Path(paths[i]);
                            path.Nodes.Add(path.Nodes.Contains(links[j].Second) ? links[j].First : links[j].Second);
                            path.Links.Add(links[j]);
                            newPaths.Add(path);
                        }
                    }

                    if (newPaths.Count > 0) { paths = new List<Path>(newPaths); }

                    if (reached) { break; }
                } while (newPaths.Count != 0);
            }

            List<Path> correctPathList = new List<Path>();
            for (int i = 0; i < paths.Count; i++)
            {
                if (paths[i].Nodes.Last() != endNode) { continue; }
                correctPathList.Add(paths[i]);
            }

            Path resultPath = null;
            for (int i = 0; i < correctPathList.Count; i++)
            {
                if (resultPath == null || resultPath.Nodes.Count > correctPathList[i].Nodes.Count)
                {
                    resultPath = correctPathList[i];
                }
            }
            
            return resultPath;
        }
    }
}
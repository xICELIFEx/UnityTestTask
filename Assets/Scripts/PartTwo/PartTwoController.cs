using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.PartTwo
{
    public class PartTwoController : PanelControllerBase
    {
        private const string RED = "red";
        private const string BLACK = "black";
        private const string GREEN = "green";
        private const string BLUE = "blue";
        
        private const string COLORS = "colors";
        
        private const string ID_A = "A";
        private const string ID_B = "B";
        private const string ID_C = "C";
        private const string ID_D = "D";
        private const string ID_E = "E";
        private const string ID_F = "F";
        private const string ID_G = "G";
        private const string ID_H = "H";
        private const string ID_J = "J";
        private const string ID_K = "K";
        private const string ID_L = "L";
        private const string ID_M = "M";
        private const string ID_N = "N";
        private const string ID_O = "O";
        
        [SerializeField] private TMP_Dropdown _startIdDropdown;
        [SerializeField] private TMP_Dropdown _endIdDropdown;
        [SerializeField] private Button _findPathButton;
        [SerializeField] private TMP_Text _statesCountText;
        [SerializeField] private TMP_Text _branchesCountText;
        [SerializeField] private TMP_Text _pathText;
        
        private IPathFinder _pathFinder;
        private NodeGraph _nodeGraph = new NodeGraph();

        private void Awake()
        {
            _pathFinder = new PathFinderShortPath();

            ConfigureGraph();
            
            List<string> idList = new List<string>() {ID_A, ID_B, ID_C, ID_D, ID_E, ID_F, ID_G, ID_H, ID_J, ID_K, ID_L, ID_M, ID_N, ID_O};
            
            _startIdDropdown.ClearOptions();
            _startIdDropdown.AddOptions(idList);
            _startIdDropdown.SetValueWithoutNotify(0);
            _endIdDropdown.ClearOptions();
            _endIdDropdown.AddOptions(idList);
            _endIdDropdown.SetValueWithoutNotify(1);
            
            _findPathButton.onClick.AddListener(OnFindPathButtonClicked);
        }

        private void OnDestroy()
        {
            _findPathButton.onClick.RemoveAllListeners();
        }
        
        private void OnFindPathButtonClicked()
        {
            string startId = _startIdDropdown.options[_startIdDropdown.value].text;
            string endId = _endIdDropdown.options[_endIdDropdown.value].text;

            Path path = _pathFinder.FindPath(_nodeGraph, startId, endId);
            _statesCountText.text = path.Nodes.Count.ToString();

            //TODO: check branches count calculation
            List<string> branches = new List<string>();
            Node currentNode;
            List<string> currentColors;
            Node previousNode;
            List<string> previousColors;
            for (int i = 0; i < path.Nodes.Count; i++)
            {
                currentNode = path.Nodes[i];
                currentColors = currentNode.GetDataForKey<List<string>>(COLORS);
                if (currentColors == null)
                {
                    Debug.LogError($"Can not find color data for node with is {currentNode.Id}");
                    break;
                }
                
                previousNode = i > 0 ? path.Nodes[i-1] : null;
                if (previousNode == null)
                {
                    Node nextNode = i < path.Nodes.Count - 1 ? path.Nodes[i+1] : null;
                    if (nextNode == null)
                    {
                        branches.Add(currentColors[0]);
                    }
                    else
                    {
                        List<string> nextColors = nextNode.GetDataForKey<List<string>>(COLORS);
                        if (nextColors == null)
                        {
                            Debug.LogError($"Can not find color data for node with is {currentNode.Id}");
                            break;
                        }
                        else
                        {
                            for (int j = 0; j < currentColors.Count; j++)
                            {
                                if (nextColors.Contains(currentColors[j]))
                                {
                                    if (!branches.Contains(currentColors[j])) { branches.Add(currentColors[j]); }
                                    break;
                                }
                            }
                        }
                    }
                }
                else
                {
                    previousColors = previousNode.GetDataForKey<List<string>>(COLORS);
                    if (previousColors == null)
                    {
                        Debug.LogError($"Can not find color data for node with is {previousNode.Id}");
                        break;
                    }
                    else
                    {
                        for (int j = 0; j < currentColors.Count; j++)
                        {
                            if (previousColors.Contains(currentColors[j]))
                            {
                                if (!branches.Contains(currentColors[j])) { branches.Add(currentColors[j]); }
                                break;
                            }
                        }
                    }
                }
            }

            _branchesCountText.text = (branches.Count - 1).ToString();

            StringBuilder wayPath = new StringBuilder();
            for (int i = 0; i < path.Nodes.Count; i++)
            {
                wayPath.Append(path.Nodes[i].Id);
                if (i < path.Nodes.Count - 1)
                {
                    wayPath.Append(" -> ");
                }
            }

            _pathText.text = wayPath.ToString();
        }
        
        private void ConfigureGraph()
        {
            AddNode(ID_A, new List<string>() {RED});
            AddNode(ID_B, new List<string>() {RED, BLACK});
            AddNode(ID_C, new List<string>() {RED, GREEN});
            AddNode(ID_D, new List<string>() {RED, BLUE});
            AddNode(ID_E, new List<string>() {RED, GREEN});
            AddNode(ID_F, new List<string>() {RED, BLACK});
            AddNode(ID_G, new List<string>() {BLACK});
            AddNode(ID_H, new List<string>() {BLACK});
            AddNode(ID_J, new List<string>() {BLACK, BLUE, GREEN});
            AddNode(ID_K, new List<string>() {GREEN});
            AddNode(ID_L, new List<string>() {GREEN, BLUE});
            AddNode(ID_M, new List<string>() {GREEN});
            AddNode(ID_N, new List<string>() {BLUE});
            AddNode(ID_O, new List<string>() {BLUE});

            //red
            _nodeGraph.AddLink(ID_A, ID_B);
            _nodeGraph.AddLink(ID_B, ID_C);
            _nodeGraph.AddLink(ID_C, ID_D);
            _nodeGraph.AddLink(ID_D, ID_E);
            _nodeGraph.AddLink(ID_E, ID_F);
            
            //black
            _nodeGraph.AddLink(ID_B, ID_H);
            _nodeGraph.AddLink(ID_H, ID_J);
            _nodeGraph.AddLink(ID_J, ID_F);
            _nodeGraph.AddLink(ID_F, ID_G);
            
            //green
            _nodeGraph.AddLink(ID_C, ID_J);
            _nodeGraph.AddLink(ID_J, ID_E);
            _nodeGraph.AddLink(ID_E, ID_M);
            _nodeGraph.AddLink(ID_M, ID_L);
            _nodeGraph.AddLink(ID_L, ID_K);
            _nodeGraph.AddLink(ID_K, ID_C);
            
            //blue
            _nodeGraph.AddLink(ID_N, ID_L);
            _nodeGraph.AddLink(ID_L, ID_D);
            _nodeGraph.AddLink(ID_D, ID_J);
            _nodeGraph.AddLink(ID_J, ID_O);
        }
        
        private void AddNode(string id, List<string> colors)
        {
            Node node = CreateNode(id, colors);
            if (node != null) { _nodeGraph.AddNode(node); }
            else { LogNotCreateNode(id); }
        }
        
        private Node CreateNode(string id, List<string> colors)
        {
            Node node = new Node();
            node.Id = id;
            
            node.AddData(COLORS, colors);
            return node;
        }

        private void LogNotCreateNode(string nodeId)
        {
            Debug.LogError($"{nodeId} node not created");
        }
    }
}
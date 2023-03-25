using UnityEngine;

namespace Assets.Scripts.PartOne
{
    public interface IMeshGenerator
    {
        Mesh GenerateMesh(int length, int height, float step, string name);
    }
}
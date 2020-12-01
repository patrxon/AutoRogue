using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField] int width = 7;
    [SerializeField] int height = 6;

    SpriteGrid spriteGrid;
    NodeGenerator nodeGenerator;

    GridNode[,] gridNodes;

    void Awake()
    {  
        nodeGenerator = new NodeGenerator(width, height);
        gridNodes = nodeGenerator.GetNodes();
        spriteGrid = new SpriteGrid(width, height, this.transform, gridNodes);
        spriteGrid.CreateBackground();
    }

    public GridNode[,] GetNodes()
    {
        return gridNodes;
    }

}

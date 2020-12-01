using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeGenerator
{
    int width;
    int height;

    GridNode[,] gridNodes;

    public NodeGenerator(int width, int height)
    {
        this.width = width;
        this.height = height;

        gridNodes = new GridNode[width, height];
 
        GenerateNodes();
        ConnectNodes();
    }

    private void GenerateNodes()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                int rand = Random.Range(0, 100);

                if (rand < 45)
                    gridNodes[x, y] = new GridNode(x, y, 1);
                else if (rand < 90)
                    gridNodes[x, y] = new GridNode(x, y, 2);
                else
                    gridNodes[x, y] = new GridNode(x, y, 100);
            }
        }

    }

    private void ConnectNodes()
    {

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (y < height - 1)
                    gridNodes[x, y].AddConnection(gridNodes[x, y + 1], 0);
                if (x < width - 1)
                    gridNodes[x, y].AddConnection(gridNodes[x + 1, y], 1);
                if (y > 0)
                    gridNodes[x, y].AddConnection(gridNodes[x, y - 1], 2);
                if (x > 0)
                    gridNodes[x, y].AddConnection(gridNodes[x - 1, y], 3);
            }
        }
    }

    public GridNode[,] GetNodes()
    {
        return gridNodes;
    }

}

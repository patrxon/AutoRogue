using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteGrid 
{
    Transform parent;

    SpriteCreator spriteCreator;

    private int width;
    private int height;

    GridNode[,] gridNodes;

    public SpriteGrid(int width, int height, Transform parent, GridNode[,] gridNodes)
    {

        this.width = width;
        this.height = height;
        this.parent = parent;
        this.gridNodes = gridNodes;

        spriteCreator = new SpriteCreator(width, height, parent);

    }

    public void CreateBackground()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {

                spriteCreator.CreatSpriteTile(x, y, gridNodes[x,y].GetMoveCost());
            }
        }
    }

}

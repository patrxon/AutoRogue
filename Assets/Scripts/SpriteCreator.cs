using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteCreator
{
    
    Sprite[] tileSprites;
    Sprite hole;
    Sprite hardTile;

    Transform parent;
    int width;
    int height;

    public SpriteCreator(int width, int height, Transform parent)
    {
        this.width = width;
        this.height = height;
        this.parent = parent;
        tileSprites = Resources.LoadAll<Sprite>("Sprites/TileArt");
        hole = Resources.Load<Sprite>("Sprites/Hole");
        hardTile = Resources.Load<Sprite>("Sprites/HardTile");
    }


    public GameObject CreatSpriteTile( int x, int y, int moveCost)
    {
        GameObject gameObject = new GameObject("Tile" + x + "" + y);
        Transform transform = gameObject.transform;
        transform.position = new Vector3(x, y, 0);
        transform.SetParent(parent);
        transform.rotation = Quaternion.identity;

        gameObject.AddComponent(typeof(SpriteRenderer));
        gameObject.GetComponent<SpriteRenderer>().sprite = GetSprite(x, y, moveCost);

        return gameObject;
    }

    private Sprite GetSprite(int x, int y, int moveCost)
    {
        if (moveCost == 2)
            return hardTile;
        else if (moveCost == 100)
            return hole;
        else if (x == 0 && y == height - 1)
            return tileSprites[0];
        else if (x != 0 && x != width - 1 && y == height - 1)
            return tileSprites[1];
        else if (x == width - 1 && y == height - 1)
            return tileSprites[2];
        else if (x == 0 && y != 0 && y != height - 1)
            return tileSprites[3];
        else if (x == width - 1 && y != 0 && y != height - 1)
            return tileSprites[5];
        else if (x == 0 && y == 0)
            return tileSprites[6];
        else if (x != 0 && x != width - 1 && y == 0)
            return tileSprites[7];
        else if (x == width - 1 && y == 0)
            return tileSprites[8];
        else
            return tileSprites[4];
                
  
    }


}

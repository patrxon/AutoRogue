using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCreator
{
    Sprite[] unitSprites = Resources.LoadAll<Sprite>("Units");

    GridNode[,] gridNodes;

    public UnitCreator(GridNode[,] gridNodes)
    {
        this.gridNodes = gridNodes;
    }

    public Unit CreateWarrior(int x, int y, int id, int type)
    { 
        GameObject newUnit = new GameObject("unit" + id);
        Transform transform = newUnit.transform;
        transform.position = new Vector3(x, y, -1);
        transform.rotation = Quaternion.identity;

        newUnit.AddComponent(typeof(SpriteRenderer));
        newUnit.GetComponent<SpriteRenderer>().sprite = unitSprites[type];

        Unit script = newUnit.AddComponent<Unit>();
        script.Initialize(gridNodes);
        script.SetPos(x, y);

        return newUnit.GetComponent<Unit>();
    }
}

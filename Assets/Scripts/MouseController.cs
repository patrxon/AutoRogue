using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController
{
    private int gridWitdh;
    private int gridHeight;

    public MouseController(int width, int height)
    {
        gridHeight = height;
        gridWitdh = width;

    }

    public bool IsMouseInGrid(Vector2 mousePos)
    {
        if(mousePos.x >= 0 && mousePos.x < gridWitdh && mousePos.y >= 0 && mousePos.y < gridHeight)
        {
            return true;
        }

        return false;
    }

    public Vector2 GetPosInGrid()
    {
        Vector2 mousePos;
        mousePos.x = Input.mousePosition.x;
        mousePos.y = Input.mousePosition.y;

        Vector3 PosInUnits = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 0));

        Vector2 GridPos;
        GridPos.x = Mathf.Floor(PosInUnits.x + 0.5f);
        GridPos.y = Mathf.Floor(PosInUnits.y + 0.5f);

        return GridPos;
    }
}

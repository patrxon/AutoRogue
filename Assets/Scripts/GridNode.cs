using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridNode
{
    private int x;
    private int y;
    private int moveCost;
    private Unit unit = null;

    GridNode up = null; //0
    GridNode right = null; //1
    GridNode down = null; //2
    GridNode left = null; //3

    public GridNode(int x, int y, int moveCost)
    {
        this.x = x;
        this.y = y;
        this.moveCost = moveCost;
    }

    public void AddConnection(GridNode node, int dir)
    {
        switch (dir)
        {
            case 0:
                up = node;
                break;
            case 1:
                right = node;
                break;
            case 2:
                down = node;
                break;
            case 3:
                left = node;
                break;
        }
    }

    public GridNode getNode(int dir)
    {
        switch (dir)
        {
            case 0:
                return up;
            case 1:
                return right;
            case 2:
                return down;          
            case 3:
                return left;               
            default:
                return null;             
        }
    }

    public void SetUnit(Unit unit)
    {
        this.unit = unit;
    }

    public Unit GetUnit()
    {
        return unit;
    }

    public void SetMoveCost(int moveCost)
    {
        this.moveCost = moveCost;
    }

    public int GetMoveCost()
    {
        return moveCost;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder
{
    private int width;
    private int height;

    private int[,] pathCost;
    private int[,] sourceDir;

    GridNode[,] gridNodes;

    public PathFinder(GridNode[,] gridNodes)
    {
        this.gridNodes = gridNodes;
        this.width = gridNodes.GetLength(0);
        this.height = gridNodes.GetLength(1);

        pathCost = new int[width, height];
        sourceDir = new int[width, height];

        
        ResetPath();
    }

    public void ResetPath()
    {
        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
                pathCost[x, y] = 1000; // "max int"

    }

    public void FindCosts(int x, int y)
    {
        pathCost[x, y] = 0;

        if (gridNodes[x, y].getNode(0) != null)
            FindCosts(x, y + 1, 0, 0);
        if (gridNodes[x, y].getNode(1) != null)
            FindCosts(x + 1, y, 0, 1);
        if (gridNodes[x, y].getNode(2) != null)
            FindCosts(x, y - 1, 0, 2);
        if (gridNodes[x, y].getNode(3) != null)
            FindCosts(x - 1, y, 0, 3);
    }

    private void FindCosts(int x, int y, int currCost, int source)
    {
        if (gridNodes[x, y].GetUnit() == null)
            if (gridNodes[x, y].GetMoveCost() + currCost < pathCost[x, y])
            {
                pathCost[x, y] = gridNodes[x, y].GetMoveCost() + currCost;
                sourceDir[x, y] = source;
                currCost = gridNodes[x, y].GetMoveCost() + currCost;

                if (gridNodes[x, y].getNode(0) != null)
                    FindCosts(x, y + 1, currCost, 0);
                if (gridNodes[x, y].getNode(1) != null)
                    FindCosts(x + 1, y, currCost, 1);
                if (gridNodes[x, y].getNode(2) != null)
                    FindCosts(x, y - 1, currCost, 2);
                if (gridNodes[x, y].getNode(3) != null)
                    FindCosts(x - 1, y, currCost, 3);
            }

    }

    public bool CheckDestination(int x, int y, int maxCost)
    {
        if (pathCost[x, y] > maxCost) return false;
        else return true;
    }

    public List<Vector2> CreatePath(int x, int y)
    {

        List<Vector2> path = new List<Vector2>();

        int timer = 15;

        while(pathCost[x, y] != 0 && timer > 0)
        {
            path.Add(new Vector2(x, y));

            timer--;

            switch (sourceDir[x,y])
            {
                case 0:
                    y--;
                    break;
                case 1:
                    x--;
                    break;
                case 2:
                    y++;
                    break;
                case 3:
                    x++;
                    break;
            }

        }

        path.Reverse();

        return path;
    }

    public int[,] GetPathCosts()
    {
        return pathCost;
    }

    // DEBUG
    public void ShowPaths()
    {
        string path = "";
        string sources = "";

        for (int j = height-1; j >= 0; j--)
        {
            for (int i = 0; i < width; i++)
            {
                if (pathCost[i,j] >= 100)
                    path += "X ";
                else if(pathCost[i, j] >= 10)
                    path += pathCost[i, j];
                else
                    path += pathCost[i, j] + " ";

                if (pathCost[i, j] < 100)
                    switch (sourceDir[i, j])
                    {
                        case 0:
                            sources += "v ";
                            continue;
                        case 1:
                            sources += "< ";
                            continue;
                        case 2:
                            sources += "^ ";
                            continue;
                        case 3:
                            sources += "> ";
                            continue;
                    }
                else
                    sources += "x ";


            }
            path += "\n";
            sources += "\n";
        }

        Debug.Log(path + "\n" + sources);
    }

}

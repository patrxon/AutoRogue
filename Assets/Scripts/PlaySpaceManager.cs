using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySpaceManager : MonoBehaviour
{
    int width;
    int height;

    int currPlayer = 0;

    GridNode[,] gridNodes;
  
    [SerializeField] MapManager mapManager;

    MouseController mouseController;
    UnitController unitController;

    void Start()
    {
        gridNodes = mapManager.GetNodes();
        width = gridNodes.GetLength(0);
        height = gridNodes.GetLength(1);
        unitController = new UnitController(gridNodes);
        mouseController = new MouseController(width, height);
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            LeftClick();
        }

        if(Input.GetMouseButtonUp(1))
        {
            RightClick();
        }

    }

    private void RightClick()
    {
        Vector2 mousePos = mouseController.GetPosInGrid();

        if (mouseController.IsMouseInGrid(mousePos))
        {
            unitController.MoveUnit(mousePos);   
        }
    }

    [SerializeField] bool createing = true;

    private void LeftClick()
    {
        Vector2 mousePos = mouseController.GetPosInGrid();

        if (mouseController.IsMouseInGrid(mousePos))
        {
            if (!unitController.SelectUnit(mousePos) && createing)
            {
                
                unitController.CreatUnit(mousePos);
                
            }
        }
    }

    public void EndTurn()
    {
        unitController.TurnEnd(currPlayer);

        currPlayer = (currPlayer + 1) % 2;
    }

}
    

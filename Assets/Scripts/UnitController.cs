using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController
{
    GridNode[,] gridNodes;
    List<Unit> units = new List<Unit>();
    Unit selectedUnit = null;

    UnitCreator unitCreator;

    MoveHighlighter moveHighlighter;

    public UnitController(GridNode[,] gridNodes)
    {
        this.gridNodes = gridNodes;
        unitCreator = new UnitCreator(gridNodes);
        moveHighlighter = Object.FindObjectOfType<MoveHighlighter>();
    }

    public void MoveUnit(Vector2 position)
    {
        if(selectedUnit != null)
        if (selectedUnit.SetPath((int)position.x, (int)position.y))
        {
            selectedUnit.MoveUnit();
            selectedUnit = null;
            moveHighlighter.HideHighlight();
        }
    }

    public bool SelectUnit(Vector2 position)
    {
        foreach (Unit unit in units)
        {
            if (unit.GetPos() == position)
            {
                selectedUnit = unit;
                selectedUnit.CreatePaths();
                int[,] moveCosts = selectedUnit.GetPathFinder().GetPathCosts();
                moveHighlighter.HighlightSpace(moveCosts, selectedUnit.GetWalkDis());

                return true;
            }       
        }

        return false;
    }

    public void CreatUnit(Vector2 position)
    {
        
        units.Add(unitCreator.CreateWarrior((int)position.x, (int)position.y, units.Count, 1));
        SelectUnit(position);
    }

    public void TurnEnd(int player)
    {
        foreach (Unit unit in units)
        {
            if (unit.owner == player)
            {
                unit.EndTurn();
            }
            else 
            {
                unit.StartTurn();
            }
            
        }
    }
}
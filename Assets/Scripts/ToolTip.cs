using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ToolTip : MonoBehaviour
{
    [SerializeField] MapManager mapManager;
    GridNode[,] gridNodes;

    [SerializeField] TextMeshProUGUI unitName;
    [SerializeField] TextMeshProUGUI unitMove;
    [SerializeField] TextMeshProUGUI tileCost;

    void Start()
    {
        gridNodes = mapManager.GetNodes();
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            LeftClick();
        }
    }

    void LeftClick()
    {

        Vector2 mousePos = getMousePos();

        if (mousePos.x >= 0 && mousePos.x < gridNodes.GetLength(0) && mousePos.y >= 0 && mousePos.y < gridNodes.GetLength(1))
        {
            try {
                unitName.text = gridNodes[(int)mousePos.x, (int)mousePos.y].GetUnit().GetName();
                unitMove.text = gridNodes[(int)mousePos.x, (int)mousePos.y].GetUnit().GetWalkDis().ToString();
            }
            catch
            {
                unitName.text = "empty";
                unitMove.text = "0";
            }

           
            tileCost.text = gridNodes[(int)mousePos.x, (int)mousePos.y].GetMoveCost().ToString();
        }
    }

    private Vector2 getMousePos()
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

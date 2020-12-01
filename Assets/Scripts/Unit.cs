using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{

    private string name = "warrior";

    [SerializeField] float speed = 5.0F;
    [SerializeField] int walkDistance = 6;

    public Vector2 position;
    float posZ;

    public int owner { get; set; } = 0; // Player 0 /Ai 1+
    public bool active { get; set; } = true;

    List<Vector2> path = new List<Vector2>();

    GridNode[,] gridNodes;
    PathFinder pathFinder;
    
    void Start()
    {
        posZ = transform.position.z;    
    }

    public void Initialize(GridNode[,] gridNodes)
    {
        this.gridNodes = gridNodes;
        pathFinder = new PathFinder(gridNodes);
    }

    [SerializeField] bool changeOwner = false;//Debug 

    void Update()
    {
        if (changeOwner)
        {
            changeOwner = false;
            owner = (owner + 1) % 2;
        }

        if (path.Count > 0)
        {
            MoveOnThePath();
        }          
    }

    private void MoveOnThePath()
    {
        float step = speed * Time.deltaTime;

        Vector3 destination = new Vector3(path[0][0], path[0][1], posZ);
        transform.position = Vector3.MoveTowards(transform.position, destination, step);

        if (Vector3.Distance(transform.position, destination) < 0.001f)
        {
            position.x = destination.x;
            position.y = destination.y;
            path.Remove(path[0]);

            if(path.Count == 0)
            {
                gridNodes[(int)position.x, (int)position.y].SetUnit(this);
            }
        }
    }

    public void StartTurn()
    {
        active = true;
    }

    public void EndTurn()
    {
        //To do Attack()    
    }

    public void CreatePaths()
    {
        pathFinder.ResetPath();
        pathFinder.FindCosts((int)position.x, (int)position.y);
    }

    public bool SetPath(int x, int y)
    {
        if (active && pathFinder.CheckDestination(x, y, walkDistance))
        {
            path = pathFinder.CreatePath(x, y);
            active = false;
            return true;
        }

        return false;
    }

    public void MoveUnit()
    {
        gridNodes[(int)position.x, (int)position.y].SetUnit(null);     
    }

    public int GetWalkDis()
    {
        return walkDistance;
    }

    public void SetPos(int x, int y)
    {
        position.x = x;
        position.y = y;
        gridNodes[x, y].SetUnit(this);
    }
    public Vector2 GetPos()
    {
        return position;
    }

    public PathFinder GetPathFinder()
    {
        return pathFinder;
    }

    public string GetName()
    {
        return name;
    }
 
}

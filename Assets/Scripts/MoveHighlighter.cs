using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveHighlighter : MonoBehaviour
{
    int width;
    int height;

    [SerializeField] Sprite maskSprite;

    GridNode[,] gridNodes;
    GameObject[,] maskGrid;

    bool[,] visible; 

    [SerializeField] GameObject mapManager;
    [SerializeField] GameObject playSpaceManager;

    void Start()
    {
        gridNodes = mapManager.GetComponent<MapManager>().GetNodes();
        width = gridNodes.GetLength(0);
        height = gridNodes.GetLength(1);
        maskGrid = new GameObject[width, height];
        visible = new bool[width, height];
        CreateMask();
    }

    private void CreateMask()
    {
        for(int x = 0; x< width; x++)
        {
            for(int y=0; y<height; y++)
            {
                GameObject gameObject = new GameObject("Tile" + x + "" + y);
                Transform transform = gameObject.transform;
                transform.SetParent(this.transform);
                transform.position = new Vector3(x, y, -1);
                transform.rotation = Quaternion.identity;

                gameObject.AddComponent(typeof(SpriteRenderer));
                gameObject.GetComponent<SpriteRenderer>().sprite = maskSprite;
                gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 1, 0.5f, 0f);

                maskGrid[x, y] = gameObject;
                visible[x, y] = false;
            }
        }
    }

    int newx;
    int newy;

    IEnumerator FadeIn()
    {
        int x = newx;
        int y = newy;

        for (float f = 0f; f <= 0.25; f += 0.05f)
        {      
            maskGrid[x, y].GetComponent<SpriteRenderer>().color = new Color(0, 1, 0.5f, f);

            yield return new WaitForSeconds(0.01f);
        }
    }

    IEnumerator FadeOut()
    {
        int x = newx;
        int y = newy;

        for (float f = 0.25f; f >= 0; f -= 0.05f)
        {
            maskGrid[x, y].GetComponent<SpriteRenderer>().color = new Color(0, 1, 0.5f, f);

            yield return new WaitForSeconds(0.02f);
        }
    }

    public void StartFaidingOut()
    {
        StartCoroutine("FadeOut");
    }

    public void StartFaidingin()
    {
        StartCoroutine("FadeIn");
    }

    public void HideHighlight()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (visible[x, y])
                {
                    newx = x;
                    newy = y;
                    visible[x, y] = false;
                    StartFaidingOut();
                }
            }
        }
    }

    public void HighlightSpace(int[,] costs, int limit)
    {

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                newx = x;
                newy = y;
                if (costs[x, y] <= limit)
                {
                    if (!visible[x, y])
                    {
                        visible[x, y] = true;
                        StartFaidingin();
                    }
                }
                else
                {
                    if (visible[x, y])
                    {
                        visible[x, y] = false;
                        StartFaidingOut();
                    }
                }
            }
        }
    }

    


}

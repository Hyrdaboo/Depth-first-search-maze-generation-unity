using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public float i, j;

    public Transform cell;
    public GameObject floor;
    public Vector3 scale;
    public GameObject manager, highlighter;
    public GameObject RightWall, BottomWall;
    
    public bool visited;

    public void Start()
    {
        
        manager = GameObject.Find("board");
        highlighter = GameObject.Find("highlighter");

        float w = manager.GetComponent<GameManager>().w;

        cell.transform.position = new Vector3(-i*w, 0.5f, j*w);
        scale = cell.transform.localScale;
        scale.x = manager.GetComponent<GameManager>().w;
        scale.z = manager.GetComponent<GameManager>().w;
        cell.localScale = scale;
    }

    public void highlight()
    {
        Vector3 highlighterScale = manager.GetComponent<GameManager>().current.transform.localScale;
        highlighterScale.y = highlighterScale.y *2;
        highlighter.transform.localScale = highlighterScale;
        Vector3 highlighterPos = manager.GetComponent<GameManager>().current.transform.position;
        highlighterPos.x -= 1;
        highlighterPos.z += 1;
        highlighter.transform.position = highlighterPos;
    }

    float index(float i, float j)
    {
        float cols = manager.GetComponent<GameManager>().cols;

        return i + (j * cols);
    }

    public Cell CheckNeighbors()
    {
        List<Cell> neighbors = new List<Cell>();

        Cell top = null;
        Cell right = null;
        Cell bottom = null;
        Cell left = null;

        float cols = manager.GetComponent<GameManager>().cols;

        if (j > 0) top = manager.GetComponent<GameManager>().grid[(int)index(i, j - 1)];
        if (i < cols - 1) right = manager.GetComponent<GameManager>().grid[(int)index(i + 1, j)];
        if (j <  cols - 1) bottom = manager.GetComponent<GameManager>().grid[(int)index(i, j + 1)];
        if (i > 0) left = manager.GetComponent<GameManager>().grid[(int)index(i - 1, j)];

        if (top != null && !top.visited)
        {
            neighbors.Add(top);
        }
        if (right != null && !right.visited)
        {
            neighbors.Add(right);
        }
        if (bottom != null && !bottom.visited)
        {
            neighbors.Add(bottom);
        }
        if (left != null && !left.visited)
        {
            neighbors.Add(left);
        }

        if (neighbors.Count > 0)
        {
            int rand = (int)Mathf.Floor(Random.Range(0, neighbors.Count));
            return neighbors[rand];
        } else
        {
            return manager.GetComponent<GameManager>().current;
        }
    }
}

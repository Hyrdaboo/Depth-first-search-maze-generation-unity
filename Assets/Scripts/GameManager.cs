using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float cols, rows;
    public float w = 1;

    public Transform board;
    public Cell cellPrefab;
    public Material highlightMaterial;

    public List<Cell> grid = new List<Cell>();

    public Stack<Cell> stack = new Stack<Cell>();

    public Cell current;

    public float width, height;    

    private void Start()
    {
       // Application.targetFrameRate = 7;

        width = board.transform.localScale.x;
        height = board.transform.localScale.z;

        cols = Mathf.Floor(width/w);
        rows = Mathf.Floor(height/w);

        for (int j = 0; j < rows; j++)
        {
            for (int i = 0; i < cols; i++)
            {
                 Cell cell = Instantiate(cellPrefab, Vector3.zero, Quaternion.identity);
                 cell.i = i;
                 cell.j = j;
                 grid.Add(cell);
            }
        }
        current = grid[0];
    }

    private void Update()
    {

        current.visited = true;

        current.highlight();
        current.floor.GetComponent<MeshRenderer>().material = highlightMaterial;

        Cell next = current.CheckNeighbors();

        if (next.visited == false)
        {
            next.visited = true;

            RemoveWalls(current, next);

            stack.Push(current);

            current = next;
        } 
        else if (stack.Count > 0)
        {
            current = stack.Pop();
        }
    }
    void RemoveWalls(Cell first, Cell second)
    {
        float x = first.i - second.i;
        if (x == 1)
        {
            Destroy(second.RightWall);
        }
        else if (x == -1)
        {
            Destroy(first.RightWall);
        }
        float y = first.j - second.j;
        if (y == 1)
        {
            Destroy(second.BottomWall);
        }
        if (y == -1)
        {
            Destroy(first.BottomWall);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveCreateor : MonoBehaviour
{
    public class Cell
    {
        public bool visited = false;
        public bool[] status = new bool[4];
    }

    public Vector2 size;
    public int startPosition = 0;
    public GameObject room;
    public Vector2 offset;

    List<Cell> board;

    // Start is called before the first frame update
    void Start()
    {
        GenerateMaze();
        GenerateCave();
    }

    void GenerateMaze()
    {
        board = new List<Cell>();
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                board.Add(new Cell());
            }
        }

        Stack<int> path = new Stack<int>();

        int currentCell = startPosition;

        int loopCount = 0;
        while (loopCount < 1000)
        {
            loopCount++;
            board[currentCell].visited = true;

            List<int> neighbors = CheckNeighbors(currentCell);

            //Trace back on the path if growth is unavailable and quit the generation if path is empty
            if (neighbors.Count == 0)
            {
                //Exit cave creation
                if (path.Count == 0)
                {
                    break;
                }
                else
                {
                    currentCell = path.Pop();

                }
            }

            //Grow in a random direction
            else
            {
                path.Push(currentCell);
                int newCell = neighbors[Random.Range(0, neighbors.Count)];

                //Down or Right direction
                if (newCell > currentCell)
                {  
                    if (newCell -1  == currentCell) //Right
                    {
                        //Open door in the appropriate direction
                        board[currentCell].status[2] = true;
                        currentCell = newCell;
                        // Create door in the new room so the doors connect
                        board[currentCell].status[3] = true; 
                    }
                    else
                    {
                        //Open door in the appropriate direction
                        board[currentCell].status[1] = true;
                        currentCell = newCell;
                        // Create door in the new room so the doors connect
                        board[currentCell].status[0] = true;
                    }
                }
                //Up or Left direction
                else
                {

                    if (newCell + 1 == currentCell) //Left
                    {
                        //Open door in the appropriate direction
                        board[currentCell].status[3] = true;
                        currentCell = newCell;
                        // Create door in the new room so the doors connect
                        board[currentCell].status[2] = true;
                    }
                    else //Up
                    {
                        //Open door in the appropriate direction
                        board[currentCell].status[0] = true;
                        currentCell = newCell;
                        // Create door in the new room so the doors connect
                        board[currentCell].status[1] = true;
                    }
                }
            }

        }

    }
    void GenerateCave()
    {
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
               var newRoom = Instantiate(room, new Vector3(i * offset.x, 0, -j * offset.y), Quaternion.identity, transform).GetComponent<RoomBehaivour>();
                newRoom.UpdateRoom(board[Mathf.FloorToInt(i + j * size.x)].status);
                newRoom.name += " " + i + "-" + j;
            }
        }
    }

    //Check all adjacent neighbor cells on the board
    List<int> CheckNeighbors(int cell)
    {
        List<int> neighbors = new List<int>();

        //Check the up neighbor
        if (cell - size.x >= 0 && !board[Mathf.FloorToInt(cell - size.x)].visited)
        {
            neighbors.Add(Mathf.FloorToInt(cell - size.x));
        }

        //Check the down neighbor
        if (cell + size.x <board.Count && !board[Mathf.FloorToInt(cell + size.x)].visited)
        {
            neighbors.Add(Mathf.FloorToInt(cell + size.x));
        }

        //Check the right neighbor
        if ((cell + 1) % size.x != 0 && !board[Mathf.FloorToInt(cell + 1)].visited)
        {
            neighbors.Add(Mathf.FloorToInt(cell +1));
        }

        //Check the left neighbor
        if (cell % size.x != 0 && !board[Mathf.FloorToInt(cell - 1)].visited)
        {
            neighbors.Add(Mathf.FloorToInt(cell - 1));
        }

        return neighbors;
    }

  
}

using System.Collections.Generic;
using UnityEngine;

public class MazeGame : MonoBehaviour
{
    private const int Width = 10;
    private const int Height = 10;

    private int[,] maze = new int[Width * 2 + 1, Height * 2 + 1];
    private bool[,] visited;
    private Stack<Vector2Int> stack = new Stack<Vector2Int>();

    public GameObject wallPrefab;
    public GameObject pathPrefab;
    public GameObject startPrefab;
    public GameObject endPrefab;
    public GameObject playerPointerPrefab;

    public Camera mainCamera;
    public Camera mazeCamera;

    private GameObject playerPointer;
    private Vector3 startPosition;

    private bool gameStarted = false;

    private Vector2Int[] directions = new Vector2Int[]
    {
        Vector2Int.up,
        Vector2Int.down,
        Vector2Int.left,
        Vector2Int.right
    };

    void Start()
    {
        mainCamera.enabled = true;
        mazeCamera.enabled = false;
        gameStarted = false;
    }

    public void StartMazeGame()
    {
        mainCamera.enabled = false;
        mazeCamera.enabled = true;
        gameStarted = true;

        InitializeMaze();
        GenerateMaze();
        AddStartAndEndPoints();
        DisplayMaze();
        SpawnPlayerPointer();
    }

    void InitializeMaze()
    {
        for (int x = 0; x < maze.GetLength(0); x++)
        {
            for (int y = 0; y < maze.GetLength(1); y++)
            {
                maze[x, y] = 0; // Wall
            }
        }

        for (int x = 1; x < maze.GetLength(0); x += 2)
        {
            for (int y = 1; y < maze.GetLength(1); y += 2)
            {
                maze[x, y] = 1; // Path
            }
        }
    }

    void GenerateMaze()
    {
        visited = new bool[Width, Height];
        Vector2Int start = new Vector2Int(0, 0);
        stack.Push(start);
        visited[start.x, start.y] = true;

        while (stack.Count > 0)
        {
            Vector2Int current = stack.Peek();
            List<Vector2Int> neighbors = GetUnvisitedNeighbors(current);

            if (neighbors.Count > 0)
            {
                Vector2Int next = neighbors[Random.Range(0, neighbors.Count)];
                Vector2Int wallPosition = GetWallPosition(current, next);
                maze[wallPosition.x, wallPosition.y] = 1;
                visited[next.x, next.y] = true;
                stack.Push(next);
            }
            else
            {
                stack.Pop();
            }
        }
    }

    void AddStartAndEndPoints()
    {
        for (int y = 1; y < maze.GetLength(1) - 1; y++)
        {
            if (maze[1, y] == 1)
            {
                maze[0, y] = 2; // Start point
                startPosition = new Vector3(0.1f * 0, 0.1f * y, 0); // Save start position
                break;
            }
        }

        for (int y = maze.GetLength(1) - 2; y > 0; y--)
        {
            if (maze[maze.GetLength(0) - 2, y] == 1)
            {
                maze[maze.GetLength(0) - 1, y] = 3; // End point
                break;
            }
        }
    }

    void DisplayMaze()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        for (int x = 0; x < maze.GetLength(0); x++)
        {
            for (int y = 0; y < maze.GetLength(1); y++)
            {
                GameObject prefab;

                if (maze[x, y] == 0)
                {
                    prefab = wallPrefab;
                }
                else if (maze[x, y] == 1)
                {
                    prefab = pathPrefab;
                }
                else if (maze[x, y] == 2)
                {
                    prefab = startPrefab;
                }
                else if (maze[x, y] == 3)
                {
                    prefab = endPrefab;
                }
                else
                {
                    continue;
                }

                GameObject cell = Instantiate(prefab, transform);
                RectTransform rectTransform = cell.GetComponent<RectTransform>();
                if (rectTransform != null)
                {
                    rectTransform.anchoredPosition = new Vector2(x * 0.1f, y * 0.1f);
                }
            }
        }
    }

void SpawnPlayerPointer()
{
    // Destroy the previous pointer if it exists
    if (playerPointer != null)
    {
        Destroy(playerPointer);
    }

    // Instantiate the player pointer prefab at the start position
    playerPointer = Instantiate(playerPointerPrefab, transform);
    playerPointer.transform.position = new Vector3(0.1f * 0, 0.1f * 0, 0); // Start position for the pointer
    playerPointer.SetActive(true);  // Make sure it's active

    // Ensure the pointer is in the same layer as the maze objects
    playerPointer.layer = LayerMask.NameToLayer("Default");
}


    void Update()
    {
        if (gameStarted)
        {
            HandlePlayerInput();
        }

        if (gameStarted && Input.GetKeyDown(KeyCode.Escape))
        {
            EndMazeGame();
        }

        if (!gameStarted && Input.GetKeyDown(KeyCode.G))
        {
            StartMazeGame();
        }
    }

void HandlePlayerInput()
{
    // Get mouse position in world space
    Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    mousePosition.z = 0; // Ensure the Z position is 0, so it stays in 2D

    // Update the player pointer's position to follow the mouse
    playerPointer.transform.position = mousePosition;

    // Check if the player pointer is colliding with any walls (tagging walls correctly is crucial)
    Collider2D hit = Physics2D.OverlapPoint(mousePosition);
    if (hit != null && hit.CompareTag("Wall")) // Ensure your walls have the "Wall" tag
    {
        // Change pointer color on collision
        playerPointer.GetComponent<SpriteRenderer>().color = Color.red;
    }
    else
    {
        // Reset the color if not colliding with a wall
        playerPointer.GetComponent<SpriteRenderer>().color = Color.white;
    }
}


    public void EndMazeGame()
    {
        mainCamera.enabled = true;
        mazeCamera.enabled = false;
        gameStarted = false;

        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        if (playerPointer != null)
        {
            Destroy(playerPointer);
        }
    }

    // Helper method to get the unvisited neighbors of a given cell
    List<Vector2Int> GetUnvisitedNeighbors(Vector2Int cell)
    {
        List<Vector2Int> neighbors = new List<Vector2Int>();

        foreach (var direction in directions)
        {
            Vector2Int neighbor = cell + direction;

            if (neighbor.x >= 0 && neighbor.x < Width && neighbor.y >= 0 && neighbor.y < Height && !visited[neighbor.x, neighbor.y])
            {
                neighbors.Add(neighbor);
            }
        }

        return neighbors;
    }

    // Helper method to get the position of the wall between two cells
    Vector2Int GetWallPosition(Vector2Int a, Vector2Int b)
    {
        return new Vector2Int(a.x + b.x + 1, a.y + b.y + 1);
    }
}

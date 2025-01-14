using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VentGame : MonoBehaviour
{
    public Camera mainCamera;
    public Camera minigameCamera;
    public GameObject customPointer;
    public GameObject vent;

   // public Transform[] spawnPositions; // Predefined positions for circles
    private int currentCircleIndex = 0;
    private List<int> spawnOrder;

    private bool gameStarted = false;

    // Reference to child images (topLeft, topRight, botLeft, botRight)
    public GameObject topLeft;
    public GameObject topRight;
    public GameObject botLeft;
    public GameObject botRight;
    public float movingDuration = 0.5f;
    public float movingDistance = 2f;

    private List<GameObject> childObjects; // List to store child objects

    void Start()
    {
        mainCamera.enabled = true;
        minigameCamera.enabled = false;

        // Make sure the cursor is visible and unlocked when starting the scene
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        customPointer.SetActive(false); // Hide the custom pointer initially

        // Store the child objects in the list
        childObjects = new List<GameObject> { topLeft, topRight, botLeft, botRight };

        // Initially, set all child objects to inactive
        foreach (var child in childObjects)
        {
            child.SetActive(false);
        }
    }

    public void StartMinigame()
    {
        mainCamera.enabled = false;
        minigameCamera.enabled = true;

        // Hide the cursor and unlock it during the minigame
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        customPointer.SetActive(true); // Show the custom pointer

        gameStarted = true;
        GenerateRandomSpawnOrder();
        RevealNextChild();
    }

    public void EndMinigame()
    {   currentCircleIndex = 0;
        mainCamera.enabled = true;
        minigameCamera.enabled = false;

        // Reset the cursor visibility and lock state when the game ends
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.None;

        customPointer.SetActive(false); // Hide the custom pointer
        gameStarted = false;

        Debug.Log("Minigame ended.");
    }

    void GenerateRandomSpawnOrder()
    {
        spawnOrder = new List<int>();
        for (int i = 0; i < childObjects.Count; i++)
        {
            spawnOrder.Add(i);
        }

        // Shuffle the spawn order
        for (int i = spawnOrder.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            int temp = spawnOrder[i];
            spawnOrder[i] = spawnOrder[randomIndex];
            spawnOrder[randomIndex] = temp;
        }
    }

    void RevealNextChild()
    {
        if (currentCircleIndex >= spawnOrder.Count)
        {
            EndMinigame();
            StartCoroutine(MoveVent(0, 0f));
            StartCoroutine(MoveVent(1, 3f));
            return;
        }

        int spawnLocationIndex = spawnOrder[currentCircleIndex];
        GameObject childToReveal = childObjects[spawnLocationIndex];

        // Activate the selected child object
        childToReveal.SetActive(true);

        currentCircleIndex++;
    }
 IEnumerator MoveVent(int smjer, float delay)

    {
        yield return new WaitForSeconds(delay);
        Vector3 direction;
        if (smjer == 0){
            direction = Vector3.forward;
        }
        else{
            direction = Vector3.back;
        }
        
        Vector3 startPosition = vent.transform.position; // Get the object's starting position
        Vector3 targetPosition = startPosition + direction * movingDistance; // Target position (move right by 'distance')

        float elapsedTime = 0f;

        while (elapsedTime < movingDuration)
        {
            // Smoothly move the object by interpolating its position
            vent.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / movingDuration);
            elapsedTime += Time.deltaTime; // Increase the elapsed time
            yield return null; // Wait until the next frame
        }

        // Ensure the object reaches the target position at the end
        vent.transform.position = targetPosition;
    }
    public bool isGameStarted(){
        return gameStarted;
    }
    void Update()
    {
        if (gameStarted)
        {
            // Update custom pointer position
            Vector2 mousePosition = Input.mousePosition;
            customPointer.GetComponent<RectTransform>().position = mousePosition;

            // Detect clicks
            if (Input.GetMouseButtonDown(0)) // Left mouse click
            {
                // Check if any of the visible child objects were clicked
                foreach (var child in childObjects)
                {
                    if (child.activeSelf)
                    {
                        RectTransform rectTransform = child.GetComponent<RectTransform>();
                        if (RectTransformUtility.RectangleContainsScreenPoint(rectTransform, mousePosition, minigameCamera))
                        {
                            Debug.Log(child.name + " clicked!");
                            child.SetActive(false); // Hide the clicked child
                            RevealNextChild();
                            break;
                        }
                    }
                }
            }
        }
    }
}

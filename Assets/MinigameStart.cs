using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameStart : MonoBehaviour
{
    public Camera mainCamera;
    public Camera minigameCamera;
    public GameObject arrowPrefab;
    public GameObject arrowCorrectPrefab;
    public int[] rotations;
    public bool gameStarted = false;
    public GameObject[] arrows;
    public int arrowCount;
    public int currentArrow;
    
    private BoxCollider RawImage; //detekcija minigamea

    // Start is called before the first frame update
    void Start()
    {
        mainCamera.enabled = true;
        minigameCamera.enabled = false;
        gameStarted = false;
    }
    public void StartMinigame()
    {
        mainCamera.enabled = false;
        minigameCamera.enabled = true;
        gameStarted = true;
        arrowCount = 0;
        currentArrow = 0;
        arrows = new GameObject[13];
        rotations = GenerateRandomRotations(6);

        Debug.Log("starto se");
        for (int i = 0; i < rotations.Length; i++)
        {

            SpawnUIObject(-0.4f, 0.3f, rotations[i], i, true);
        }
        
    }
    void ResetMinigame()
    {
 
        EndMinigame();
        StartMinigame();

    }
    void EndMinigame()
    {
        mainCamera.enabled = true;
        
        minigameCamera.enabled = false;
        gameStarted = false;

        Debug.Log("endo se");
        for (int i = 0; i < arrows.Length; i++)
        {

            Destroy(arrows[i]);
        }
        arrowCount=0;
    }
    void MinigameSuccess()
    {
        mainCamera.enabled = true;
        minigameCamera.enabled = false;
        gameStarted = false;

        Debug.Log("uspio");
        for (int i = 0; i < arrows.Length; i++)
        {

            Destroy(arrows[i]);
        }
        arrowCount=0;
    }
    void Update()
    {


        if (gameStarted)
        { // ako je u igri
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D)) //ako je stisno nes za igru
            {
                if (Input.GetKeyDown(KeyCode.W) && rotations[currentArrow] == 0 //ako je tocno stisno
                || Input.GetKeyDown(KeyCode.D) && rotations[currentArrow] == 270
                || Input.GetKeyDown(KeyCode.S) && rotations[currentArrow] == 180
                || Input.GetKeyDown(KeyCode.A) && rotations[currentArrow] == 90)
                {
                    Destroy(arrows[currentArrow]);
                    SpawnUIObject(-0.4f, 0.3f, rotations[currentArrow], currentArrow, false);
                    currentArrow++;       
                    if (currentArrow==rotations.Length){
                        MinigameSuccess();
                    }
                }
                else //ako je krivo stisno
                {
                    ResetMinigame();
                }
            }
            else if (Input.anyKeyDown)
            { // ako nije stisno nes za igru
                EndMinigame();
            }
        }

    }

    public void SpawnUIObject(float x, float y, int rotation, int place, bool crni)
    {
        // Instantiate the prefab as a child of the Canvas
        if (crni){
        GameObject arrow = Instantiate(arrowPrefab, gameObject.transform);
        arrows[arrowCount] = arrow;
        arrowCount++;

        // Optionally, set the position of the spawned UI object
        RectTransform rectTransform = arrow.GetComponent<RectTransform>();
        if (rectTransform != null)
        {
            // Set anchored position within the Canvas
            //rectTransform.anchoredPosition = new Vector2(Random.Range(-200, 200), Random.Range(-100, 100));
            rectTransform.anchoredPosition = new Vector2(x + place * 0.15f, y);
            rectTransform.rotation = Quaternion.Euler(0, 0, rotation);
        }
        arrow.transform.SetAsLastSibling();
        }
        else{
            GameObject arrow = Instantiate(arrowCorrectPrefab, gameObject.transform);
        arrows[arrowCount] = arrow;
        arrowCount++;

        // Optionally, set the position of the spawned UI object
        RectTransform rectTransform = arrow.GetComponent<RectTransform>();
        if (rectTransform != null)
        {
            // Set anchored position within the Canvas
            //rectTransform.anchoredPosition = new Vector2(Random.Range(-200, 200), Random.Range(-100, 100));
            rectTransform.anchoredPosition = new Vector2(x + place * 0.15f, y);
            rectTransform.rotation = Quaternion.Euler(0, 0, rotation);
        }
        arrow.transform.SetAsLastSibling();
        }

    }

    void OnMouseDown()
    {
        mainCamera.enabled = !mainCamera.enabled;
        minigameCamera.enabled = !minigameCamera.enabled;
        // Code here is called when the GameObject is clicked on.
    }

    int[] GenerateRandomRotations(int count)
    {
        int[] randomRotations = new int[count];
        int[] possibleRotations = { 0, 90, 180, 270 };

        for (int i = 0; i < count; i++)
        {
            // Select a random index from the possible rotations
            int randomIndex = Random.Range(0, possibleRotations.Length);
            randomRotations[i] = possibleRotations[randomIndex];
        }

        return randomRotations;
    }
}


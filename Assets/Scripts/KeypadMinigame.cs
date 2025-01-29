using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class KeypadMinigame : MonoBehaviour
{
    public GameObject buttonPrefab;  // Assign a UI Button prefab in the Inspector
    public Transform buttonParent;   // Assign a UI Panel or empty GameObject as parent
    public Text messageText;         // Assign a UI Text element

    private List<Button> buttons = new List<Button>();
    private int nextNumber = 1;
    private bool gameStarted = false;

    void Start()
    {
        GenerateButtons();

    }

    void GenerateButtons()
    {
        nextNumber = 1;
        buttons.Clear();

        List<int> numbers = new List<int>();
        for (int i = 1; i <= 10; i++) numbers.Add(i);
       

        int columns = 5; // Number of columns
        float spacingX = 120f; // Adjust horizontal spacing
        float spacingY = 120f; // Adjust vertical spacing
        Vector2 startPos = new Vector2(-250, 100); // Adjust starting position

        // Clear previous buttons
        foreach (Transform child in buttonParent)
        {
            if (child.GetType() != typeof(Text))
            {
                Destroy(child.gameObject);
            }
        }

        for (int i = 0; i < 10; i++)
        {
            GameObject newButton = Instantiate(buttonPrefab, buttonParent);
            UnityEngine.Debug.Log("Creating button #" + (i + 1));
            Button btn = newButton.GetComponent<Button>();
            TMP_Text btnText = newButton.GetComponentInChildren<TMP_Text>();

            int number = numbers[i];
            btnText.text = number.ToString();
            btn.onClick.AddListener(() => OnButtonClick(number, btn));

            // Set position manually
            int row = i / columns;
            int col = i % columns;
            RectTransform rectTransform = newButton.GetComponent<RectTransform>();

            // Ensure correct positioning inside the UI
            rectTransform.localPosition = startPos + new Vector2(col * spacingX, -row * spacingY);
            rectTransform.localScale = Vector3.one;

            buttons.Add(btn);
        }

        messageText.text = "Click the numbers in order!";
    }

    void OnButtonClick(int number, Button btn)
    {
        UnityEngine.Debug.Log("KLIKNUT");
        if (number == nextNumber)
        {
            btn.interactable = false; // Disable button after correct click
            nextNumber++;

            if (nextNumber > 10)
            {
                messageText.text = "You won! Restarting...";
                Invoke("GenerateButtons", 2f);
            }
        }
        else
        {
            messageText.text = "Wrong! Try again.";
            Invoke("GenerateButtons", 1.5f);
        }
    }

    public bool isGameStarted()
    {
        return gameStarted;
    }
    void Update()
    {
        if (!gameStarted)
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                buttonParent.transform.gameObject.SetActive(true);
                gameStarted = true;
                
                        Cursor.lockState = CursorLockMode.None; // Unlock the cursor
        Cursor.visible = true; // Make the cursor visible
            }
        }
        else
        {
            Cursor.lockState = CursorLockMode.None; // Unlock the cursor
        Cursor.visible = true; // Make the cursor visible
            if (Input.anyKeyDown)
            {
                buttonParent.transform.gameObject.SetActive(false);
            }
        }
    }
}

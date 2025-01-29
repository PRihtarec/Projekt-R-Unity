using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using System.Collections;

public class KeypadMinigame : MonoBehaviour
{
    public GameObject buttonPrefab;  
    public Transform buttonParent;   
    public Text messageText;       
    public Text passwordText;        

    private List<Button> buttons = new List<Button>();
    private int nextNumber;
    private int nextNumberIndex;
    private bool gameStarted = false;
  
    private NumberManager numberManager;
    List<int> password;
    public Camera keypadCamera;
    public Camera mainCamera;

    void Start()
    {
        numberManager = FindObjectOfType<NumberManager>();
        keypadCamera.gameObject.SetActive(false);
    }

    void GenerateButtons()
    {
        password = NumberManager.AllGeneratedNumbers;
        nextNumberIndex = 0;
        nextNumber = password[0];
        buttons.Clear();

        List<int> numbers = new List<int>();
        for (int i = 1; i <= 10; i++) numbers.Add(i);

        int columns = 5; 
        float spacingX = 80; 
        float spacingY = 80; 
        Vector2 startPos = new Vector2(-150, 0); 

        foreach (Transform child in buttonParent)
        {
            if (child.name != "messageText" && child.name != "password")
            {
                Destroy(child.gameObject);
            }
        }

        for (int i = 0; i < 10; i++)
        {
            GameObject newButton = Instantiate(buttonPrefab, buttonParent);
            Button btn = newButton.GetComponent<Button>();
            TMP_Text btnText = newButton.GetComponentInChildren<TMP_Text>();

            int number = numbers[i];
            btnText.text = number.ToString();
            btn.onClick.AddListener(() => OnButtonClick(number, btn));

            int row = i / columns;
            int col = i % columns;
            RectTransform rectTransform = newButton.GetComponent<RectTransform>();

            rectTransform.localPosition = startPos + new Vector2(col * spacingX, -row * spacingY);
            rectTransform.localScale = Vector3.one;

            buttons.Add(btn);
        }

        messageText.text = "Enter password!";
        passwordText.text="";
    }

    void OnButtonClick(int number, Button btn)
    {
        if (number == nextNumber)
        {
            nextNumberIndex++;
            passwordText.text = passwordText.text + number;

            if (nextNumberIndex > 5)
            {
                messageText.text = "Correct!";
                Invoke("EndMinigame", 2f);
                return;
            }

            nextNumber = password[nextNumberIndex];
        }
        else
        {
            messageText.text = "Wrong! Try again.";
            passwordText.text = "";
            
            // Flash all buttons red
            StartCoroutine(FlashButtonsRed());
            
            Invoke("GenerateButtons", 1.5f);
        }
    }

    
    private IEnumerator FlashButtonsRed()
    {
       
        List<Color> originalColors = new List<Color>();
        foreach (Button btn in buttons)
        {
            originalColors.Add(btn.GetComponent<Image>().color);
        }

     
        for (int i = 0; i < 2; i++)
        {
     
            foreach (Button btn in buttons)
            {
                btn.GetComponent<Image>().color = Color.red;
            }

      
            yield return new WaitForSeconds(0.2f);

      
            for (int j = 0; j < buttons.Count; j++)
            {
                buttons[j].GetComponent<Image>().color = originalColors[j];
            }

         
            yield return new WaitForSeconds(0.2f);
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
                StartMinigame();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                EndMinigame();
            }
        }
    }

    public void StartMinigame()
    {
        keypadCamera.gameObject.SetActive(true);
        mainCamera.gameObject.SetActive(false);
        GenerateButtons();
        buttonParent.transform.gameObject.SetActive(true);
        gameStarted = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void EndMinigame()
    {
        keypadCamera.gameObject.SetActive(false);
        mainCamera.gameObject.SetActive(true);
        buttonParent.transform.gameObject.SetActive(false);
        gameStarted = false;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}

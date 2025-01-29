using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamesController : MonoBehaviour
{
    // Start is called before the first frame update
    private MinigameStart arrowsGame;
    private VentGame ventGame;
    private KeypadMinigame keypadGame;
    private bool wonGame;
    private bool lostGame;
    void Start()
    {
        arrowsGame = FindObjectOfType<MinigameStart>();
        ventGame = FindObjectOfType<VentGame>();
        keypadGame = FindObjectOfType<KeypadMinigame>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public bool isMinigameInProgress(){
        return arrowsGame.isGameStarted() || ventGame.isGameStarted() || keypadGame.isGameStarted();;
    }
    public bool isMinigameWon(){
        bool wonGame2 = wonGame;
        wonGame = false;
        return wonGame2;
    }
    public bool isMinigameLost(){
        bool lostGame2 = lostGame;
        lostGame = false;
        return lostGame2;
    }

}

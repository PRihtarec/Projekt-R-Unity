using System.Collections.Generic;
using UnityEngine;

public class NumberManager : MonoBehaviour
{
    public static List<int> AllGeneratedNumbers = new List<int>();

    private static int addedCards = 0;       //radi debugganja, nije potrebno za funkcionalnost

    public static void AddNumbers(int num1, int num2)
    {
        AllGeneratedNumbers.Add(num1);
        AllGeneratedNumbers.Add(num2);

        addedCards += 2;

        if(addedCards == 6)
            Debug.Log("sifra:" + string.Join("", AllGeneratedNumbers));     //radi debugganja
    }
    void Start(){
        AllGeneratedNumbers = new List<int>();
    }
}
    

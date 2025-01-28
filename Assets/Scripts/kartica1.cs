using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardRandomizer1 : MonoBehaviour
{
    int num1;
    int num2;
    [SerializeField] TMP_Text display;

    void Start()
    {
        num1 = Random.Range(1, 10);
        num2 = Random.Range(1, 10);
        display.text = num1.ToString() + num2.ToString() + "xxxx";

        NumberManager.AddNumbers(num1, num2);     //salje brojeve u public listu da dobijemo konacnu tocnu kombinaciju, 0 su placeholderi
    }
}

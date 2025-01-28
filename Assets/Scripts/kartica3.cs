using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardRandomizer3 : MonoBehaviour
{
    int num5;
    int num6;
    [SerializeField] TMP_Text display;

    void Start()
    {
        num5 = Random.Range(1, 10);
        num6 = Random.Range(1, 10);
        display.text = "xxxx" + num5.ToString() + num6.ToString();

        NumberManager.AddNumbers(num5, num6);     //salje brojeve u public listu da dobijemo konacnu tocnu kombinaciju
    }
}

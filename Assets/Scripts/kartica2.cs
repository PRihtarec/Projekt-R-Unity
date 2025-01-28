using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardRandomizer2 : MonoBehaviour
{
    int num3;
    int num4;
    [SerializeField] TMP_Text display;

    void Start()
    {
        num3 = Random.Range(1, 10);
        num4 = Random.Range(1, 10);
        display.text = "xx" + num3.ToString() + num4.ToString() + "xx";

        NumberManager.AddNumbers(num3, num4);     //salje brojeve u public listu da dobijemo konacnu tocnu kombinaciju
    }
}

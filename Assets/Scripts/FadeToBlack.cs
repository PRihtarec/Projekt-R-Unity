using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class FadeToBlackAndLoadMenu : MonoBehaviour
{
    [SerializeField] private Image fadeImage;
    [SerializeField] private TextMeshProUGUI youDiedText;
    [SerializeField] private float fadeDuration = 2f;
    [SerializeField] private float displayDuration = 3f;

    private void Start()
    {
        fadeImage.color = new Color(0, 0, 0, 0);
        if (youDiedText != null)
        {
            youDiedText.alpha = 0;
        }
    }

    public void TriggerFadeAndLoadMenu()
    {
        StartCoroutine(FadeAndLoadMenu());
    }

    private IEnumerator FadeAndLoadMenu()
    {
        yield return new WaitForSeconds(2f);

        float timeElapsed = 0f;
        while (timeElapsed < fadeDuration)
        {
            timeElapsed += Time.deltaTime;
            float alphaValue = Mathf.Clamp01(timeElapsed / fadeDuration);

            fadeImage.color = new Color(0, 0, 0, alphaValue);

            if (youDiedText != null)
            {
                youDiedText.alpha = alphaValue;
            }

            yield return null;
        }

        yield return new WaitForSeconds(displayDuration);

        SceneManager.LoadScene("main menu"); // vrati na main menu
    }
}

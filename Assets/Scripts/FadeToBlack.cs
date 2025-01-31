using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class FadeToBlackAndLoadMenu : MonoBehaviour
{
    public AudioSource jumpscareSource;
    public AudioSource chompSource;
    public AudioSource screamSource;
    public AudioSource hodanjeSource;
    public AudioSource muzika;
    public GameObject monster;

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
        hodanjeSource.Stop();
        muzika.Stop();
        AudioSource[] audioSources = monster.GetComponents<AudioSource>();

            foreach (AudioSource audio in audioSources)
            {
                if (audio.clip != null && audio.clip.name == "brzocudoviste")
                {
                    audio.Stop(); 
                    break;
                }
            }
        jumpscareSource.Play();
        chompSource.Play();
        Invoke("PlayAudio", 1f);

        yield return new WaitForSeconds(0.6f);

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

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        
        SceneManager.LoadScene("main menu"); // vrati na main menu

        
    }
    void PlayAudio()
    {
        screamSource.Play();
    }
}

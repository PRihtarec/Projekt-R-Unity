using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TextFlicker : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public CanvasGroup canvasGroup;
    public float fadeInTime = 1f; // Time to fade in
    public float fadeOutTime = 1f; // Time to fade out
    public float flickerSpeed = 0.5f; // Time between each fade cycle
    private bool isHovered = false; // Track if the mouse is hovering over the text
    private bool shouldFade = true; // Flag to control whether fading should happen

    private void Start()
    {
        if (canvasGroup == null)
            canvasGroup = GetComponent<CanvasGroup>();

        StartCoroutine(FlickerText());
    }

    private IEnumerator FlickerText()
    {
        while (true)
        {
            if (!isHovered && shouldFade)
            {
                // Fade In
                float t = 0;
                while (t < fadeInTime && !isHovered)
                {
                    t += Time.unscaledDeltaTime; // Use unscaledDeltaTime to ignore timeScale
                    canvasGroup.alpha = Mathf.Lerp(0.3f, 1, t / fadeInTime);
                    yield return null;
                }

                // Fade Out
                t = 0;
                while (t < fadeOutTime && !isHovered)
                {
                    t += Time.unscaledDeltaTime; // Use unscaledDeltaTime here too
                    canvasGroup.alpha = Mathf.Lerp(1, 0.3f, t / fadeOutTime);
                    yield return null;
                }

                // Wait for the next cycle
                yield return new WaitForSecondsRealtime(flickerSpeed); // Ensures delay works when paused
            }
            else
            {
                // If hovered, immediately set the opacity to 1
                canvasGroup.alpha = 1;
                yield return null; // Skip fading effect while hovered
            }
        }
    }

    // IPointerEnterHandler: Triggered when mouse enters the text area
    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovered = true; // Set hovered state to true
        shouldFade = false; // Stop fading when hovered
        canvasGroup.alpha = 1; // Set opacity to 1 immediately
    }

    // IPointerExitHandler: Triggered when mouse exits the text area
    public void OnPointerExit(PointerEventData eventData)
    {
        isHovered = false; // Set hovered state to false
        shouldFade = true; // Resume fading after hover
    }
}

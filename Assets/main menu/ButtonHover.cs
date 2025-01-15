using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ButtonHoverTMP : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
{
    public TMP_Text buttonText; // Reference to the TextMeshPro component
    private Color originalColor; // To store the original text color
    private bool isSelected = false; // Tracks whether the button is selected

    void Start()
    {
        if (buttonText == null)
        {
            buttonText = GetComponentInChildren<TMP_Text>();
        }

        if (buttonText != null)
        {
            originalColor = buttonText.color;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (buttonText != null && !isSelected)
        {
            Color hoverColor = originalColor;
            hoverColor.a = 1f; // Fully opaque
            buttonText.color = hoverColor;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (buttonText != null && !isSelected)
        {
            buttonText.color = originalColor;
        }
    }

    public void OnSelect(BaseEventData eventData)
    {
        if (buttonText != null)
        {
            isSelected = true;
            Color selectedColor = originalColor;
            selectedColor.a = 1f; // Fully opaque
            buttonText.color = selectedColor;
        }
    }

    public void OnDeselect(BaseEventData eventData)
    {
        if (buttonText != null)
        {
            isSelected = false;
            buttonText.color = originalColor;
        }
    }
}

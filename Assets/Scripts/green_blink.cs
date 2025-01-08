using UnityEngine;

public class Green_blink : MonoBehaviour
{
    public Color blinkColor = Color.green;  // The color to blink
    public float blinkInterval = 0.5f;      // Time between blinks
    public float lightIntensity = 0.5f;     // Light intensity when blinking

    private Renderer cubeRenderer;
    private Color originalColor;
    private Light cubeLight;
    private bool isBlinking = false;

    void Start()
    {
        cubeRenderer = GetComponent<Renderer>();
        originalColor = cubeRenderer.material.color;

        // Get or add a Light component
        cubeLight = GetComponent<Light>();
        if (cubeLight == null)
        {
            cubeLight = gameObject.AddComponent<Light>();
            cubeLight.type = LightType.Point;
            cubeLight.range = 5f;
            cubeLight.color = blinkColor;
        }

        StartCoroutine(BlinkCube());
    }

    System.Collections.IEnumerator BlinkCube()
    {
        while (true)
        {
            isBlinking = !isBlinking;

            // Toggle cube color and light intensity
            cubeRenderer.material.color = isBlinking ? blinkColor : originalColor;
            cubeLight.intensity = isBlinking ? lightIntensity : 0;

            yield return new WaitForSeconds(blinkInterval);
        }
    }
}

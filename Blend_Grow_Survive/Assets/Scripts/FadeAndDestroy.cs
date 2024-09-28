using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeAndDestroy : MonoBehaviour
{
    public float fadeDuration = 3f; // Duration of the fade in seconds
    private float fadeSpeed;
    private Material objectMaterial;
    private Color objectColor;

    void Start()
    {
        // Get the material of the object
        objectMaterial = GetComponent<Renderer>().material;

        // Store the original color of the object
        objectColor = objectMaterial.color;

        // Calculate the fade speed based on the fade duration
        fadeSpeed = 1f / fadeDuration;

        // Start the fade process
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        float alphaValue = objectColor.a; // Store the initial alpha value (usually 1 for opaque)

        // Gradually reduce the alpha value over time
        while (alphaValue > 0f)
        {
            alphaValue -= fadeSpeed * Time.deltaTime;
            objectColor.a = Mathf.Clamp(alphaValue, 0f, 1f); // Clamp the alpha between 0 and 1
            objectMaterial.color = objectColor; // Set the new color with updated alpha

            yield return null; // Wait for the next frame
        }

        // Destroy the object after it becomes fully transparent
        Destroy(gameObject);
    }
}
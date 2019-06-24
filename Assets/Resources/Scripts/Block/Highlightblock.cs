using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Highlight Script. Highlights a block when it's being hit by the player.

public class Highlightblock : MonoBehaviour
{
    public float speed;
    float currentTime;
    public float transparent;
    Color colorStart, colorEnd, currentColor;
    Renderer mat;

    bool isActive;

    void Start()
    {
        currentTime = 1;
        mat = GetComponent<Renderer>();
        colorStart = new Color(mat.material.color.r, mat.material.color.g, mat.material.color.b, transparent);
        colorEnd = new Color(mat.material.color.r, mat.material.color.g, mat.material.color.b, 0);
        gameObject.SetActive(false);
    }


    void Update()
    {
        if (isActive)
        {
            currentTime -= Time.deltaTime;
            currentColor = Color.Lerp(colorEnd, colorStart, currentTime);
            mat.material.SetColor("_BaseColor", currentColor);

            if (currentTime <= 0)
            {
                isActive = false;
                gameObject.SetActive(false);
            }
        }
    }

    public void startHighlight()
    {
        currentTime = 1;
        isActive = true;
        gameObject.SetActive(true);
        mat.material.SetColor("_BaseColor", colorStart);
    }
}

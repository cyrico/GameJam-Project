using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeColorByRange : MonoBehaviour
{
    public Color colorA = new Color(1.0f, 1.0f, 0.7f, 1.0f);
    public Color colorB = new Color(0.5f, 0.1f, 0.0f, 1.0f);

    public List<Renderer> renders;

    // Start is called before the first frame update
    void Start()
    {
        Color newColor = Color.Lerp(colorA, colorB, Random.Range(0.0f, 1.0f));
        foreach (Renderer renderer in renders)
        {
            renderer.material.color = newColor;
        }
    }
}

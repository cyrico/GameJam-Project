using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeColorByAdjacency : MonoBehaviour
{
    public Color startColor = Color.white; 
    public float maxColorDelta = 30;

    public List<Renderer> renders; 

    // Start is called before the first frame update
    void Start()
    {
        Color newColor = GetAdjacentColor(startColor);
        foreach (Renderer renderer in renders)
        {
            renderer.material.color = newColor;
        }
    }

    private Color GetAdjacentColor(Color color)
    {
        Vector3 currentColor = new Vector3(color.r, color.g, color.b);
        Vector3 rotationAxis = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
        Vector3 newColor = Quaternion.AngleAxis(maxColorDelta, rotationAxis) * currentColor;
        return new Color(newColor.x, newColor.y, newColor.z, 1);
    }
}

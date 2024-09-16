using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureRandomizer : MonoBehaviour
{
    public List<Texture> possibleTextures;

    // Start is called before the first frame update
    void Start()
    {
        Texture newTexter = possibleTextures[Random.Range(0, possibleTextures.Count)];
        GetComponent<Renderer>().material.SetTexture("_BaseMap", newTexter);
    }
}

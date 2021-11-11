using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScroll : MonoBehaviour
{
    // Configuration parameters
    [SerializeField] float backgroundScrollSpeed = 0.1f;
    // Cached references
    Material material;
    Vector2 offset;



    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<Renderer>().material;
        offset = new Vector2(0f, backgroundScrollSpeed);


    }

    // Update is called once per frame
    void Update()
    {
        material.mainTextureOffset += offset * Time.deltaTime;
    }
}

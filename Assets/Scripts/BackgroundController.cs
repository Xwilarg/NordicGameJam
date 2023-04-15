using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    [SerializeField]
    [Range(0f, 0.02f)]
    private float _brightnessVariationSpeed = 0.01f;
    private Material _starryBackgroundMat;
    
    void Start()
    {
        _starryBackgroundMat = GetComponent<SpriteRenderer>().material;
    }

    void Update()
    {
        _starryBackgroundMat.SetFloat("_BrightnessVariationScale", Mathf.PingPong(Time.time * _brightnessVariationSpeed, 0.2f));
    }
}

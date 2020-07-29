using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReticlePanel : MonoBehaviour
{
    float _size = 50f;
    private float _minSize = 50f;
    private float _maxSize = 500f;
    
    private RectTransform reticle;
    public float _sizeModifier = 0f;
    
    private void Start()
    {
        reticle = GetComponent<RectTransform>();
    }

    void Update()
    {
        _size = Mathf.Clamp(1100 * _sizeModifier, _minSize, _maxSize); //todo it's hard code, different resolutions can bring different results 
        reticle.sizeDelta = new Vector2(_size, _size);
    }
}

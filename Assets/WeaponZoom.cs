using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class WeaponZoom : MonoBehaviour
{
    [SerializeField] private float _standartView = 45f;
    [SerializeField] private float _zoomView = 20f;
    [Range(0f,1f)][SerializeField] private float _declineSensitivity = 0.5f;


    private Camera camera;
    private RigidbodyFirstPersonController controller;
    private float _startXSensitivity;
    private float _startYSensitivity;
    
    private void Start()
    {
        camera = GetComponentInParent<Camera>();
        controller = GetComponentInParent<RigidbodyFirstPersonController>();
        _startXSensitivity = controller.mouseLook.XSensitivity;
        _startYSensitivity = controller.mouseLook.YSensitivity;
    }

    void Update()
    {
        if (Input.GetButton("Fire2"))
        {
            camera.fieldOfView = _zoomView;
            
            controller.mouseLook.XSensitivity = _startXSensitivity * _declineSensitivity;
            controller.mouseLook.YSensitivity = _startYSensitivity * _declineSensitivity;
        }
        else
        {
            camera.fieldOfView = _standartView;
            
            controller.mouseLook.XSensitivity = _startXSensitivity;
            controller.mouseLook.YSensitivity = _startYSensitivity;
        }
    }
}

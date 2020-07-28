using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponZoom : MonoBehaviour
{

    void Update()
    {
        if (Input.GetButton("Fire2"))
        {
            GetComponentInChildren<Camera>().fieldOfView = 20;
        }
        else
        {
            GetComponentInChildren<Camera>().fieldOfView = 45;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.GetComponent<PlayerHealth>())
        {
            //MakePickUpStuff();
            Destroy(gameObject);
        }
    }
}

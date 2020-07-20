using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.VFX;

public class WeaponScript : MonoBehaviour
{

    [SerializeField] Camera FPCamera;
    [SerializeField] float _range;
    [SerializeField] int _powerOfHit = 1;
    [SerializeField] private ParticleSystem muzzleFlesh;
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
            MuzzleFlesh();
        }
    }

    private void MuzzleFlesh()
    {
        muzzleFlesh.Play();
    }

    private void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, _range))
        {
            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            if (target)
            {
                target.DecreaseHealth(_powerOfHit);
            }
            
            //todo make some visual effects for hitting the enemy;
            //todo call a method to decreases enemy health;
        }
        else
        {
            
        }
    }
}

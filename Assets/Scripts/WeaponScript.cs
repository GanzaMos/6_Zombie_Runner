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
    [SerializeField] private ParticleSystem collisionParticle;
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
            CollisionParticle(hit);
            EnemyHealth targetHealth = hit.transform.GetComponent<EnemyHealth>();
            if (targetHealth)
            {
                targetHealth.DecreaseHealth(_powerOfHit);
            }
            EnemyAI targetAI = hit.transform.GetComponent<EnemyAI>();
            if (targetAI)
            {
                targetAI._isProvoke = true;
            }
        }
        else
        {
            
        }
    }

    void CollisionParticle(RaycastHit hit)
    {
        var particle = Instantiate(collisionParticle, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(particle.gameObject, 1f);
    }
}

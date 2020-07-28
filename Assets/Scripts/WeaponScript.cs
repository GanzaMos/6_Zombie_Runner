using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.VFX;
using Random = UnityEngine.Random;

public class WeaponScript : MonoBehaviour
{

    [SerializeField] Camera FPCamera;
    [SerializeField] float _range;
    [SerializeField] int _powerOfHit = 1;
    [SerializeField] private ParticleSystem muzzleFlesh;
    [SerializeField] private ParticleSystem collisionParticle;
    [SerializeField] private float _shotSoundRadius = 10;
    [Range(0f, 1f)][SerializeField] private float _chanсeToProvokeByShootSound = 0.5f;
    [SerializeField] private Ammo _ammoSlot;
    
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (_ammoSlot.GetCurrentAmmo() != 0)
            {
                Shoot();
                MuzzleFlesh();
                ProvokingEnemiesByLoud();
                _ammoSlot.ReduceCurrentAmmo();
            }
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
                targetAI.Provoking();
            }
        }
    }

    private void ProvokingEnemiesByLoud()
    {
        Collider[] objectsInProvokeRadius = Physics.OverlapSphere(transform.position, _shotSoundRadius);
        foreach (var maybeEnemy in objectsInProvokeRadius)
        {
            if (maybeEnemy.GetComponent<EnemyAI>())
            {
                if (Random.Range(0f, 1f) <= _chanсeToProvokeByShootSound)
                {
                    maybeEnemy.GetComponent<EnemyAI>()._isProvoke = true;
                }
            }
        }
    }

    void CollisionParticle(RaycastHit hit)
    {
        var particle = Instantiate(collisionParticle, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(particle.gameObject, 1f);
    }
}

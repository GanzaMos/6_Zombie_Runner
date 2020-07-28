using System;
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
    [SerializeField] private float _timeBetweenShoots = 0.5f;

    private bool _readyToShoot = true;

    public enum FireType {Single, Auto};
    public FireType _thisWeaponFireType = FireType.Single;

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && _thisWeaponFireType == FireType.Single || 
            Input.GetButton("Fire1") && _thisWeaponFireType == FireType.Auto)
        {
            if (_ammoSlot.GetCurrentAmmo() != 0 && _readyToShoot)
            {
                StartCoroutine(Shoot());
            }
        }
    }

    private void MuzzleFlesh()
    {
        muzzleFlesh.Play();
    }

    IEnumerator Shoot()
    {
        _readyToShoot = false;
        MuzzleFlesh();
        ProvokingEnemiesByLoud();
        _ammoSlot.ReduceCurrentAmmo();
        HitOnTarget();
        yield return new WaitForSeconds(_timeBetweenShoots);
        _readyToShoot = true;
    }

    private void HitOnTarget()
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

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.VFX;
using Random = UnityEngine.Random;

public class WeaponScript : MonoBehaviour
{
    [Header("Prefabs Settings")] 
    [SerializeField] Camera FPCamera;
    [SerializeField] private ParticleSystem muzzleFlesh;
    [SerializeField] private ParticleSystem collisionParticle;
    
    [Header("Basic Shoot Settings")] 
    [SerializeField] float _range;
    [SerializeField] float _powerOfHit = 1;
    [SerializeField] private float _timeBetweenShoots = 0.5f;
    [SerializeField] private int _bulletsPerHitAmount = 1;

    public enum FireType {Single, Auto};
    public FireType _weaponFireType = FireType.Single;
    
    [Header("Provoke Settings")] 
    [SerializeField] private float _shotSoundRadius = 10;
    [Range(0f, 1f)][SerializeField] private float _chanсeToProvokeByShootSound = 0.5f;
    
    [Header("Ammo Settings")] 
    [SerializeField] private Ammo _ammoSlot;
    
    [Header("Recoil Settings")] 
    [Range(0f, 0.5f)][SerializeField] private float _minBulletSpread = 0f;
    [Range(0f, 0.5f)][SerializeField] private float _maxBulletSpread = 0f;
    [Range(0f, 0.5f)][SerializeField] private float _bulletSpreadPerShoot = 0f;
    [Range(0f, 1f)][SerializeField] private float _SpreadDecreasingPerSecond = 0f;

    private float _currentBulletSpread;
    private bool _readyToShoot = true;
    private ReticlePanel reticlePanel;

    
    private void Start()
    {
        _currentBulletSpread = _minBulletSpread;
        reticlePanel = FindObjectOfType<ReticlePanel>();
    }

    void Update()
    {
        CheckTermsToShoot();
        RecoilDecrease();
        reticlePanel._sizeModifier = _currentBulletSpread;
    }

    private void CheckTermsToShoot()
    {
        if (Input.GetButtonDown("Fire1") && _weaponFireType == FireType.Single ||
            Input.GetButton("Fire1") && _weaponFireType == FireType.Auto)
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
        HitOnTarget();
        _ammoSlot.ReduceCurrentAmmo();
        RecoilIncrease();
        yield return new WaitForSeconds(_timeBetweenShoots);
        _readyToShoot = true;
    }

    void RecoilIncrease()
    {
        _currentBulletSpread = Mathf.Clamp(
            _currentBulletSpread + _bulletSpreadPerShoot,
            _minBulletSpread,
            _maxBulletSpread);
    }

    void RecoilDecrease()
    {
        _currentBulletSpread = Mathf.Clamp(
            _currentBulletSpread = _currentBulletSpread - _SpreadDecreasingPerSecond * Time.deltaTime,
            _minBulletSpread,
            _maxBulletSpread);
    }

    private void HitOnTarget()
    {
        for (int i = 0; i < _bulletsPerHitAmount; i++)
        {
            RaycastHit hit;
            if (Physics.Raycast(
                FPCamera.transform.position, 
                FPCamera.transform.forward + new Vector3(
                    Random.Range(-_currentBulletSpread, _currentBulletSpread),
                    Random.Range(-_currentBulletSpread, _currentBulletSpread),
                    0f),
                out hit, _range))
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

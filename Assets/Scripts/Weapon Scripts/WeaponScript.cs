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
    [SerializeField] public WeaponName weaponName;
    
    [Header("Basic Shoot Settings")] 
    [SerializeField] float _range;
    [SerializeField] float _powerOfHit = 1;
    [SerializeField] private float _timeBetweenShoots = 0.5f;
    [SerializeField] private int _bulletsPerHitAmount = 1;

    [SerializeField] AmmoType ammoType;

    public enum FireType {Single, Auto};
    public FireType _weaponFireType = FireType.Single;
    
    [Header("Provoke Settings")] 
    [SerializeField] private float _shotSoundRadius = 10;
    [Range(0f, 1f)][SerializeField] private float _chanсeToProvokeByShootSound = 0.5f;
    
    [Header("Ammo Settings")] 
    [SerializeField] private Ammo _ammoSlot;
    
    [Header("Recoil Settings")] 
    [Range(0f, 0.5f)][SerializeField] public float _minBulletSpread = 0f;
    [Range(0f, 0.5f)][SerializeField] public float _maxBulletSpread = 0f;
    [Range(0f, 0.5f)][SerializeField] private float _bulletSpreadPerShoot = 0f;
    [Range(0f, 1f)][SerializeField] private float _spreadDecreasingPerSecond = 0f;
    [Range(0f, 0.5f)][SerializeField] private float _bulletSpreadZoomFactor = 0f;

    [Header("In Game Settings")]
    [SerializeField] public bool _available = true;

    private float _currentBulletSpread;
    private float _bulletMovingSpread;
    private float _bulletFireSpread;
    private bool _readyToShoot = true;
    private ReticlePanel reticlePanel;

    
    private void Start()
    {
        _currentBulletSpread = _minBulletSpread;
        _bulletSpreadZoomFactor = 1f;
        reticlePanel = FindObjectOfType<ReticlePanel>();
    }

    private void OnEnable()
    {
        _readyToShoot = true;
    }

    void Update()
    {
        CheckTermsToShoot();
        Recoil();
    }

    //INPUT FUNCTIONS
    
    private void CheckTermsToShoot()
    {
        if (Input.GetButtonDown("Fire1") && _weaponFireType == FireType.Single ||
            Input.GetButton("Fire1") && _weaponFireType == FireType.Auto)
        {
            if (_ammoSlot.GetCurrentAmmo(ammoType) != 0 && _readyToShoot)
            {
                StartCoroutine(Shoot());
            }
        }
    }
    
    IEnumerator Shoot()
    {
        _readyToShoot = false;
        MuzzleFlesh();
        ProvokingEnemiesByLoud();
        HitOnTarget();
        _ammoSlot.ReduceCurrentAmmo(ammoType);
        RecoilIncreaseByShoot();
        yield return new WaitForSeconds(_timeBetweenShoots);
        _readyToShoot = true;
    }
    
    //RECOIL FUNCTIONS
    
    private void Recoil()
    {
        _currentBulletSpread = Mathf.Clamp(
            _currentBulletSpread - _spreadDecreasingPerSecond * Time.deltaTime,
            (_bulletMovingSpread + _minBulletSpread) * _bulletSpreadZoomFactor,
            (_bulletMovingSpread + _maxBulletSpread) * _bulletSpreadZoomFactor);
        
       
        reticlePanel._sizeModifier = _currentBulletSpread;
    }
    
    void RecoilIncreaseByShoot()
    {
        _currentBulletSpread += _bulletSpreadPerShoot;
    }

    public void BulletSpreadMovingFactor(float _factor)
    {
        _bulletMovingSpread = _factor * _bulletSpreadZoomFactor;
    }

    public void SetUpBulletSpreadZoomFactor(float _zoomFactor)
    {
        _bulletSpreadZoomFactor = _zoomFactor;
    }
    
    //VISUAL EFFECT FUNCTIONS
    
    private void MuzzleFlesh()
    {
        muzzleFlesh.Play();
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
    
    void CollisionParticle(RaycastHit hit)
    {
        var particle = Instantiate(collisionParticle, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(particle.gameObject, 1f);
    }

    //PROVOKING FUNCTIONS
    
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


    //GIZMOS
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _shotSoundRadius);
        Gizmos.DrawWireSphere(transform.position, _range);
    }
}

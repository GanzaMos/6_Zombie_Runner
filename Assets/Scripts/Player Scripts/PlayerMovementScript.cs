using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerMovementScript : MonoBehaviour
{
    private Rigidbody rigidbody;
    private RigidbodyFirstPersonController controller;
    private WeaponScript weaponScript;
    
    public float _maxBulletSpreadWhenMoving = 0.2f;

    private float _BulletSpreadFactor;
    private float _maxWalkSpeed;
    private float _maxRunSpeed;
    
    void Start()
    {
        controller = GetComponent<RigidbodyFirstPersonController>();
        _maxWalkSpeed = controller.movementSettings.ForwardSpeed;
        _maxRunSpeed = _maxWalkSpeed * controller.movementSettings.RunMultiplier;

        rigidbody = GetComponent<Rigidbody>();
        
        weaponScript = GetComponentInChildren<WeaponScript>();
        CalculateSpreadAmplitudeForCurrentWeapon();
    }

    public void CalculateSpreadAmplitudeForCurrentWeapon()
    {
        //todo different Spread when moving for different weapons
    }
    
    void Update()
    {
        float _currentSpeed = rigidbody.velocity.magnitude;
        _BulletSpreadFactor = _maxBulletSpreadWhenMoving * (_currentSpeed / _maxRunSpeed);
        weaponScript.BulletSpreadMovingFactor(_BulletSpreadFactor);
    }
}

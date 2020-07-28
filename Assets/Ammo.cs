using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Ammo : MonoBehaviour
{
    [SerializeField] private int _ammoAmount = 30;

    public int GetCurrentAmmo()
    {
        return _ammoAmount;
    }

    public void ReduceCurrentAmmo()
    {
        _ammoAmount--;
    }
}

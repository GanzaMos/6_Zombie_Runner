using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class DeathHandler : MonoBehaviour


{
    [SerializeField] Canvas DeathCanvas;

    void Start()
    {
        DeathCanvas.enabled = false;
    }

    public void StartDeath()
    {
        DeathCanvas.enabled = true;
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

}

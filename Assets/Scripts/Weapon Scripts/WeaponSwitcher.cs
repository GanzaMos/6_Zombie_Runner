using UnityEngine;
using UnityEngine.PlayerLoop;

public class WeaponSwitcher : MonoBehaviour
{
    [SerializeField] public int currentWeapon = 0;

    private WeaponScript[] weapons;
    
    void Start()
    {
        weapons = GetComponentsInChildren<WeaponScript>(includeInactive: true);
        SetWeaponActive();
    }
    
    public void WeaponPickUpHandler(int weaponNumber)
    {
        if (weapons[weaponNumber]._available == false)
        {
            weapons[weaponNumber]._available = true;
            currentWeapon = weaponNumber;
            SetWeaponActive();
        }
    }
    
    void Update()
    {
        int previousWeapon = currentWeapon;
        
        ProcessKeyInput();
        ProcessScrollWheel();

        if (previousWeapon != currentWeapon)
        {
            SetWeaponActive();
        }
    }



    void ProcessKeyInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && weapons[0]._available) { currentWeapon = 0;}
        if (Input.GetKeyDown(KeyCode.Alpha2) && weapons[1]._available) { currentWeapon = 1;}
        if (Input.GetKeyDown(KeyCode.Alpha3) && weapons[2]._available) { currentWeapon = 2;}
        if (Input.GetKeyDown(KeyCode.Alpha4) && weapons[3]._available) { currentWeapon = 3;}
    }

    void ProcessScrollWheel()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            int i = currentWeapon + 1;

            for (int j = 0; j < weapons.Length; j++)
            {
                if (i > weapons.Length - 1)
                {
                    currentWeapon = 0;
                    break;
                }
                else
                {
                    if (weapons[i]._available)
                    {
                        currentWeapon = i;
                        break;
                    }
                }

                i++;
            }
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            int i = currentWeapon - 1;

            for (int j = 0; j < weapons.Length; j++)
            {
                if (i < 0)
                {
                    i = weapons.Length - 1;
                    if (weapons[i]._available)
                    {
                        currentWeapon = i;
                        break;
                    }
                }
                else
                {
                    if (weapons[i]._available)
                    {
                        currentWeapon = i;
                        break;
                    }
                }
                i--;
            }
        }
    }

    void SetWeaponActive()
    {
        int i = 0;
        foreach (WeaponScript weapon in weapons)
        {
            if (weapons[i] == weapons[currentWeapon])
            {
                weapon.gameObject.SetActive(true); 
            }
            else
            {
                weapon.gameObject.SetActive(false); 
            }
            i++;
        }
    }
    
}

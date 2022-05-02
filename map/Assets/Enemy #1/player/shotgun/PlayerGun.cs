using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    [SerializeField] private Gunobject[] guns;

    private Gunobject currentGun;
  
    void Start()
    {
        foreach (Gunobject gun in guns)
        {
            gun.gameObject.SetActive(false);
        }
        currentGun = guns[0];
        currentGun.gameObject.SetActive(true);
    }

    
    void Update()
    {
        for (int i=1; i<= guns.Length; i ++)
        {
            if (Input.GetKeyDown(i.ToString()))
            {
                foreach (Gunobject gun in guns)
                {
                    gun.gameObject.SetActive(false);
                }
                currentGun = guns[i - 1];
                currentGun.gameObject.SetActive(true);
            }
        }

        currentGun.setClickHold(Input.GetMouseButton(0));
        if (Input.GetKeyDown("r"))
        {
            currentGun.Reload();
        }

    }
}

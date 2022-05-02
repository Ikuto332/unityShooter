using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerstate : MonoBehaviour
{

    public GameObject Gunslecttion;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Gunslecttion.SetActive(true);
        }
    }
}

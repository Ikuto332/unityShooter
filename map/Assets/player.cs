using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public GameObject Gunslecttion;
    public GameObject Gunslecttion2;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Gunslecttion.SetActive(true);
            Gunslecttion2.SetActive(false);
        }
    }
}

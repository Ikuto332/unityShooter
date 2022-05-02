using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUnslection : MonoBehaviour
{

    public GameObject findGUN;
    public GameObject findGUN2;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Reload"))
        {
            findGUN = GameObject.Find("/Shotgun");
            findGUN.SetActive(true);
        }
       
    }
}

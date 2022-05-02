using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gunHndgun : MonoBehaviour
{
    Animator ani;

    public Text AmmoUI;

    public float cooldownSpeed;

    public float fireRate;

    public float recoilCooddown;

    public float accuracy;

    public float maxSpreadAngle;

    public float timeTillMaxSpread;

    public GameObject bullet;

    public GameObject shootPoint;

    public AudioSource gunshot;

    public AudioClip depoly;
    public AudioClip reload1;
    public AudioClip reload2;

    public AudioSource gunreload;

    public AudioClip singleShot;

    public GameObject flashGUN;

    public int ammo = 10;
    public int ammo_supply = 30;

    public float timer1 = 3f;
    public float timer2 = 2.2f;

    public int reloadstarter = 0;
    public int notfullAmmo;


    public int reloadingA = 0;
    public int reloadingB = 0;

    public float RR = 0.05f;
    public int ammoBB = 0;
    public int ammocate = 0;
    public int ammocate2 = 0;
    public bool ammoplus;
    public int busy = 0;

    public int soundFIX = 0;

    // Start is called before the first frame update
    void Start()
    {
        gunreload.PlayOneShot(depoly);

        AmmoUI.text = ammo + "/" + ammo_supply;
        ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
    if(accuracy < 30)
        {
            accuracy += Time.deltaTime;
        }




        if (Input.GetButtonDown("Reload") && ammo <= 0 && ammo_supply !=0)
        {
            soundFIX = 1;
            reloadstarter = 1;
            ani.SetBool("reload", true);
            ani.SetBool("busy", true);
        }

        if (soundFIX == 1 && timer1 >= 3f && ammo !=10 && reloadstarter ==1 ) 
        {
            gunreload.PlayOneShot(reload1);        
            soundFIX = 0;
          
        }

        if (reloadstarter == 1)
        {
           
            timer1 -= Time.deltaTime;

            if (timer1 <= 0)
            {
                if (ammo_supply <= 9)
                {



                    timer1 = 3f;
                    notfullAmmo += ammo_supply;
                    ammo = notfullAmmo;
                    ammo_supply -= notfullAmmo;
                    reloadstarter = 0;
                    ani.SetBool("reload", false);
                    ani.SetBool("busy", false);
                    AmmoUI.text = ammo + "/" + ammo_supply;
         
                }
                if (ammo_supply >= 10)
                {
                    reloadstarter = 0;
                    ammo = 10;
                    ammo_supply -= 10;
                    timer1 = 3f;
                   
                    ani.SetBool("reload", false);
                    ani.SetBool("busy", false);
                    AmmoUI.text = ammo + "/" + ammo_supply;

           
                }

            }
        }







        if (Input.GetButton("Reload") && ammo !=0 && ammo_supply >=1  && busy ==0 && ammo!=10) 
        {            
            reloadingA = 1;
            reloadingB = 1;
          
            ani.SetBool("reload2", true);
            ani.SetBool("busy", true);
        }
        if (reloadingA == 1)
        {
            gunreload.PlayOneShot(reload2);
            ammocate = ammo;
            ammocate2 = ammo_supply;
            ammoplus = true;
            reloadingA = 0;
        }

        if (reloadingB == 1)
        {
            busy = 1;
            timer2 -= Time.deltaTime;
        }
        if (ammoplus == true)
        {
            RR -= Time.deltaTime;       
        }
     
        if (RR <= 0)
        {
            ammocate += 1;
            ammocate2 -= 1;
            ammoBB += 1;
            RR = 0.05f;

            if (ammocate >= 10 || ammocate2 <= 0)
            {
                ammoplus = false;
                ammo += ammoBB;
                ammo_supply -= ammoBB;
                ammoBB = 0;
                ammocate = 0;
                ammocate2 = 0;
                ammoplus = false;
                
                ani.SetBool("reload2", false);
                ani.SetBool("busy", false);
               
            }
        }
        if (timer2 <= 0)
        {

            reloadingB = 0;
            timer2 = 2.2f;
            busy = 0;
            AmmoUI.text = ammo + "/" + ammo_supply;
        }



        //-------------------------------------------------------------







        if (Input.GetButton("zoom"))
        {
            ani.SetBool("zoom_para", true);
            accuracy = 0;
        }
        else
        {
            ani.SetBool("zoom_para", false);
            accuracy = 50;
        }

       cooldownSpeed += Time.deltaTime *60;
    


        if (busy == 0 && Input.GetButtonDown("Fire1") && cooldownSpeed >= fireRate && ammo >= 1 && busy == 0) 
        {
           
            float randomNumber = Random.Range(0, 3);
            
            flashGUN = GameObject.Find("/Player/handgun/FpsArms/Reference/FpsArms.2/Group004/Box008/Bout/B_Out(Handgun)/flashGUN");
            flashGUN.SetActive(true);
            accuracy -= Time.deltaTime * 5f;

            if(randomNumber == 0)
            {
                ani.SetBool("SHOOT", true);
            }
            if (randomNumber == 1)
            {
                ani.SetBool("SHOOT2", true);
            }
            if (randomNumber == 2)
            {
                ani.SetBool("SHOOT3", true);
            }


            if(ammo <= 0)
            {
                ani.SetTrigger("emty");
            }






            if (cooldownSpeed >= fireRate)
            {
                Shoot();
                ammo -= 1;
                AmmoUI.text = ammo + "/" + ammo_supply;

                gunshot.PlayOneShot(singleShot);

                cooldownSpeed = 1;
               recoilCooddown = 2;
            }
        }
        else
        {
            flashGUN = GameObject.Find("/Player/handgun/FpsArms/Reference/FpsArms.2/Group004/Box008/Bout/B_Out(Handgun)/flashGUN");
            flashGUN.SetActive(false);
            ani.SetBool("SHOOT", false);
            ani.SetBool("SHOOT2", false);
            ani.SetBool("SHOOT3", false);     
            
          recoilCooddown -= Time.deltaTime;

          if (recoilCooddown <= 1)
           {
               
                accuracy = 50f;
            }


        }

    }
    void Shoot()
    {
        RaycastHit hit;
        Quaternion fireRotation = Quaternion.LookRotation(transform.forward);

        float curretSpread = Mathf.Lerp(0.0f, maxSpreadAngle, accuracy / timeTillMaxSpread);

        fireRotation = Quaternion.RotateTowards(fireRotation, Random.rotation, Random.Range(0.0f, curretSpread));

        if (Physics.Raycast(transform.position, fireRotation * Vector3.forward, out hit, Mathf.Infinity))
        {
            GameObject tempBullet = Instantiate(bullet, shootPoint.transform.position, fireRotation);
            tempBullet.GetComponent<MoveBullet>().hitPoint = hit.point;

        }
    }
}
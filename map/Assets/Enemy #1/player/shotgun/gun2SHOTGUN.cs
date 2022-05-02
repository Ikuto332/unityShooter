using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class gun2SHOTGUN : MonoBehaviour
{
    Animator ani;

    public float cooldownSpeed;

    public float fireRate;

    public float recoilCooddown;

    private float accuracy;

    public float maxSpreadAngle;

    public float timeTillMaxSpread;

    

    public GameObject bullet;
    public GameObject shootPoint;
    public AudioSource gunshot;
    public AudioClip singleShot;
    public GameObject flashGUN;


    public int ammo = 10;
    public int ammoSupply = 20;

    public float timer = 0.8f; //ກຽມຢັດ
    public float timer2 = 0.44f;//ຄວາມໄວໃນການຍັດ
    public float timer3 = 1f;//ການລໍຖ້າກັບ ຄືນ standing

    public bool ReloadStarter;
    public int ReloadBoolean = 0;
    public bool reloadCancal;
    public int busy = 0;
   

    // Start is called before the first frame update
    void Start()
    {

        ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (accuracy < 50)
        {
            accuracy += Time.deltaTime;
        }
















        //--
        //--------------------------------------------------









        //---------------------------------------------------------
        if (Input.GetButton("Reload") && ammo <= 9 && ammoSupply >=1)
        {
         
            ReloadBoolean = 1;
            ReloadStarter = true;
            busy = 1;
        }
       
        if (ReloadStarter == true)
        {
      
            ani.SetBool("reload", true);          
            timer -= Time.deltaTime;         
        }
      
            
            if (timer <= 0)
        {        
            ReloadStarter = false;          
            timer = 0.8f;                     
        }
        //--------------------------------------------------------------------
        if (ReloadBoolean == 1)
        {
            timer2 -= Time.deltaTime;
        }

        if (timer2 <=0 && ammoSupply >= 1)
        {
            ammo += 1;
            ammoSupply -= 1;
            timer2 = 0.44f;
        }

        if (ammoSupply <= 0 || ammo >= 10)
        {
            ReloadBoolean = 0;          
            ani.SetBool("reload", false);

            if(ReloadBoolean == 0)
            {
                timer3 -= Time.deltaTime;
            }
        }
        if (timer3 <= 0)
        {           
            busy = 0;
            timer3 = 1.2f;
        }
        //---------------------------------------------
        if (reloadCancal == true)
        {
            timer3 = 1.2f;
            timer3 -= Time.deltaTime;
            ani.SetBool("reload", false);
            
        }







        if (Input.GetButton("zoom"))
        {
            ani.SetBool("zoom_para", true);
        }
        else
        {
            ani.SetBool("zoom_para", false);
        }

        cooldownSpeed += Time.deltaTime * 60f;


        if (Input.GetButtonDown("Fire1")){
            reloadCancal = true;
        }




        if (busy == 0 && Input.GetButtonDown("Fire1") && cooldownSpeed >= fireRate && ammo >= 1 && busy == 0)
                {
            accuracy -= Time.deltaTime * 5f;
            reloadCancal = true;
                    ani.SetBool("reloadcancal", true);           
                ammo -= 1;
                flashGUN = GameObject.Find("/Player/Shotgun/FpsArms.2/RightShoulder/RightArm/RightForeArm_/RightForearmRoll/RightHand/gun 2/Box007/B_out(SHOTGUN)/flashGUN");
                flashGUN.SetActive(true);
                ani.SetBool("SHOOT", true);

                accuracy += Time.deltaTime * 1f;

                if (cooldownSpeed >= fireRate)
                {
                    Shoot();
                
                gunshot.PlayOneShot(singleShot);
                    cooldownSpeed = 0;
                    recoilCooddown = 1;
                }
            }
            else
            {
                reloadCancal = false;
                ani.SetBool("reloadcancal", false);
                flashGUN = GameObject.Find("/Player/Shotgun/FpsArms.2/RightShoulder/RightArm/RightForeArm_/RightForearmRoll/RightHand/gun 2/Box007/B_out(SHOTGUN)/flashGUN");
                flashGUN.SetActive(false);
                recoilCooddown -= Time.deltaTime;
                ani.SetBool("SHOOT", false);
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
        Quaternion fireRotation2 = Quaternion.LookRotation(transform.forward);

        float curretSpread = Mathf.Lerp(0.0f, maxSpreadAngle, accuracy / timeTillMaxSpread);
   


        fireRotation = Quaternion.RotateTowards(fireRotation, Random.rotation, Random.Range(0.0f, curretSpread));

        if (Physics.Raycast(transform.position, fireRotation * Vector3.forward, out hit, Mathf.Infinity))
        {
            GameObject tempBullet = Instantiate(bullet, shootPoint.transform.position, fireRotation);
            tempBullet.GetComponent<MoveBullet>().hitPoint = hit.point;

        }
    }

  




}
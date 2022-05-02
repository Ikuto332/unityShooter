using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gunobject : MonoBehaviour
{
   
    public enum FireType
    {
        semi,
        auto
    }

    [SerializeField] private GameObject firepoint;
    [SerializeField] private GameObject bulletOBJ;
    [SerializeField] private FireType fireType;
    [SerializeField] private float fireRange;
    [SerializeField] private int fireCount;
    [SerializeField] private float fireCool;
    [SerializeField] private int ammoMax;
    [SerializeField] private int reloadcount;
    [SerializeField] private float reloadtime;
    [SerializeField] private float spreadRate;


    [SerializeField] private AudioClip firesound;
    [SerializeField] private AudioClip emptysound;
    [SerializeField] private AudioClip reloadsound;

    private int ammonow;
    private float firecoolnow = 0;

    private AudioSource soundplayer;

    private bool isreload = false;
    private bool reloading = false;
    private bool isclickHold = false;
    private bool isSemifire = false;








    void Start()
    {
        ammonow = ammoMax;
        soundplayer = GetComponent<AudioSource>();
    }


    void Update()
    {
        if (isclickHold)
        {
            if(!isreload && !reloading && firecoolnow <= 0 && ((fireType == FireType.semi && !isSemifire) || fireType != FireType.semi))
            {
                if(fireType == FireType.semi)
                {
                    isSemifire = true;
                }
                if (ammonow > 0)
                {
                    soundplayer.clip = firesound;
                    soundplayer.Play();
                    ammonow -= 1;
                    firecoolnow = fireCool;
                    if (bulletOBJ)
                    {
                        for( int i=0;i<fireCount; i++)
                        {
                            GameObject proj = Instantiate(bulletOBJ, firepoint.transform.position, firepoint.transform.rotation);
                            float randomX = Random.Range(-spreadRate, spreadRate);
                            float randomY = Random.Range(-spreadRate, spreadRate);
                            float randomAngleX = proj.transform.rotation.x * randomX;
                            float randomAngleY = proj.transform.rotation.x * randomY;
                            proj.transform.localEulerAngles = new Vector3(proj.transform.localEulerAngles.x + (randomAngleX), proj.transform.localEulerAngles.y + (randomAngleY), proj.transform.localEulerAngles.z);
                        }
                        
                    }
                    else
                    {
                        for (int i = 0; i < fireCount; i++)
                        {
                            RaycastHit hit;
                            float randomX = Random.Range(-spreadRate, spreadRate);
                            float randomY = Random.Range(-spreadRate, spreadRate);
                            Ray ray = new Ray(firepoint.transform.position, firepoint.transform.forward + (firepoint.transform.right * randomX) + (firepoint.transform.up * randomY));
                            if (Physics.Raycast(ray, out hit, fireRange)) ;
                        }
                    }
                }
            }
            else
            {
                if (fireType == FireType.semi)
                {
                    isSemifire = true;
                }
                soundplayer.clip = emptysound;
                soundplayer.Play();
            }
        }
      
        else
        {
            if(reloading && reloadcount < ammoMax)
            {
                isreload = false;
            }
        }
        firecoolnow -= Time.deltaTime;


    }


    public void setClickHold(bool click)
    {
        isclickHold = click;
        if (isclickHold == false)
        {
            isSemifire = false;
        }
    }
    public void Reload()
    {
        if (!isreload && ammonow < ammoMax)
        {
            StartCoroutine(Reloading());
        }
    }
    private IEnumerator Reloading()
    {
        isreload = true;
        reloading = true;
        while (isreload)
        {
            soundplayer.clip = reloadsound;
            soundplayer.Play();
            yield return new WaitForSeconds(reloadtime);
            ammonow += reloadcount;
            if(ammonow == ammoMax)
            {
                isreload = false;
            }
            reloading = false;
        }
    }
}

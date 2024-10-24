using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleFlash : MonoBehaviour
{
    
    [SerializeField] GameObject gun;
    [SerializeField] int ammoLF;
    private Light muzzle;
    // Start is called before the first frame update
    void Start()
    {
        muzzle = GetComponent<Light>();
        gew98anim gew98anim = gun.GetComponent<gew98anim>();
        ammoLF=gew98anim.ammo;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        gew98anim gew98anim = gun.GetComponent<gew98anim>();
        if(ammoLF-gew98anim.ammo==1)
        {
            muzzle.intensity=10f;
        }
        else if(muzzle.intensity>0)
        {
            muzzle.intensity-=1f;
        }
        ammoLF=gew98anim.ammo;
    }
    
}

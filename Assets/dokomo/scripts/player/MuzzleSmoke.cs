using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleSmoke : MonoBehaviour
{
    
    [SerializeField] GameObject gun;
    [SerializeField] int ammoLF;
    [SerializeField] GameObject muzzleSmokePrefab;
    // Start is called before the first frame update
    void Start()
    {
        gew98anim gew98anim = gun.GetComponent<gew98anim>();
        ammoLF=gew98anim.ammo;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        gew98anim gew98anim = gun.GetComponent<gew98anim>();
        if(ammoLF-gew98anim.ammo==1)
        {
            Instantiate(muzzleSmokePrefab, transform.position, transform.rotation);
            Destroy(muzzleSmokePrefab, 1f);
        }
        ammoLF=gew98anim.ammo;
    }
    
}

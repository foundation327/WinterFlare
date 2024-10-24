using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grenadeanim : MonoBehaviour
{
    private Animator anim;
    public bool animating;
    public bool secondaryactive;
    [SerializeField] GameObject primary;
    [SerializeField] GameObject player;
    public int state;
    void Start()
    {
        anim = GetComponent<Animator>();
        animating=false;
        secondaryactive=false;
        state=0;
    }

    void Update()
    {
        Character character = player.GetComponent<Character>();
        gew98anim gew98anim = primary.GetComponent<gew98anim>();
        #region draw
        if(gew98anim.PrimaryActive==0&gew98anim.animating==0&animating==false&secondaryactive==false&state==0)
        {
            animating=true;
            anim.SetTrigger("draw");
            secondaryactive=true;
            Invoke("notanimating",36f/30f);
        }
        #endregion

        #region vaultover
        if(character.isVaultingOverATallWall==1&animating==false&secondaryactive==true&state==0)
        {
            animating=true;
            anim.SetTrigger("vaultover");
            Invoke("notanimating",34f/30f);
        }
        #endregion

        #region throw
        if(Input.GetMouseButtonDown(0)&animating==false&secondaryactive==true&state==0)
        {
            animating=true;
            anim.SetTrigger("throw");
            Invoke("stateactivator",53f/30f);
            Invoke("notanimating",53f/30f);
        }
        if(animating==false&secondaryactive==true&state==1)
        {
            animating=true;
            anim.SetTrigger("throw2");
            Invoke("notanimating",19f/30f);
            Invoke("secondarydeactivate",19f/30f);
            Invoke("reload",3f);
        }
        #endregion

        #region holster
        if(Input.GetKeyDown(KeyCode.Alpha1)&gew98anim.PrimaryActive==0&animating==false&secondaryactive==true&state==0)
        {
            animating=true;
            anim.SetTrigger("holster");
            Invoke("notanimating",36f/30f);
            Invoke("secondarydeactivate",36f/30f);
        }
        #endregion

    }
    void notanimating()
    {
        animating=false;
    }
    void stateactivator()
    {
        state=1;
    }
    void secondarydeactivate()
    {
        secondaryactive=false;
    }
    void reload()
    {
        state=0;
    }
}

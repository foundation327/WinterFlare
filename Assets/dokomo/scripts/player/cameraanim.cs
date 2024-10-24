using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraanim : MonoBehaviour
{
    private Animator anim;
    private float drawrate=0.62f;//取り出し時間
    private float holsterrate=1.3f;//しまう時間
    private float firerate=39f/30f;//撃ってからもう一回撃てるまでの時間
    private float reloadrate=3.067f;//リロード時間
    private int PrimaryActive=0;//武器を使っている間1
    private int ammo=5;//残弾数
    private int animating=0;//アニメーション再生中に1
    private int zooming=0;//ズームインズームアウト遷移中に1
    private int bashing;
    [SerializeField] Vector3 reset;
    [SerializeField] Vector3 slide;
    [SerializeField] float speed;//ズームインズームアウト速度
    [SerializeField] bool running;
    [SerializeField] GameObject player;
    [SerializeField] GameObject proceduralanim;
    AudioSource audioSource;

    void Start()
    {
        anim = GetComponent<Animator>();
        PrimaryActive = 1;
        audioSource = GetComponent<AudioSource>();
        bashing=0;
    }
    void FixedUpdate()
    {
        Character character = player.GetComponent<Character>();
        Mover mover = proceduralanim.GetComponent<Mover>();

        //取り出すアニメーション
        if(Input.GetKeyDown(KeyCode.Alpha1)&animating==0&PrimaryActive==0&zooming==0&&character.isRunning==false){
            Invoke("draw",36f/30f);
        }
        //しまうアニメーション
        if(Input.GetKeyDown(KeyCode.Alpha2)&animating==0&PrimaryActive==1&zooming==0&&character.isRunning==false){
            anim.SetTrigger("holster");
            animating=1;
            Invoke("holster",holsterrate);
        }
        //撃つアニメーション
        if(Input.GetMouseButtonDown(0)&animating==0&ammo>0&PrimaryActive==1&&character.isRunning==false&&zooming==0)
        {
            anim.SetTrigger("fire");
            animating=1;
            ammo-=1;
            Invoke("fire",firerate);
        }
        if(Input.GetMouseButtonDown(0)&animating==0&ammo>0&PrimaryActive==1&&character.isRunning==false&&zooming==1)
        {
            anim.SetTrigger("fireADS");
            animating=1;
            ammo-=1;
            Invoke("fire",firerate);
        }

        //リロードアニメーション
        if(Input.GetKeyDown("r")&ammo==0&animating==0&PrimaryActive==1&zooming==0&&character.isRunning==false)
        {
            animating=1;
            anim.SetTrigger("reload");
            Invoke("reload",reloadrate);
        }
        if(Input.GetKeyDown("r")&ammo<5&ammo>0&animating==0&PrimaryActive==1&zooming==0&&character.isRunning==false)
        {
            animating=1;
            anim.SetTrigger("reloadwet");
            Invoke("reload",5f);
        }

        //腰だめ/ads モード切替
        if(Input.GetMouseButton(1)&PrimaryActive==1&&character.isRunning==false)
        {
            zooming=1;
        }
        if((Input.GetMouseButtonUp(1)&PrimaryActive==1)||character.isRunning==true)
        {
            zooming=0;
        }
        
        //adsアニメーション
        if(zooming==1&PrimaryActive==1&&character.isRunning==false)
        {
                transform.localPosition=Vector3.Lerp(transform.localPosition, slide, Time.deltaTime*speed);
        }
        if((zooming==0&PrimaryActive==1)||character.isRunning==true)
        {
            if(animating==0)transform.localPosition=Vector3.Lerp(transform.localPosition, reset, Time.deltaTime*speed);
            if(animating==1)transform.localPosition=Vector3.Lerp(transform.localPosition, reset, Time.deltaTime*speed*0.3f);
        }

        #region run
        if(animating==0&&character.isRunning==true&&mover.moving==1&&running==false&PrimaryActive==1)
        {
            anim.SetTrigger("run");
            animating=1;
            running=true;
        }
        if(running)
        {
            anim.SetTrigger("running");
        }
        if(character.isRunning==false&&running==true&PrimaryActive==1)
        {
            running=false;
            anim.SetTrigger("unrun");
            Invoke("notanimating",0.667f);
        }
        #endregion

        #region vaultover
        if(animating==0&&character.isVaultingOverATallWall==1&PrimaryActive==1)
        {
            animating=1;
            anim.SetTrigger("vaultoverhigh");
            Invoke("notanimating",27f/30f);
        }
        #endregion

        #region bayonet
        if(animating==0&&Input.GetMouseButton(2)&bashing==0&PrimaryActive==1)
        {
            animating=1;
            bashing=1;
            anim.SetTrigger("bash");
        }
        if(Input.GetMouseButton(2)&bashing==1&PrimaryActive==1)
        {
            bashing=2;
            anim.SetTrigger("bash2");
            Invoke("bash",16f/30f);
        }
        #endregion
    }

    //関数
    void draw()
    {
        anim.SetTrigger("draw");
        animating=1;
        Invoke("notanimating",drawrate);
        Invoke("primaryactivate",drawrate);
    }
    void holster()
    {
        PrimaryActive=0;
        animating=0;
    }
    void fire()
    {
        animating=0;
    }
    void reload()
    {
        ammo=5;
        animating=0;
    }
    void notanimating()
    {
        animating=0;
    }
    void primaryactivate()
    {
        PrimaryActive=1;
    }
    void bash()
    {
        animating=0;
        bashing=0;
    }
}
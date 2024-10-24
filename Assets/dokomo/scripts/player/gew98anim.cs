using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gew98anim : MonoBehaviour
{
    private Animator anim;
    private float drawrate=17f/30f;//取り出し時間
    private float holsterrate=17f/30f;//しまう時間
    private float firerate=45f/30f;//撃ってからもう一回撃てるまでの時間
    private float reloadrate=100f/30f;//リロード時間
    public int PrimaryActive=0;//武器を使っている間1
    public int ammo=5;//残弾数
    public int animating=0;//アニメーション再生中に1
    public int zooming=0;//ズームインズームアウト遷移中に1
    public int bashing;
    [SerializeField] Vector3 reset;
    [SerializeField] Vector3 slide;
    [SerializeField] float speed;//ズームインズームアウト速度
    [SerializeField] bool running;
    [SerializeField] GameObject player;
    [SerializeField] GameObject proceduralanim;
    [SerializeField] GameObject secondary;
    [SerializeField] AudioClip sound1;
    [SerializeField] AudioClip sound2;
    //[SerializeField] AudioClip sound3;
    //[SerializeField] AudioClip sound4;
    AudioSource audioSource;

    void Start()
    {
        anim = GetComponent<Animator>();
        PrimaryActive = 1;
        audioSource = GetComponent<AudioSource>();
        bashing=0;
    }
    void Update()
    {
        Character character = player.GetComponent<Character>();
        Mover mover = proceduralanim.GetComponent<Mover>();
        grenadeanim grenadeanim = secondary.GetComponent<grenadeanim>();

        //取り出すアニメーション
        if(grenadeanim.state==1&grenadeanim.secondaryactive==false&grenadeanim.animating==false&animating==0&PrimaryActive==0&zooming==0){
            animating=1;
            Invoke("draw",0);
        }
        if(Input.GetKeyDown(KeyCode.Alpha1)&grenadeanim.animating==false&animating==0&PrimaryActive==0&zooming==0&&character.isRunning==false){
            animating=1;
            Invoke("draw",36f/30f);
        }
        //しまうアニメーション
        if(Input.GetKeyDown(KeyCode.Alpha2)&animating==0&PrimaryActive==1&zooming==0&&character.isRunning==false&&grenadeanim.state==0){
            anim.SetTrigger("holster");
            animating=1;
            Invoke("holster",holsterrate);
        }
        //撃つアニメーション
        if(Input.GetMouseButtonDown(0)&animating==0&ammo>0&PrimaryActive==1&character.isRunning==false)
        {
            anim.SetTrigger("fire");
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

        //腰だめ/ads モード切替
        if(Input.GetMouseButton(1)&PrimaryActive==1&&character.isRunning==false)
        {
            zooming=1;
        }
        if(((!Input.GetMouseButton(1))&PrimaryActive==1)||character.isRunning==true)
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
            Invoke("notanimating",0.3f);
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
        if(animating==0&&Input.GetMouseButton(2)&bashing==0&PrimaryActive==1&zooming==0)
        {
            animating=1;
            bashing=1;
            anim.SetTrigger("bash");
        }
        if((!Input.GetMouseButton(2))&bashing==1&PrimaryActive==1)
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
        PrimaryActive=1;
        anim.SetTrigger("draw");
        Invoke("notanimating",drawrate);
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
    void bash()
    {
        animating=0;
        bashing=0;
    }
}
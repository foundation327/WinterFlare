using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class Character : MonoBehaviour
{
    [SerializeField] GameObject playerCamera;
    [SerializeField] float walk=3f;
    [SerializeField] float run=5f;
    [SerializeField] float gravity=10f;

    [SerializeField] float lookSpeed=2f;
    [SerializeField] float viewlock=90f;

    Vector3 moveDirection=Vector3.zero;
    float positionX=0;
    float rotationX=0;
    float rotationZ=0;
    private Vector3 headposition=Vector3.zero;

    public bool Movable=true;
    [SerializeField] int crouching;

    CharacterController characterController;
    Vector3 center;

    public bool isRunning;
    public int isVaultingOverATallWall;
    [SerializeField] float wallheight=2.25f;
    float ground;
    [SerializeField] GameObject viewmodel;

    void Start()
    {
        characterController=GetComponent<CharacterController>();
        Cursor.lockState=CursorLockMode.Locked;
        Cursor.visible=false;
        crouching=0;
        center=Vector3.zero;
        ground=1f;
        isVaultingOverATallWall=0;
        headposition.y=0.62f;
    }

    void Update()
    {
        gew98anim gew98anim = viewmodel.GetComponent<gew98anim>();
        #region Camera
        if(Input.GetKey("e"))
        {
            if(rotationZ>-10f)rotationZ-=1f;
            if(positionX<0.2f)positionX+=0.02f;
        }
        if(Input.GetKey("q"))
        {
            if(rotationZ<10f)rotationZ+=1f;
            if(positionX>-0.2f)positionX-=0.02f;
        }
        if(!Input.GetKey("q")&!Input.GetKey("e"))
        {
            if(rotationZ>0)rotationZ-=1f;
            if(rotationZ<0)rotationZ+=1f;
            if(positionX>0)positionX-=0.02f;
            if(positionX<0)positionX+=0.02f;
        }
        if(Movable)
        {
            characterController.Move(moveDirection*Time.deltaTime);
            rotationX+=-Input.GetAxis("Mouse Y")*lookSpeed;
            rotationX=Mathf.Clamp(rotationX,-viewlock,viewlock);
            playerCamera.transform.localRotation=Quaternion.Euler(rotationX,0,rotationZ);
            headposition.x=positionX;
            playerCamera.transform.localPosition=headposition;
            transform.rotation*=Quaternion.Euler(0,Input.GetAxis("Mouse X")*lookSpeed,0);
        }
        #endregion

        #region VaultOver
        Vector3 forvault=this.transform.localPosition;
        if(Movable==true&&Input.GetKeyDown(KeyCode.Space)&&gew98anim.animating==0&&isVaultingOverATallWall==0&&characterController.isGrounded)
        {
            Movable=false;
            isVaultingOverATallWall=1;
            Invoke("VaultingOverATallWall",0.1f);
            Invoke("ExhaustedOnATallWall",7f/30f);
            Invoke("VaultingOverATallWall",10f/30f);
            Invoke("VaultedOverATallWall",27f/30f);
            ground=forvault.y;
        }
        #endregion 

        #region standing
        if(Movable==true&&crouching==0&&characterController.height<2f&&center.y>0)
        {
            characterController.height+=0.03f;
            center.y-=0.03f;
            characterController.center = center;
        }
        #endregion
    }
    void FixedUpdate()
    {
        gew98anim gew98anim = viewmodel.GetComponent<gew98anim>();
        #region Movement;
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        isRunning=Input.GetKey(KeyCode.LeftShift)&&crouching==0&&(Input.GetAxis("Vertical")>0);
        float curSpeedX=Movable?(isRunning?run : walk)*Input.GetAxis("Vertical") : 0;
        float curSpeedY=Movable?(isRunning?run : walk)*Input.GetAxis("Horizontal") : 0;
        float movementDirectionY=moveDirection.y;
        moveDirection=(forward*curSpeedX)+(right*curSpeedY);
        #endregion

        #region Jump
        if(Movable==true)
        {
            moveDirection.y=movementDirectionY;
            if(!characterController.isGrounded)
            {
                moveDirection.y-=gravity*Time.deltaTime;
            }
        }
        #endregion

        #region Crouch
        if(Movable==true&&Input.GetKey(KeyCode.LeftControl))
        {
            crouching=1;
        }
        if(Movable==true&&(!Input.GetKey(KeyCode.LeftControl)))
        {
            crouching=0;
        }
        if(Movable==true&&crouching==1&&characterController.height>1.5f&&center.y<1f)
        {
            characterController.height-=0.03f;
            center.y+=0.03f;
            characterController.center = center;
        }
        #endregion

         #region VaultOver
        Vector3 forvault=this.transform.localPosition;
        if(isVaultingOverATallWall==2)
        {
            if(wallheight>this.transform.localPosition.y-ground)
            {
                forvault.y+=0.05f;
                forvault+=this.transform.forward*0.05f;
            }
            else
            {
                forvault.y+=0.02f;
                forvault+=this.transform.forward*0.05f;
            }
            this.transform.localPosition=forvault;
        }
        if(isVaultingOverATallWall==3)
        {
            forvault.y-=0.003f;
            forvault+=this.transform.forward*0.003f;
            this.transform.localPosition=forvault;
        }
        #endregion 
        
    }
    void VaultingOverATallWall()
    {
        isVaultingOverATallWall=2;
    }
    void ExhaustedOnATallWall()
    {
        isVaultingOverATallWall=3;
    }
    void VaultedOverATallWall()
    {
        Movable=true;
        isVaultingOverATallWall=0;
    }
}

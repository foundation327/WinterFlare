using UnityEngine;
using System.Collections;
using System;

public class Mover : MonoBehaviour 
{
  private float Speedx = 3.75f;
  private float valuex = 1f;
  private float Speedy = 15f;
  private float valuey = 3f;
  private float Speedz = 7.5f;
  private float valuez = 2f;
  private float rspeedx = 13f;
  private float rvaluex = 0.1f;
  private float rspeedy = 7.5f;
  private float rvaluey = 0.01f;
  private float rspeedz = 3.5f;
  private float rvaluez = -0.01f;
  
  private Vector3 Directionx = Vector3.zero;
  private Vector3 Directiony = Vector3.zero;
  private Vector3 Directionz = Vector3.zero;
  private Vector3 momentox = Vector3.zero;
  private Vector3 momentoy = Vector3.zero;
  private Vector3 momentoz = Vector3.zero;

  [SerializeField] Transform player;
  [SerializeField] GameObject viewmodel;
  [SerializeField] GameObject Player;
  [SerializeField] float multiplier=0.05f;
  private Vector3 old;
  private Vector3 neo;
  public int moving=0;
  private Vector3 end;
  private Quaternion endq;

  void Start()
  {
    old=player.position;
    Directionx.x=1.0f;
    Directiony.y=1.0f;
    Directionz.z=1.0f;
    momentox.x=1.0f;
    momentoy.y=1.0f;
    momentoz.z=1.0f;
  }
  void FixedUpdate () 
  {
    Character character = Player.GetComponent<Character>();
    gew98anim gew98anim = viewmodel.GetComponent<gew98anim>();
    Transform ThisTransform = GetComponent<Transform>();

    neo=player.position;

    if(gew98anim.zooming==1)
    {
      multiplier=0.001f;
    }
    else
    {
      if(gew98anim.PrimaryActive==1)multiplier=0.05f;
      else  multiplier=0.01f;
    }
    if(Math.Abs(neo.x-old.x)>0.001f||Math.Abs(neo.y-old.y)>0.001f&&gew98anim.zooming==0&&gew98anim.animating==0)
    {
      moving=1;
    }
    else
    {
      moving=0;
    }

    if(moving==1)
    {
      ThisTransform.position += multiplier*Directionx.normalized * valuex * function1(Time.time*Speedx) * Time.deltaTime;
      ThisTransform.position += multiplier*Directiony.normalized * valuey * function1(Time.time*Speedy) * Time.deltaTime;
      ThisTransform.position += multiplier*Directionz.normalized * valuez * function1(Time.time*Speedz) * Time.deltaTime;
      ThisTransform.Rotate(multiplier*rvaluex*function1(Time.time*rspeedx),0,0);
      ThisTransform.Rotate(0,multiplier*rvaluey*function1(Time.time*rspeedy),0);
      ThisTransform.Rotate(0,0,multiplier*rvaluez*function1(Time.time*rspeedz));
    }else{
      end = Vector3.zero;
      endq.Set(0,0,0,1.0f);
      transform.localPosition = Vector3.MoveTowards(transform.localPosition, end, 0.1f*Time.deltaTime);
      transform.localRotation = Quaternion.Slerp(transform.localRotation,endq,0.1f*Time.deltaTime);
    }

    old=player.position;
  }

  private float function1(float t)
  {
    gew98anim gew98anim = viewmodel.GetComponent<gew98anim>();
    Character character = Player.GetComponent<Character>();
    float r=(float)Math.Sin((gew98anim.PrimaryActive==1||!character.isRunning)? t : t);
    return r;
  }
}
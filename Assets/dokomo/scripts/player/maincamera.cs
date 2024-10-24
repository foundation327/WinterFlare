using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class maincamera : MonoBehaviour
{
    private Camera cam;
    [SerializeField] float fov;
    [SerializeField] float off;
    [SerializeField] float on;
    [SerializeField] float zooming;
    [SerializeField] float speed;
    [SerializeField] GameObject viewmodel;
    void Start () {
        cam = GetComponent<Camera>();
        fov=off;
        zooming=0;
    }
 
    void FixedUpdate() {
        gew98anim gew98anim = viewmodel.GetComponent<gew98anim>();

        if(Input.GetMouseButton(1)&&gew98anim.zooming==1)
        {
            zooming=1;
        }
        if(Input.GetMouseButtonUp(1)||gew98anim.zooming==0)
        {
            zooming=0;
        }
        if(zooming==1&fov>on)
        {
            fov-=speed;
        }

        if(zooming==0&fov<off)
        {
            fov+=speed;
        }
        if(fov>off)
        {
            fov=off;
        }
        if(fov<on)
        {
            fov=on;
        }
        cam.fieldOfView = fov;
    }
}

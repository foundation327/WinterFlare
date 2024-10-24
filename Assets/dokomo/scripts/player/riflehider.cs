using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class riflehider : MonoBehaviour
{
    [SerializeField] GameObject viewmodel;
    private SkinnedMeshRenderer skinnedMeshRenderer;
    // Start is called before the first frame update
    void Start()
    {
        skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        gew98anim gew98anim = viewmodel.GetComponent<gew98anim>();
        if(gew98anim.PrimaryActive==1)
        {
            skinnedMeshRenderer.enabled = true;
        }
        if(gew98anim.PrimaryActive==0)
        {
            skinnedMeshRenderer.enabled = false;
        }
    }
}

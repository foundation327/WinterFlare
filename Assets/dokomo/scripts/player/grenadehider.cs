using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grenadehider : MonoBehaviour
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
        grenadeanim grenadeanim = viewmodel.GetComponent<grenadeanim>();
        if(grenadeanim.secondaryactive)
        {
            skinnedMeshRenderer.enabled = true;
        }
        if(!grenadeanim.secondaryactive)
        {
            skinnedMeshRenderer.enabled = false;
        }
    }
}

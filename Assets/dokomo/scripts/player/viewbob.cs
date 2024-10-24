using UnityEngine;

public class viewbob : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] GameObject target2;
    [SerializeField] GameObject Primary;
    private Quaternion CameraAngle; 
    private Vector3 CameraAngleEuler;
    private void FixedUpdate()
    {
        gew98anim gew98anim = Primary.GetComponent<gew98anim>();
        if(gew98anim.PrimaryActive==1)
        {
            primary();
        }
        else
        {
            secondary();
        }
    }
    private void primary()
    {
        CameraAngle=target.transform.localRotation;
        CameraAngleEuler=CameraAngle.eulerAngles;
        CameraAngleEuler.z=0;
        CameraAngle=Quaternion.Euler(CameraAngleEuler);
        this.transform.localRotation=CameraAngle;
    }
    private void secondary()
    {
        CameraAngle=target2.transform.localRotation;
        CameraAngleEuler=CameraAngle.eulerAngles;
        CameraAngleEuler.z=0;
        CameraAngle=Quaternion.Euler(CameraAngleEuler);
        this.transform.localRotation=CameraAngle;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    private Collider objectCollider;
    private Rigidbody rb;

    void Start()
    {
        objectCollider = GetComponent<SphereCollider>();
        objectCollider.isTrigger = true; //Triggerとして扱う
        rb = GetComponent<Rigidbody>();
    }

    void OnTriggerEnter(Collider collision)
    {
        
        if (collision.gameObject.CompareTag("Block")) //タグがBlockのオブジェクトと衝突した場合
        {
            Destroy(this.gameObject); //弾を消す
        }
        

        if (collision.gameObject.CompareTag("Enemy")) //タグがEnemyのオブジェクトと衝突した場合
        {
            Destroy(collision.gameObject); //衝突した相手を消す
            Destroy(this.gameObject); //弾を消す
        }
    }
}

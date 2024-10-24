using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pp : MonoBehaviour
{
    [SerializeField] float bulletSpeed; //弾の速度
    [SerializeField] GameObject bulletPrefab; //弾のPrefabを入れるための変数
    [SerializeField] GameObject gun;
    [SerializeField] int ammoLF;

    void Start()
    {
        grenadeanim grenadeanim = gun.GetComponent<grenadeanim>();
        ammoLF=grenadeanim.state;
    }

    // Update is called once per frame
    void Update()
    {
        grenadeanim grenadeanim = gun.GetComponent<grenadeanim>();
        if (ammoLF==0&&grenadeanim.state==1)
        {
            Invoke("Shot",18f/30f); //Zキー入力で弾を発射
        }
        ammoLF=grenadeanim.state;
    }
    void Shot()
    {
        GameObject newbullet = Instantiate(bulletPrefab, this.transform.position, Quaternion.identity); //弾を生成
        Rigidbody bulletRigidbody = newbullet.GetComponent<Rigidbody>();
        bulletRigidbody.AddForce(this.transform.forward * bulletSpeed); //キャラクターが向いている方向に弾に力を加える
        bulletRigidbody.AddTorque((Vector3.up+Vector3.forward)* Mathf.PI * 1000f);
        Destroy(newbullet, 10); //10秒後に弾を消す  
    }
}

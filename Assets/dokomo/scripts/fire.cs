using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fire : MonoBehaviour
{
    [SerializeField] float bulletSpeed; //弾の速度
    [SerializeField] GameObject bulletPrefab; //弾のPrefabを入れるための変数
    [SerializeField] GameObject gun;
    [SerializeField] int ammoLF;

    void Start()
    {
        gew98anim gew98anim = gun.GetComponent<gew98anim>();
        ammoLF=gew98anim.ammo;
    }

    // Update is called once per frame
    void Update()
    {
        gew98anim gew98anim = gun.GetComponent<gew98anim>();
        if (ammoLF-gew98anim.ammo==1)
        {
            Shot(); //Zキー入力で弾を発射
        }
        ammoLF=gew98anim.ammo;
    }
    void Shot()
    {
        GameObject newbullet = Instantiate(bulletPrefab, this.transform.position, Quaternion.identity); //弾を生成
        Rigidbody bulletRigidbody = newbullet.GetComponent<Rigidbody>();
        bulletRigidbody.AddForce(this.transform.forward * bulletSpeed); //キャラクターが向いている方向に弾に力を加える
        Destroy(newbullet, 10); //10秒後に弾を消す  
    }
}

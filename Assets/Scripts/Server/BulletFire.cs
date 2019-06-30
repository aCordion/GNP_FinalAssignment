using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFire : MonoBehaviour {

    Object pBullet;

    private void Awake()
    {
        pBullet = Resources.Load("Prefabs/Bullet");
    }

    public void Fire()
    {
        Object bullets = Instantiate(pBullet, transform);
        Destroy(bullets, 3.0f);
    }

}

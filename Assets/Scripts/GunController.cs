using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GunController : MonoBehaviour
{
    public GameObject bullet;
    public Transform spawnPoint;
    public float fireSpeed = 40;

    private void FireBullet()
    {
        GameObject spawnBullet = Instantiate(bullet, spawnPoint.position, spawnPoint.rotation);
        spawnBullet.GetComponent<Rigidbody>().velocity = -spawnPoint.right * fireSpeed;
        spawnBullet.GetComponent<BullesDamageBool>().category = "Player";
        Destroy(spawnBullet, 5);
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            FireBullet();
        }
    }
}

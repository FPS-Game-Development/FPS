using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class GunController : MonoBehaviour
{
    public GameObject bullet;
    public GameObject note;
    public GameObject text;

    public Transform spawnPoint;
    public float fireSpeed = 40;

    public float Magazine = 20;
    public float MaxMagazine = 20;

    GunBar gunBar;

    private void Start()
    {
        gunBar = GetComponent<GunBar>();
    }

    private void FireBullet()
    {
        GameObject spawnBullet = Instantiate(bullet, spawnPoint.position, spawnPoint.rotation);
        spawnBullet.GetComponent<Rigidbody>().velocity = -spawnPoint.right * fireSpeed;
        spawnBullet.GetComponent<BullesDamageBool>().category = "Player";
        gunBar.BarDamage();
        Destroy(spawnBullet, 5);
    }
    void Update()
    {
        if(Magazine > 0){
            if (Input.GetMouseButtonDown(0))
            {
                FireBullet();
                Magazine = Magazine -1;
                
            }else if(Input.GetKeyDown(KeyCode.R)){
                Magazine = 0;
                Reloading();

            }
        }else if(Magazine == 0){
            Reloading();
            
        }
        text.GetComponent<TMP_Text>().text = Magazine + "/" + MaxMagazine;
        
    }

    private float ReloadTimer = -1;          //换弹计时器
    public float ReloadInterval = 3;    //换弹间隔

    private void Reloading()
    {
        
        if(ReloadTimer == -1){
            ReloadTimer = Time.time;
            note.SetActive(true);
        }
        
        // Debug.Log("Player Reloading");
        if (ReloadTimer + ReloadInterval <=  Time.time){
            Magazine = 20;
            ReloadTimer = -1;
            note.SetActive(false);
            gunBar.BarLoad();
        }
    }
}

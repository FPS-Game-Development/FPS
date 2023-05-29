using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using static UnityEngine.GraphicsBuffer;

/// <summary>
/// 控制敌人行为
/// </summary>
[RequireComponent(typeof(EnemyAnimations))]
[RequireComponent(typeof(EnemyMove))]
[RequireComponent(typeof(EnemyStatus))]
public class EnemyController : MonoBehaviour
{
    /// <summary>
    /// 定义敌人状态的枚举
    /// </summary>
    public enum State
    {
        Attack,
        WayFinding,
        Reloading
    }

    private int Magazine = 20;

    /// <summary>
    /// 当前状态
    /// </summary>
    public State currentState = State.WayFinding;

    private EnemyMove enemyMove;
    private EnemyAnimations enemyAnimations;
    void Start()
    {
        // target = FindObjectOfType<RigidbodyFirstPersonController>().transform;
        enemyMove = GetComponent<EnemyMove>();
        enemyAnimations = GetComponent<EnemyAnimations>();
        Magazine = 30;
    
    }


    void Update()
    {
        //״̬�л�
        switch (currentState)
        {
            case State.Attack:        //����״̬
                Attack();
                break;
            case State.WayFinding:       //Ѱ·״̬
                WayFinding();
                break;
            case State.Reloading:       //Ѱ·״̬
                Reloading();
                break;

        }
    }

    private void WayFinding()
    {
        AnimePlay(enemyAnimations.runAnimeName);
        if (!enemyMove.WayFinding()) 
        {
            currentState = State.Attack;
        }
    }

    private float ReloadTimer = -1;          //换弹计时器
    public float ReloadInterval = 10;    //换弹间隔

    private void Reloading()
    {
        
        if(ReloadTimer == -1){
            ReloadTimer = Time.time;
        }
        
        AnimePlay(enemyAnimations.idleAnimeName);
        // Debug.Log("Reloading");
        if (ReloadTimer + ReloadInterval <=  Time.time){
            Magazine = 20;
            currentState = State.WayFinding;
            ReloadTimer = -1;
        }
    }

    /// <summary>
    ///攻击模块
    /// </summary>
    private float attackTimer;    //攻击计时器
    public float attackInterval = 0.1f;    //攻击间隔

    public GameObject bullet;    //子弹对象
    public Transform spawnPoint;    //开火点
    public float fireSpeed = 40;    //子弹速度
    private void Attack()
    {
        if (enemyMove.WayFinding()) 
        {
            currentState = State.WayFinding;
        }
        
        if (attackTimer <= Time.time &&  Magazine >= 1)
        {
            GameObject spawnBullet = Instantiate(bullet, spawnPoint.position, spawnPoint.rotation);
            spawnBullet.GetComponent<BullesDamageBool>().category = "Enemy";
            spawnBullet.GetComponent<Rigidbody>().velocity = -spawnPoint.right * fireSpeed;
            Destroy(spawnBullet, 5);
            // anim[enemyAnimations.attackAnimeName].speed = 3.0f;


            AnimePlay(enemyAnimations.attackAnimeName);
            attackTimer = Time.time + attackInterval;
            Magazine = Magazine - 1;
            if(Magazine == 0){
                currentState = State.Reloading;
            }
        } 
    }
    /// <summary>
    /// 播放动画
    /// </summary>
    /// <param name="animeName">动画名称</param>
    private void AnimePlay(string animeName)
    {
        if (!enemyAnimations.action.IsPlaying(animeName))
        {
            // enemyAnimations.action.Scale = 3;

            // if(animeNameattackAnimeName
            enemyAnimations.action.PlayAnime(animeName);
        }
    }
}

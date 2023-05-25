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
        WayFinding
    }
    /// <summary>
    /// 当前状态
    /// </summary>
    public State currentState = State.WayFinding;

    private EnemyMove enemyMove;
    private EnemyAnimations enemyAnimations;
    void Start()
    {
        target = FindObjectOfType<RigidbodyFirstPersonController>().transform;
        enemyMove = GetComponent<EnemyMove>();
        enemyAnimations = GetComponent<EnemyAnimations>();
    }
    void Update()
    {
        //状态切换
        switch (currentState)
        {
            case State.Attack:        //攻击状态
                Attack();
                break;
            case State.WayFinding:       //寻路状态
                WayFinding();
                break;
        }
        CheckAttckRange();
    }

    /// <summary>
    /// 检查玩家是否在攻击范围内
    /// </summary>
    private Transform target;    //玩家对象
    [Header("检测玩家")]
    public float detectDistance = 30;    //扇形距离
    public float detectAngleRange = 120;    //扇形的角度
    public float attackDistance = 10;
    private void CheckAttckRange()
    {
        Vector3 norVec = transform.rotation * Vector3.forward * 5;
        float jiaJiao = Mathf.Acos(Vector3.Dot(norVec.normalized, (target.position - transform.position).normalized)) * Mathf.Rad2Deg;     //计算两个向量间的夹角
        float dis = Vector3.Distance(transform.position, target.position);
        if (Vector3.Distance(transform.position, target.position) < detectDistance && jiaJiao <= detectAngleRange * 0.5f)
        {
            enemyMove.RotateByLookAtTarget(new Vector3(target.position.x + 0.4f, target.position.y - 1.2f, target.position.z));     // 基准为玩家脚底
            if (dis < attackDistance)
                currentState = State.Attack;
            else
                currentState = State.WayFinding;
        }
        else
        {
            currentState = State.WayFinding;
        }
    }

    /// <summary>
    /// 寻路模块
    /// </summary>
    private void WayFinding()
    {
        if (!enemyMove.WayFinding())
        {
            AnimePlay(enemyAnimations.idleAnimeName);
            currentState = State.Attack;
        }
        else
            AnimePlay(enemyAnimations.runAnimeName);
    }

    /// <summary>
    ///攻击模块
    /// </summary>
    private float attackTimer;    //攻击计时器
    public float attackInterval = 2;    //攻击间隔

    public GameObject bullet;    //子弹对象
    public Transform spawnPoint;    //开火点
    public float fireSpeed = 40;    //子弹速度
    private void Attack()
    {
        if (attackTimer <= Time.time)
        {
            GameObject spawnBullet = Instantiate(bullet, spawnPoint.position, spawnPoint.rotation);
            spawnBullet.GetComponent<BullesDamageBool>().category = "Enemy";
            spawnBullet.GetComponent<Rigidbody>().velocity = -spawnPoint.right * fireSpeed;
            Destroy(spawnBullet, 5);
            AnimePlay(enemyAnimations.attackAnimeName);
            attackTimer = Time.time + attackInterval;
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
            enemyAnimations.action.PlayAnime(animeName);
        }
    }
}

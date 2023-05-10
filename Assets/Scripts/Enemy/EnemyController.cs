using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }

    //寻路模块
    private void WayFinding()
    {
        AnimePlay(enemyAnimations.runAnimeName);
        if (!enemyMove.WayFinding()) 
        {
            currentState = State.Attack;
        }
    }
    //攻击计时器
    private float attackTimer;
    //攻击间隔
    public float attackInterval = 3;
    //攻击模块
    private void Attack()
    {
        if (attackTimer <= Time.time)
        {
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

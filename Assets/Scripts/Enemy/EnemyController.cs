using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���Ƶ�����Ϊ
/// </summary>
[RequireComponent(typeof(EnemyAnimations))]
[RequireComponent(typeof(EnemyMove))]
[RequireComponent(typeof(EnemyStatus))]
public class EnemyController : MonoBehaviour
{
    /// <summary>
    /// �������״̬��ö��
    /// </summary>
    public enum State
    {
        Attack,
        WayFinding
    }
    /// <summary>
    /// ��ǰ״̬
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
        //״̬�л�
        switch (currentState)
        {
            case State.Attack:        //����״̬
                Attack();
                break;
            case State.WayFinding:       //Ѱ·״̬
                WayFinding();
                break;

        }
    }

    //Ѱ·ģ��
    private void WayFinding()
    {
        AnimePlay(enemyAnimations.runAnimeName);
        if (!enemyMove.WayFinding()) 
        {
            currentState = State.Attack;
        }
    }
    //������ʱ��
    private float attackTimer;
    //�������
    public float attackInterval = 3;
    //����ģ��
    private void Attack()
    {
        if (attackTimer <= Time.time)
        {
            AnimePlay(enemyAnimations.attackAnimeName);
            attackTimer = Time.time + attackInterval;
        }
    }
    /// <summary>
    /// ���Ŷ���
    /// </summary>
    /// <param name="animeName">��������</param>
    private void AnimePlay(string animeName)
    {
        if (!enemyAnimations.action.IsPlaying(animeName))
        {
            enemyAnimations.action.PlayAnime(animeName);
        }
    }
}

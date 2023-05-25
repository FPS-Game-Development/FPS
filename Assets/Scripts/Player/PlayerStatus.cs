using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 定义玩家状态，提供受伤功能
/// </summary>
public class PlayerStatus : MonoBehaviour
{
    PlayerHealthBar healthBar;
    private void Start()
    {
        healthBar = GetComponent<PlayerHealthBar>();
    }
    /// <summary>
    /// 当前血量
    /// </summary>
    public float hp = 200;
    /// <summary>
    /// 最大血量
    /// </summary>
    public float maxHP = 200;

    public float HP            //当前血量
    {
        get
        {
            return hp;
        }
        set
        {
            if (value < 0)
            {
                hp = 0;
            }
            else if (value > maxHP)
            {
                hp = maxHP;
            }
            else
            {
                hp = value;
            }
        }
    }

    /// <summary>
    /// 玩家受伤
    /// </summary>
    /// <param name="damageNum">扣血数值</param>
    public void Damage(float damageNum)
    {
        HP -= damageNum;
        healthBar.BarDamage();
        if (HP <= 0)
        {
            Death();
        }
    }
    /// <summary>
    /// 死亡延迟时间
    /// </summary>
    public float deathDelayTime = 0;
    /// <summary>
    /// 玩家死亡
    /// </summary>
    public void Death()
    {
        Destroy(gameObject, deathDelayTime);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 定义敌人状态，提供受伤、死亡功能
/// </summary>
public class EnemyStatus : MonoBehaviour
{
    EnemyHealthBar healthBar;
    private void Start()
    {
        healthBar = GetComponent<EnemyHealthBar>();
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
    /// 敌人受伤
    /// </summary>
    /// <param name="damageNum">扣血数值</param>
    public void Damage(float damageNum)
    {
        HP -= damageNum;
        healthBar.BarDamage();
        if(HP <= 0)
        {
            Death();
        }
    }
    /// <summary>
    /// 死亡延迟时间
    /// </summary>
    public float deathDelayTime = 0;
    public EnemyGenerator Generator;
    /// <summary>
    /// 敌人死亡
    /// </summary>
    public void Death()
    {
        Destroy(gameObject,deathDelayTime);
        GetComponent<EnemyMove>().way.IsUsable = true;
        Generator.GenerateEnemy();
    }
}

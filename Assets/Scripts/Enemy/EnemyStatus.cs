using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �������״̬���ṩ���ˡ���������
/// </summary>
public class EnemyStatus : MonoBehaviour
{
    EnemyHealthBar healthBar;
    private void Start()
    {
        healthBar = GetComponent<EnemyHealthBar>();
    }
    /// <summary>
    /// ��ǰѪ��
    /// </summary>
    public float hp = 200;
    /// <summary>
    /// ���Ѫ��
    /// </summary>
    public float maxHP = 200;

    public float HP            //��ǰѪ��
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
    /// ��������
    /// </summary>
    /// <param name="damageNum">��Ѫ��ֵ</param>
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
    /// �����ӳ�ʱ��
    /// </summary>
    public float deathDelayTime = 0;
    public EnemyGenerator Generator;
    /// <summary>
    /// ��������
    /// </summary>
    public void Death()
    {
        Destroy(gameObject,deathDelayTime);
        GetComponent<EnemyMove>().way.IsUsable = true;
        Generator.GenerateEnemy();
    }
}

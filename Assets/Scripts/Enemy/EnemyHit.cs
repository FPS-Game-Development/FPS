using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �������״̬���ṩ���ˡ���������
/// </summary>
public class EnemyHit : MonoBehaviour
{
    EnemyStatus enemyStatus;

    private void Start()
    {
        enemyStatus = GetComponent<EnemyStatus>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            BullesDamageBool bullet = collision.gameObject.GetComponent<BullesDamageBool>();
            if (bullet != null && !bullet.hasHit)
            {
                bullet.hasHit = true;
                if (enemyStatus.HP > 0)
                    enemyStatus.Damage(10f);
            }
        }
    }
}

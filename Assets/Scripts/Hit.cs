using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : MonoBehaviour
{
    BullesDamageBool isHitted;

    private void Start()
    {
        isHitted = GetComponent<BullesDamageBool>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (isHitted.category == "Player")
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                EnemyStatus enemyStatus = collision.gameObject.GetComponent<EnemyStatus>();
                if (isHitted != null && !isHitted.hasHit)
                {
                    isHitted.hasHit = true;
                    if (enemyStatus.HP > 0)
                        enemyStatus.Damage(10f);
                }
            }
        }
        if (isHitted.category == "Enemy")
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                PlayerStatus playerStatus = collision.gameObject.GetComponent<PlayerStatus>();
                if (isHitted != null && !isHitted.hasHit)
                {
                    isHitted.hasHit = true;
                    if (playerStatus.HP > 0)
                        playerStatus.Damage(10f);
                }
            }
        }
    }
}

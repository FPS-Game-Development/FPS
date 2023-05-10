using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class Test : MonoBehaviour
{
    EnemyStatus enemyStatus;
    private void Start()
    {
        enemyStatus = GetComponent<EnemyStatus>();
    }
    private void Update()
    {
        if(enemyStatus.HP>0)
            enemyStatus.Damage(0.1f);
    }
}

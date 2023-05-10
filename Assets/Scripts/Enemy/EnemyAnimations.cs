using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������Ҫ���ŵĶ�������
/// </summary>
public class EnemyAnimations: MonoBehaviour
{
    public string runAnimeName;
    public string attackAnimeName;
    public string idleAnimeName;
    public string deathAnimeName;

    public AnimationAction action;
    private void Awake()
    {
        //���ö�������ģ��
        action = new AnimationAction(GetComponentInChildren<Animation>());
    }
}

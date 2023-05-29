using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 定义需要播放的动画名称
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
        //调用动画运作模块
        // AnimationState state = GetComponentInChildren<Animation>()[attackAnimeName];
        // if (!state) state.speed = speed;
        
        action = new AnimationAction(GetComponentInChildren<Animation>());
        
    }
}

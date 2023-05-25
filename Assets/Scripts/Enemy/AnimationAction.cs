using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 提供动画行为
/// </summary>
public class AnimationAction
{
    private readonly Animation anim;
    /// <summary>
    /// 提供动画行为
    /// </summary>
    /// <param name="anim">动画</param>
    public AnimationAction(Animation anim)
    {
        this.anim = anim;
    }
    /// <summary>
    /// 动画播放
    /// </summary>
    /// <param name="animName">动画名称</param>
    public void PlayAnime(string animName)
    {
        anim.CrossFade(animName);
    }
    /// <summary>
    /// 指定动画是否在播放
    /// </summary>
    /// <param name="animName">动画名称</param>
    /// <returns></returns>
    public bool IsPlaying(string animName)
    {
        return anim.IsPlaying(animName);
    }
}

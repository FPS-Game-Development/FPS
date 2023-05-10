using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �ṩ������Ϊ
/// </summary>
public class AnimationAction
{
    private readonly Animation anim;
    /// <summary>
    /// �ṩ������Ϊ
    /// </summary>
    /// <param name="anim">����</param>
    public AnimationAction(Animation anim)
    {
        this.anim = anim;
    }
    /// <summary>
    /// ��������
    /// </summary>
    /// <param name="animName">��������</param>
    public void PlayAnime(string animName)
    {
        anim.CrossFade(animName);
    }
    /// <summary>
    /// ָ�������Ƿ��ڲ���
    /// </summary>
    /// <param name="animName">��������</param>
    /// <returns></returns>
    public bool IsPlaying(string animName)
    {
        return anim.IsPlaying(animName);
    }
}

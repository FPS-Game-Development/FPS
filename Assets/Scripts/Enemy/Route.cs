using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// �������·��
/// </summary>
public class Route
{
    /// <summary>
    /// ·�ߴ���
    /// </summary>
    public Transform RepresentWayLine { get; set;}
    /// <summary>
    /// ·���Ƿ����
    /// </summary>
    public bool IsUsable { get; set; }

    /// <summary>
    /// ����·��
    /// </summary>
    /// <param name="wayLinePoints">·�ߵ㼯��</param>
    /// <param name="wayPointCount">·�ߵ���Ŀ</param>
    public Route(Transform WayLine)
    {
        this.RepresentWayLine = WayLine;
        this.IsUsable = true;
    }
}

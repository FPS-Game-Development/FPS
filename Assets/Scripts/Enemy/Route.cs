using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 定义敌人路线
/// </summary>
public class Route
{
    /// <summary>
    /// 路线代表
    /// </summary>
    public Transform RepresentWayLine { get; set;}
    /// <summary>
    /// 路线是否可用
    /// </summary>
    public bool IsUsable { get; set; }

    /// <summary>
    /// 创建路线
    /// </summary>
    /// <param name="wayLinePoints">路线点集合</param>
    /// <param name="wayPointCount">路线点数目</param>
    public Route(Transform WayLine)
    {
        this.RepresentWayLine = WayLine;
        this.IsUsable = true;
    }
}

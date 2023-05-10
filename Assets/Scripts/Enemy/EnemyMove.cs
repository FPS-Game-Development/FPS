using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

/// <summary>
/// 控制敌人的移动、旋转和寻路
/// </summary>
public class EnemyMove : MonoBehaviour
{
    /// <summary>
    /// 敌人速度
    /// </summary>
    public float speed = 2;
    /// <summary>
    /// 敌人路线
    /// </summary>
    public Route way;
    public float rotateSpeed = 10;


    /// <summary>
    /// 向前移动
    /// </summary>
    public void MoveForward()
    {
        transform.Translate(0,0,speed*Time.deltaTime);
    }
    /// <summary>
    /// 向目标点旋转
    /// </summary>
    /// <param name="target">目标点</param>
    public void RotateByLookAtTarget(Vector3 target)
    {
        Vector3 targetDirection = target - transform.position;
        if (targetDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * Time.deltaTime);
        }
        //transform.LookAt(target);
    }

    //寻路的目标路线点
    private int wayPointIndex = 0;
    /// <summary>
    /// 寻路
    /// </summary>
    /// <returns>路线是否有效</returns>
    public bool WayFinding()
    {
        if (wayPointIndex >= way.RepresentWayLine.childCount) return false;
        RotateByLookAtTarget(way.RepresentWayLine.GetChild(wayPointIndex).position);
        MoveForward();
        if (Vector3.Distance(this.transform.position, way.RepresentWayLine.GetChild(wayPointIndex).position) < 0.1)
            wayPointIndex++;
        return true;
    }
}

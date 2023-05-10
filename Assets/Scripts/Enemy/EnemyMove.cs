using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

/// <summary>
/// ���Ƶ��˵��ƶ�����ת��Ѱ·
/// </summary>
public class EnemyMove : MonoBehaviour
{
    /// <summary>
    /// �����ٶ�
    /// </summary>
    public float speed = 2;
    /// <summary>
    /// ����·��
    /// </summary>
    public Route way;
    public float rotateSpeed = 10;


    /// <summary>
    /// ��ǰ�ƶ�
    /// </summary>
    public void MoveForward()
    {
        transform.Translate(0,0,speed*Time.deltaTime);
    }
    /// <summary>
    /// ��Ŀ�����ת
    /// </summary>
    /// <param name="target">Ŀ���</param>
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

    //Ѱ·��Ŀ��·�ߵ�
    private int wayPointIndex = 0;
    /// <summary>
    /// Ѱ·
    /// </summary>
    /// <returns>·���Ƿ���Ч</returns>
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

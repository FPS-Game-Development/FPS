using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityStandardAssets.Characters.FirstPerson;

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

    private Transform target;
    [Header("检测玩家")]
    public float detectDistance = 10;//扇形距离
    public float detectAngleRange = 120;//扇形的角度


    private void Awake()
    {
        target = FindObjectOfType<RigidbodyFirstPersonController>().transform;
    }

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

        
        if (Vector3.Distance(transform.position, target.position) < detectDistance)
        {
            Vector3 norVec = transform.rotation * Vector3.forward * 5;
            Debug.DrawRay(transform.position, norVec, Color.red);  // 敌人前进方向
            Debug.DrawLine(transform.position, target.position, Color.green);  //敌人与玩家连线
            float jiaJiao = Mathf.Acos(Vector3.Dot(norVec.normalized, (target.position - transform.position).normalized)) * Mathf.Rad2Deg; //计算两个向量间的夹角
            if (jiaJiao <= detectAngleRange * 0.5f)
            {
                Debug.Log("在扇形范围内");
                RotateByLookAtTarget(target.position);
                MoveForward();
                return true;
            }
            return false;

        }else
        {
            if (wayPointIndex >= way.RepresentWayLine.childCount) return false;
            RotateByLookAtTarget(way.RepresentWayLine.GetChild(wayPointIndex).position);
            MoveForward();
            if (Vector3.Distance(this.transform.position, way.RepresentWayLine.GetChild(wayPointIndex).position) < 0.1)
                wayPointIndex++;
            return true;

        }





    }




  
}

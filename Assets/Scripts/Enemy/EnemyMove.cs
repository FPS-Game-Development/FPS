using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityStandardAssets.Characters.FirstPerson;

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

    private Transform target;
    [Header("检测玩家")]
    public float detectDistance = 15;//扇形距离
    public float detectAngleRange = 120;//扇形的角度
    public float attackDistance = 10;
    public bool  waytype = true;

    private void Awake()
    {
        target = FindObjectOfType<RigidbodyFirstPersonController>().transform;
    }
    

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

    public bool WayFinding()
    {
        Vector3 norVec = transform.rotation * Vector3.forward * 5;
        float jiaJiao = Mathf.Acos(Vector3.Dot(norVec.normalized, (target.position - transform.position).normalized)) * Mathf.Rad2Deg; //计算两个向量间的夹角
        Debug.DrawRay(transform.position, norVec, Color.red);  // 敌人前进方向
        Debug.DrawLine(transform.position, target.position, Color.green);  //敌人与玩家连线

        float dis = Vector3.Distance(transform.position, target.position);
        
        if (dis < detectDistance && jiaJiao <= detectAngleRange * 0.5f)
        {
            Debug.Log("在扇形范围内");
            RotateByLookAtTarget(new Vector3(target.position.x, target.position.y - 1.2f, target.position.z)); // 基准为玩家脚底
            
            if (dis < attackDistance)
                /// 朝玩家射击
                return false;
         
            MoveForward();
            /// 朝玩家前进
            return true;

        }else
        {
            
            if ((wayPointIndex >= way.RepresentWayLine.childCount  && waytype) || 
                
                (wayPointIndex <= -1 && !waytype))  {
                /// 走到终点
                
                waytype = !waytype;
                // return true;

                if(waytype)wayPointIndex++;
                else wayPointIndex--;
                Debug.Log(wayPointIndex);
            } 
            
            RotateByLookAtTarget(way.RepresentWayLine.GetChild(wayPointIndex).position);
            MoveForward();
            if (Vector3.Distance(this.transform.position, way.RepresentWayLine.GetChild(wayPointIndex).position) < 0.1){
                Debug.Log(wayPointIndex);
                if(waytype)wayPointIndex++;
                else wayPointIndex--;
            }
           /// 继续寻路
            return true;
            
        }
    }
}

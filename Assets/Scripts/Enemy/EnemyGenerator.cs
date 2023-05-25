using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 生成敌人
/// </summary>
public class EnemyGenerator : MonoBehaviour
{
    /// <summary>
    /// 敌人预制件数组
    /// </summary>
    public GameObject[] enemyType;
    /// <summary>
    /// 敌人最大生成数量
    /// </summary>
    public int maxCount = 5;
    /// <summary>
    /// 敌人起始生成数量
    /// </summary>
    public int startCount = 2;
    //敌人现存数量
    public int CurCount = 0;
    //敌人路线集合
    private Route[] routes;
    /// <summary>
    /// 最大随机延迟时间
    /// </summary>
    public float maxDelayTime=5;
    private void Start()
    {
        //初始化路线集合
        GtherRoutes();
        for(int i = 0; i< startCount; i++)
        {
            GenerateEnemy();
        }
    }
    public void GenerateEnemy()
    {
        //判断是否继续生成敌人
        if (CurCount >= maxCount) return;
        CurCount++;
        Invoke(nameof(GenerateOneEnemy), Random.Range(0, maxDelayTime));
    }
    /// <summary>
    /// 生成一个敌人
    /// </summary>
    private void GenerateOneEnemy()
    {
        //选择可用路线之一
        Route choosedRoute = ChooseRoute(routes);
        //创建敌人（出生地以及敌人类型）
        GameObject enemy = CreatEnemy(choosedRoute);
        //输入路线
        enemy.GetComponent<EnemyMove>().way = choosedRoute;
        choosedRoute.IsUsable = false;
    }
    //敌人出生位置
    private Vector3 startPoint;
    //敌人出生旋转角度
    private Quaternion startRotation;
    //生成敌人
    private GameObject CreatEnemy(Route choosedRoute)
    {
        startPoint = choosedRoute.RepresentWayLine.position;
        startRotation = choosedRoute.RepresentWayLine.rotation;
        int typeIndex = Random.Range(0, enemyType.Length);
        GameObject enemy = Instantiate(enemyType[typeIndex], startPoint, startRotation) as GameObject;
        //传递生成器对象引用
        enemy.GetComponent<EnemyStatus>().Generator = this;
        return enemy;
    }
    //选择路线
    private Route ChooseRoute(Route[] routes)
    {
        Route[] usefulRoutes = UsefulRoute(routes);
        int routeIndex = Random.Range(0, usefulRoutes.Length);
        return usefulRoutes[routeIndex];
    }
    //可用路线集合
    private Route[] UsefulRoute(Route[] routes)
    {
        List<Route> routesList = new(routes.Length);
        foreach(var route in routes)
        {
            if (route.IsUsable) routesList.Add(route);
        }
        return routesList.ToArray();
    }
    //路线集合
    private void GtherRoutes()
    {
        routes = new Route[transform.childCount];
        for (int i = 0; i < transform.childCount; i++) 
        {
            routes[i]=new Route(transform.GetChild(i));
        }
    }

}

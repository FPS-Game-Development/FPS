using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���ɵ���
/// </summary>
public class EnemyGenerator : MonoBehaviour
{
    /// <summary>
    /// ����Ԥ�Ƽ�����
    /// </summary>
    public GameObject[] enemyType;
    /// <summary>
    /// ���������������
    /// </summary>
    public int maxCount = 5;
    /// <summary>
    /// ������ʼ��������
    /// </summary>
    public int startCount = 2;
    //�����ִ�����
    public int CurCount = 0;
    //����·�߼���
    private Route[] routes;
    /// <summary>
    /// �������ӳ�ʱ��
    /// </summary>
    public float maxDelayTime=5;
    private void Start()
    {
        //��ʼ��·�߼���
        GtherRoutes();
        for(int i = 0; i< startCount; i++)
        {
            GenerateEnemy();
        }
    }
    public void GenerateEnemy()
    {
        //�ж��Ƿ�������ɵ���
        if (CurCount >= maxCount) return;
        CurCount++;
        Invoke(nameof(GenerateOneEnemy), Random.Range(0, maxDelayTime));
    }
    /// <summary>
    /// ����һ������
    /// </summary>
    private void GenerateOneEnemy()
    {
        //ѡ�����·��֮һ
        Route choosedRoute = ChooseRoute(routes);
        //�������ˣ��������Լ��������ͣ�
        GameObject enemy = CreatEnemy(choosedRoute);
        //����·��
        enemy.GetComponent<EnemyMove>().way = choosedRoute;
        choosedRoute.IsUsable = false;
    }
    //���˳���λ��
    private Vector3 startPoint;
    //���˳�����ת�Ƕ�
    private Quaternion startRotation;
    //���ɵ���
    private GameObject CreatEnemy(Route choosedRoute)
    {
        startPoint = choosedRoute.RepresentWayLine.position;
        startRotation = choosedRoute.RepresentWayLine.rotation;
        int typeIndex = Random.Range(0, enemyType.Length);
        GameObject enemy = Instantiate(enemyType[typeIndex], startPoint, startRotation) as GameObject;
        //������������������
        enemy.GetComponent<EnemyStatus>().Generator = this;
        return enemy;
    }
    //ѡ��·��
    private Route ChooseRoute(Route[] routes)
    {
        Route[] usefulRoutes = UsefulRoute(routes);
        int routeIndex = Random.Range(0, usefulRoutes.Length);
        return usefulRoutes[routeIndex];
    }
    //����·�߼���
    private Route[] UsefulRoute(Route[] routes)
    {
        List<Route> routesList = new(routes.Length);
        foreach(var route in routes)
        {
            if (route.IsUsable) routesList.Add(route);
        }
        return routesList.ToArray();
    }
    //·�߼���
    private void GtherRoutes()
    {
        routes = new Route[transform.childCount];
        for (int i = 0; i < transform.childCount; i++) 
        {
            routes[i]=new Route(transform.GetChild(i));
        }
    }

}

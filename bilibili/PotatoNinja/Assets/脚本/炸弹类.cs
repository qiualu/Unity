using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class 炸弹类 : MonoBehaviour
{

 
    private Rigidbody 炸弹物理组件;    // fruitRigidbody 
    private SphereCollider 炸弹碰撞体;
 

    private Camera 主摄像机引用;

 
    public int 分数 = 1;

 
    private bool 鼠标判定状态 = false;

    private bool 触发状态 = true;


    private void Awake()
    {
        炸弹物理组件 = GetComponent<Rigidbody>();
        //土豆碰撞体 = GetComponent<Collider>();
        炸弹碰撞体 = GetComponent<SphereCollider>();
        主摄像机引用 = Camera.main;
    }

  
    private void 结束游戏()
    {
 

        土豆忍者管理类.土豆忍者管理.爆炸结束游戏();

    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("和鼠标碰撞了！");
            结束游戏();
        }
    }


 
    public void 开始判定()
    {
        鼠标判定状态 = true;
    }
    public void 结束判定()
    {
        鼠标判定状态 = false;
    }
    public void 连续判定()
    {
        // 获取鼠标世界坐标（仅XY平面）
        Vector3 鼠标世界坐标 = 主摄像机引用.ScreenToWorldPoint(
            new Vector3(Input.mousePosition.x, Input.mousePosition.y, 主摄像机引用.nearClipPlane));

        //Debug.Log($" {gameObject.name} 鼠标世界坐标 ： {鼠标世界坐标} ");
        // 转换为2D坐标
        Vector2 鼠标2D坐标 = new Vector2(鼠标世界坐标.x, 鼠标世界坐标.y);
        //Debug.Log($" {gameObject.name} 鼠标2D坐标 ： {鼠标2D坐标} " );

        // 确保获取的是SphereCollider组件
        if (炸弹碰撞体 == null)
        {
            炸弹碰撞体 = GetComponent<SphereCollider>();
            if (炸弹碰撞体 == null) return;
        }

        // 计算实际中心点（考虑偏移量）
        Vector3 实际中心3D = transform.TransformPoint(炸弹碰撞体.center);
        Vector2 碰撞体中心 = new Vector2(实际中心3D.x, 实际中心3D.y);

        // 计算缩放后的半径（取最大缩放值）
        float 实际半径 = 炸弹碰撞体.radius *
            Mathf.Max(Mathf.Abs(transform.lossyScale.x),
                     Mathf.Abs(transform.lossyScale.y),
                     Mathf.Abs(transform.lossyScale.z));

        // 计算距离
        float 距离 = Vector2.Distance(鼠标2D坐标, 碰撞体中心);

        //Debug.Log($" {gameObject.name} 鼠标2D坐标: {鼠标2D坐标} 碰撞体：{碰撞体中心}，缩放半径：{实际半径}，鼠标距离：{距离}");

        if (距离 <= 实际半径)
        {
            触发状态 = false;
            结束游戏();
        }

    }

    private void Update()
    {
        if (触发状态) {
            if (Input.GetMouseButtonDown(0))
            {
                开始判定();
                //Debug.Log("开始刀片");
            }
            else if (Input.GetMouseButtonUp(0))
            {
                结束判定();
                //Debug.Log("结束刀片");
            }
            else if (鼠标判定状态)
            {
                连续判定();
            }
            else if(Input.GetMouseButton(0))
            {
                //Debug.Log("鼠标按压状态");
                // 只要按住鼠标就持续检测（不依赖鼠标移动事件）
                开始判定();
            }
        }
    }



}

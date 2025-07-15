using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class 土豆类 : MonoBehaviour
{

    public GameObject 完整水果;
    public GameObject 切开后;

    private Rigidbody 土豆物理组件;    // fruitRigidbody
    //private Collider 土豆碰撞体;       // fruitCollider
    private SphereCollider 土豆碰撞体;
    private ParticleSystem 散开效果; // juiceEffect

    private Camera 主摄像机引用;

    //private int 切割距离 = 2;

    public int 分数 = 1;

    private Vector3 上一帧鼠标位置;
    private bool 鼠标判定状态 = false;

    private bool 触发状态 = true;


    private void Awake()
    {
        土豆物理组件 = GetComponent<Rigidbody>();
        //土豆碰撞体 = GetComponent<Collider>();
        土豆碰撞体 = GetComponent<SphereCollider>();
        散开效果 = GetComponentInChildren<ParticleSystem>();
        散开效果.Stop(); // 添加这行代码阻止自动播放

        主摄像机引用 = Camera.main;

        完整水果.gameObject.SetActive(true);
        切开后.gameObject.SetActive(false);

    }

 

    private void 切割函数(Vector3 direction, Vector3 position, float force)
    {
        土豆忍者管理类.土豆忍者管理.计分函数(分数);
        土豆碰撞体.enabled = false; // 关闭碰撞体
        完整水果.SetActive(false); // 隐藏完整体
                               // 启用切好的水果
        切开后.SetActive(true);
        散开效果.Play();

        // 根据切割角度旋转 // Rotate based on the slice angle
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        切开后.transform.rotation = Quaternion.Euler(0f, 0f, angle);

        // 获取所有切片的刚体
        Rigidbody[] slices = 切开后.GetComponentsInChildren<Rigidbody>();

        //// 给每个切片施加力
        //foreach (Rigidbody slice in slices)
        //{
        //    slice.velocity = 水果的物理组件.velocity;
        //    slice.AddForceAtPosition(direction * force, position, ForceMode.Impulse);
        //}

        if (slices.Length == 2)
        {
            // 计算两个切片的中心点连线方向（互相排斥的方向）
            Vector3 separationDir = (slices[1].position - slices[0].position).normalized;
            float separationForce = force * 0.5f + 5; // 分离力是切割力的一半

            // 给每个切片施加力
            for (int i = 0; i < slices.Length; i++)
            {
                slices[i].velocity = 土豆物理组件.velocity;

                // 切割方向的力
                slices[i].AddForceAtPosition(direction * force, position, ForceMode.Impulse);

                // 互相排斥的力（正负方向取决于切片索引）
                slices[i].AddForce(separationDir * (i == 0 ? 1 : -1) * separationForce, ForceMode.Impulse);
            }
            Debug.Log("两个物体力！");

        }
        else
        {
            // 如果不是两个切片，保持原来的逻辑
            foreach (Rigidbody slice in slices)
            {
                slice.velocity = 土豆物理组件.velocity;
                slice.AddForceAtPosition(direction * force, position, ForceMode.Impulse);
            }
        }



    }

    private void 鼠标切割函数(Vector2 direction, Vector2 position, Vector3 startpos)
    {
        土豆忍者管理类.土豆忍者管理.计分函数(分数);
        土豆碰撞体.enabled = false; // 关闭碰撞体
        完整水果.SetActive(false); // 隐藏完整体
                               // 启用切好的水果
        切开后.SetActive(true);
        散开效果.Play();

        float force = 土豆忍者管理类.土豆忍者管理.切割力;

        // 根据切割角度旋转 // Rotate based on the slice angle
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        切开后.transform.rotation = Quaternion.Euler(0f, 0f, angle);

        // 获取所有切片的刚体
        Rigidbody[] slices = 切开后.GetComponentsInChildren<Rigidbody>();

        //// 给每个切片施加力
        //foreach (Rigidbody slice in slices)
        //{
        //    slice.velocity = 水果的物理组件.velocity;
        //    slice.AddForceAtPosition(direction * force, position, ForceMode.Impulse);
        //}

        if (slices.Length == 2)
        {
            // 计算两个切片的中心点连线方向（互相排斥的方向）
            Vector3 separationDir = (slices[1].position - slices[0].position).normalized;
            float separationForce = force * 0.5f + 5; // 分离力是切割力的一半

            // 给每个切片施加力
            for (int i = 0; i < slices.Length; i++)
            {
                slices[i].velocity = 土豆物理组件.velocity;

                // 切割方向的力
                slices[i].AddForceAtPosition(direction * force, position, ForceMode.Impulse);

                // 互相排斥的力（正负方向取决于切片索引）
                slices[i].AddForce(separationDir * (i == 0 ? 1 : -1) * separationForce, ForceMode.Impulse);
            }
            Debug.Log("两个物体力！");

        }
        else
        {
            ////Vector2 startpos 根据这个开始随机方向旋转 四散  
            //// 多切片随机四散逻辑
            //foreach (Rigidbody slice in slices)
            //{
            //    slice.velocity = 土豆物理组件.velocity;

            //    // 生成随机角度(0-360度)
            //    float randomAngle = Random.Range(0f, 360f);
            //    Vector3 randomDir = new Vector3(
            //        Mathf.Cos(randomAngle * Mathf.Deg2Rad),
            //        Mathf.Sin(randomAngle * Mathf.Deg2Rad),
            //        0);

            //    // 基础力 + 随机力变化(±30%)
            //    float randomForce = force * Random.Range(0.7f, 1.3f);

            //    // 施加随机方向的力
            //    slice.AddForce(randomDir * randomForce, ForceMode.Impulse);

            //    // 添加少量随机扭矩让碎片旋转
            //    slice.AddTorque(new Vector3(
            //        Random.Range(-5f, 5f),
            //        Random.Range(-5f, 5f),
            //        Random.Range(-5f, 5f)),
            //        ForceMode.Impulse);
            //}
            // -------------------------------------------------------------

            // 多切片随机四散逻辑（基于startpos向外扩散）
            foreach (Rigidbody slice in slices)
            {
                slice.velocity = 土豆物理组件.velocity;

                // 计算从切割点指向碎片的方向（关键修改）
                Vector3 outwardDir = (slice.position - startpos).normalized;

                // 生成带偏差的随机角度（±45度范围内）
                float angleDeviation = Random.Range(-45f, 45f);
                Quaternion deviationRot = Quaternion.Euler(0, 0, angleDeviation);
                Vector3 finalDir = deviationRot * outwardDir;

                // 力的大小随距离衰减（近处力大，远处力小）
                float distanceFactor = Mathf.Clamp01(1 - Vector3.Distance(startpos, slice.position) / 5f);
                float randomForce = force * (0.5f + distanceFactor * 0.5f) * Random.Range(0.7f, 1.3f);

                // 施加力（80%向外方向 + 20%随机方向）
                slice.AddForce(finalDir * randomForce * 0.8f + Random.onUnitSphere * randomForce * 0.2f,
                              ForceMode.Impulse);

                // 改进的扭矩（旋转方向与运动方向关联）
                slice.AddTorque(new Vector3(
                    finalDir.y * 10f,  // 与运动方向相关的扭矩
                    -finalDir.x * 10f,
                    Random.Range(-15f, 15f)),  // 保留部分随机旋转
                    ForceMode.Impulse);
            }


        }



    }



    private void OnTriggerEnter(Collider other)
    {
        //if (other.CompareTag("Player"))
        //{
        //    Debug.Log("和鼠标碰撞了！");
        //    刀痕迹类 刀痕迹 = other.GetComponent<刀痕迹类>();
        //    切割函数(刀痕迹.公开属性, 刀痕迹.transform.position, 刀痕迹.切割力);
        //}

        if (other.CompareTag("Player"))
        {
            // 转换为XY平面距离检测
            Vector2 thisPos = new Vector2(transform.position.x, transform.position.y);
            Vector2 otherPos = new Vector2(other.transform.position.x, other.transform.position.y);

            if (Vector2.Distance(thisPos, otherPos) < 0.5f) // 自定义碰撞阈值
            {
                刀痕迹类 刀痕迹 = other.GetComponent<刀痕迹类>();
                切割函数(刀痕迹.公开属性, 刀痕迹.transform.position, 刀痕迹.切割力);
            }
        }

        //else
        //{
        //    Debug.Log("互相碰撞了！");
        //}
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
        if (土豆碰撞体 == null)
        {
            土豆碰撞体 = GetComponent<SphereCollider>();
            if (土豆碰撞体 == null) return;
        }

        // 计算实际中心点（考虑偏移量）
        Vector3 实际中心3D = transform.TransformPoint(土豆碰撞体.center);
        Vector2 碰撞体中心 = new Vector2(实际中心3D.x, 实际中心3D.y);

        // 计算缩放后的半径（取最大缩放值）
        float 实际半径 = 土豆碰撞体.radius *
            Mathf.Max(Mathf.Abs(transform.lossyScale.x),
                     Mathf.Abs(transform.lossyScale.y),
                     Mathf.Abs(transform.lossyScale.z));

        // 计算距离
        float 距离 = Vector2.Distance(鼠标2D坐标, 碰撞体中心);

        //Debug.Log($" {gameObject.name} 鼠标2D坐标: {鼠标2D坐标} 碰撞体：{碰撞体中心}，缩放半径：{实际半径}，鼠标距离：{距离}");

        if (距离 <= 实际半径)
        {
            触发状态 = false;

            //Debug.Log($" {gameObject.name} 距离够了");/

            Vector2 切割方向 = (鼠标2D坐标 - 碰撞体中心).normalized;
            鼠标切割函数(切割方向, 鼠标2D坐标, 实际中心3D);
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

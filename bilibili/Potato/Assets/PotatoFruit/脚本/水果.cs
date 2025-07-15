using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class 水果 : MonoBehaviour
{

    public GameObject 完整水果;
    public GameObject 切开后;

    private Rigidbody 水果的物理组件;    // fruitRigidbody
    private Collider 水果的碰撞体;       // fruitCollider
    private ParticleSystem 果汁粒子效果; // juiceEffect

    public int 分数 = 1;


    private void Awake()
    {
        水果的物理组件 = GetComponent<Rigidbody>();
        水果的碰撞体 = GetComponent<Collider>();
        果汁粒子效果 = GetComponentInChildren<ParticleSystem>();
        果汁粒子效果.Stop(); // 添加这行代码阻止自动播放
    }

    private void 切割函数(Vector3 direction, Vector3 position, float force)
    {
        切土豆管理.Instance.计分函数(分数);
        水果的碰撞体.enabled = false; // 关闭碰撞体
        完整水果.SetActive(false); // 隐藏完整体
                                  // 启用切好的水果
        切开后.SetActive(true);
        果汁粒子效果.Play();

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
                slices[i].velocity = 水果的物理组件.velocity;

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
                slice.velocity = 水果的物理组件.velocity;
                slice.AddForceAtPosition(direction * force, position, ForceMode.Impulse);
            }
        }



    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Debug.Log("和鼠标碰撞了！");
            刀片类 刀片 = other.GetComponent<刀片类>();
            切割函数(刀片.公开属性, 刀片.transform.position, 刀片.切割时施加的力大小);
        }
        //else{
        //    Debug.Log("互相碰撞了！");
        //}
    }
 


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 铺设地板 : MonoBehaviour
{
    // 不需要再手动设置public变量，改用路径加载
    private GameObject 地板预制体; // 私有变量，通过代码加载

    // Start时自动加载预制体
    void Start()
    {
        // 加载路径：Assets/Resources/保预制体/地板预制体.prefab
        // Resources.Load的路径是"相对于Resources文件夹的路径"，不需要写扩展名
        地板预制体 = Resources.Load<GameObject>("保预制体/地板预制体");

        // 检查是否加载成功
        if (地板预制体 == null)
        {
            Debug.LogError("预制体加载失败！请检查路径是否正确：保预制体/地板预制体");
            return;
        }
    }

    public void 执行铺设地板()
    {
        铺满地板();
    }

    private void 铺满地板()
    {
        // 先判断是否加载到了预制体
        if (地板预制体 == null)
        {
            Debug.LogError("预制体未加载成功，无法铺设！");
            return;
        }

        关卡数据类 关卡数据 = 保卫管理.实例.数据加载实例.当前关卡数据;

        Vector3 起始位置 = new Vector3(
            关卡数据.基础信息.起点.x,
            关卡数据.基础信息.起点.y,
            10
        );

        for (int x = 0; x < 关卡数据.基础信息.宽高.x; x++)
        {
            for (int y = 0; y < 关卡数据.基础信息.宽高.y; y++)
            {
                Vector3 地板位置 = 起始位置 + new Vector3(
                    x * 关卡数据.基础信息.地板大小,
                    y * 关卡数据.基础信息.地板大小,
                    0
                );

                // 直接使用加载好的预制体（无需手动设置）
                GameObject 地板 = 对象池管理器.实例.获取对象(
                    地板预制体,
                    地板位置,
                    Quaternion.identity
                );

                //地板鼠标交互类 地板交互脚本 = 地板.GetComponent<地板鼠标交互类>();
                //if (地板交互脚本 != null)
                //{
                //    地板交互脚本.地板id = x * 14 + y;
                //    地板交互脚本.网格坐标 = new Vector2Int(x, y);
                //}
                //else
                //{
                //    Debug.LogError("地板对象上没有挂载 地板鼠标交互类 脚本！");
                //}

                //if (地板 != null)
                //{
                //    地板.SetActive(true);
                //}
            }
        }
    }
}
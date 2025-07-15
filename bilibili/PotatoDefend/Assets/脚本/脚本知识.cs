using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 脚本知识 : MonoBehaviour
{


    void printjc() {

        // 实际路径 "E:\Projece\Unity\bilibili\PotatoDefend\Assets\Resources\关卡文件\初级关卡.xml"
        string 关卡配置文件 = "Levels/初级关卡";

        float 地板大小 = 1f;    // 单个Cube的尺寸


        //// 输出普通信息
        //Debug.Log("这是一条普通的日志信息");
        //// 输出警告信息
        //Debug.LogWarning("这是一条警告信息");
        //// 输出错误信息
        //Debug.LogError("这是一条错误信息");

        // 输出带上下文的信息（点击日志可直接定位到该对象）
        Debug.Log("这是带有上下文的日志", this.gameObject);

        Debug.Log("保卫萝卜开始管理");
        关卡数据 关卡 = 关卡解析器.解析关卡(关卡配置文件);
        // 使用解析后的数据初始化游戏
        Debug.Log($"加载关卡: {关卡.名称}");
        Debug.Log($"路径点数量: {关卡.路径点列表.Count}");
        Debug.Log($"防御塔数量: {关卡.防御塔列表.Count}");
        Debug.Log($"波次数量: {关卡.波次列表.Count}");
        Debug.Log($"宽高： {关卡.基础变量.宽} {关卡.基础变量.高} 位置x: {关卡.基础变量.起点.x} 位置y: {关卡.基础变量.起点.y}");


        // 初始化防御塔UI
        foreach (var 防御塔 in 关卡.防御塔列表)
        {
            Debug.Log($"防御塔: {防御塔.类型} (花费:{防御塔.花费}, 伤害:{防御塔.伤害})");
        }


        foreach (var 路径点 in 关卡.路径点列表)
        {
            Debug.Log($"路径点x: {路径点.w} 路径点y:{路径点.h}");
            // 创建Cube
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.position = new Vector3(路径点.w + 关卡.基础变量.起点.x, 关卡.基础变量.起点.y - 路径点.h, 0);
            cube.transform.localScale = new Vector3(地板大小, 地板大小, 地板大小);
            //cube.transform.SetParent(路径父对象.transform);
            cube.name = $"路径点_{路径点.w}_{路径点.h}";

            // 设置Cube颜色
            //Renderer renderer = cube.GetComponent<Renderer>();
            //renderer.material.color = 地板颜色;

            //// 禁用碰撞体（可选）
            //Collider collider = cube.GetComponent<Collider>();
            //collider.enabled = false;
        }


    }








    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

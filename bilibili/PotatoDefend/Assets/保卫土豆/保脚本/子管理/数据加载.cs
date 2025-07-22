using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;
using System;

// 最外层容器类，使用字典存储所有关卡
[Serializable]
public class 关卡容器类 : Dictionary<string, 关卡数据类>
{
    // 继承自Dictionary<string, 关卡数据类>，自动支持任意关卡名和数量

    // 获取所有关卡名称
    public List<string> 获取所有关卡名称()
    {
        return new List<string>(Keys);
    }

    // 获取所有关卡数据
    public List<关卡数据类> 获取所有关卡数据()
    {
        return new List<关卡数据类>(Values);
    }
}


[Serializable]
public class 关卡数据类
{
    // 基础信息
    public string 名字;
    public 基础信息类 基础信息;

    // 路径点和基座位置列表
    public List<路径信息类> 路径点;
    public List<路径信息类> 基座位置;


    // 嵌套类：基础信息
    [Serializable]
    public class 基础信息类
    {
        public 坐标类 起点;
        public 坐标类 宽高;
        public float 地板大小;
    }

    // 嵌套类：坐标位置
    [Serializable]
    public class 坐标类
    {
        public float x;
        public float y;
    }

    // 嵌套类：路径点信息
    [Serializable]
    public class 路径信息类
    {
        public float w;
        public float h;
        public string 名字;
        public int 血量;
        public string 类型;
        public int 价值;
    }
}


public class 数据加载 : MonoBehaviour
{
    public 关卡容器类 所有关卡容器;  // 使用字典存储所有关卡
    public 关卡数据类 当前关卡数据;   // 当前选中的关卡
    public string 当前关卡名;          // 当前选中的关卡名称

    public void 读取数据()
    {
        通过Json加载();
    }

    public void 通过Json加载()
    {
        所有关卡容器 = null;
        当前关卡数据 = null;
        当前关卡名 = "";

        string 文件路径 = Path.Combine(
            Application.streamingAssetsPath,
            "保卫土豆数据",
            保卫管理.实例.是否重置玩家数据 ? "玩家信息初始化.json" : "玩家信息.json"
        );

        if (File.Exists(文件路径))
        {
            try
            {
                using (StreamReader 流读取器 = new StreamReader(文件路径))
                {
                    string json字符串 = 流读取器.ReadToEnd();
                    // 解析为字典类型
                    所有关卡容器 = JsonMapper.ToObject<关卡容器类>(json字符串);

                    if (所有关卡容器 != null && 所有关卡容器.Count > 0)
                    {
                        Debug.Log($"成功加载 {所有关卡容器.Count} 个关卡");

                        // 默认选中第一个关卡
                        var 第一个关卡名 = 所有关卡容器.GetEnumerator().Current.Key;
                        切换到关卡(第一个关卡名);

                        // 输出所有关卡信息
                        foreach (var 关卡名 in 所有关卡容器.获取所有关卡名称())
                        {
                            Debug.Log($"找到关卡: {关卡名} - {所有关卡容器[关卡名].名字}");
                        }
                    }
                    else
                    {
                        Debug.LogError("解析成功，但未找到任何关卡数据");
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"解析JSON时出错: {e.Message}");
            }
        }
        else
        {
            Debug.LogError("文件不存在: " + 文件路径);
        }
    }

    // 切换到指定关卡
    public void 切换到关卡(string 关卡名)
    {
        if (所有关卡容器 == null || !所有关卡容器.ContainsKey(关卡名))
        {
            Debug.LogError($"关卡 {关卡名} 不存在");
            return;
        }

        当前关卡名 = 关卡名;
        当前关卡数据 = 所有关卡容器[关卡名];
        Debug.Log($"已切换到关卡: {关卡名} ({当前关卡数据.名字})");
    }

    // 添加新关卡
    public void 添加新关卡(string 关卡名, 关卡数据类 新关卡数据)
    {
        if (所有关卡容器 == null)
        {
            所有关卡容器 = new 关卡容器类();
        }

        if (所有关卡容器.ContainsKey(关卡名))
        {
            Debug.LogWarning($"关卡 {关卡名} 已存在，将被覆盖");
        }

        所有关卡容器[关卡名] = 新关卡数据;
        Debug.Log($"已添加/更新关卡: {关卡名}");
    }

    // 删除关卡
    public void 删除关卡(string 关卡名)
    {
        if (所有关卡容器 != null && 所有关卡容器.ContainsKey(关卡名))
        {
            所有关卡容器.Remove(关卡名);
            Debug.Log($"已删除关卡: {关卡名}");

            // 如果删除的是当前关卡，自动切换到第一个关卡
            if (当前关卡名 == 关卡名)
            {
                if (所有关卡容器.Count > 0)
                {
                    切换到关卡(所有关卡容器.获取所有关卡名称()[0]);
                }
                else
                {
                    当前关卡数据 = null;
                    当前关卡名 = "";
                }
            }
        }
    }

    // 保存所有关卡数据
    public void 保存关卡数据()
    {
        if (所有关卡容器 == null || 所有关卡容器.Count == 0)
        {
            Debug.LogError("没有可保存的关卡数据");
            return;
        }

        string 文件路径 = Path.Combine(
            Application.streamingAssetsPath,
            "保卫土豆数据",
            "玩家信息.json"
        );

        try
        {
            // 序列化为带缩进的JSON
            string json字符串 = JsonMapper.ToJson(所有关卡容器, true);

            using (StreamWriter 流写入器 = new StreamWriter(文件路径))
            {
                流写入器.Write(json字符串);
            }

            Debug.Log($"成功保存 {所有关卡容器.Count} 个关卡数据到: {文件路径}");
        }
        catch (Exception e)
        {
            Debug.LogError($"保存关卡数据失败: {e.Message}");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;
using System;

// 注意：不推荐继承Dictionary，改用内部包含Dictionary的方式，避免序列化问题
[Serializable]
public class 关卡容器类
{
    // 用内部字典存储关卡数据，解决LitJson解析Dictionary继承类的问题
    public Dictionary<string, 关卡数据类> 关卡字典 = new Dictionary<string, 关卡数据类>();

    // 获取所有关卡名称
    public List<string> 获取所有关卡名称()
    {
        return new List<string>(关卡字典.Keys);
    }

    // 获取所有关卡数据
    public List<关卡数据类> 获取所有关卡数据()
    {
        return new List<关卡数据类>(关卡字典.Values);
    }

    // 检查是否包含关卡
    public bool 包含关卡(string 关卡名)
    {
        return 关卡字典.ContainsKey(关卡名);
    }

    // 获取关卡数据
    public bool 尝试获取关卡(string 关卡名, out 关卡数据类 关卡数据)
    {
        return 关卡字典.TryGetValue(关卡名, out 关卡数据);
    }

    // 添加或更新关卡
    public void 添加或更新关卡(string 关卡名, 关卡数据类 数据)
    {
        if (关卡字典.ContainsKey(关卡名))
        {
            关卡字典[关卡名] = 数据;
        }
        else
        {
            关卡字典.Add(关卡名, 数据);
        }
    }

    // 移除关卡
    public bool 移除关卡(string 关卡名)
    {
        return 关卡字典.Remove(关卡名);
    }

    // 获取关卡数量
    public int 数量()
    {
        return 关卡字典.Count;
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


public class 数据加载
{
    public 关卡容器类 所有关卡容器 = new 关卡容器类();  // 初始化容器，避免null
    public 关卡数据类 当前关卡数据;   // 当前选中的关卡
    public string 当前关卡名;          // 当前选中的关卡名称

    public void 读取数据()
    {
        通过Json加载();
    }

    public void 通过Json加载()
    {
        // 重置数据，但保留容器实例
        所有关卡容器.关卡字典.Clear();
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
                    Debug.Log("加载的JSON内容: " + json字符串.Substring(0, Mathf.Min(200, json字符串.Length)) + "...");

                    // 直接解析为Dictionary，再转换为我们的容器类
                    var 解析结果 = JsonMapper.ToObject<Dictionary<string, 关卡数据类>>(json字符串);

                    if (解析结果 != null && 解析结果.Count > 0)
                    {
                        // 将解析结果导入到关卡容器
                        foreach (var 键值对 in 解析结果)
                        {
                            所有关卡容器.添加或更新关卡(键值对.Key, 键值对.Value);
                        }

                        Debug.Log($"成功加载 {所有关卡容器.数量()} 个关卡");

                        // 安全获取第一个关卡名
                        if (所有关卡容器.数量() > 0)
                        {
                            string 第一个关卡名 = 所有关卡容器.获取所有关卡名称()[0];
                            切换到关卡(第一个关卡名);
                        }

                        // 输出所有关卡信息
                        foreach (var 关卡名 in 所有关卡容器.获取所有关卡名称())
                        {
                            if (所有关卡容器.尝试获取关卡(关卡名, out var 关卡数据))
                            {
                                Debug.Log($"找到关卡: {关卡名} - {关卡数据.名字}");
                            }
                            else
                            {
                                Debug.LogWarning($"关卡 {关卡名} 数据为空");
                            }
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
                Debug.LogError($"错误堆栈: {e.StackTrace}");
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
        // 严格的空值和有效性检查
        if (string.IsNullOrEmpty(关卡名))
        {
            Debug.LogError("切换关卡失败：关卡名为空");
            return;
        }

        if (所有关卡容器 == null)
        {
            Debug.LogError("切换关卡失败：关卡容器为空");
            return;
        }

        if (!所有关卡容器.包含关卡(关卡名))
        {
            Debug.LogError($"切换关卡失败：关卡 {关卡名} 不存在");
            return;
        }

        if (所有关卡容器.尝试获取关卡(关卡名, out var 关卡数据))
        {
            当前关卡名 = 关卡名;
            当前关卡数据 = 关卡数据;
            Debug.Log($"已切换到关卡: {关卡名} ({当前关卡数据.名字})");
        }
        else
        {
            Debug.LogError($"切换关卡失败：关卡 {关卡名} 数据为空");
        }
    }

    // 添加新关卡
    public void 添加新关卡(string 关卡名, 关卡数据类 新关卡数据)
    {
        if (string.IsNullOrEmpty(关卡名))
        {
            Debug.LogError("添加关卡失败：关卡名为空");
            return;
        }

        if (新关卡数据 == null)
        {
            Debug.LogError("添加关卡失败：关卡数据为空");
            return;
        }

        if (所有关卡容器 == null)
        {
            所有关卡容器 = new 关卡容器类();
        }

        if (所有关卡容器.包含关卡(关卡名))
        {
            Debug.LogWarning($"关卡 {关卡名} 已存在，将被覆盖");
        }

        所有关卡容器.添加或更新关卡(关卡名, 新关卡数据);
        Debug.Log($"已添加/更新关卡: {关卡名}");
    }

    // 删除关卡
    public void 删除关卡(string 关卡名)
    {
        if (string.IsNullOrEmpty(关卡名))
        {
            Debug.LogError("删除关卡失败：关卡名为空");
            return;
        }

        if (所有关卡容器 == null)
        {
            Debug.LogError("删除关卡失败：关卡容器为空");
            return;
        }

        if (所有关卡容器.包含关卡(关卡名))
        {
            bool 成功删除 = 所有关卡容器.移除关卡(关卡名);
            if (成功删除)
            {
                Debug.Log($"已删除关卡: {关卡名}");

                // 如果删除的是当前关卡，自动切换到第一个关卡
                if (当前关卡名 == 关卡名)
                {
                    if (所有关卡容器.数量() > 0)
                    {
                        string 第一个关卡名 = 所有关卡容器.获取所有关卡名称()[0];
                        切换到关卡(第一个关卡名);
                    }
                    else
                    {
                        当前关卡数据 = null;
                        当前关卡名 = "";
                        Debug.Log("所有关卡已删除，当前无选中关卡");
                    }
                }
            }
            else
            {
                Debug.LogError($"删除关卡失败：{关卡名}");
            }
        }
        else
        {
            Debug.LogWarning($"删除关卡失败：关卡 {关卡名} 不存在");
        }
    }

    // 保存所有关卡数据
    public void 保存关卡数据()
    {
        if (所有关卡容器 == null || 所有关卡容器.数量() == 0)
        {
            Debug.LogError("没有可保存的关卡数据");
            return;
        }

        Debug.Log(" ************ 保存关卡数据 ************ ");

        string 文件路径 = Path.Combine(
            Application.streamingAssetsPath,
            "保卫土豆数据",
            "玩家信息.json"
        );

        try
        {
            // 直接序列化内部字典，确保与JSON格式匹配
            string 保存的json字符串 = JsonMapper.ToJson(所有关卡容器.关卡字典);

            // 确保目录存在
            string 目录路径 = Path.GetDirectoryName(文件路径);
            if (!Directory.Exists(目录路径))
            {
                Directory.CreateDirectory(目录路径);
            }

            // 写入文件
            using (StreamWriter 流写入器 = new StreamWriter(文件路径))
            {
                流写入器.Write(保存的json字符串);
            }

            Debug.Log($"成功保存 {所有关卡容器.数量()} 个关卡数据到: {文件路径}");
        }
        catch (Exception e)
        {
            Debug.LogError($"保存关卡数据失败: {e.Message}");
            Debug.LogError($"错误堆栈: {e.StackTrace}");
        }
    }
}
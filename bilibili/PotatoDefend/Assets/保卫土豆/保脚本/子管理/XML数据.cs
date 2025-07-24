using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using System.Xml;
using System;




// 起点数据
[System.Serializable]
public class 起点数据
{
    public float x;
    public float y;
}

[System.Serializable]
public class 基础信息
{
    public 起点数据 起点 = new 起点数据();
    public int 宽;
    public int 高;
    public float 地板大小;
}


// 路径点数据
[System.Serializable]
public class 路径点数据
{
    // id 给一个 血量值 
    public float w, h;
    public int 血量,价值;
    public string  名字, 类型;

    public 路径点数据(float w, float h, int 血量, int 价值, string 名字, string 类型)
    {
        this.w = w;
        this.h = h;
        this.名字 = 名字;
        this.血量 = 血量;
        this.类型 = 类型;
        this.价值 = 价值;
    }
}

 
// 防御塔数据
[System.Serializable]
public class 防御塔数据
{
    public int id;
    public string 类型;
    public string 描述;
    public int 花费;
    public float 伤害;
    public float 射程;
    // 可选属性
    public float 减速 = 0f;
    public float 范围伤害 = 0f;
}

// 敌人数据
[System.Serializable]
public class 敌人数据
{
    public string 类型;
    public int 数量;
    public float 生成间隔;
    public float 生命;
    public float 速度;
    public int 奖励;
    // 可选属性
    public float 护甲 = 0f;
    public bool 无视地形 = false;
}


// 波次数据
[System.Serializable]
public class 波次数据
{
    public int 编号;
    public float 延迟;
    public List<敌人数据> 敌人列表 = new List<敌人数据>();
}

// 关卡数据
[System.Serializable]
public class 关卡数据
{
    public int id;
    public string 名称;
    public 基础信息 基础变量 = new 基础信息(); // 新增位置字段
    public List<路径点数据> 路径点列表 = new List<路径点数据>();
    public List<路径点数据> 基座位置 = new List<路径点数据>();

    public List<防御塔数据> 防御塔列表 = new List<防御塔数据>();
    public List<波次数据> 波次列表 = new List<波次数据>();
}



// XML解析器
public class 关卡解析器
{
    public static 关卡数据 解析关卡(string xml路径)
    {
        关卡数据 关卡 = new 关卡数据();
        XmlDocument xml文档 = new XmlDocument();

        try
        {

            // 加载XML文件 
            //Debug.Log($"xml资源 加载 : {xml路径}");
            //TextAsset xml资源 = Resources.Load<TextAsset>("Levels/" + xml路径);
            TextAsset xml资源 = Resources.Load<TextAsset>(xml路径);
            xml文档.LoadXml(xml资源.text);


            try
            {
                // 解析关卡基本信息
                XmlNode 关卡节点 = xml文档.SelectSingleNode("关卡");
                关卡.id = int.Parse(关卡节点.Attributes["id"].Value);
                关卡.名称 = 关卡节点.Attributes["名称"].Value;
            }
            catch (System.Exception e)
            {
                Debug.LogError("解析关卡基本信息 失败: " + e.Message);
            }

            try
            {
                XmlNode 基础变量 = xml文档.SelectSingleNode("关卡/基础信息");
                XmlNode 起点节点 = 基础变量.SelectSingleNode("起点");
                关卡.基础变量.起点.x = float.Parse(起点节点.Attributes["x"].Value);
                关卡.基础变量.起点.y = float.Parse(起点节点.Attributes["y"].Value);
                XmlNode 宽节点 = 基础变量.SelectSingleNode("宽");
                XmlNode 高节点 = 基础变量.SelectSingleNode("高");
                关卡.基础变量.宽 = int.Parse(宽节点.Attributes["w"].Value);
                关卡.基础变量.高 = int.Parse(高节点.Attributes["h"].Value);
                XmlNode 地板大小 = 基础变量.SelectSingleNode("地板大小");
                关卡.基础变量.地板大小 = float.Parse(地板大小.Attributes["地板大小"].Value);

            }
            catch (System.Exception e)
            {
                Debug.LogError("解析关卡/基础信息 失败: " + e.Message);
            }



            try
            {
                // 解析路径点
                XmlNodeList 路径点节点列表 = xml文档.SelectNodes("关卡/路径/路径点");
                foreach (XmlNode 节点 in 路径点节点列表)
                {
                    float w = float.Parse(节点.Attributes["w"].Value);
                    float h = float.Parse(节点.Attributes["h"].Value);
                    //public int 血量, 价值;
                    //public string 名字, 类型;
                    int 血量 = int.Parse(节点.Attributes["血量"].Value);
                    int 价值 = int.Parse(节点.Attributes["价值"].Value);
                    // 直接获取字符串值，不需要解析
                    string 名字 = 节点.Attributes["名字"].Value;
                    string 类型 = 节点.Attributes["类型"].Value;

                    关卡.路径点列表.Add(new 路径点数据(w, h, 血量, 价值, 名字, 类型));
                }

            }
            catch (System.Exception e)
            {
                Debug.LogError("解析路径点 失败: " + e.Message);
            }

            try
            {
                // 解析路径点
                XmlNodeList 路径点节点列表 = xml文档.SelectNodes("关卡/基座位置/路径点");
                foreach (XmlNode 节点 in 路径点节点列表)
                {
                    float w = float.Parse(节点.Attributes["w"].Value);
                    float h = float.Parse(节点.Attributes["h"].Value);
                    //public int 血量, 价值;
                    //public string 名字, 类型;
                    int 血量 = int.Parse(节点.Attributes["血量"].Value);
                    int 价值 = int.Parse(节点.Attributes["价值"].Value);
                    // 直接获取字符串值，不需要解析
                    string 名字 = 节点.Attributes["名字"].Value;
                    string 类型 = 节点.Attributes["类型"].Value;

                    关卡.基座位置.Add(new 路径点数据(w, h, 血量, 价值, 名字, 类型));
                }

            }
            catch (System.Exception e)
            {
                Debug.LogError("解析路径点 失败: " + e.Message);
            }


            try
            {
                // 解析防御塔
                XmlNodeList 防御塔节点列表 = xml文档.SelectNodes("关卡/防御塔列表/防御塔");
                foreach (XmlNode 节点 in 防御塔节点列表)
                {
                    防御塔数据 防御塔 = new 防御塔数据
                    {
                        id = int.Parse(节点.Attributes["id"].Value),
                        类型 = 节点.Attributes["类型"].Value,
                        花费 = int.Parse(节点.Attributes["花费"].Value),
                        伤害 = float.Parse(节点.Attributes["伤害"].Value),
                        射程 = float.Parse(节点.Attributes["射程"].Value),
                        描述 = 节点.Attributes["描述"]?.Value ?? 节点.Attributes["类型"].Value
                    };

                    // 解析可选属性
                    if (节点.Attributes["减速"] != null)
                        防御塔.减速 = float.Parse(节点.Attributes["减速"].Value);
                    if (节点.Attributes["范围伤害"] != null)
                        防御塔.范围伤害 = float.Parse(节点.Attributes["范围伤害"].Value);

                    关卡.防御塔列表.Add(防御塔);
                }

            }
            catch (System.Exception e)
            {
                Debug.LogError("解析防御塔 失败: " + e.Message);
            }


            try
            {
                // 解析波次
                XmlNodeList 波次节点列表 = xml文档.SelectNodes("关卡/波次列表/波次");
                foreach (XmlNode 波次节点 in 波次节点列表)
                {
                    波次数据 波次 = new 波次数据
                    {
                        编号 = int.Parse(波次节点.Attributes["编号"].Value),
                        延迟 = float.Parse(波次节点.Attributes["延迟"].Value)
                    };

                    // 解析波次中的敌人
                    XmlNodeList 敌人节点列表 = 波次节点.SelectNodes("敌人");
                    foreach (XmlNode 敌人节点 in 敌人节点列表)
                    {
                        敌人数据 敌人 = new 敌人数据
                        {
                            类型 = 敌人节点.Attributes["类型"].Value,
                            数量 = int.Parse(敌人节点.Attributes["数量"].Value),
                            生成间隔 = float.Parse(敌人节点.Attributes["生成间隔"].Value),
                            生命 = float.Parse(敌人节点.Attributes["生命"].Value),
                            速度 = float.Parse(敌人节点.Attributes["速度"].Value),
                            奖励 = int.Parse(敌人节点.Attributes["奖励"].Value)
                        };

                        // 解析可选属性
                        if (敌人节点.Attributes["护甲"] != null)
                            敌人.护甲 = float.Parse(敌人节点.Attributes["护甲"].Value);
                        if (敌人节点.Attributes["无视地形"] != null)
                            敌人.无视地形 = bool.Parse(敌人节点.Attributes["无视地形"].Value);

                        波次.敌人列表.Add(敌人);
                    }

                    关卡.波次列表.Add(波次);
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError("解析波次 失败: " + e.Message);
            }

        }
        catch (System.Exception e)
        {
            Debug.LogError("解析关卡失败: " + e.Message);
        }

        return 关卡;
    }
}





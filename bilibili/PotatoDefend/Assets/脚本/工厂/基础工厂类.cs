using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 游戏物体工厂的接口
/// </summary>

public class 基础工厂类 : 基础工厂基类  // IBaseFacotry
{
    //当前拥有的gameObject类型的资源(UI,UIPanel,Game) 切记：它存放的是游戏物体资源
    //protected Dictionary<string, GameObject> facotryDict = new Dictionary<string, GameObject>();
    protected Dictionary<string, GameObject> 工厂字典 = new Dictionary<string, GameObject>();  // facotryDict

    //对象池字典
    //protected Dictionary<string, Stack<GameObject>> objectPoolDict = new Dictionary<string, Stack<GameObject>>();
    //对象池(就是我们具体存贮的游戏物体类型的对象)注：对应的是一个具体的游戏物体对象
    protected Dictionary<string, Stack<GameObject>> 对象池字典 = new Dictionary<string, Stack<GameObject>>();  // objectPoolDict


    //加载路径  protected string loadPath;
    protected string 加载路径;


    public 基础工厂类()
    {
        加载路径 = "Prefabs/";
    }
 
    // 放入池子
    public void 回收物品(string 物品名称, GameObject 物品)  // PushItem(string itemName, GameObject item)
    {
        物品.SetActive(false);  // item.SetActive(false);
        物品.transform.SetParent(游戏管理.实例.transform);  // item.transform.SetParent(GameManager.Instance.transform);
        if (对象池字典.ContainsKey(物品名称))  // if (objectPoolDict.ContainsKey(itemName))
        {
            对象池字典[物品名称].Push(物品);  // objectPoolDict[itemName].Push(item);
        }
        else
        {
            Debug.Log("当前字典没有" + 物品名称 + "的栈");  // Debug.Log("当前字典没有"+itemName+"的栈");
        }
    }


    // 取实例
    public GameObject 获取物品(string 物品名称)  // GetItem(string itemName)
    {
        GameObject 物品对象 = null;  // GameObject itemGo = null;
        if (对象池字典.ContainsKey(物品名称))  // if (objectPoolDict.ContainsKey(itemName))//包含此对象池
        {
            if (对象池字典[物品名称].Count == 0)  // if (objectPoolDict[itemName].Count==0)
            {
                GameObject 资源 = 获取资源(物品名称);  // GameObject go = GetResource(itemName);
                物品对象 = 游戏管理.实例.创建物品(资源);  // itemGo = GameManager.Instance.CreateItem(go);
            }
            else
            {
                物品对象 = 对象池字典[物品名称].Pop();  // itemGo = objectPoolDict[itemName].Pop();
                物品对象.SetActive(true);  // itemGo.SetActive(true);
            }
        }
        else  // 不包含此对象池
        {
            对象池字典.Add(物品名称, new Stack<GameObject>());  // objectPoolDict.Add(itemName,new Stack<GameObject>());
            GameObject 资源 = 获取资源(物品名称);  // GameObject go = GetResource(itemName);
            物品对象 = 游戏管理.实例.创建物品(资源);  // itemGo = GameManager.Instance.CreateItem(go);
        }

        if (物品对象 == null)  // if (itemGo==null)
        {
            Debug.Log(物品名称 + "的实例获取失败");  // Debug.Log(itemName+"的实例获取失败");
        }

        return 物品对象;  // return itemGo;
    }

    // 取资源
    private GameObject 获取资源(string 物品名称)  // GetResource(string itemName)
    {
        GameObject 物品资源 = null;  // GameObject itemGo = null;
        string 物品加载路径 = 加载路径 + 物品名称;  // string itemLoadPath = loadPath + itemName;
        if (工厂字典.ContainsKey(物品名称))  // if (facotryDict.ContainsKey(itemName))
        {
            物品资源 = 工厂字典[物品名称];  // itemGo = facotryDict[itemName];
        }
        else
        {
            物品资源 = Resources.Load<GameObject>(物品加载路径);  // itemGo = Resources.Load<GameObject>(itemLoadPath);
            工厂字典.Add(物品名称, 物品资源);  // facotryDict.Add(itemName,itemGo);
        }
        if (物品资源 == null)  // if (itemGo==null)
        {
            Debug.Log(物品名称 + "的资源获取失败");  // Debug.Log(itemName+"的资源获取失败");
            Debug.Log("失败路径：" + 物品加载路径);  // Debug.Log("失败路径："+itemLoadPath);
        }
        return 物品资源;  // return itemGo;
    }


}








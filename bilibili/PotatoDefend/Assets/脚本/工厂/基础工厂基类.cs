using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface 基础工厂基类 
{
    GameObject 获取物品(string 物品名称);  // 原: GetItem(string itemName)

    void 回收物品(string 物品名称, GameObject 物品);  // 原: PushItem(string itemName, GameObject item)

}

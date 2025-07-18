using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 界面面板工厂 : 基础工厂类
{
    public 界面面板工厂()
    {
        Debug.Log($" ********   界面面板工厂  **********  ");
        加载路径 += "界面镶嵌/";
    }
}


//public class UIPanelFactory : BaseFactory
//{

//    public UIPanelFactory()
//    {
//        loadPath += "UIPanel/";
//    }
//}


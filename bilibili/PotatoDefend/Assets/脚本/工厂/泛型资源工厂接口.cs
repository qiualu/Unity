using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 
public interface 泛型资源工厂接口<T>
{
    //T GetSingleResources(string resourcePath);
    T 获取单个资源(string 资源路径);  // GetSingleResources(string resourcePath)
}
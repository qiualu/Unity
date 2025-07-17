using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using DG.Tweening; // 必须引用命名空间



public class cude测试 : MonoBehaviour
{

    // 鼠标进入地板范围时
    private void OnMouseEnter()
    {
        Debug.Log($"鼠标进入地板 ");

    }

    // 鼠标离开地板范围时
    private void OnMouseExit()
    {
        Debug.Log($"鼠标离开地板 ");


    }

    // 鼠标按下时
    private void OnMouseDown()
    {
        Debug.Log($"鼠标按下时  ");

        // 示例：让物体在2秒内移动到(5,0,0)位置
        transform.DOMove(new Vector3(5, 0, 0), 2f)
            .SetEase(Ease.InOutQuad)
            .OnComplete(() => Debug.Log("移动完成！"));


    }

    // 鼠标松开时
    private void OnMouseUp()
    {
        Debug.Log($"鼠标松开时  ");


    }





}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 画布持久化场景转移 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XmlTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}


//public class ������ : MonoBehaviour
//{
//    public GameObject �ӵ�Ԥ����;
//    public Transform �����;

//    private void ����()
//    {
//        // �Ӷ���ػ�ȡ�ӵ�
//        GameObject �ӵ� = ����ع�����.ʵ��.��ȡ����(�ӵ�Ԥ����, �����.position, �����.rotation);

//        // �����ӵ�����
//        �ӵ�.GetComponent<�ӵ�>().��ʼ��(Ŀ��);
//    }
//}

//public class �ӵ� : MonoBehaviour
//{
//    private Transform Ŀ��;
//    private float �ٶ� = 10f;

//    public void ��ʼ��(Transform Ŀ��)
//    {
//        this.Ŀ�� = Ŀ��;
//    }

//    private void Update()
//    {
//        if (Ŀ�� == null)
//        {
//            ����();
//            return;
//        }

//        // �ƶ��߼�...

//        if (��⵽��ײ())
//        {
//            ����˺�();
//            ����();
//        }
//    }

//    private void ����()
//    {
//        ����ع�����.ʵ��.���ն���(gameObject);
//    }
//}


/*

   public float ����ʱ�� = 0.5f;  // ���붯��ʱ��
   public float ����ʱ�� = 0.5f;  // ��������ʱ��

   public float ����ֵ = 0.0f;  // ��������ʱ��
   public float ����ֵ = 0.5f;  // ��������ʱ�� 
   private float ����ֵ = 0.0f;  // ��������ʱ�� ��ʱ����ֵ   

   private bool ��ʾ״̬ = false;


   ����ʱ�� / ����ʱ��;
 == 
   ����ֵ /  0.5 

   ����ֵ  = 

public float ����ֵ = 0.0f;  // ��ȫ͸��
public float ����ֵ = 0.5f;  // ��͸�� (�������Ϊ1.0f��ʾ��ȫ��͸��)
public float �����ٶ� = 1.0f;  // �����ٶȣ�ֵԽ��仯Խ��
private float ����ֵ = 0.0f;  // ��ǰ͸����ֵ
private bool ��ʾ״̬ = false;
private Coroutine ����Э��;

// ��������/�����Ĺ�������
public void �л���ʾ״̬(bool Ŀ��״̬)
{
    // ���״̬δ�仯����ִ���κβ���
    if (��ʾ״̬ == Ŀ��״̬) return;
    
    // ����Ŀ��״̬
    ��ʾ״̬ = Ŀ��״̬;
    
    // ��������������е�Э�̣���ֹͣ��
    if (����Э�� != null)
    {
        StopCoroutine(����Э��);
    }
    
    // �����µĹ���Э��
    ����Э�� = StartCoroutine(ִ�й���());
}

private IEnumerator ִ�й���()
{
    // ����Ŀ��״̬ȷ��Ŀ��ֵ
    float Ŀ��ֵ = ��ʾ״̬ ? ����ֵ : ����ֵ;
    
    // ѭ��ֱ������ֵ�ӽ�Ŀ��ֵ
    while (Mathf.Abs(����ֵ - Ŀ��ֵ) > 0.01f)
    {
        // ���ݵ�ǰ״̬��Ŀ��ֵ��������ֵ
        if (��ʾ״̬)
        {
            // ���룺����ֵ����
            ����ֵ = Mathf.MoveTowards(����ֵ, Ŀ��ֵ, �����ٶ� * Time.deltaTime);
        }
        else
        {
            // ����������ֵ����
            ����ֵ = Mathf.MoveTowards(����ֵ, Ŀ��ֵ, �����ٶ� * Time.deltaTime);
        }
        
        // Ӧ�ù���ֵ������
        if (�ذ����ʵ�� != null)
        {
            Color ��ǰ��ɫ = �ذ����ʵ��.color;
            ��ǰ��ɫ.a = ����ֵ;
            �ذ����ʵ��.color = ��ǰ��ɫ;
        }
        
        // �ȴ���һ֡
        yield return null;
    }
    
    // ȷ������ֵ��ȷ
    ����ֵ = Ŀ��ֵ;
    if (�ذ����ʵ�� != null)
    {
        Color ��ǰ��ɫ = �ذ����ʵ��.color;
        ��ǰ��ɫ.a = ����ֵ;
        �ذ����ʵ��.color = ��ǰ��ɫ;
    }
    
    ����Э�� = null;
}




*/



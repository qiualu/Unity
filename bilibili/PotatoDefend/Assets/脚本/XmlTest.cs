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

/******
 * 
 * 
 








using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class �ذ���꽻���� : MonoBehaviour
//using UnityEngine;
//using System.Collections;

public class �ذ���꽻���� : MonoBehaviour
{

    public GameObject �ذ�Ԫ��;  // ��������
    public GameObject �����˵�;  // ����ȷ������

    // ����ģʽ 
    //public bool ����ģʽ = false;
    public bool ����ģʽ = true;

    // ԭʼ���ű�������ʼ״̬��
    private Vector3 ԭʼ����;
    private Vector3 ԭʼ���ű���;

    // ��ͣʱ��Ŀ�����ű���
    public float ��ͣ���� = 1.2f;
    // ���ʱ��Ŀ�����ű���
    public float ������� = 1.5f;
    // ���Ź����ٶȣ���ֵԽ��Խ�죩
    public float �����ٶ� = 10f;

    private bool ������ͣ = false;
    private bool ���ڵ�� = false;

    private Material �ذ����ʵ��;  // ����ʵ��������Ӱ����������

    public float ����ʱ�� = 0.5f;  // ���붯��ʱ��
    public float ����ʱ�� = 0.5f;  // ��������ʱ��

    public float ������ɫ�ٶ� = 1.0f;  // �����ٶȣ�ֵԽ��仯Խ��
    public float �Զ�����ʱ�� = 2.0f;  // �����ٶȣ�ֵԽ��仯Խ��

    private float �Զ�������ʱ = 0.0f;  // �����ٶȣ�ֵԽ��仯Խ��
    public float ����ֵ = 0.0f;  // ��������ʱ��
    public float ����ֵ = 0.5f;  // ��������ʱ��
 
    private float ����ֵ = 0.0f;  // ��������ʱ�� ��ʱ����ֵ   

    private bool ��ʾ״̬ = false;

     
    private Color ԭʼ��ɫ;  // ����ԭʼ��ɫ
    private Coroutine ��ǰ����Э��;  // ��ǰ���еĶ���Э��
    private Coroutine ����Э��;  // ��ǰ���еĶ���Э��
    private Coroutine �������ɼ�ʱЭ��;  // ��ǰ���еĶ���Э��
    private Coroutine ����Э��;

    public int �ذ�id = -1;

    // �ؿ�����������ã����ڼ�¼���λ��
    private �ؿ������� �ؿ�������;
    // ��¼��ǰ�ذ��������е�λ��
    public Vector2Int ��������;

    private void Awake()
    {
        // ��¼��ʼ���ţ�ȷ���ذ�Ԥ�����ʼ������ȷ��
        ԭʼ���� = transform.localScale;
        ԭʼ���ű��� = ԭʼ���� * 1.5f;

        // ȷ���ذ�Ԫ���Ѹ�ֵ
        if (�ذ�Ԫ�� != null)
        {
            // ��ȡ��Ⱦ���
            Renderer renderer = �ذ�Ԫ��.GetComponent<Renderer>();
            if (renderer != null)
            {
                // ��������ʵ��������Ӱ������ʹ����ͬ���ʵĶ���
                �ذ����ʵ�� = Instantiate(renderer.material);
                renderer.material = �ذ����ʵ��;

                // ����ԭʼ��ɫ������ԭʼ͸���ȣ�
                ԭʼ��ɫ = �ذ����ʵ��.color;
            }
            else
            {
                Debug.LogError("�ذ�Ԫ����û��Renderer�����");
            }
        }
        else
        {
            Debug.LogError("�븳ֵ�ذ�Ԫ�أ�");
        }
        //���ŵ�������();

    }

    // ������ذ巶Χʱ
    private void OnMouseEnter()
    {
        Debug.Log($"������ذ� �ذ�id:{�ذ�id}  ��ʾ״̬: {��ʾ״̬} ");
        ������ͣ = true;
        // ����Ŀ�����ţ�ԭʼ���� * ��ͣ������������Vector3����
        ��ʼ����Э��(ԭʼ���� * ��ͣ����);
 
        
        �л���ʾ״̬();
    }

    // ����뿪�ذ巶Χʱ
    private void OnMouseExit()
    {
        Debug.Log($"����뿪�ذ� �ذ�id:{�ذ�id}  ��ʾ״̬: {��ʾ״̬} ");
        ������ͣ = false;
    
    }

    // ��갴��ʱ
    private void OnMouseDown()
    {
        Debug.Log($"��갴��ʱ �ذ�id:{�ذ�id}  ��ʾ״̬: {��ʾ״̬} ");
        ���ڵ�� = true;

        //Debug.Log($"����ģʽ : {����ģʽ}");

        if (����ģʽ)
        {
            ԭʼ���� = ԭʼ���ű���;
            // ��������Ϊ������ţ��޹��ɣ�
            transform.localScale = ԭʼ���� * �������;
            �ؿ�������.�ؿ�����ʵ��.��¼���λ��(this);
            //�ؿ�������.�ؿ�����ʵ��.��¼���λ��(this);
        }

        �л���ʾ״̬();
    }

    // ����ɿ�ʱ
    private void OnMouseUp()
    {
        Debug.Log($"����ɿ�ʱ �ذ�id:{�ذ�id}  ��ʾ״̬: {��ʾ״̬} ");
        ���ڵ�� = false;
        if (������ͣ){��ʼ����Э��(ԭʼ���� * ��ͣ����); }else{��ʼ����Э��(ԭʼ����);}

    }

    // ��������Э�̣�������ΪVector3Ŀ�����ţ�
    private void ��ʼ����Э��(Vector3 Ŀ������)
    {
        // ֹͣ��ǰЭ�̣����������ɳ�ͻ��
        if (����Э�� != null)
        {
            StopCoroutine(����Э��);
        }
        // ������Э�̣�����Vector3Ŀ��
        ����Э�� = StartCoroutine(ƽ��������Ŀ��(Ŀ������));
    }

    // ƽ�����ŵ�Э�̣�����Vector3Ŀ�����ţ�
    private IEnumerator ƽ��������Ŀ��(Vector3 Ŀ������)
    {
        // �𲽹��ɵ�Ŀ������
        while (Vector3.Distance(transform.localScale, Ŀ������) > 0.01f)
        {
            // ÿ֡��ֵ����
            transform.localScale = Vector3.Lerp(
                transform.localScale,
                Ŀ������,
                �����ٶ� * Time.deltaTime
            );
            yield return null; // �ȴ���һ֡
        }
        // ���վ�ȷ����Ŀ������
        transform.localScale = Ŀ������;
    }

    // ���붯������͸������͸��
    // �����������Ӳ�͸����͸��
    public void �л���ʾ״̬()
    {
        // ���״̬δ�仯����ִ���κβ���
        ��ʾ״̬ = true;
        // ��������������е�Э�̣���ֹͣ��
        if (����Э�� != null)
        {
            StopCoroutine(����Э��);
        }
        // �����µĹ���Э��
        ����Э�� = StartCoroutine(ִ�й���());
        // ֹͣ��ʱ 
        if (�������ɼ�ʱЭ�� != null) // �������ɼ�ʱЭ��
        {
            StopCoroutine(�������ɼ�ʱЭ��);
        }

    }
    public void �л�����״̬()
    {
        // ���״̬δ�仯����ִ���κβ���
        ��ʾ״̬ = false;
        // ��������������е�Э�̣���ֹͣ��
        if (����Э�� != null) // �������ɼ�ʱЭ��
        {
            StopCoroutine(����Э��);
        }
        // �����µĹ���Э��
        ����Э�� = StartCoroutine(ִ�й���());
    }

    //����Э��
    private IEnumerator ִ�й���()
    {
        // ����Ŀ��״̬ȷ��Ŀ��ֵ
        float Ŀ��ֵ = ��ʾ״̬ ? ����ֵ : ����ֵ;

        // ѭ��ֱ������ֵ�ӽ�Ŀ��ֵ
        while (Mathf.Abs(����ֵ - Ŀ��ֵ) > 0.01f)
        {
            Ŀ��ֵ = ��ʾ״̬ ? ����ֵ : ����ֵ;

            // ���ݵ�ǰ״̬��Ŀ��ֵ��������ֵ
            if (��ʾ״̬)
            {
                // ���룺����ֵ����
                ����ֵ = Mathf.MoveTowards(����ֵ, Ŀ��ֵ, ������ɫ�ٶ� * Time.deltaTime);
            }
            else
            {
                // ����������ֵ����
                ����ֵ = Mathf.MoveTowards(����ֵ, Ŀ��ֵ, ������ɫ�ٶ� * Time.deltaTime);
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
        if (��ʾ״̬) // ��ʾ״̬ 
        {
            // ��������������е�Э�̣���ֹͣ��
            if (�������ɼ�ʱЭ�� != null) // �������ɼ�ʱЭ��
            {
                StopCoroutine(�������ɼ�ʱЭ��);
            }
            // �����µĹ���Э��
            �������ɼ�ʱЭ�� = StartCoroutine(�������ɼ�ʱ());
        } 
    }

    private IEnumerator �������ɼ�ʱ()
    {
        Debug.Log($"�������ɼ�ʱ  �ذ�id: {�ذ�id} ");
        // ����Ŀ��״̬ȷ��Ŀ��ֵ
        float Ŀ��ֵ = �Զ�����ʱ��;
        �Զ�������ʱ = 0.0f;
        // ѭ��ֱ������ֵ�ӽ�Ŀ��ֵ
        while (�Զ�������ʱ < Ŀ��ֵ )
        {
            �Զ�������ʱ += Time.deltaTime;
            // �ȴ���һ֡
            yield return null;
        }
        �л�����״̬();
        �������ɼ�ʱЭ�� = null;
    }



    // �Ӷ���ؼ���ʱ����״̬
    private void OnEnable()
    {
        transform.localScale = ԭʼ����;
        ������ͣ = false;
        ���ڵ�� = false;
        if (����Э�� != null)
        {
            StopCoroutine(����Э��);
        }
    }


}



private enum �ذ�״̬����
{
    ֱ������,   // 0
    ������ʾ,   // 1
    ��������,   // 2
    ֱ����ʾ,   // 3
    �ȴ�����    // 8
}

    public int ����ȼ�;
    public int ʣ�����;
    public int ʣ��ţ��;
    public int ����ID;
}


 * *******/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ������
/// </summary>
public class Hook : MonoBehaviour
{
    private static Hook instance;

    public static Hook GetInstance()
    {
        return instance;
    }

    [HideInInspector]
    public bool isHolding = false;
    public float speedChangedPercent = 1f; // �ٶȼ�С����

    Vector3 holdPosition; // ��Ʒ�����к��λ��
    GameObject holdingItem; // ���ӹ��е�����
    int itemValue; // ��ס����ļ�ֵ

    bool strengthen = false;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if(isHolding)
        {
            HookItem();
        }
    }

    // ������Ʒ����Ʒ���湳���ƶ�
    void HookItem()
    {
        holdPosition = transform.Find("ItemHolder").position;
        holdingItem.transform.position = holdPosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ����Ѿ���ס��Ʒ���߹��Ӵ��������׶Σ�ֱ���˳�����
        if(isHolding || HookMovement.GetInstance().GetMoveUp())
        {
            return; 
        }

        // ����ס������� sortingOrder ��ߣ���ֹ�������������ס
        collision.GetComponent<SpriteRenderer>().sortingOrder = 5;
        //������Ч
        // ��������ģ��򲥷���ף��Ч
        if(collision.CompareTag("Diamond") || collision.CompareTag("LargeGold"))
        {
            FindObjectOfType<AudioManager>().Play("CheerSound");
        }
        // �����ʯͷ���򲥷� ײ��ʯͷ����Ч
        if (collision.CompareTag("LargeStone") || collision.CompareTag("MiddleStone"))
        {
            FindObjectOfType<AudioManager>().Play("StoneSound");
        }

        if(collision.CompareTag("TNT"))
        {
            //isHolding = false;
            //Destroy(collision.gameObject);
            collision.GetComponent<SpriteRenderer>().enabled = false;  // ��ըҩͰ����ʾ

            Explode(collision.transform);

            FindObjectOfType<AudioManager>().Play("Explode");
        }

        isHolding = true;
        holdingItem = collision.gameObject;
    }

    // ըҩͰ��ը
    [SerializeField]
    private float explosionRadius = 1.5f;
    private void Explode(Transform point)
    {
        // Physics.OverlapSphere: Returns an array with all colliders touching or inside the sphere.
        
        Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, explosionRadius);
        foreach (Collider2D collider in colliders)
        {
            if(collider.transform.parent.name == "ItemHolder" && collider.gameObject != point.gameObject)
            {
                Destroy(collider.gameObject);
            }
        }
    }

    // �ڳ�������ʾ���ӵ��ı�ը��Χ
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }

    public void HookChange()
    {
        // �������� tag �����ٶ���Ӧ��С  �����Ʒ��ֵ
        switch (holdingItem.tag)
        {
            case "Diamond":
                speedChangedPercent = 0.7f;
                itemValue = 600;
                break;
            case "LargeGold":
                speedChangedPercent = 0.1f;
                itemValue = 500;
                break;
            case "MiddleGold":
                speedChangedPercent = 0.3f;
                itemValue = 100;
                break;
            case "SmallGold":
                speedChangedPercent = 0.7f;
                itemValue = 50;
                break;
            case "LargeStone":
                speedChangedPercent = 0.1f;
                itemValue = 40;
                break;
            case "MiddleStone":
                speedChangedPercent = 0.2f;
                itemValue = 20;
                break;
            case "Bone":
                speedChangedPercent = 0.7f;
                itemValue = 10;
                break;
            case "TNT":
                speedChangedPercent = 1f;
                itemValue = 0;
                break;
            case "Bag":
                RandomBag();
                break;
            default:
                Debug.LogError("����Ʒ��ǩ�����ڣ�");
                break;
        }

        // �������˴���ʿ����ס�������ƶ��ٶ�ʼ������Ϊԭ��������
        if(strengthen)
        {
            speedChangedPercent = 3f;
        }
    }

    private void RandomBag()
    {
        // ������� speedReducePercent itemValue
        // ���������   
        // 0-50 �����Ǯ       50-75 ը��         75-100 ����ʿ

        speedChangedPercent = Random.Range(0.1f, 1.8f);

        int r = Random.Range(0, 100);
        if (r <= 75)
        {
            print("���Ǯ");
            itemValue = Random.Range(20, 600);
        }
        
        // û��ը����������Դ���������ˡ�����
        //else if (r > 50 && r <= 75)
        //{
        //    print("���ը��");
        //    itemValue = 0;
        //}
        else
        {
            print("����ʿ");
            strengthen = true;
            itemValue = 0;
        }
    }

    public void GetItem()
    {
        isHolding = false; 
        
        if(itemValue > 0)  // ���÷ִ������ �ӷ�
        {
            GameManager.GetInstance().Score(itemValue);
        }

        Destroy(holdingItem);
    }

    public int GetItemValue()
    {
        return itemValue;
    }
}

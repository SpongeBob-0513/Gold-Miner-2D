using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 钩金子
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
    public float speedChangedPercent = 1f; // 速度减小比例

    Vector3 holdPosition; // 物品被钩中后的位置
    GameObject holdingItem; // 钩子钩中的物体
    int itemValue; // 钩住物体的价值

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

    // 勾到物品，物品跟随钩子移动
    void HookItem()
    {
        holdPosition = transform.Find("ItemHolder").position;
        holdingItem.transform.position = holdPosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 如果已经钩住物品或者钩子处于上升阶段，直接退出函数
        if(isHolding || HookMovement.GetInstance().GetMoveUp())
        {
            return; 
        }

        // 将钩住的物体的 sortingOrder 提高，防止被其他物体给遮住
        collision.GetComponent<SpriteRenderer>().sortingOrder = 5;
        //播放音效
        // 如果是珍贵的，则播放庆祝音效
        if(collision.CompareTag("Diamond") || collision.CompareTag("LargeGold"))
        {
            FindObjectOfType<AudioManager>().Play("CheerSound");
        }
        // 如果是石头，则播放 撞击石头的音效
        if (collision.CompareTag("LargeStone") || collision.CompareTag("MiddleStone"))
        {
            FindObjectOfType<AudioManager>().Play("StoneSound");
        }

        if(collision.CompareTag("TNT"))
        {
            //isHolding = false;
            //Destroy(collision.gameObject);
            collision.GetComponent<SpriteRenderer>().enabled = false;  // 让炸药桶不显示

            Explode(collision.transform);

            FindObjectOfType<AudioManager>().Play("Explode");
        }

        isHolding = true;
        holdingItem = collision.gameObject;
    }

    // 炸药桶爆炸
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

    // 在场景中显示出子弹的爆炸范围
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }

    public void HookChange()
    {
        // 根据物体 tag 钩子速度相应减小  获得物品价值
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
                Debug.LogError("该物品标签不存在！");
                break;
        }

        // 如果获得了大力士，钩住物体后的移动速度始终扩大为原来的两倍
        if(strengthen)
        {
            speedChangedPercent = 3f;
        }
    }

    private void RandomBag()
    {
        // 随机设置 speedReducePercent itemValue
        // 生成随机数   
        // 0-50 随机的钱       50-75 炸弹         75-100 大力士

        speedChangedPercent = Random.Range(0.1f, 1.8f);

        int r = Random.Range(0, 100);
        if (r <= 75)
        {
            print("随机钱");
            itemValue = Random.Range(20, 600);
        }
        
        // 没得炸弹动画等资源，懒得找了。。。
        //else if (r > 50 && r <= 75)
        //{
        //    print("获得炸弹");
        //    itemValue = 0;
        //}
        else
        {
            print("大力士");
            strengthen = true;
            itemValue = 0;
        }
    }

    public void GetItem()
    {
        isHolding = false; 
        
        if(itemValue > 0)  // 当得分大于零才 加分
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

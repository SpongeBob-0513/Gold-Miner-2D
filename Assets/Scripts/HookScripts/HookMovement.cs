using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HookMovement : MonoBehaviour
{
    private static HookMovement instance;

    public static HookMovement GetInstance()
    {
        return instance;
    }

    // 甩钩子时在 Z 轴旋转的范围
    public float minZ = -65f, maxZ = 65f;
    public float rotateSpeed = 70f;

    private float rotateAngle;
    private bool rotateRight;
    private bool canRotate;//---------------------------------------------------------

    public float moveSpeed = 3f;
    private float initialMoveSpeed;

    private float minY = -3f; // 钩子往下放在 Y 轴上的最小值
    private float initialY;

    private bool moveDown = false; //-------------------------------------------------
    private bool speedReduced = false;

    private Transform initialTransform;

    // 通过 line Renderer 表示出绳子
    private RopeRenderer ropeRenderer;

    private void Awake()
    {
        instance = this;
        ropeRenderer = GetComponent<RopeRenderer>();
        initialTransform = transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        initialY = transform.position.y;
        initialMoveSpeed = moveSpeed;

        canRotate = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.GetInstance().GetCountdown() == 0)
        {
            return;
        }

        Rotate(); // 甩钩子
        GetInput(); // 监听鼠标点击    
        MoveRope(); // 收放钩子
    }

    // 甩钩子 如果不能甩的话直接退出函数
    void Rotate()
    {
        if(!canRotate)
        {
            return;
        }
        
        PlayerAnimation.GetInstance().IdleAnim();
        
        
        if(rotateRight)
        {
            rotateAngle += rotateSpeed * Time.deltaTime;
        }
        else
        {
            rotateAngle -= rotateSpeed * Time.deltaTime;
        }

        transform.rotation = Quaternion.AngleAxis(rotateAngle, Vector3.forward); // Vector3.forward 代表以 Z 轴为轴心旋转

        if(rotateAngle >= maxZ)
        {
            rotateRight = false;
        }
        else if(rotateAngle <= minZ)
        {
            rotateRight = true;
        }
    }

    // 监听鼠标点击  如果鼠标点击并且可以甩 就将钩子设为不能甩，往下放
    void GetInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (canRotate)
            {
                canRotate = false;
                moveDown = true;
            }
        }
    }

    bool ropeStretchSoundNeedCheck = true, pullSoundNeedCheck = true;
    bool hookChanged = false;
    // 绳子拉长  如果能甩直接返回  如果不能甩        如果是下拉的状态，放绳子，如果不是下拉状态，则往上拉
    void MoveRope()
    {
        if(canRotate)
        {
            return;
        }

        // 如果钩子停止旋转，就进行收放绳子
        if(!canRotate)
        {         
            Vector3 temp = transform.position;

            if(moveDown)
            {              
                temp -= transform.up * Time.deltaTime * moveSpeed;

                // 添加放绳子的音效
                if (ropeStretchSoundNeedCheck)
                {
                    ropeStretchSoundNeedCheck = false;
                    FindObjectOfType<AudioManager>().Play("RopeStretchSound");
                }
            }
            else
            {
                temp += transform.up * Time.deltaTime * moveSpeed;

                // 停止放绳子的音效
                FindObjectOfType<AudioManager>().Stop("RopeStretchSound");
                ropeStretchSoundNeedCheck = true;               
            }

            transform.position = temp;

            // 如果绳子到达 Y 轴的最小值或者钩住物品，改变 moveDown
            if (temp.y <= minY || Hook.GetInstance().isHolding)
            {
                // 播放拉绳子音效
                if(pullSoundNeedCheck)
                {
                    pullSoundNeedCheck = false;
                    FindObjectOfType<AudioManager>().Play("PullSound");
                }
                

                // 播放拉钩子动画
                PlayerAnimation.GetInstance().RopeWrapAnim();

                moveDown = false;
                
                // 如果钩住物品，钩子速度对应减小
                if(Hook.GetInstance().isHolding)
                {
                    
                    if(hookChanged == false)
                    {
                        hookChanged = true;
                        Hook.GetInstance().HookChange();
                    }

                    if(!speedReduced)
                    {
                        speedReduced = true;
                        moveSpeed *= Hook.GetInstance().speedChangedPercent;
                    }       
                }
            }

            // 当钩子返回原位置时
            if(temp.y >= initialY)
            {
                // 进行绳子复位，经过调试发现，如果绳子速度很快的话，钩子跟原位置会有偏差，这跟 >= 判断有关，但是用 == 更不靠谱，我选择重置位置！
                // 钩子时这个脚本挂载的 GameObject 的孩子   明天改！！！
                transform.position = initialTransform.position;

                // 停止拉绳子音效
                FindObjectOfType<AudioManager>().Stop("PullSound");
                pullSoundNeedCheck = true;

                // 如果此时钩子中有物品，得分
                if (Hook.GetInstance().isHolding)
                {
                    Hook.GetInstance().GetItem();
                }

               
/** 如果是刚刚往下放钩子此时钩子的位置仍在初始位置，这种情况下也会执行 canRotate = true，因此这里需要添加一个判断条件 if (!moveDown) 
*  调试了好久才发现这个小错误，因为这个错误发生的概率比较小，尽管在 canRotate = true; 
*  这条语句之前至少执行了一次 temp -= transform.up * Time.deltaTime * moveSpeed;
*  但是这跟电脑运行游戏时的帧率有关，很有可能这个移动距离很小，小到低于了计算机精度，从而判断钩子在原位置
*/
                if (!moveDown)
                {
                    canRotate = true;
                }
                   
                // deactivate line renderer        deactivate: 释放 有意使不能操作
                ropeRenderer.RenderLine(temp, false);

                // 钩子参数再次设置为可修改的状态
                hookChanged = false;

                // 重置钩子速度
                speedReduced = false;
                moveSpeed = initialMoveSpeed;
            }

            // 渲染出绳子
            ropeRenderer.RenderLine(temp, true);
        }
    }

    public bool GetMoveUp()
    {
        return !moveDown;
    }
}

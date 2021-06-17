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

    // ˦����ʱ�� Z ����ת�ķ�Χ
    public float minZ = -65f, maxZ = 65f;
    public float rotateSpeed = 70f;

    private float rotateAngle;
    private bool rotateRight;
    private bool canRotate;//---------------------------------------------------------

    public float moveSpeed = 3f;
    private float initialMoveSpeed;

    private float minY = -3f; // �������·��� Y ���ϵ���Сֵ
    private float initialY;

    private bool moveDown = false; //-------------------------------------------------
    private bool speedReduced = false;

    private Transform initialTransform;

    // ͨ�� line Renderer ��ʾ������
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

        Rotate(); // ˦����
        GetInput(); // ���������    
        MoveRope(); // �շŹ���
    }

    // ˦���� �������˦�Ļ�ֱ���˳�����
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

        transform.rotation = Quaternion.AngleAxis(rotateAngle, Vector3.forward); // Vector3.forward ������ Z ��Ϊ������ת

        if(rotateAngle >= maxZ)
        {
            rotateRight = false;
        }
        else if(rotateAngle <= minZ)
        {
            rotateRight = true;
        }
    }

    // ���������  �����������ҿ���˦ �ͽ�������Ϊ����˦�����·�
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
    // ��������  �����˦ֱ�ӷ���  �������˦        �����������״̬�������ӣ������������״̬����������
    void MoveRope()
    {
        if(canRotate)
        {
            return;
        }

        // �������ֹͣ��ת���ͽ����շ�����
        if(!canRotate)
        {         
            Vector3 temp = transform.position;

            if(moveDown)
            {              
                temp -= transform.up * Time.deltaTime * moveSpeed;

                // ��ӷ����ӵ���Ч
                if (ropeStretchSoundNeedCheck)
                {
                    ropeStretchSoundNeedCheck = false;
                    FindObjectOfType<AudioManager>().Play("RopeStretchSound");
                }
            }
            else
            {
                temp += transform.up * Time.deltaTime * moveSpeed;

                // ֹͣ�����ӵ���Ч
                FindObjectOfType<AudioManager>().Stop("RopeStretchSound");
                ropeStretchSoundNeedCheck = true;               
            }

            transform.position = temp;

            // ������ӵ��� Y �����Сֵ���߹�ס��Ʒ���ı� moveDown
            if (temp.y <= minY || Hook.GetInstance().isHolding)
            {
                // ������������Ч
                if(pullSoundNeedCheck)
                {
                    pullSoundNeedCheck = false;
                    FindObjectOfType<AudioManager>().Play("PullSound");
                }
                

                // ���������Ӷ���
                PlayerAnimation.GetInstance().RopeWrapAnim();

                moveDown = false;
                
                // �����ס��Ʒ�������ٶȶ�Ӧ��С
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

            // �����ӷ���ԭλ��ʱ
            if(temp.y >= initialY)
            {
                // �������Ӹ�λ���������Է��֣���������ٶȺܿ�Ļ������Ӹ�ԭλ�û���ƫ���� >= �ж��йأ������� == �������ף���ѡ������λ�ã�
                // ����ʱ����ű����ص� GameObject �ĺ���   ����ģ�����
                transform.position = initialTransform.position;

                // ֹͣ��������Ч
                FindObjectOfType<AudioManager>().Stop("PullSound");
                pullSoundNeedCheck = true;

                // �����ʱ����������Ʒ���÷�
                if (Hook.GetInstance().isHolding)
                {
                    Hook.GetInstance().GetItem();
                }

               
/** ����Ǹո����·Ź��Ӵ�ʱ���ӵ�λ�����ڳ�ʼλ�ã����������Ҳ��ִ�� canRotate = true�����������Ҫ���һ���ж����� if (!moveDown) 
*  �����˺þòŷ������С������Ϊ����������ĸ��ʱȽ�С�������� canRotate = true; 
*  �������֮ǰ����ִ����һ�� temp -= transform.up * Time.deltaTime * moveSpeed;
*  �����������������Ϸʱ��֡���йأ����п�������ƶ������С��С�������˼�������ȣ��Ӷ��жϹ�����ԭλ��
*/
                if (!moveDown)
                {
                    canRotate = true;
                }
                   
                // deactivate line renderer        deactivate: �ͷ� ����ʹ���ܲ���
                ropeRenderer.RenderLine(temp, false);

                // ���Ӳ����ٴ�����Ϊ���޸ĵ�״̬
                hookChanged = false;

                // ���ù����ٶ�
                speedReduced = false;
                moveSpeed = initialMoveSpeed;
            }

            // ��Ⱦ������
            ropeRenderer.RenderLine(temp, true);
        }
    }

    public bool GetMoveUp()
    {
        return !moveDown;
    }
}

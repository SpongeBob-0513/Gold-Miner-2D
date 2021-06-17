using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 管理动画，无需挂载在 GameObject 上
/// </summary>
public class PlayerAnimation : MonoBehaviour
{
    private static PlayerAnimation instance;

    public static PlayerAnimation GetInstance()
    {
        return instance;
    }

    private Animator anim;

    private void Awake()
    {
        instance = this;
        anim = GetComponent<Animator>();
    }

    public void IdleAnim()
    {
        anim.Play("Idle");
    }

    public void RopeWrapAnim()
    {
        anim.Play("RopeWrap");
    }
}

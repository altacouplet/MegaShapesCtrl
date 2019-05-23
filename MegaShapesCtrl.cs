using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MegaShapesCtrl : MonoBehaviour
{
    private MegaShapeFollow msf;
    MegaPathTarget target;

    //スタート時のスピード
    public float StartSpeed = 200f;

    [Header("＠通常モード＠")]
    [Header("出現してから動き出すまでの時間")]
    public float WaitDelayTime;
    private float ElaspedWaitDelayTime;

    [Header("＠途中でTargetShapeを切り替える分岐モード＠")]
    public bool JunctionFlag;
    [Header("分岐後のスピード")]
    public float SwitchedSpeed = 200f;
    [Header("分岐点での待ち時間")]
    public float JunctionWaitTime = 3.0f;
    public float ElaspedJunctionWaitTime;
    [Header("分岐先LineShape")]
    public MegaShapeLine Jmsl;
    private bool switched;

    void Start()
    {
        msf = GetComponent<MegaShapeFollow>();
        msf.Alpha = 0;

        switched = false;
    }
    
    void Update()
    {
        // 通常モード
        if (!JunctionFlag) {
            ElaspedWaitDelayTime += 1 * Time.deltaTime;
            if (ElaspedWaitDelayTime >= WaitDelayTime)
            {
                msf.speed = StartSpeed;
            }
        }

        // 分岐モード
        if (JunctionFlag && !switched)
        {
            msf.speed = StartSpeed;

            if (!switched)
            {
                SwitchTarget();
            }
        }
    }

    void SwitchTarget()
    {
        // Alphaがほぼ最大まで達したとき
        if (msf.Alpha > 0.9989f)
        {
            ElaspedJunctionWaitTime += 1 * Time.deltaTime;
            // その場で停止し、指定時間が過ぎたら
            if (ElaspedJunctionWaitTime > JunctionWaitTime)
            {
                msf.speed = SwitchedSpeed;

                // ターゲットLineShapeを変更
                target = msf.Targets[0];
                target.shape = Jmsl;

                // 分岐完了フラグをON
                switched = true;

                msf.Alpha = 0;
            }
        }
    }

}

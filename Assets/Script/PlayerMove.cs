using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //调用组件
    public Rigidbody Rb;
    public Transform TargetOrentation;
    public Transform Tf;
    public Animator playerMoveAnim;//动画组件
    //常量
    public float gravity;//重力加速度
    public float maxTurningAngle;//最大步进转向角度
    public float walkSpeed;
    public float runSpeed;
    public float sprintSpeed;
    public float turnSpeed;
    public float moveSpeedChangeRate;//移动速度插值变化的上限
    public float moveSpeedFix;//移动速度修正
    //变量
    public float axisH;//运动轴水平方向值
    public float axisV;//运动轴竖直方向值
    public Vector3 axisVector;//运动轴的向量
    public Vector3 localAxisVector;//运动轴向量本地
    public Vector3 fallVelocity;//自由落体的向量
    public float axisValue;//运动轴整合值
    public float moveSpeed;//当前理论上使用的移动速度
    public float orentationAngle;//相机朝向角（全局）
    public float localTargetAngle;//目标方向角（本地）
    public float globalTargetAngle;//目标方向角（全局）
    public float globalOrentationAngle;//当前朝向（全局）
    public float TurnAngle;//剩余转向角
    public float TurnRad;//剩余转向弧度
    public float velocityY;//Rb的纵向速度
    //状态
    public bool isWalking;//是否潜行：0正常，1潜行
    public bool isSprinting;//是否奔跑：0走，1跑
    public int moveMode;//运动模式 ：0潜行，1行走，2跑步
    //Debug
    public float moveVelocity;//当前实际的移动速度
    void Start()
    {
        //调用组件

        //配置参数
        gravity = 9.81f;
        walkSpeed = 2f;
        runSpeed = 5f;
        sprintSpeed = 8f;
        turnSpeed = 5f;
        moveSpeedChangeRate = 0.05f;
        moveSpeedFix = 0.5f;
        isSprinting = false;//默认在走路状态

        moveMode = 1;
    }

    void Update()
    {
        GetInput();
        UpdateMoveMode();
        CalculateTurning();
        UpdateAnimBlendTree();
    }

    private void FixedUpdate()
    {
        UpdateMove();
    }

    public void GetInput()
    {
        //检测移动模式的改变
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isWalking = !isWalking;
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            isSprinting = !isSprinting;
        }
        //检测移动方向输入
        axisH = Input.GetAxis("Horizontal");
        axisV = Input.GetAxis("Vertical");
        axisVector = new Vector3(axisH,0,axisV);
        axisValue = Mathf.Max(Mathf.Abs(axisV), Mathf.Abs(axisH));
    }
    public void UpdateMoveMode()
    {
        //判断移动模式
        if (isWalking)
        {
            moveMode = 0;
        }
        else
        {
            if (isSprinting)
            {
                moveMode = 2;
            }
            else
            {
                moveMode = 1;
            }
        }
        //目标移动速度渐变
        switch (moveMode)
        {
            case 0:moveSpeed += Mathf.Clamp(walkSpeed-moveSpeed,-1* moveSpeedChangeRate, moveSpeedChangeRate);break;
            case 1 :moveSpeed += Mathf.Clamp(runSpeed-moveSpeed,-1* moveSpeedChangeRate, moveSpeedChangeRate);break;
            case 2:moveSpeed += Mathf.Clamp(sprintSpeed-moveSpeed,-1* moveSpeedChangeRate, moveSpeedChangeRate);break;
        }
        //设置移动速度
        moveVelocity = axisValue * moveSpeed;
    }
    public void CalculateTurning()
    {
        //计算本地的轴运动
        localAxisVector = transform.InverseTransformVector(axisVector);
        //用轴的输入计算得到前进朝向
        localTargetAngle = Mathf.Atan2(axisH,axisV) * Mathf.Rad2Deg;//本地前进朝向角
        orentationAngle = TargetOrentation.transform.localRotation.eulerAngles.y;//相机朝向角
        //用本地前进方向角与相机朝向角加和计算全局目标前进朝向角
        globalTargetAngle = localTargetAngle + orentationAngle;//全局目标朝向
        globalOrentationAngle = Tf.localRotation.eulerAngles.y;//全局当前朝向
        //用当前朝向与目标朝向做差得到剩余转向角度
        if (globalTargetAngle - globalOrentationAngle >= 180)
        {
            TurnAngle = globalTargetAngle - globalOrentationAngle - 360;
        }
        else if (globalTargetAngle - globalOrentationAngle <= -180)
        {
            TurnAngle = globalTargetAngle - globalOrentationAngle + 360;
        }
        else
        {
            TurnAngle = globalTargetAngle - globalOrentationAngle;
        }
    }
    public void UpdateMove()
    {
        //velocityY = Rb.velocity.y;
        //Rb.velocity = axisValue * transform.forward * moveVelocity + velocityY * transform.up;

        transform.localPosition +=  transform.forward * moveVelocity*Time.deltaTime*moveSpeedFix;


        if (axisValue != 0)
        {
            TurnRad = TurnAngle * Mathf.Deg2Rad;
            Rb.MoveRotation(Rb.rotation * Quaternion.Euler(0, TurnRad * turnSpeed, 0));
        }
        
    } 
    public void UpdateAnimBlendTree()
    {
        playerMoveAnim.SetFloat("moveSpeed", moveVelocity);
        playerMoveAnim.SetFloat("turnAngle", TurnAngle);
    }
}

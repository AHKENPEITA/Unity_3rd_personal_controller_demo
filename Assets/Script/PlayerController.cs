using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //组件
    public Transform TargetOrentation;
    public Transform Tf;
    //常量

    //变量
    ///模式
    public float wheelAxis;
    ///前进
    public float axisH;
    public float axisV;
    public Vector3 axisVector;
    /// 转向
    public Vector3 localAxisVector;//运动轴向量本地
    public float orentationAngle;//相机朝向角（全局）
    public float localTargetAngle;//目标方向角（本地）
    public float globalTargetAngle;//目标方向角（全局）
    public float globalOrentationAngle;//当前朝向（全局）
    public float TurnAngle;//剩余转向角


    void Start()
    {
    }

    void Update()
    {
        GetInput();
        CalculateTurning();
    }

    private void FixedUpdate()
    {
        
        transform.gameObject.GetComponent<MoveScript>().Move();
    }


    public void GetInput()
    {
        //检测滚轮输入
        wheelAxis = Input.GetAxis("Mouse ScrollWheel");
        if (wheelAxis != 0)
        {
            transform.gameObject.GetComponent<MoveScript>().UpdateMoveGear();
        }
        //检测方向键输入
        axisH = Input.GetAxis("Horizontal");
        axisV = Input.GetAxis("Vertical");
        transform.gameObject.GetComponent<MoveScript>().axisValue = Mathf.Max(Mathf.Abs(axisV), Mathf.Abs(axisH));
        axisVector = new Vector3(axisH, 0, axisV);

    }

    

    public void CalculateTurning()
    {
        //计算本地的轴运动
        localAxisVector = transform.InverseTransformVector(axisVector);
        //用轴的输入计算得到前进朝向
        localTargetAngle = Mathf.Atan2(axisH, axisV) * Mathf.Rad2Deg;//本地前进朝向角
        orentationAngle = TargetOrentation.transform.localRotation.eulerAngles.y;//相机朝向角
        //用本地前进方向角与相机朝向角加和计算全局目标前进朝向角
        globalTargetAngle = localTargetAngle + orentationAngle;//全局目标朝向
        globalOrentationAngle = Tf.localRotation.eulerAngles.y;//全局当前朝向
        //用当前朝向与目标朝向做差得到剩余转向角度
        if (globalTargetAngle - globalOrentationAngle >= 180)
        {
            transform.gameObject.GetComponent<MoveScript>().turnAngle = globalTargetAngle - globalOrentationAngle - 360;
        }
        else if (globalTargetAngle - globalOrentationAngle <= -180)
        {
            transform.gameObject.GetComponent<MoveScript>().turnAngle = globalTargetAngle - globalOrentationAngle + 360;
        }
        else
        {
            transform.gameObject.GetComponent<MoveScript>().turnAngle = globalTargetAngle - globalOrentationAngle;
        }
    }
}

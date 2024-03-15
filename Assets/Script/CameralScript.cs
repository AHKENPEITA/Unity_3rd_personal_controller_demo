using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameralScript : MonoBehaviour
{
    //组件
    public GameObject Object;//目标组件
    public Transform Tf;//本体
    public Transform Orentation;//朝向组件
    public Transform Camera;//相机组件
    public Transform Focus;//焦点组件
    public Transform CameraPosition;//机位组件
    //常量
    public float mouseSensitivity;//鼠标灵敏度
    public float minPitch, maxPitch;//俯仰限制
    //变量
    private float mouseX, mouseY;//鼠标移动
    public  float yawRotation,pitchRotation;//偏航，俯仰的值

    void Start()
    {
        //获取组件
        Tf = GetComponent<Transform>();
        //配置参数
        mouseSensitivity = 500f;
        minPitch = -60f;
        maxPitch = 90f;
    }

    void Update()
    {
        GetInput();//获取输入
        UpdateFollowObject();//跟随目标
        UpdateCameralPositionTransform();//更新相机机位的位置（俯仰及偏航）
        UpdateCameraTransform();//将相机架设到新的目标机位
    }
    public void GetInput()
    {
        //获取鼠标移动输入
        mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
    }
    public void UpdateFollowObject()
    {
        Tf.position = Object.transform.position;
    }
    public void UpdateCameralPositionTransform() 
    {
        //更新偏航、俯仰的旋转值
        yawRotation += mouseX;
        pitchRotation -= mouseY;
        //限制俯仰上下限
        pitchRotation = Mathf.Clamp(pitchRotation, minPitch,maxPitch);
        //将偏航和俯仰值运用于相机基点，将偏航值运用于旋转基点
        Orentation.localRotation = Quaternion.Euler(0, yawRotation, 0);
        Focus.localRotation = Quaternion.Euler(pitchRotation,0, 0);
        
    }
    public void UpdateCameraTransform()
    {
        //应用新的位移和旋转
        Camera.transform.position = CameraPosition.position;
        Camera.transform.rotation = CameraPosition.rotation;
    }
}

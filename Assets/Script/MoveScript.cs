using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour
{
    //组件
    public Rigidbody Rb;
    public Animator PlayerMoveAnim;
    //常量
    public float inherentWalkSpeed;
    public float inherantRunSpeed;
    public float inherentSprintSpeed;
    public float[] moveSpeedLvlTimer;
    public float turnSpeed;
    public float moveSpeedChangeRate;
    //变量
    public float moveSpeed;
    public float animMoveSpeed;
    public float turnAngle;
    public float axisValue;
    public int moveGear;
    public int maxGear;
    //状态

    private void Start()
    {
        inherentWalkSpeed = 2;
        inherantRunSpeed = 5;
        inherentSprintSpeed = 9;
        moveSpeedLvlTimer = new float[11] {1.00f, 1.05f,1.10f, 1.15f, 1.20f, 1.25f, 1.30f ,1.35f,1.40f,1.45f,1.50f};
        turnSpeed = 3f;
        moveSpeedChangeRate = 4f;
        //
        moveGear = 5;
    }
    private void FixedUpdate()
    {
    }
    public void Move()
    {
        UpdateMoveGear();
        UpdateTurn();
        UpdateAnimBlendTree();
    }
    public void UpdateMoveGear()
    {
        int wheelAxis = (int)transform.gameObject.GetComponent<PlayerController>().wheelAxis;
        float loadIndex = transform.gameObject.GetComponent<CharacterAttribute>().loadIndex;
        maxGear = Mathf.Clamp((int)(11 - Mathf.Ceil(loadIndex * 10)),1,10);
        moveGear = Mathf.Clamp(moveGear+wheelAxis,1,maxGear);
        moveSpeed += Mathf.Clamp((moveGear*10 - moveSpeed),-1*moveSpeedChangeRate,moveSpeedChangeRate);

    }
    public void UpdateTurn()
    {
        if (axisValue != 0)
        {
            if (turnAngle > 0)
            {
                Rb.MoveRotation(Rb.rotation * Quaternion.Euler(0, Mathf.Min(turnSpeed,turnAngle), 0));
            }else if (turnAngle < 0)
            {
                Rb.MoveRotation(Rb.rotation * Quaternion.Euler(0, Mathf.Max(-1*turnSpeed, turnAngle, -1 * turnSpeed), 0));
            }

            
        }
    }
    public void UpdateAnimBlendTree()
    {
        int moveLvl = transform.gameObject.GetComponent<SkillManager>().moveSkill.Lvl;
        PlayerMoveAnim.speed = moveSpeedLvlTimer[moveLvl];

        PlayerMoveAnim.SetFloat("moveSpeed", moveSpeed*axisValue);
        PlayerMoveAnim.SetFloat("turnAngle", turnAngle);

        
    }
}

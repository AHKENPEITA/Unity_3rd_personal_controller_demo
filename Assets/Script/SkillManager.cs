using UnityEngine;

public class SkillManager : MonoBehaviour
{
    //技能等级

    ///身体运动
    public SkillLevel moveSkill; //【运动】最大奔跑速度上限，以及平均运动速度，全速奔跑对体力的消耗，跳跃高度，攀爬速度
    public SkillLevel loadSkill;//【负重】最大负重上限，以及负重后运动速度，超负荷运载下对体力的消耗
    public SkillLevel thoughnessSkill;//【韧性】攻击抗性，在极端环境下的生存能力（抗热、抗寒、耐饥，耐渴）
    public SkillLevel immunitySkill;//【免疫】被病毒感染的可能性，生病造成的伤害，病痛状态持续时间
    ///头脑智力
    public SkillLevel LogicSkill;//【逻辑】思考速度，研究情报的速度，解决计算问题所花的时间，学习新技能所花的时间
    public SkillLevel reciteSkill;//【背诵】阅读速度，记忆容量大小
    public SkillLevel eruditionSkill;//【博学】遇到新情报时不需研究就理解的能力，思考时直接判定成功的概率
    public SkillLevel confidenceSkill;//【信念】意志被消耗的速度，信心被动摇的概率
    ///才能技术
    public SkillLevel manageSkill;//【管理】所率领军队的人数上限，行政区数量，管理成本
    public SkillLevel charmSkill;//【魅力】谈判
    public SkillLevel businessSkill;//【经商】标记价格落差，砍价成功的可能性，解锁能力（0买卖，1识别价值，2价格落差，3特色产物，4记账本，5开设作坊，6融资合并，7成立商行，8跨国经营，9股市预测，X市场操控）
    public SkillLevel cureSkill;//【医疗】治疗队友的速度，解锁治疗能力（0消毒，1止血，2包扎，3注射，4夹板，5正骨，6开药，7外科手术，8肢体修复，9纳米治疗，X基因重塑）
    ///劳动实践
    public SkillLevel exploitSkill;//【开采】（采矿、伐木、采油）的速度和体力消耗
    public SkillLevel farmSkill;//【农牧】技能等级，耕作时对体力的消耗，种出植株的成活率，收获得到成果的品质，饲养的成功率，健康程度，钓鱼技能
    public SkillLevel cookSkill;//【烹饪】技能等级，影响烹饪速度，烹饪品质，出现杰作和垃圾的概率
    public SkillLevel machanicSkill;//【机械】制造技能等级，建造建筑，制作物品的速度，精度，解锁更高级蓝图的使用解锁机械使用（0钳子，1钉锤，2手锯，3台钻，4手工车床，5锻造台，6浇筑台，7数控机床，8加工平台，9智能工厂，X工业体）
    ///近身格斗
    public SkillLevel attackSkill;//【进攻】影响出击前摇速度，成功击中敌人的可能性
    public SkillLevel defenceSkill;//【防守】影响完美抵挡的可能性，被击中后减伤的大小
    public SkillLevel strikeSkill;//【格斗】技能等级，徒手格斗的伤害、恢复速度、精力消耗
    public SkillLevel evadeSkill;//【规避】技能等级，受到攻击后直接miss的可能性
    ///武器使用
    public SkillLevel accurateSkill;//【精准】出射角度误差
    public SkillLevel ballisticSkill;//【弹道】预估弹道长度和精度
    public SkillLevel fillSkill;//【装填】装填速度
    public SkillLevel equipmentSkill;//【重武】（自行火炮、装甲战车、导弹、轰炸机、炮艇）的操作滞后性，解锁重武器使用（0重机枪，1迫击炮，2野战炮，3装甲战车，4自行火炮，5截击机，6炮艇，7战略轰炸机，8弹道导弹，9武库舰，X轨道打击）
    ///驾驶代步
    public SkillLevel rideSkill;//【骑术】驯服坐骑的难度，坐骑的操作滞后性，突然发飙的可能性
    public SkillLevel driveSkill;//【驾车】车辆的操作滞后性
    public SkillLevel pilotSkill;//【航空】飞机的操作滞后性
    public SkillLevel sailSkill;//【航海】船只的操纵性
    ///逃亡生存
    public SkillLevel hideSkill;//【潜行】
    public SkillLevel pickSkill;//【撬锁】
    public SkillLevel traceSkill;//【追踪】
    public SkillLevel stealSkill;//【偷窃】
    //
    void Update()
    {
        UpdateAllExp();
    }

    public void UpdateAllExp()//涨所有技能的经验
    {
        UpdateMoveExp();
        UpdateLoadExp();
    }
    public void UpdateMoveExp()//涨运动经验
    {
        float axisValue = transform.gameObject.GetComponent<MoveScript>().axisValue;
        int moveGear = transform.gameObject.GetComponent<MoveScript>().moveGear;
        moveSkill.ExpAdd(axisValue * Mathf.Max(0,moveGear-moveSkill.Lvl)/(10-moveSkill.Lvl)*Time.deltaTime );
    }
    public void UpdateLoadExp()//涨负载经验
    {
        loadSkill.ExpAdd(Mathf.Clamp(transform.gameObject.GetComponent<MoveScript>().axisValue * transform.gameObject.GetComponent<CharacterAttribute>().loadIndex ,0,1)*Time.deltaTime) ;
    }


    
    
}

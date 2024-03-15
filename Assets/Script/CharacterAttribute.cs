using UnityEngine;

public class CharacterAttribute : MonoBehaviour
{
    //基本信息

    ///身份
    public string characterName;//名字
    public string characterClan;//氏族
    public string characterNationality;//民族
    public string characterRace;//种族
    ///负重
    public float bodyWeight;//身体重量
    public float loadWeight;//负载重量
    public float weaponWeight;//武器重量//重要！！！！！此处的武器重量是包含在负载重量中的，不重复参与总重的计算，只是用于计算进攻的频率而独立表示出来的
    public float loadIndex;//负荷指数
    public int loadState;//负载状态



    //身体素质

    ///速度
    //public float walkSpeed;
    //public float runSpeed;
    //public float sprintSpeed;
    ///负载
    




    void Start()
    {
        characterName = "一凡";
        characterClan = "丁";
        characterNationality = "暗夜之子";
        characterRace = "骨人";

        bodyWeight = transform.GetComponent<Rigidbody>().mass;
        loadWeight = 0;//之后加入物品栏系统后再改
        weaponWeight = 0;//之后加入物品栏系统后再改


    }

    void Update()
    {
        UpdateLoadIndex();
        UpdateLoadState();

        
    }







    //方法

    //函数
    public string FFullName(string clan, string name)
    {
        return clan.Substring(0, 1) + name.Substring(0, 1);
    }//用氏族和名字返回全名
    public void UpdateLoadIndex()
    {
        float rate = loadWeight/bodyWeight;
        loadIndex= rate - transform.gameObject.GetComponent<SkillManager>().loadSkill.Lvl * 0.1f;
        //负荷指数是描述当前负载状态的值，首先用负载除以体重，用这个值减去当前等级的无负荷标准，再将其与超负荷至无负荷的跨度相比(目前这个值恒定为1)。
    }//传入体重，负重，和当前负重技能等级计算负重指数
    public void UpdateLoadState()
    {
        if (loadIndex <= 0)
        {
            loadState= 0;
        }
        else if (loadIndex <= 0.5)
        {
            loadState= 1;
        }
        else if (loadIndex <= 1)
        {
            loadState= 2;
        }
        else
        {
            loadState= 3;
        }
    }//传入负荷指数计算当前负荷状态



    //public float State2Load(int Lvl,int State)
    //{
    //    if ((Lvl <= 10 && Lvl >= 0) && (State == 0|| State == 1 || State == 2))
    //    {
    //        if (State == 0)
    //        {
    //            //无负荷上限
    //            return 50 + Lvl * 5;
    //        }
    //        else if (State == 1)
    //        {
    //            //轻负荷上限
    //            return 50 + Lvl * 6 + 10;
    //        }
    //        else
    //        {
    //            //重负荷上限
    //            return 50 + Lvl * 7 + 20;
    //            //超过此上限的便是超负荷
    //        }
    //    }
    //    else
    //    {
    //        Debug.LogError("输入数据错误！！！\n预期输入：0<=Lvl<=10,LoadState=1/2/3\n实际输入：Lvl=" + Lvl + ",LoadState=" + State);
    //        return 0;
    //    }
    //    ///解释：不同负重技能有各自对应的负载状态所承担负载的重量区间，其对应方式如下：
    //    ///
    //    ///负重技能     无负荷（0）     轻负荷（1）     重负荷（2）     超负荷（3）
    //    ///     0               50]                        (50-60]               (60-70]                 (70+ 
    //    ///     1                55]                        (55-66]               (66-77]                 (77+
    //    ///     2               60]                        (60-72]               (72-84]                 (84+
    //    ///     3               65]                        (65-78]                (78-91]                  (91+
    //    ///     4               70]                        (70-84]               (84-98]                  (98+
    //    ///     5               75]                        (75-90]             (90-105]                 (105+
    //    ///     6               80]                        (80-96]              (96-112]                 (112+
    //    ///     7               85]                       (85-102]            (102-119]                 (119+
    //    ///     8               90]                       (90-108]           (108-126]                 (126+
    //    ///     9               95]                      (108-114]            (114-133]                 (133+
    //    ///     X             100]                     (100-120]           (120-140]                  (140+
    //    ///     
    //    ///表中所有的数据可用以下公式得出：
    //    ///无负荷的闭上限（即轻负荷的开下限）为50+Lvl*5;
    //    ///轻负荷的闭上限（即重负荷的开下限）为50+Lvl*6+10;
    //    ///重负荷的闭上限（即超负荷的开下限）为50+Lvl*7+20;
    //    ///（其中Lvl是负重技能等级）
    //    ///
    //    ///该函数作用即为输入【技能等级】和【负载状态】，得到【该状态的负载上限】
    //}//输入负重技能等级以及负荷状态，查询该状态的负荷重量(下限)//暂时不用
    //public int Load2State(int Lvl,float Load)
    //{
    //    //无负荷状态
    //    if (Load<= 0.1 * Lvl)
    //    {
    //        return 0;
    //    }
    //    //轻负荷状态
    //    else if (Load<= 0.1 * Lvl + 0.5)
    //    {
    //        return 1;
    //    }
    //    //重负荷状态
    //    else if(Load <= 0.1 * Lvl + 1.0)
    //    {
    //        return 2;
    //    }
    //    //超负荷状态
    //    else
    //    {
    //        return 3;
    //    }
    //    ///解释：负荷指数是一个用负载与体重相比计算得到的指数，用来判定当前的负载处于什么状态；
    //    ///该指数在不同负重技能等级下与负载状态的对应关系如下：
    //    ///
    //    ///负重技能     无负荷（0）     轻负荷（1）     重负荷（2）     超负荷（3）
    //    ///     0               0.0]                       (0.0-0.5]              (0.5-1.0]                       (1.0
    //    ///     1                 0.1]                        (0.1-0.6]              (0.6-1.1]                       (1.1
    //    ///     2               0.2]                       (0.2-0.7]              (0.7-1.2]                       (1.2
    //    ///     3               0.3]                       (0.3-0.8]              (0.8-1.3]                       (1.3
    //    ///     4               0.4]                       (0.4-0.9]              (0.9-1.4]                       (1.4
    //    ///     5               0.5]                        (0.5-1.0]              (1.0-1.5]                       (1.5
    //    ///     6               0.6]                         (0.6-1.1]               (1.1-1.6]                       (1.6
    //    ///     7               0.7]                        (0.7-1.2]              (1.2-1.7]                       (1.7
    //    ///     8               0.8]                        (0.8-1.3]              (1.3-1.8]                       (1.8
    //    ///     9               0.9]                        (0.9-1.4]              (1.4-1.9]                       (1.9
    //    ///    X                 1.0]                        (1.0-1.5]              (1.5-2.0]                       (2.0
    //    ///     
    //    ///表中所有的数据可用以下公式得出：
    //    ///无负荷的闭上限（即轻负荷的开下限）为0.1*Lvl
    //    ///轻负荷的闭上限（即重负荷的开下限）为0.1*Lvl+0.5;
    //    ///重负荷的闭上限（即超负荷的开下限）为0.1*Lvl+1.0;
    //    ///（其中Lvl是负重技能等级）
    //    ///
    //    /// 
    //    /// 
    //    ///该函数作用即为输入【技能等级】和【当前负载重量】，得到【现在的负载状态】
    //    ///
    //    ///例如，当前体重是50，当前持有的负重技能是0级，那么当负重为20时，负荷指数就是20/50=0.4,为轻负荷状态，返回1；
    //    ///              同样的条件下，当负重为30时，负荷指数就是30/50=0.6，为重负荷状态，返回2；
    //    /// 又如，当前体重是100，当前持有的负重节能是X级（10级），那么当负重为200时，负荷指数时200/100=2.0，是重负荷状态（极限），返回2；
    //    ///              同样的条件下负重为210时，负荷指数为210/100=2.1，为超负荷状态，返回3；
    //    ///              
    //}//输入负重技能等级及当前负载，查询当前负载状态//暂时不用
}



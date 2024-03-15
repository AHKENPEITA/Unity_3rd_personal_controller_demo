using UnityEngine;
[System.Serializable]//此申明可以让该类直接在inspecter面板中显示
public class SkillLevel
{
    //变量
    public int Lvl;//技能等级
    public float Exp;//经验槽
    public int Per;//占升级百分比
    private int[] LvlUpExpConsume= new int [10] { 90, 135, 205, 310, 465, 700, 1055, 1585, 2390, 3600 };
    //默认的构造函数
    public SkillLevel()
    {
        Lvl = 0;
        Exp = 0;
        Per = 0;
    }
    //传入初始等级的重载构造函数
    public SkillLevel(int inLvl)
    {
        if (inLvl <= LvlUpExpConsume.Length)
        {
            Lvl = inLvl;
            Exp = 0;
            Per = 0;
        }
        else
        {
            Debug.LogError("构造技能失败！\n原因：指定等级超过最大技能等级上限\n最大上限："+LvlUpExpConsume.Length+"\n指定等级："+inLvl);
        }
    }
    //升级的方法
    public void LvlUp()
    {
        //只有在不是满级时才能升级,若是满级了则清空经验值
        if (Lvl < LvlUpExpConsume.Length)
        {
            //这是每升一级所消耗的经验值
            if (Exp >= LvlUpExpConsume[Lvl])
            {
                Exp -= LvlUpExpConsume[Lvl];
                Lvl++;
                //继续递归调用，直到剩余经验不再满足升级要求
                LvlUp();
            }
        }
        else
        {
            Exp = 0;
        }
    }
    //涨经验，并同时更新经验百分比
    public void ExpAdd(float value)
    {
        if (Lvl < LvlUpExpConsume.Length)
        {
            Exp += value;
            if (Exp>= LvlUpExpConsume[Lvl])
            {
                LvlUp();
            }
            
        }
        else
        {
            Exp = 0;
        }
        UpdatePer();
    }
    //更新经验百分比的方法
    public void UpdatePer()
    {
        if (Lvl < LvlUpExpConsume.Length)
        {
            Per = (int)(Exp / LvlUpExpConsume[Lvl]*100);
        }
        else
        {
            Per = 100;
        }
    }
}

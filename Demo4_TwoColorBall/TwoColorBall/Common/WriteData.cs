// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:WriteData
// Guid:c426bd62-1823-4187-8390-904a85be9d62
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatTime:2022-05-02 上午 08:59:57
// ----------------------------------------------------------------

namespace TwoColorBall.Common;

/// <summary>
/// 记录数据
/// </summary>
public class WriteData
{
    /// <summary>
    /// 购号记录
    /// </summary>
    /// <param name="name"></param>
    /// <param name="sequence"></param>
    /// <param name="balls"></param>
    /// <param name="time"></param>
    public void RecordBall(string name, string sequence, int[] balls, string time)
    {
        // 标记值
        const string _sequence = "N";
        const string _red = "R";
        const string _blue = "B";
        const string _time = "T";
        string filename = name + ".txt";
        using (StreamWriter file = File.AppendText(filename))
        {
            string red = string.Empty;
            for (int i = 0; i < balls[0..6].Length; i++)
            {
                red += balls[i].ToString("D2");
            }
            string blue = balls[6].ToString("D2");
            string record = _sequence + sequence + _red + red + _blue + blue + _time + time;
            file.WriteLine(record);
        }
    }

    /// <summary>
    /// 开奖记录
    /// </summary>
    /// <param name="name"></param>
    /// <param name="balls"></param>
    /// <param name="time"></param>
    /// <param name="lottery"></param>
    public void RecordLottery(string name, int[] balls, string time, string lottery, string bonusCumulative, string bonusTotal)
    {
        string filename = name + ".txt";
        using (StreamWriter file = File.AppendText(filename))
        {
            string red = string.Empty;
            for (int i = 0; i < balls[0..6].Length; i++)
            {
                red += balls[i].ToString("D2") + " ";
            }
            string blue = balls[6].ToString("D2");
            string record = "红色球：" + red + "蓝色球：" + blue;
            file.Write("【开奖号码】{0}", record);
            file.Write("\t【模拟开奖】时间：{0}", time);
            file.WriteLine();
            file.Write("\t本次开奖奖池金额为：{0}元;奖池中累积的奖金之和为：{1}元;奖金总额为：{2}元;", lottery, bonusCumulative, bonusTotal);
            file.WriteLine();
        }
    }

    /// <summary>
    /// 中奖记录和金额记录
    /// </summary>
    /// <param name="num"></param>
    /// <param name="ball"></param>
    /// <param name="time"></param>
    /// <param name="whichball"></param>
    public void RecordPrize(string num, int[] ball, string time, string whichball, string prizeGrade, string prizeAmount)
    {
        string filename = @"PrizeRecord.txt";
        using (StreamWriter file = File.AppendText(filename))
        {
            file.Write("第【{0}】注：红色球：", num);
            for (int i = 0; i < 6; i++)
            {
                file.Write("{0,2:D2} ", ball[i]);
            }
            file.Write("蓝色球：{0,2:D2}", ball[6]);
            file.Write("\t【{0}】", whichball);
            file.Write("时间：{0}", time);
            file.Write("\t\t[该注中了{0}等奖！获得奖金：{1}元；]", prizeGrade, prizeAmount);
            file.WriteLine();
        }
    }

    /// <summary>
    /// 中奖概括记录
    /// </summary>
    /// <param name="name"></param>
    /// <param name="prizeCount"></param>
    /// <param name="prizeAmounts"></param>
    public void RecordPrizeAmounts(int[] prizeCount, string prizeAmounts)
    {
        string filename = @"PrizeRecord.txt";
        using (StreamWriter file = File.AppendText(filename))
        {
            file.WriteLine($"\t你共中奖{prizeCount[6]}注，获得总奖金：{prizeAmounts}元！其中：" +
                $"一等奖{prizeCount[0]}注；" +
                $"二等奖{prizeCount[1]}注；" +
                $"三等奖{prizeCount[2]}注；" +
                $"四等奖{prizeCount[3]}注；" +
                $"五等奖{prizeCount[4]}注；" +
                $"六等奖{ prizeCount[5]}注；");
        }
    }
}
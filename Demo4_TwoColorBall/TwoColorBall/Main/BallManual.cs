// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:BallManual
// Guid:63af13fc-5ad7-4a56-9631-85cbd96ce4bd
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatTime:2022-05-02 上午 08:25:58
// ----------------------------------------------------------------

using TwoColorBall.Common;

namespace TwoColorBall.Main;

/// <summary>
/// 手动购号
/// </summary>
public class BallManual
{
    private Wallet _myWallet = new();
    private WriteData _writeData = new();
    private int[] _balls = new int[7];

    /// <summary>
    /// 手动购号
    /// </summary>
    public void Manual()
    {
        Console.WriteLine();
        Console.WriteLine("                    =============================手动购号开始=============================");
        int buytimes = CheckWallet();
        Buy(buytimes);
        Console.WriteLine("                    =============================手动购号结束=============================");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("                                           (你可以选择继续购号或开奖)");
        Console.ResetColor();
        Console.WriteLine();
    }

    /// <summary>
    /// 确认购买注数并判断余额是否可用
    /// </summary>
    /// <returns></returns>
    private int CheckWallet()
    {
        int times = 0;
        int entranceMark = 1;
        while (entranceMark != 0)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("请输入你要购买几注双色球:");
            Console.ResetColor();
            times = int.Parse(Console.ReadLine() ?? "0");
            if ((decimal)times * 2 > _myWallet.Balance)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\t你的账户余额不够购买{0}注双色球，请充值；", times);
                Console.ResetColor();
                _myWallet.RechargeOrConsumptManual();
            }
            else
            {
                entranceMark = 0;
                break;
            }
        }
        return times;
    }

    /// <summary>
    /// 购买下注
    /// </summary>
    /// <param name="buytimes"></param>
    private void Buy(int buytimes)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\t你正在手动购买【{0}】注双色球...", buytimes);
        Console.ResetColor();
        for (int sequence = 1; sequence <= buytimes; sequence++)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("请输入第[{0,2}]注你要选择的(1 - 33之间的)6个[红色球]号码：", sequence.ToString("D2"));
            Console.ResetColor();
            Red();
            Blue();
            Display(sequence);
            Console.Write("\t【手动购号】");
            string time = DateTime.Now.ToString();
            Console.Write("时间：{0}", time);
            _writeData.RecordBall(this.GetType().Name, sequence.ToString("D2"), _balls, time);
            Console.WriteLine();
        }
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\t系统已为你完成购买【{0,2}】注双色球！", buytimes);
        Console.ResetColor();
        _myWallet.RechargeOrConsumptAutomatic(-(decimal)buytimes * 2);
    }

    /// <summary>
    /// 红色球
    /// </summary>
    private void Red()
    {
        int[] redballs = new int[6];
        for (int ballth = 0; ballth < redballs.Length; ballth++)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("第{0}个红色球：", ballth + 1);
            Console.ResetColor();
            // 当前球
            int currentball = int.Parse(Console.ReadLine() ?? "0");
            // 检查
            currentball = Inspect(ballth, currentball);
            // 赋值当前球
            redballs[ballth] = currentball;
        }
        // 数字排序
        for (int i = 0; i < redballs.Length - 1; i++)
        {
            for (int j = 0; j < redballs.Length - 1 - i; j++)
            {
                if (redballs[j] > redballs[j + 1])
                {
                    int max = redballs[j];
                    redballs[j] = redballs[j + 1];
                    redballs[j + 1] = max;
                }
            }
        }
        // 赋值
        for (int i = 0; i < _balls[0..6].Length; i++)
        {
            _balls[i] = redballs[i];
        }

        // 检查数字是否符合
        int Inspect(int iballth, int icurrentball)
        {
            // 判断当前球是否在（1-33）范围内，若不是，重新输入，若是，赋值
            while (!(icurrentball >= 1 && icurrentball <= 33))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("第{0}个红色球：(输入范围错误！[{1}]超出了输入范围,请重新输入(1-33之间)的数)", iballth + 1, icurrentball);
                Console.ResetColor();
                icurrentball = int.Parse(Console.ReadLine() ?? "0");
                icurrentball = Inspect(iballth, icurrentball);
                break;
            }
            // 重复序号
            int repeatSeq = 0;
            // 重复个数
            int repeatNum = 0;
            // 重复标记
            int repeatMark = 1;
            // 判断当前数字与已选择数字是否有重复
            for (int ith = 0; ith < iballth; ith++)
            {
                if (icurrentball == redballs[ith])
                {
                    repeatSeq = ith;
                    repeatNum++;
                }
            }
            // 若有重复，则重新输入
            if (repeatNum != 0)
            {
                // 提示第几个数重复
                while (repeatMark != 0)
                {
                    string repeatTips = "除";
                    for (int i = 0; i < iballth; i++)
                    {
                        // 重复数字
                        string repeatNums = string.Empty;
                        for (int t = 0; t <= i; t++)
                        {
                            repeatNums = "[" + Convert.ToString(redballs[t]) + "]";
                        }
                        repeatTips += repeatNums;
                    }
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("第{0}个红色球：(输入重复错误！你购买的第{1}个红色球已存在数字{2}，请重新输入(1-33之间)({3})的数)", iballth + 1, repeatSeq + 1, icurrentball, repeatTips);
                    icurrentball = int.Parse(Console.ReadLine() ?? "0");
                    Console.ResetColor();
                    icurrentball = Inspect(iballth, icurrentball);
                    repeatMark = 0;
                }
            }
            return icurrentball;
        }
    }

    /// <summary>
    /// 蓝色球
    /// </summary>
    private void Blue()
    {
        int blueball = 0;
        //判断输入数字是否在（1-16）范围内，若是取值，若不是重新输入
        while (!(blueball >= 1 && blueball <= 16))
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("请输入你要选择的(1-16之间)1个[蓝色球]号码：");
            Console.Write("蓝色球：");
            Console.ResetColor();
            blueball = int.Parse(Console.ReadLine() ?? "0");
        }
        _balls[6] = blueball;
    }

    /// <summary>
    /// 显示记录
    /// </summary>
    private void Display(int sequence)
    {
        Console.Write("第【{0,2}】注：", sequence.ToString("D2"));
        Console.Write("红色球：");
        for (int i = 0; i < _balls[0..6].Length; i++)
        {
            Task.Delay(50).Wait();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("{0,2:D2} ", _balls[i]);
            Console.ResetColor();
        }
        Console.Write("蓝色球：");
        Task.Delay(50).Wait();
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.Write("{0,2:D2} ", _balls[6]);
        Console.ResetColor();
    }
}
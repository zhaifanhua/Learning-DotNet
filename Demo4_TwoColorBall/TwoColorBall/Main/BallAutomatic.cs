// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:BallAutomatic
// Guid:8c918fa6-8fdb-486d-a214-ce7627c70fd8
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatTime:2022-05-02 上午 08:24:53
// ----------------------------------------------------------------

using TwoColorBall.Common;

namespace TwoColorBall.Main;

/// <summary>
/// 自动购号
/// </summary>
public class BallAutomatic
{
    private Wallet _myWallet = new();
    private WriteData _writeData = new();
    private Random _random = new();
    private int[] _balls = new int[7];

    /// <summary>
    /// 自动购号
    /// </summary>
    public void Automatic()
    {
        Console.WriteLine();
        Console.WriteLine("                    =============================自动购号开始=============================");
        int buytimes = CheckWallet();
        Buy(buytimes);
        Console.WriteLine("                    =============================自动购号结束=============================");
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
        Console.WriteLine("\t系统正在为你购买【{0}】注双色球...", buytimes);
        Console.ResetColor();

        for (int sequence = 1; sequence <= buytimes; sequence++)
        {
            Console.Write("第【{0,2}】注：", sequence.ToString("D2"));
            Red();
            Blue();
            Console.Write("\t【自动购号】");
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
    public void Red()
    {
        int[] redballs = new int[6];
        // 重复检验
        for (int i = 0; i < redballs.Length; i++)
        {
            int ball = _random.Next(1, 34);
            for (int j = 0; j < i; j++)
            {
                while (ball == redballs[j])
                {
                    ball = _random.Next(1, 34);
                }
            }
            redballs[i] = ball;
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
        // 显示记录
        Console.Write("红色球：");
        for (int i = 0; i < redballs.Length; i++)
        {
            Task.Delay(50).Wait();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("{0,2:D2} ", redballs[i]);
            _balls[i] = redballs[i];
            Console.ResetColor();
        }
    }

    /// <summary>
    /// 蓝色球
    /// </summary>
    public void Blue()
    {
        int blueball = _random.Next(1, 17);
        Console.Write("蓝色球：");
        Task.Delay(50).Wait();
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.Write("{0,2:D2} ", blueball);
        _balls[6] = blueball;
        Console.ResetColor();
    }
}
// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:Ball
// Guid:25182033-35b8-414c-8e5a-2d88a9677ab0
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatTime:2022-05-02 上午 08:20:21
// ----------------------------------------------------------------

namespace TwoColorBall.Main;

/// <summary>
/// 模拟双色球
/// </summary>
public class Ball
{
    private BallAutomatic _ballAutomatic = new();
    private BallManual _ballManual = new();
    private Lottery _lottery = new();
    private Wallet _wallet = new();

    /// <summary>
    /// 开始模拟
    /// </summary>
    public void Play()
    {
        Console.WriteLine("         =======================================模拟双色球开始=======================================");
        // 新用户充值
        Promotion();
        // 入口标记
        int entranceMark = 1;
        while (entranceMark != 0)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("请选择：【Q/q】自动购号；【W/w】手动购号；【E/e】对已购双色球开奖；【R/r】充值或提现；【T/t】返回主菜单；");
            Console.ResetColor();
            string selectKey = Console.ReadLine() ?? "";
            switch (selectKey.ToUpper())
            {
                case "Q":
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\t你选择了自动购号；");
                    Console.ResetColor();
                    _ballAutomatic.Automatic();
                    break;

                case "W":
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\t你选择了手动购号；");
                    Console.ResetColor();
                    _ballManual.Manual();
                    break;

                case "E":
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\t你选择了对已购买双色球开奖；");
                    Console.ResetColor();
                    _lottery.Prize();
                    break;

                case "R":
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\t你选择了充值或提现；");
                    Console.ResetColor();
                    _wallet.RechargeOrConsumptManual();
                    break;

                case "T":
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\t你选择了返回主菜单；");
                    Console.ResetColor();
                    entranceMark = 0;
                    break;

                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\t你的输入有误，请重新输入!");
                    Console.ResetColor();
                    break;
            }
        }
        Console.WriteLine("         =======================================模拟双色球结束=======================================");
        Console.WriteLine();
    }

    /// <summary>
    /// 新用户赠送余额
    /// </summary>
    private void Promotion()
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("\t为了你的首次体验，系统已为你赠送100.00元用于购买双色球！\n");
        _wallet.RechargeOrConsumptAutomatic((decimal)100.00);
        Console.ResetColor();
    }
}
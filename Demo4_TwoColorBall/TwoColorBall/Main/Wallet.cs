// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:Wallet
// Guid:bbf94a5b-f7f3-4202-a11c-aba3dac239c3
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatTime:2022-05-02 上午 08:23:35
// ----------------------------------------------------------------

namespace TwoColorBall.Main;

/// <summary>
/// 账户钱包
/// </summary>
public class Wallet
{
    // 余额
    private static decimal _balance = 0;

    public decimal Balance
    {
        get => _balance;
        set => _balance = value >= 0 ? value : 0;
    }

    /// <summary>
    /// 手动充值或消费
    /// </summary>
    public void RechargeOrConsumptManual()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("请输入你的账户存取额(正为充值，负为提现)：");
        Console.ResetColor();

        decimal money = decimal.Parse(Console.ReadLine() ?? string.Format("0"));
        RechargeOrConsumptAutomatic(money);
    }

    /// <summary>
    /// 自动充值或消费
    /// </summary>
    /// <param name="money"></param>
    public void RechargeOrConsumptAutomatic(decimal money)
    {
        decimal moneyabs = Math.Abs(money);
        if (money >= 0)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\t充值或奖励金额为：{0}元；", FormatMoneyToDecimal(moneyabs));
            Balance += moneyabs;
            Console.ResetColor();
        }
        else
        {
            if (Math.Abs(money) > Balance)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\t账户余额不足此提现或消费；");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\t提现或消费金额为：{0}元；", FormatMoneyToDecimal(moneyabs));
                Balance -= moneyabs;
                Console.ResetColor();
            }
        }
        //输出账户余额
        Console.WriteLine("\t你的账户的余额为：{0}元；", FormatMoneyToDecimal(Balance));
    }

    /// <summary>
    /// 格式化金额(1234,5678.90)
    /// </summary>
    /// <param name="money"></param>
    /// <returns></returns>
    public string FormatMoneyToDecimal(decimal money)
    {
        try
        {
            string moneyStr = money.ToString();
            string moneyRes = string.Empty;
            string moneyInt = string.Empty;
            string moneyDecimal = string.Empty;
            if (moneyStr.Contains('.'))
            {
                moneyInt = moneyStr.Split('.')[0].ToString();
                moneyDecimal = "." + moneyStr.Split('.')[1].ToString();
                moneyRes = FormatMoneyToInt(moneyInt);
            }
            else
            {
                moneyRes = FormatMoneyToInt(moneyStr);
            }
            string FormatMoneyToInt(string moneyint)
            {
                if (moneyint.ToString().Length > 4)
                {
                    string moneyNotFormat = moneyint.Substring(0, moneyint.Length - 4);
                    string moneyFormat = moneyint.Substring(moneyint.Length - 4, 4);
                    if (moneyNotFormat.Length > 4)
                    {
                        return FormatMoneyToInt(moneyNotFormat) + "," + moneyFormat;
                    }
                    else
                    {
                        return moneyNotFormat + "," + moneyFormat;
                    }
                }
                else
                {
                    return moneyint;
                }
            }
            return moneyRes + moneyDecimal;
        }
        catch (Exception)
        {
            throw;
        }
    }
}
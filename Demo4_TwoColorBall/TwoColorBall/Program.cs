// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:Program
// Guid:372ee8df-d0d5-4ceb-b033-613860c6257c
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatTime:2022-05-02 上午 08:11:21
// ----------------------------------------------------------------

using System.Runtime.InteropServices;
using TwoColorBall.Main;

namespace TwoColorBall;

/// <summary>
/// 程序入口
/// </summary>
public class Program
{
    private const int _windowHeight = 50;
    private const int _windowWidth = 150;

    /// <summary>
    /// 程序开始
    /// </summary>
    /// <param name="args"></param>
    /// <exception cref="Exception"></exception>
    public static void Main(string[] args)
    {
        AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionTrapper;
        try
        {
            // 设置窗口宽高
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Console.WindowHeight = _windowHeight;
                Console.WindowWidth = _windowWidth;
            }
            Console.ResetColor();
            Console.WriteLine($"\t\t\t\t Copyright (C){DateTime.Now.Year} ZhaiFanhua All Rights Reserved.");
            Console.WriteLine("你好！欢迎你进入摘繁华的模拟双色球程序！");
            Console.WriteLine("===================================================程序开始===================================================");
            // 入口标记
            int entranceMark = 1;
            while (entranceMark != 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("请输入以下按键选择：【Q/q】查看规则；【W/w】进入模拟；【E/e】结束程序；");
                Console.ResetColor();
                string selectKey = Console.ReadLine() ?? "";
                switch (selectKey.ToUpper())
                {
                    case "Q":
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\t你选择了查看规则；");
                        Console.ResetColor();
                        Rule rule = new();
                        rule.Start();
                        break;

                    case "W":
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\t你选择了进入模拟；");
                        Console.ResetColor();
                        Ball ball = new();
                        ball.Play();
                        break;

                    case "E":
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\t你选择了结束程序!");
                        Console.ResetColor();
                        entranceMark = 0;
                        break;

                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\t你的输入有误，请重新选择!");
                        Console.ResetColor();
                        break;
                }
            }
            Console.WriteLine("===================================================程序结束===================================================");
            Console.WriteLine("程序结束，请按任意键退出。");
            Console.WriteLine($"\t\t\t\t Copyright (C){DateTime.Now.Year} ZhaiFanhua All Rights Reserved.");
            _ = Console.ReadKey(true);
        }
        catch (Exception ex)
        {
            throw new Exception("程序出错！错误消息：\n" + ex.Message);
        }
    }

    /// <summary>
    /// 全局异常处理
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public static void UnhandledExceptionTrapper(object sender, UnhandledExceptionEventArgs e)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(e.ExceptionObject.ToString());
        Console.ResetColor();
        Console.WriteLine("请按任意键退出！");
        _ = Console.ReadLine();
        Environment.Exit(1);
    }
}
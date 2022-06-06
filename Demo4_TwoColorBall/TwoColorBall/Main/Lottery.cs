// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:Lottery
// Guid:1ae901e8-4a3f-4281-a62f-a2c9a9640396
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatTime:2022-05-02 上午 08:38:22
// ----------------------------------------------------------------

using TwoColorBall.Common;

namespace TwoColorBall.Main;

/// <summary>
/// 开奖
/// </summary>
public class Lottery
{
    private Wallet _wallet = new();
    private ReadData _readData = new();
    private WriteData _writeData = new();
    private Random _random = new();

    // 购号记录是否存在
    private Dictionary<string, bool> _ballIsRecord = new();

    // 模拟奖池
    private decimal _lottery = 0;

    // 奖池中累积的奖金之和
    private decimal _bonusCumulative = 0;

    // 奖金总额
    private decimal _bonusTotal = 0;

    // 单次中奖金额
    private decimal _bonusAmount = 0;

    // 总中奖金额
    private decimal _bonusAmounts = 0;

    // 中奖号码
    private int[] _prizeBall = new int[7];

    // 中奖总数
    private int[] _prizeCount = new int[7];

    // 中奖等级
    private string _prizeGrade = string.Empty;

    /// <summary>
    /// 开奖入口
    /// </summary>
    public void Prize()
    {
        // 删除开奖记录
        _readData.DeleteFile(this.GetType().Name);
        _readData.DeleteFile(@"PrizeRecord");
        // 判断是否开奖
        bool isPrize = WhetherPrize();
        // 有购票记录则开奖，没有则不开奖
        if (isPrize)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("\t存在购号记录，即将开奖！\n");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine("                                ================模拟开奖开始================");
            StartPrize();
            ComparativeData();
            Console.WriteLine("                                ================模拟开奖结束================");
            Console.WriteLine();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("\t你没有任何形式的购号记录，请先购号后开奖！\n");
            Console.ResetColor();
            Console.WriteLine();
        }
        // 删除购号记录
        _readData.DeleteFile(typeof(BallAutomatic).Name);
        _readData.DeleteFile(typeof(BallManual).Name);
    }

    /// <summary>
    /// 判断是否开奖
    /// </summary>
    /// <returns></returns>
    private bool WhetherPrize()
    {
        // 删除开奖前创建的模拟开奖历史记录
        _readData.DeleteFile(this.GetType().Name);
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\t正在判断是否开奖...");
        Console.ResetColor();
        // 判断购号记录是否存在
        _ballIsRecord = FindData();
        bool isFind = _ballIsRecord.ToList().Any(e => e.Value == true);
        return isFind;
    }

    /// <summary>
    /// 判断是否存在购号记录
    /// </summary>
    /// <returns></returns>
    private Dictionary<string, bool> FindData()
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write("\t正在判断是否存在购号记录...");
        Console.ResetColor();
        Dictionary<string, bool> isFind = new Dictionary<string, bool>
        {
            { typeof(BallAutomatic).Name,false},
            { typeof(BallManual).Name,false}
        };
        return _readData.FindData(isFind);
    }

    /// <summary>
    /// 产生开奖号
    /// </summary>
    private void StartPrize()
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\t正在产生开奖号码...");
        Console.ResetColor();
        BallAutomatic myBallAutomatic = new BallAutomatic();
        Console.Write("【开奖号码】");
        Red();
        Blue();
        Console.Write("\t【模拟开奖】");
        string time = DateTime.Now.ToString();
        Console.Write("时间：{0}", time);
        Console.WriteLine();
        Console.WriteLine("\t开奖号码已产生!");
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write("\t正在计算奖池金额...");
        Console.ResetColor();
        // 奖池
        _lottery = _random.Next(10000000, 999999999);
        // 奖池中累积的奖金之和
        _bonusCumulative = _random.Next(1000000, 99999999);
        // 奖金总额
        _bonusTotal = _lottery + _bonusCumulative;
        Task.Delay(1000).Wait();
        Console.Write("\t本次开奖奖池金额为：{0}元;奖池中累积的奖金之和为：{1}元;奖金总额为：{2}元;",
             _wallet.FormatMoneyToDecimal(_lottery),
             _wallet.FormatMoneyToDecimal(_bonusCumulative),
             _wallet.FormatMoneyToDecimal(_bonusTotal));
        Console.WriteLine();
        // 打印模拟开奖记录
        _writeData.RecordLottery(this.GetType().Name, _prizeBall, time,
            _wallet.FormatMoneyToDecimal(_lottery),
            _wallet.FormatMoneyToDecimal(_bonusCumulative),
            _wallet.FormatMoneyToDecimal(_bonusTotal));
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
            _prizeBall[i] = redballs[i];
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
        _prizeBall[6] = blueball;
        Console.ResetColor();
    }

    /// <summary>
    /// 对比数据
    /// </summary>
    public void ComparativeData()
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\t正在查找数据，请稍候...");
        Console.ResetColor();
        Console.WriteLine("                                        ========查看中奖开始========");
        // 查找数据
        foreach (var br in _ballIsRecord)
        {
            string tip = br.Key == typeof(BallAutomatic).Name ? "系统购号" : "手动购号";
            if (br.Value)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\t查找到{tip}数据，正在读取...");
                Console.ResetColor();
                JudgeData(br.Key);
            }
            else
            {
                Console.WriteLine($"\t你没有{tip}数据！");
            }
        }
        // 判断总中奖数
        if (_prizeCount[6] == 0)
        {
            Console.WriteLine("\t抱歉，你没有中奖！");
        }
        else
        {
            Console.WriteLine($"\t你共中奖{_prizeCount[6]}注，获得总奖金：{_bonusAmounts}元！其中：" +
                $"一等奖{_prizeCount[0]}注；" +
                $"二等奖{_prizeCount[1]}注；" +
                $"三等奖{_prizeCount[2]}注；" +
                $"四等奖{_prizeCount[3]}注；" +
                $"五等奖{_prizeCount[4]}注；" +
                $"六等奖{ _prizeCount[5]}注；");
        }
        _wallet.RechargeOrConsumptAutomatic(_bonusAmounts);
        _writeData.RecordPrizeAmounts(_prizeCount, _wallet.FormatMoneyToDecimal(_bonusAmounts));
        Console.WriteLine("                                        ========查看中奖结束========");
        // 总中奖数清零
        Array.Clear(_prizeCount, 0, _prizeCount.Length);
        // 总中奖金额清零
        _bonusAmounts = 0;
    }

    /// <summary>
    /// 判断数据
    /// </summary>
    /// <param name="name"></param>
    public void JudgeData(string name)
    {
        // 读取行数
        int rows = _readData.GetRowsCount(name);
        // 提示
        string tip = name == typeof(BallAutomatic).Name ? "系统购号" : "手动购号";
        for (int everyrow = 1; everyrow <= rows; everyrow++)
        {
            // 读取数据
            string[] datastr = new string[4];
            datastr = _readData.ReadBallData(name, everyrow);
            // 定义一个读取文件后数组，用于比较是否中奖
            int[] databall = new int[7];
            // 拆分后的序号
            string sequence = datastr[1];
            // 拆分后的红球
            string redballstr = datastr[2];
            for (int i = 0; i < 6; i++)
            {
                databall[i] = int.Parse(redballstr.Substring(i * 2, 2));
            }
            // 拆分后的蓝球
            databall[6] = int.Parse(datastr[3]);
            // 拆分后的时间
            string time = datastr[4];
            // 开奖数字对比之后计数
            int red = 0;
            int blue = 0;
            for (int i = 0; i < _prizeBall.Length; i++)
            {
                if (_prizeBall[i] == databall[i])
                {
                    _ = i < 6 ? red++ : blue++;
                }
            }
            // 判断中奖
            JudgePrize(sequence, databall, time, tip, red, blue);
        }
    }

    /// <summary>
    /// 判断中奖
    /// </summary>
    /// <param name="sequence"></param>
    /// <param name="databall"></param>
    /// <param name="time"></param>
    /// <param name="tip"></param>
    /// <param name="red"></param>
    /// <param name="blue"></param>
    public void JudgePrize(string sequence, int[] databall, string time, string tip, int red, int blue)
    {
        // 是否输出
        bool output = false;
        if (red == 6 && blue == 1)
        {
            _prizeGrade = "一";
            _bonusAmount = _bonusTotal * 0.75m;
            if (_bonusAmount >= 10000000)
            {
                _bonusAmount = 10000000;
            }
            _prizeCount[6]++;
            _prizeCount[0]++;
            output = true;
        }
        if (red == 6 && blue == 0)
        {
            _prizeGrade = "二";
            _bonusAmount = _bonusTotal * 0.25m;
            if (_bonusAmount >= 10000000)
            {
                _bonusAmount = 5000000;
            }
            _prizeCount[6]++;
            _prizeCount[1]++;
            output = true;
        }
        if (red == 5 && blue == 1)
        {
            _prizeGrade = "三";
            _bonusAmount = 3000;
            _prizeCount[6]++;
            _prizeCount[2]++;
            output = true;
        }
        if ((red == 5 && blue == 0) || (red == 4 && blue == 1))
        {
            _prizeGrade = "四";
            _bonusAmount = 200;
            _prizeCount[6]++;
            _prizeCount[3]++;
            output = true;
        }
        if ((red == 4 && blue == 0) || (red == 3 && blue == 1))
        {
            _prizeGrade = "五";
            _bonusAmount = 10;
            _prizeCount[6]++;
            _prizeCount[4]++;
            output = true;
        }
        if ((red == 2 && blue == 1) || (red == 1 && blue == 1) || (red == 0 && blue == 1))
        {
            _prizeGrade = "六";
            _bonusAmount = 5;
            _prizeCount[6]++;
            _prizeCount[5]++;
            output = true;
        }
        if (output)
        {
            _bonusAmounts += _bonusAmount;
            // 中奖输出
            Console.Write("第【{0}】注：红色球：", sequence);
            Console.ForegroundColor = ConsoleColor.Red;
            for (int i = 0; i < 6; i++)
            {
                Console.Write("{0,2:D2} ", databall[i]);
            }
            Console.ResetColor();
            Console.Write("蓝色球：");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("{0,2:D2}", databall[6]);
            Console.ResetColor();
            Console.Write("\t【{0}】", tip);
            Console.Write("时间：{0}", time);
            Console.Write("\t[该注中了{0}等奖！获得奖金：{1}元；]", _prizeGrade, _wallet.FormatMoneyToDecimal(_bonusAmount));
            _writeData.RecordPrize(sequence, databall, time, tip, _prizeGrade, _wallet.FormatMoneyToDecimal(_bonusAmount));
            Console.WriteLine();
        }
    }
}
// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:ReadData
// Guid:55cd3ffd-3af8-418a-a091-5eb3f6250fb5
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatTime:2022-05-02 上午 08:41:21
// ----------------------------------------------------------------

namespace TwoColorBall.Common;

/// <summary>
/// 读取数据
/// </summary>
public class ReadData
{
    /// <summary>
    /// 查找文件
    /// </summary>
    /// <param name="filename"></param>
    public bool FindFile(string name)
    {
        string filename = name + ".txt";
        return File.Exists(filename);
    }

    /// <summary>
    /// 删除文件
    /// </summary>
    /// <param name="name"></param>
    public void DeleteFile(string name)
    {
        string filename = name + ".txt";
        if (File.Exists(filename))
        {
            File.Delete(filename);
        }
    }

    /// <summary>
    /// 查找记录
    /// </summary>
    /// <param name="ballbuys"></param>
    /// <returns></returns>
    public Dictionary<string, bool> FindData(Dictionary<string, bool> ballbuys)
    {
        return ballbuys.ToDictionary(e => e.Key, e => FindFile(e.Key));
    }

    /// <summary>
    /// 获取行数
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public int GetRowsCount(string name)
    {
        string filename = name + ".txt";
        int rowsCount = 0;
        using (StreamReader read = File.OpenText(filename))
        {
            string result = read.ReadToEnd();
            rowsCount = result.Split('\n').Length - 1;
        }
        return rowsCount;
    }

    /// <summary>
    /// 读取数据
    /// </summary>
    /// <param name="name"></param>
    /// <param name="row"></param>
    public string[] ReadBallData(string name, int row)
    {
        string filename = name + ".txt";
        string[] data = File.ReadAllLines(filename);
        string[] datastr = data[row - 1].Split(new char[4] { 'N', 'R', 'B', 'T' });
        return datastr;
    }
}
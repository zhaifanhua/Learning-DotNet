using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Reflection;
using System.Configuration;

/// <summary>
/// 数据库帮助类
/// </summary>
public static class DBHelper
{
    /*连接字符串解释: 
     * "Server"或"Data Source"属性:服务器名
     * "Database"或"Initial Catalog"属性:数据库名
     * "Persist Security Info"属性:是否保存安全信息，即数据库连接成功后是否保存密码信息(true或false)
     * --------密码验证登录--------
     * "Uid"或"User Id"属性:连接数据库的验证用户名
     * "Pwd"或"Password"属性:连接数据库的验证密码
     * --------Windows登录--------
     * "AttachDbFilename"属性:服务器的文件路径
     * "Integrated Security"属性:Windows登录（true或false，SSPI即为true）
     */

    //数据库连接属性,从config配置文件中获取连接字符串Connection并解密
    private static string connectionString = @"Data Source=.;Initial Catalog=Baidu;Integrated Security=True;";
    private static SqlConnection connection;

    /// <summary>
    /// 执行SQL语句，并返回受影响的行数。
    /// </summary>
    /// <param name="safeSql">sql字符串</param>
    /// <param name="values">参数值</param>
    /// <returns>受影响的行数</returns>
    public static int ExecuteGetNonQuery(string sql, params SqlParameter[] values)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddRange(values);
            int result = cmd.ExecuteNonQuery();
            return result;
        }
    }
    /// <summary>
    /// 执行存储过程，并返回受影响的行数。
    /// </summary>
    /// <param name="safeSql">存储过程名</param>
    /// <param name="values">存储过程参数值</param>
    /// <returns>受影响的行数</returns>
    public static int ExecuteProcGetNonQuery(string safeSql, params SqlParameter[] values)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand(safeSql, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddRange(values);
            int result = cmd.ExecuteNonQuery();
            return result;
        }
    }
    /// <summary>
    /// 执行SQL语句，并返回首行首列数据。
    /// </summary>
    /// <param name="safeSql">sql字符串</param>
    /// <param name="values">参数值</param>
    /// <returns>首行首列数据</returns>
    public static string ExecuteGetScalar(string sql, params SqlParameter[] values)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddRange(values);
            string result = Convert.ToString(cmd.ExecuteScalar());
            return result;
        }
    }
    /// <summary>
    /// 执行存储过程，并返回首行首列数据。
    /// </summary>
    /// <param name="safeSql">存储过程名</param>
    /// <param name="values">参数值</param>
    /// <returns>首行首列数据</returns>
    public static string ExecuteProcGetScalar(string sql, params SqlParameter[] values)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddRange(values);
            string result = Convert.ToString(cmd.ExecuteScalar());
            return result;
        }
    }
    /// <summary>
    /// 执行SQL语句，并返回SqlDataReader。
    /// </summary>
    /// <param name="safeSql">sql字符串</param>
    /// <param name="values">参数值</param>
    /// <returns>返回DataReader</returns>
    public static SqlDataReader ExecuteGetReader(string sql, params SqlParameter[] values)
    {
        SqlConnection conn = new SqlConnection(connectionString);
        conn.Open();
        SqlCommand cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddRange(values);
        try
        {
            SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            return reader;
        }
        catch
        {
            conn.Close();
            throw;
        }
    }
    /// <summary>
    /// 执行存储过程，并返回SqlDataReader。
    /// </summary>
    /// <param name="safeSql">存储过程名</param>
    /// <param name="values">参数值</param>
    /// <returns>返回DataReader</returns>
    public static SqlDataReader ExecuteProcGetReader(string sql, params SqlParameter[] values)
    {
        SqlConnection conn = new SqlConnection(connectionString);
        conn.Open();
        SqlCommand cmd = new SqlCommand(sql, conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddRange(values);
        try
        {
            SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            return reader;
        }
        catch
        {
            conn.Close();
            throw;
        }
    }
    /// <summary>
    /// 执行SQL语句，并返回DataTable对象。
    /// </summary>
    /// <param name="safeSql">SQL语句</param>
    /// <returns>返回DataTable</returns>
    public static DataTable ExecuteSqlGetDataTable(string safeSql)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand(safeSql, conn);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            return ds.Tables[0];
        }
    }
    /// <summary>
    /// 执行存储过程，并返回DataTable对象。
    /// </summary>
    /// <param name="safeSql">存储过程名</param>
    /// <param name="values">参数值</param>
    /// <returns>返回DataTable对象</returns>
    public static DataTable ExecuteProcGetDataTable(string safeSql, params SqlParameter[] values)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand(safeSql, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddRange(values);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            return ds.Tables[0];
        }
    }
    /// <summary>
    /// 执行SQL语句和泛型方法，并返回泛型集合。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="safeSql">sql字符串</param>
    /// <param name="values">参数值</param>
    /// <returns>返回泛型集合</returns>
    public static List<T> ExecuteSqlGetList<T>(string safeSql, params SqlParameter[] values)
    {
        List<T> list = new List<T>();
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand(safeSql, conn);
            cmd.Parameters.AddRange(values);
            using (SqlDataReader reader = cmd.ExecuteReader())
            {   //typeof()检测类型
                Type type = typeof(T);//类型的声明(可声明一个不确定的类型)
                while (reader.Read())
                {
                    T t = (T)Activator.CreateInstance(type);
                    //通过反射去遍历属性
                    foreach (PropertyInfo info in type.GetProperties())
                    {
                        info.SetValue(t, reader[info.Name] is DBNull ? null : reader[info.Name], null);
                    }
                    list.Add(t);
                }
            }
        }
        return list;//命令行为
    }
    /// <summary>
    /// 执行SQL语句和泛型方法，并返回泛型对象。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="safeSql">sql字符串</param>
    /// <param name="values">参数值</param>
    /// <returns>返回泛型对象</returns>
    public static T ExecuteSqlGetModel<T>(string safeSql, params SqlParameter[] values)
    {
        Type type = typeof(T);//类型的声明Type
        T t = default(T);//赋默认值null,可能是值类型
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand(safeSql, conn);
            cmd.Parameters.AddRange(values);
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    t = (T)Activator.CreateInstance(type);
                    //通过反射去遍历属性
                    foreach (PropertyInfo info in type.GetProperties())
                    {
                        info.SetValue(t, reader[info.Name] is DBNull ? null : reader[info.Name], null);
                    }
                }
            }
        }
        return t;//命令行为
    }
    /// <summary>
    /// 执行事务的通用方法。
    /// </summary>
    /// <param name="safeSql">sql字符串</param>
    /// <param name="values">参数值</param>
    /// <returns>返回事务</returns>
    public static bool ExecuteTransaction(string[] safeSql, params SqlParameter[] values)
    {
        bool result = false;
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.AddRange(values);
            cmd.Connection = conn;//关联联接对象
            cmd.Transaction = conn.BeginTransaction();//开始事务
            try
            {
                for (int i = 0; i < safeSql.Length; i++)
                {
                    cmd.CommandText = safeSql[i];
                    cmd.ExecuteNonQuery();//执行
                }
                cmd.Transaction.Commit();//提交
                result = true;
            }
            catch (Exception)
            {
                cmd.Transaction.Rollback();//回滚
                result = false;
            }
        }
        return result;
    }
    /// <summary>
    /// 连接数据库。
    /// </summary>
    public static SqlConnection Connection
    {
        get
        {
            if (connection == null)
            {
                using (connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                }
            }
            else if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            else if (connection.State == ConnectionState.Broken)
            {
                connection.Close();
                connection.Open();
            }
            return connection;
        }
    }
    /// <summary>
    /// 关闭数据库连接。
    /// </summary>
    public static void Close()
    {
        ///判断连接是否已经创建
        if (connection != null)
        {
            ///判断连接的状态是否打开
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }
    }
    /// <summary>
    /// 释放资源。
    /// </summary>
    public static void Dispose()
    {
        // 确认连接是否已经关闭
        if (connection != null)
        {
            connection.Dispose();
            connection = null;
        }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Reflection;
using Microsoft.Data.SqlClient;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using System.IO;
using static PMSS.Extensions.Common.AllClass;
using System.Web;
using PMSS.Extensions;
using Extensions.Common.STResultAPI;

public class STFunction
{
    public static DataTable LinqToDataTable<T>(IEnumerable<T> Data)
    {
        DataTable dtReturn = new DataTable();
        if (Data.ToList().Count == 0) return null;
        // Could add a check to verify that there is an element 0

        T TopRec = Data.ElementAt(0);

        // Use reflection to get property names, to create table

        // column names

        PropertyInfo[] oProps =
        ((Type)TopRec.GetType()).GetProperties();

        foreach (PropertyInfo pi in oProps)
        {

            Type colType = pi.PropertyType; if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
            {

                colType = colType.GetGenericArguments()[0];

            }

            dtReturn.Columns.Add(new DataColumn(pi.Name, colType));
        }

        foreach (T rec in Data)
        {

            DataRow dr = dtReturn.NewRow(); foreach (PropertyInfo pi in oProps)
            {
                dr[pi.Name] = pi.GetValue(rec, null) == null ? DBNull.Value : pi.GetValue(rec, null);
            }
            dtReturn.Rows.Add(dr);

        }
        return dtReturn;
    }

    public static void SQLExcute(string sConnection, string sQuery)
    {
        SqlConnection objConn = new SqlConnection(sConnection);
        SqlCommand cmd;
        try
        {
            objConn.Open();
            cmd = new SqlCommand(sQuery, objConn);
            cmd.ExecuteNonQuery();
        }
        catch { }
        finally { objConn.Close(); }
    }

    //public static DataTable SQLGetTable(string sConnection, string sQuery)
    //{
    //    using (SqlConnection _conn = new SqlConnection(sConnection))
    //    {
    //        DataTable _dt = new DataTable();
    //        _conn.Open();
    //        new SqlDataAdapter(sQuery, _conn).Fill(_dt);
    //        _conn.Close();
    //        return _dt;
    //    }
    //}

    public static DataTable SQLGetTable(string sConnection, string sQuery, string[] arrValue)
    {
        using (SqlConnection _conn = new SqlConnection(sConnection))
        {
            DataTable _dt = new DataTable();
            _conn.Open();
            SqlDataAdapter adapter = new SqlDataAdapter();
            string[] arrParameter = new string[arrValue.Length];
            for (int i = 0; i < arrValue.Length; i++)
            {
                if (arrValue[i].Split(",").Count() > 1)
                {
                    arrParameter[i] = "";
                    for (int j = 0; j < arrValue[i].Split(",").Count(); j++)
                    {
                        arrParameter[i] += "@P" + i + j;
                        if (j != arrValue[i].Split(",").Count() - 1)
                        {
                            arrParameter[i] += ",";
                        }
                    }
                }
                else
                {
                    arrParameter[i] = "@P" + i;
                }

            }
            sQuery = string.Format(sQuery, (Object[])arrParameter);
            adapter.SelectCommand = new SqlCommand(sQuery, _conn);
            for (int i = 0; i < arrValue.Length; i++)
            {
                // SqlParameter Parameter = new SqlParameter(arrParameter[i], arrDbType[i]);
                // Parameter.Value = arrValue[i];
                // adapter.SelectCommand.Parameters.Add(Parameter);
                if (arrValue[i].Split(",").Count() > 1)
                {
                    for (int j = 0; j < arrValue[i].Split(",").Count(); j++)
                    {
                        adapter.SelectCommand.Parameters.Add(new SqlParameter(arrParameter[i].Split(",")[j], arrValue[i].Split(",")[j]));
                    }
                }
                else
                {
                    adapter.SelectCommand.Parameters.Add(new SqlParameter(arrParameter[i], arrValue[i]));
                }
            }
            adapter.Fill(_dt);
            _conn.Close();
            return _dt;
        }
    }

    public static int SQLCountRecord(string sConnection, string sQuery)
    {
        using (SqlConnection _conn = new SqlConnection(sConnection))
        {
            DataTable _dt = new DataTable();
            _conn.Open();
            new SqlDataAdapter(sQuery, _conn).Fill(_dt);
            if (_dt.Rows.Count > 0)
            {
                int _return = _dt.Rows.Count;
                _dt.Dispose();
                return _return;
            }
            return 0;
        }

    }

    public static int SQLCountRecord(SqlConnection sqlCon, string sQuery)
    {

        DataTable _dt = new DataTable();
        if (sqlCon.State == ConnectionState.Closed) sqlCon.Open();
        new SqlDataAdapter(sQuery, sqlCon).Fill(_dt);
        if (_dt.Rows.Count > 0)
        {
            int _return = _dt.Rows.Count;
            _dt.Dispose();
            return _return;
        }
        return 0;

    }

    public static string SQLGetValue(string sConnection, string sQuery)
    {
        using (SqlConnection _conn = new SqlConnection(sConnection))
        {
            DataTable _dt = new DataTable();
            _conn.Open();
            new SqlDataAdapter(sQuery, _conn).Fill(_dt);
            if (_dt.Rows.Count >= 1)
            {
                string _return = _dt.Rows[0][0].ToString();
                _dt.Dispose();
                return _return;
            }
            return "";
        }
    }

    public static string SQLGetValue(SqlConnection sqlCon, string sQuery)
    {
        DataTable _dt = new DataTable();
        new SqlDataAdapter(sQuery, sqlCon).Fill(_dt);
        if (_dt.Rows.Count >= 1)
        {
            string _return = _dt.Rows[0][0].ToString();
            _dt.Dispose();
            return _return;
        }
        return "";
    }

    public static string SQLGenerateID(string sConnection, string sQuery)
    {
        using (SqlConnection _conn = new SqlConnection(sConnection))
        {
            string sReturn = "";
            DataTable _dt = new DataTable();
            _conn.Open();
            new SqlDataAdapter(sQuery, _conn).Fill(_dt);

            if (_dt.Rows.Count >= 1)
            {
                sReturn = "" + (Convert.ToDecimal(_dt.Rows[0][0]) + 1);
            }
            else
            {
                sReturn = "1";
            }
            _dt.Dispose();
            return sReturn;
        }
    }

    public static string SQLGenerateID(SqlConnection sqlCon, string sQuery)
    {
        if (sqlCon.State == ConnectionState.Closed) sqlCon.Open();
        string sReturn = "";
        DataTable _dt = new DataTable();

        new SqlDataAdapter(sQuery, sqlCon).Fill(_dt);

        if (_dt.Rows.Count >= 1)
        {
            sReturn = "" + (Convert.ToDecimal(_dt.Rows[0][0]) + 1);
        }
        else
        {
            sReturn = "1";
        }
        _dt.Dispose();
        return sReturn;
    }

    public static T GetAppSetting<T>(string key)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();
        var result = configuration.GetValue<T>("AppSetting:" + key);
        return result;
    }

    public static void CallEntity(DbContextOptionsBuilder optionsBuilder, string sConnectionName)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();
        optionsBuilder.UseSqlServer(configuration.GetConnectionString(sConnectionName));
    }

    public static string GetConnectionString(string sConnName)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();
        string result = configuration.GetConnectionString(sConnName);
        return result;
    }
    public static string GetAppSettingJson(string GetParameter)
    {
        string Result = "";
        var builder = new ConfigurationBuilder();
        builder.SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", false);
        IConfigurationRoot configuration = builder.Build();
        IConfigurationSection section = configuration.GetSection(GetParameter);
        Result = section.Value;
        return Result;
    }
    public static T GetAPISetting<T>(string key)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();
        var result = configuration.GetValue<T>("API:" + key);
        return result;
    }
}
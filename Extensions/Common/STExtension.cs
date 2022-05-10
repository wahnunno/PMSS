using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Globalization;
using System.Web;

/// <summary>
/// Summary description for sysExtenstion
/// </summary>

namespace Extensions.Common.STExtension
{
    public static class STExtension
    {
        public static bool IsNumber(this string instance)
        {
            foreach (char ch in instance)
            {
                if (!char.IsNumber(ch)) return false;
            }
            return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="collections"></param>
        /// <returns></returns>

        public static bool IsDigit(this string instance)
        {
            foreach (char ch in instance)
            {
                var checkT = ch;
                if (!instance.Any(char.IsDigit)) return false;
            }
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="collections"></param>
        /// <returns></returns>
        public static bool IsContainedBy(this object instance, IEnumerable collections)
        {
            foreach (var item in collections)
            {
                if (item.Equals(instance)) return true;
            }
            return false;
        }

        /// <summary>
        /// Equals Value, Ex. "1".IsContainedBy("11,12,13");
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="sval">format : "1,2,3" or "x,y,z"</param>
        /// <returns></returns>
        public static bool IsContainedBy(this object instance, string sval)
        {
            string[] arr = (sval + "").Split(',');
            foreach (var item in arr)
            {
                if (item.Equals(instance)) return true;
            }
            return false;
        }

        /// <summary>
        /// Convert IEnumerable variable to string with SQL: In Condition
        /// int[] arrUser = { 1, 2, 3 };
        /// string sql = "select * from TUser where nUserID in (" + arrUser.ToSQLInCondition() + ")";
        /// </summary>
        /// <param name="collections"></param>
        /// <returns>case null value return ''</returns>
        public static string ToSQLInCondition(this IEnumerable instance)
        {
            string result = "";
            foreach (var item in instance)
            {
                result += ",'" + ReplaceInjection(item + "") + "'";
            }
            result = result.Length > 1 ? result.Remove(0, 1) : "''";
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <returns>'Value'</returns>
        public static string ToSQLString(this string instance)
        {
            return "'" + ReplaceInjection(instance + "") + "'";
        }

        #region Numberic
        public static string ToStringFromNumber(this int? instance)
        {
            return instance != null ? instance.Value.ToString("n0") : "";
        }

        public static string ToStringFromNumber(this decimal? instance, int nDigit)
        {
            return instance != null ? instance.Value.ToString("n" + nDigit) : "";
        }

        public static string ToStringFromNumber(this decimal? instance, int nDigit, bool IsPercent = false)
        {
            return instance != null ? instance.Value.ToString(("n" + nDigit) + (IsPercent ? "%" : "")) : "";
        }

        public static string ToStringFromNumber(this double? instance, int nDigit, bool IsPercent = false)
        {
            return instance != null ? instance.Value.ToString(("n" + nDigit) + (IsPercent ? "%" : "")) : "";
        }

        public static string ReplaceExponential(this string sVal)
        {
            string sRsult = "";
            try
            {
                decimal nTemp = 0;
                bool check = decimal.TryParse((sVal + "").Replace(",", ""), System.Globalization.NumberStyles.Float, null, out nTemp);
                if (check)
                {
                    decimal d = decimal.Parse((sVal + "").Replace(",", ""), System.Globalization.NumberStyles.Float);
                    sRsult = (d + "").Replace(",", "");
                }
                else
                {
                    sRsult = sVal;
                }
            }
            catch
            {
                sRsult = sVal;
            }

            return sRsult;
        }

        public static int? ToIntOrNull(this string instance)
        {
            int? nTemp = null;
            if (!string.IsNullOrEmpty(instance))
            {
                instance = ReplaceExponential(instance);
                int nCheck = 0;
                bool cCheck = int.TryParse(instance, out nCheck);
                if (cCheck)
                {
                    nTemp = int.Parse(instance);
                }
            }
            return nTemp;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <returns>Is Null return zero</returns>
        public static int ToInt(this string instance)
        {
            return instance.ToIntOrNull() ?? 0;
        }

        public static int ToInt(this int? instance)
        {
            return instance.HasValue ? instance.Value : 0;
        }
        public static DateTime ToDateTime(this double instance)
        {
            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddMilliseconds(instance).ToLocalTime();
            return dtDateTime;
        }

        public static decimal? ToDecimalOrNull(this string instance)
        {
            decimal? nTemp = null;
            if (!string.IsNullOrEmpty(instance))
            {
                decimal nCheck = 0;
                instance = ReplaceExponential(instance);
                bool cCheck = decimal.TryParse(instance, out nCheck);
                if (cCheck)
                {
                    nTemp = decimal.Parse(instance);
                }
            }

            return nTemp;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <returns>Is Null return zero</returns>
        public static decimal ToDecimal(this string instance)
        {
            return instance.ToDecimalOrNull() ?? 0m;
        }

        public static double? ToDoubleOrNull(this string instance)
        {
            double? nTemp = null;
            double nCheck = 0;
            if (!string.IsNullOrEmpty(instance))
            {
                instance = ReplaceExponential(instance);
                bool cCheck = double.TryParse(instance, out nCheck);
                if (cCheck)
                {
                    nTemp = double.Parse(instance);
                }
            }

            return nTemp;
        }

        public static double ToDouble(this string instance)
        {
            return instance.ToDoubleOrNull() ?? 0;
        }

        public static double? ToDouble(this string instance,int ndecimal)
        {
            if (instance.ToDoubleOrNull() != null)
            {
                var val = instance.ToDoubleOrNull();
                return Math.Round(val??0,ndecimal);

            }
            else
            {
                return null;
            }
        }
        #endregion

        #region Datetime
        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="sFormat">dd/MM/yyyy, dd/MM/yyyy HH:mm, dd/MM/yyyy HH:mm tt</param>
        /// <param name="sCulture">en-US, th-TH</param>
        /// <returns></returns>
        public static string ToStringFromDate(this DateTime? instance, string sFormat = "dd/MM/yyyy", string sCulture = "en-US")
        {
            return instance != null ? instance.Value.ToStringFromDate(sFormat, sCulture) : "";
        }
        public static string ToStringFromDateTime(this DateTime? instance, string sFormat = "dd/MM/yyyy HH:mm", string sCulture = "en-US")
        {
            return instance != null ? instance.Value.ToStringFromDate(sFormat, sCulture) : "";
        }
        public static DateTime? ToDateFromString(this string instance, string sFormat = "dd/MM/yyyy", string sCulture = "en-US")
        {
            try
            {
                string strTime = "";
                DateTime? dResult = null;
                DateTime dTemp;

                bool checkDate = DateTime.TryParseExact(instance + " " + ((strTime) != "" ? strTime : "00.00"), "yyyy-MM-dd HH.mm", new CultureInfo("en-US"), DateTimeStyles.None, out dTemp);
                if (!checkDate)
                {
                    if (strTime.Length < 5)
                    {
                        dResult = DateTime.TryParseExact(instance + " " + ((strTime) != "" ? "0" + strTime : "00.00"), "yyyy-MM-dd HH.mm", new CultureInfo("en-US"), DateTimeStyles.None, out dTemp) ? dTemp : (DateTime?)null;
                    }
                }
                else
                {
                    dResult = DateTime.TryParseExact(instance + " " + ((strTime) != "" ? strTime : "00.00"), "yyyy-MM-dd HH.mm", new CultureInfo("en-US"), DateTimeStyles.None, out dTemp) ? dTemp : (DateTime?)null;
                }

                if (!dResult.HasValue && !string.IsNullOrEmpty(instance))
                {
                    string[] sDateTemp = instance.Split('/');
                    string sDate_Index = sDateTemp[0];
                    if (sDate_Index.Count() == 1)
                    {
                        sDate_Index = "0" + sDate_Index;
                    }
                    string sMonth_Index = sDateTemp[1];
                    if (sMonth_Index.Count() == 1)
                    {
                        sMonth_Index = "0" + sMonth_Index;
                    }
                    instance = sDateTemp[2].Substring(0, 4).Trim() + "-" + sMonth_Index + "-" + sDate_Index;
                    checkDate = DateTime.TryParseExact(instance, "yyyy-MM-dd", new CultureInfo("en-US"), DateTimeStyles.None, out dTemp);
                    if (!checkDate)
                    {
                        if (strTime.Length < 5)
                        {
                            dResult = DateTime.TryParseExact(instance, "yyyy-MM-dd", new CultureInfo("en-US"), DateTimeStyles.None, out dTemp) ? dTemp : (DateTime?)null;
                        }
                    }
                    else
                    {
                        dResult = DateTime.TryParseExact(instance, "yyyy-MM-dd", new CultureInfo("en-US"), DateTimeStyles.None, out dTemp) ? dTemp : (DateTime?)null;
                    }
                }

                return dResult;
            }
            catch { return null; }

        }

        public static DateTime? ToDateTimeFromString(this string instance, string sFormat = "dd/MM/yyyy HH:mm", string sCulture = "en-US")
        {
            try
            {
                DateTime? dResult = null;
                DateTime dTemp;

                dResult = DateTime.TryParseExact(instance, sFormat, new CultureInfo("en-US"), DateTimeStyles.None, out dTemp) ? dTemp : (DateTime?)null;
                return dResult;
            }
            catch { return null; }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="sFormat">dd/MM/yyyy, dd/MM/yyyy HH:mm, dd/MM/yyyy HH:mm tt</param>
        /// <param name="sCulture">en-US, th-TH</param>
        /// <returns></returns>
        public static string ToStringFromDate(this DateTime instance, string sFormat = "dd/MM/yyyy", string sCulture = "en-US")
        {
            sCulture = string.IsNullOrEmpty(sCulture) ? "en-US" : sCulture;
            return instance.ToString(sFormat, new CultureInfo(sCulture));
        }
        public static string ToStringFromDateTime(this DateTime instance, string sFormat = "dd/MM/yyyy HH:mm", string sCulture = "en-US")
        {
            return instance.ToStringFromDate(sFormat, sCulture);
        }

        public static string ToStringFromDateTimeSecord(this DateTime instance, string sFormat = "dd/MM/yyyy HH:mm:ss", string sCulture = "en-US")
        {
            return instance.ToStringFromDateTime(sFormat, sCulture);
        }
        public static string ToStringFromDateTimeMilisecord(this DateTime instance, string sFormat = "dd/MM/yyyy HH:mm:ss:ms", string sCulture = "en-US")
        {
            return instance.ToStringFromDateTime(sFormat, sCulture);
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static string GetNameFromEmail(this object instance)
        {
            return instance != null ? (instance + "").Split('@')[0] + "" : "";
        }

        /// <summary>
        /// Extension
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="nStartIndex"></param>
        /// <param name="nLength"></param>
        /// <returns></returns>
        public static string SubStr(this string instance, int nStartIndex, int nLength)
        {
            if (!string.IsNullOrEmpty(instance))
            {
                if (instance.Length < (nStartIndex + nLength))
                {
                    return instance.Substring(nStartIndex, instance.Length);
                }
                else
                {
                    return instance.Substring(nStartIndex, nLength);
                }
            }
            else
                return "";
        }

        public static string SubStrMax(this string instance, int nMaxLength)
        {
            return instance.SubStr(0, nMaxLength);
        }

        public static string TrimAll(this string instance)
        {
            return (instance + "").Trim();
        }

        //public static string EncryptParameter(this string instance)
        //{
        //    return !string.IsNullOrEmpty(instance) ? HttpUtility.UrlEncode(STCrypt.Encrypt(instance)) : "";
        //}

        //public static string EncryptParameter(this int instance)
        //{
        //    return EncryptParameter(instance.ToString());
        //}

        //public static string DecryptParameter(this string instance)
        //{
        //    string Result = "";
        //    // try
        //    // {
        //    // 	Result = !string.IsNullOrEmpty(instance) ? STCrypt.Decrypt(instance) : "";
        //    // }
        //    // catch (Exception ex)
        //    // {
        //    // 	string Message = ex.Message;
        //    // }
        //    string Message = "";
        //    try
        //    {
        //        Result = !string.IsNullOrEmpty(instance) ? HttpUtility.UrlDecode(STCrypt.Decrypt(instance)) : "";
        //    }
        //    catch (Exception ex)
        //    {
        //        Message = ex.Message;
        //        try
        //        {
        //            Result = !string.IsNullOrEmpty(instance) ? STCrypt.Decrypt(HttpUtility.UrlDecode(instance)) : "";
        //        }
        //        catch (Exception ex2)
        //        {
        //            Message = ex2.Message;
        //            try
        //            {
        //                Result = !string.IsNullOrEmpty(instance) ? STCrypt.Decrypt(instance) : "";
        //            }
        //            catch (Exception ex3)
        //            {
        //                Message = ex3.Message;
        //            }
        //        }
        //    }

        //    return Result;
        //}

        #region Replace
        /// <summary>
        /// Anti SQL Injection
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ReplaceInjection(this string instance)
        {
            string[] _blacklist = new string[] { "'", "\\", "\"", "*/", ";", "--", "<script", "/*", "</script>" };
            string strRep = instance;
            if (strRep == null || strRep.Trim().Equals(String.Empty))
                return strRep;
            foreach (string _blk in _blacklist) { strRep = strRep.Replace(_blk, ""); }

            return strRep;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="collections"></param>
        /// <returns></returns>
        public static string Replace(this string instance, IEnumerable collections, string sNewValue = "")
        {
            string strRep = instance;
            if (strRep == null || strRep.Trim().Equals(String.Empty))
                return strRep;
            foreach (var _blk in collections) { strRep = strRep.Replace(_blk + "", sNewValue); }

            return strRep;
        }

        public static string ReplaceComma(this string instance)
        {
            return (instance + "").Replace(",", "");
        }

        public static string ReplaceHtml(this string str)
        {
            string[] _blacklist = new string[] { "<sub>", "</sub>", "<sup>", "</sup>", "<br/>" };
            string strRep = str;
            if (strRep == null || strRep.Trim().Equals(String.Empty))
                return strRep;
            foreach (string _blk in _blacklist) { strRep = strRep.Replace(_blk, ""); }

            return strRep;
        }

        public static string ToExcelSheetName(this string instance)
        {
            string[] _blacklist = new string[] { "'", "\\", "\"", "*/", ";", "--", "<script", "/*", "</script>", "/", "[", "]", ":", "?", "<", ">", "*" };
            string strRep = instance;
            if (strRep == null || strRep.Trim().Equals(String.Empty))
                return strRep;
            foreach (string _blk in _blacklist)
            {
                strRep = strRep.Replace(_blk, "");
            }
            strRep = strRep.SubStrMax(31);
            return strRep;
        }
        #endregion

        #region Sorting
        private static PropertyInfo GetPropertyInfo(Type objType, string name)
        {
            var properties = objType.GetProperties();
            var matchedProperty = properties.FirstOrDefault(p => p.Name == name);
            if (matchedProperty == null)
                throw new ArgumentException("name");

            return matchedProperty;
        }

        private static LambdaExpression GetOrderExpression(Type objType, PropertyInfo pi)
        {
            var paramExpr = Expression.Parameter(objType);
            var propAccess = Expression.PropertyOrField(paramExpr, pi.Name);
            var expr = Expression.Lambda(propAccess, paramExpr);
            return expr;
        }

        public static IEnumerable<T> OrderBy<T>(this IEnumerable<T> query, string name)
        {
            var propInfo = GetPropertyInfo(typeof(T), name);
            var expr = GetOrderExpression(typeof(T), propInfo);

            var method = typeof(Enumerable).GetMethods().FirstOrDefault(m => m.Name == "OrderBy" && m.GetParameters().Length == 2);
            var genericMethod = method.MakeGenericMethod(typeof(T), propInfo.PropertyType);
            return (IEnumerable<T>)genericMethod.Invoke(null, new object[] { query, expr.Compile() });
        }

        public static IEnumerable<T> OrderByDescending<T>(this IEnumerable<T> query, string name)
        {
            var propInfo = GetPropertyInfo(typeof(T), name);
            var expr = GetOrderExpression(typeof(T), propInfo);

            var method = typeof(Enumerable).GetMethods().FirstOrDefault(m => m.Name == "OrderByDescending" && m.GetParameters().Length == 2);
            var genericMethod = method.MakeGenericMethod(typeof(T), propInfo.PropertyType);
            return (IEnumerable<T>)genericMethod.Invoke(null, new object[] { query, expr.Compile() });
        }

        public static IQueryable<T> OrderBy<T>(this IQueryable<T> query, string name)
        {
            var propInfo = GetPropertyInfo(typeof(T), name);
            var expr = GetOrderExpression(typeof(T), propInfo);

            var method = typeof(Queryable).GetMethods().FirstOrDefault(m => m.Name == "OrderBy" && m.GetParameters().Length == 2);
            var genericMethod = method.MakeGenericMethod(typeof(T), propInfo.PropertyType);
            return (IQueryable<T>)genericMethod.Invoke(null, new object[] { query, expr });
        }

        public static IQueryable<T> OrderByDescending<T>(this IQueryable<T> query, string name)
        {
            var propInfo = GetPropertyInfo(typeof(T), name);
            var expr = GetOrderExpression(typeof(T), propInfo);

            var method = typeof(Enumerable).GetMethods().FirstOrDefault(m => m.Name == "OrderByDescending" && m.GetParameters().Length == 2);
            var genericMethod = method.MakeGenericMethod(typeof(T), propInfo.PropertyType);
            return (IQueryable<T>)genericMethod.Invoke(null, new object[] { query, expr.Compile() });
        }
        #endregion

        #region Sum
        public static decimal? SumOrDefault(this List<decimal?> instance)
        {
            if (instance.Any(x => x.HasValue))
                return instance.Sum();
            else
                return null;
        }

        public static double? SumOrDefault(this List<double?> instance)
        {
            if (instance.Any(x => x.HasValue))
                return instance.Sum();
            else
                return null;
        }

        public static int? SumOrDefault(this List<int?> instance)
        {
            if (instance.Any(x => x.HasValue))
                return instance.Sum();
            else
                return null;
        }
        #endregion
    }
}
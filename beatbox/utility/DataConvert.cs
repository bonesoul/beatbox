using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public static class DataConvert
{
    #region Safe Int
    /// <summary>
    /// Converts the string representation of a number to its 32-bit signed integer.
    /// </summary>
    /// <param name="s">A string containing a number to convert.</param>
    /// <returns>return 32-bit signed integer  if s was converted successfully; otherwise, 0.</returns>
    public static int SafeInt(string s)
    {
        if (string.IsNullOrEmpty(s))
        {
            return 0;
        }
        int i;

        bool success = int.TryParse(s, out i);

        if (success)
        {
            return i;
        }
        else
        {
            return Convert.ToInt32(SafeDouble(s));
        }
    }


    /// <summary>
    /// Converts object to its 32-bit signed integer.      
    /// if convert fails, then return 0 
    /// </summary>
    /// <param name="obj">A object to convert</param>
    /// <returns>return 32-bit signed integer if s was converted successfully; otherwise, 0.</returns>
    public static int SafeInt(object obj)
    {
        if (typeof(Enum).IsInstanceOfType(obj))
        {
            return (int)obj;
        }
        return SafeInt(string.Format("{0}", obj));
    }
    #endregion

    #region Safe Double
    /// <summary>
    /// Converts the string representation of a number to its double-precision floating-point
    ///  number equivalent.
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static double SafeDouble(string s)
    {
        double d;
        double.TryParse(s, out d);
        return d;
    }

    /// <summary>
    /// Safe convert object to double precision number
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static double SafeDouble(object obj)
    {
        return SafeDouble(string.Format("{0}", obj));
    }
    #endregion

    #region Safe DateTime
    /// <summary>
    /// return the System.DateTime value equivalent to
    /// the date and time contained in s, if the conversion succeeded, or System.DateTime.MinValue
    /// </summary>
    /// <param name="s">A string containing a date and time to convert.</param>
    /// <returns></returns>
    public static DateTime SafeDateTime(string s)
    {

        DateTime value;

        try
        {
            value = Convert.ToDateTime(s, new System.Globalization.CultureInfo("tr-TR"));
        }
        catch
        {
            value = Convert.ToDateTime("1.1.1900");
        }

        return value;


    }

    /// <summary>
    /// Safe conver object to DateTime
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static DateTime SafeDateTime(object obj)
    {
        return SafeDateTime(string.Format("{0}", obj));
    }
    #endregion

    #region Safe String
    /// <summary>
    /// Safe convert DateTime to string. if is MinValue or MaxValue, return empty string.
    /// </summary>
    /// <param name="d"></param>
    /// <returns></returns>
    public static string SafeString(DateTime d)
    {
        if (d == DateTime.MinValue || d == DateTime.MaxValue) return string.Empty;
        else return d.ToString();
    }

    /// <summary>
    /// if string is null, return empty string, else return string.
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static string SafeString(string s)
    {
        if (string.IsNullOrEmpty(s)) return string.Empty;
        else return s;
    }

    /// <summary>
    /// safe convert object to string
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static string SafeString(object obj)
    {
        return SafeString(string.Format("{0}", obj));
    }
    #endregion

    #region Safe Decimal
    /// <summary>
    /// Safe Decimal
    /// </summary>
    /// <param name="str">string</param>
    /// <returns>decimal</returns>
    public static decimal SafeDecimal(string str)
    {
        decimal d;
        decimal.TryParse(str, out d);
        return d;
    }

    /// <summary>
    /// Safe Decimal
    /// </summary>
    /// <param name="obj">object can convert to decimal</param>
    /// <returns>decimal</returns>
    public static decimal SafeDecimal(object obj)
    {
        return SafeDecimal(string.Format("{0}", obj));
    }
    #endregion

    #region Safe Float
    /// <summary>
    /// Converts the string representation of a number to float
    /// </summary>
    /// <param name="str">string</param>
    /// <returns>float</returns>
    public static float SafeFloat(string str)
    {
        float f;
        float.TryParse(str, out f);
        return f;
    }

    /// <summary>
    /// Converts the object representation of a number to float
    /// </summary>
    /// <param name="obj">object</param>
    /// <returns>float</returns>
    public static float SafeFloat(object obj)
    {
        return SafeFloat(string.Format("{0}", obj));
    }


    #endregion

    #region Safe GUID
    /// <summary>
    /// Safe convert string to Guid, if convert fail, then return Guid.Empty
    /// </summary>
    /// <param name="str">string can convert to Guid</param>
    /// <returns>Guid, if convert fail, then return Guid.Empty</returns>
    public static Guid SafeGuid(string str)
    {
        try
        {
            Guid g = new Guid(str);
            return g;
        }
        catch
        {
            return Guid.Empty;
        }
    }

    /// <summary>
    /// Safe convert string to Guid, if convert fail, then return Guid.Empty
    /// </summary>
    /// <param name="obj">object that can convert Guid</param>
    /// <returns>Guid, if convert fail, then return Guid.Empty</returns>
    public static Guid SafeGuid(object obj)
    {
        return SafeGuid(string.Format("{0}", obj));
    }
    #endregion

    public static byte SafeByte(object obj)
    {
        return SafeByte(string.Format("{0}", obj));
    }
    public static byte SafeByte(string s)
    {
        byte b;
        byte.TryParse(s, out b);
        return b;
    }

}

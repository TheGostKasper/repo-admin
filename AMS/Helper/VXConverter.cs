using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace AMS.Helper
{
    public static class VXConverter
    {
        public static DateTime FromUnixTime(long unixTime)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return epoch.AddSeconds(unixTime);
        }

        public static int ToUnixTime(DateTime date)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return Convert.ToInt32((date - epoch).TotalSeconds);
        }
        public static long ToUnixTimeMilli(DateTime date)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return (long)((date - epoch).TotalMilliseconds);
        }
        public static string ObjectToJson(object obj)
        {
            var serializer = new JavaScriptSerializer();
            return serializer.Serialize(obj);
        }
        public static T JsonToObject<T>(string str)
        {
            var serializer = new JavaScriptSerializer();
            return serializer.Deserialize<T>(str);
        }
        public static DateTime AddSecondsToDateTime(int seconds)
        {
            TimeSpan time = TimeSpan.FromSeconds(seconds);
            DateTime dateTime = DateTime.Today.Add(time);
            return dateTime;
        }
        public static string ConvertSecondsToTime(int seconds)
        {
            TimeSpan time = TimeSpan.FromSeconds(seconds);
            return string.Format("{0:D2}h:{1:D2}m:{2:D2}s", time.Hours, time.Minutes, time.Seconds);
        }
        public static double CalcuateTripCost(int distance)
        {
            var totalPrice = 9;
            var meterPrice = Convert.ToDouble(ConfigurationManager.AppSettings["MeterPrice"]);
            // distance = distance - 804.672;
            var distancePrice = Math.Round(distance * meterPrice) + totalPrice;
            return (double)distancePrice;
        }

        public static string GetDefaultValue(string value)
        {
            return (value == "" || value == null) ? "--" : value;
        }

        public static DateTime GetDateFromWeekDay(DateTime dt, string day, string openTime)
        {
            var _date = DateTime.Parse(dt.ToString());
            var num = (int)_date.DayOfWeek;
            var num2 = (int)((DayOfWeek)Enum.Parse(typeof(DayOfWeek), day));
            var days = num2 - num;

            TimeSpan time = TimeSpan.Parse(openTime.ToLower().Replace("am", "").Replace("pm", ""));
            _date = _date.Date + time;

            if (days > 0)
                return _date.AddDays(days);
            else
                return _date.AddDays(7 + days);
        }
        public static string EncodeUrl(string url)
        {
            return HttpUtility.UrlEncode(url);
        }
    }
}
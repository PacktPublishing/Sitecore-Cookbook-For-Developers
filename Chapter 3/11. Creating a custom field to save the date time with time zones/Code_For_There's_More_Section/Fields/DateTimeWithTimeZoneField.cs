using Sitecore;
using Sitecore.Data.Fields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SitecoreCookbook.Fields
{
    public class DateTimeWithTimeZoneField : CustomField
    {
        public override string ToString()
        {
            DateTime dateTime = this.DateTime;
            return dateTime.ToString("yyyyMMddTHHmmss");
        }
        /// <summary>
        /// Convert saved DateTime value and Time Zone in local Time Zone
        /// </summary>
        public DateTime DateTime
        {
            get
            {
                DateTime dateTime = System.DateTime.MinValue;

                string[] fieldValue = this.Value.Split(new char[] { '|' }, System.StringSplitOptions.None);

                string datetimeText = dateTime.ToString("yyyyMMddTHHmmss");
                string timezone = TimeZoneInfo.Local.Id;

                if (fieldValue.Length > 0)
                {
                    datetimeText = fieldValue[0];
                    dateTime = DateUtil.ParseDateTime(datetimeText, dateTime);
                }
                if (fieldValue.Length == 2)
                {
                    timezone = fieldValue[1];
                    TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById(timezone);
                    return dateTime.AddMinutes(info.BaseUtcOffset.TotalMinutes);
                }
                return dateTime;
            }
        }

        public DateTimeWithTimeZoneField(Field innerField)
            : base(innerField)
        {
        }

        public static implicit operator DateTimeWithTimeZoneField(Field field)
        {
            if (field != null)
                return new DateTimeWithTimeZoneField(field);
            return (DateTimeWithTimeZoneField)null;
        }
    }
}
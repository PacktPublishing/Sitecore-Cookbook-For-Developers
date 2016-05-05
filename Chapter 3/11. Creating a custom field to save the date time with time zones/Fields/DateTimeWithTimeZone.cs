using Sitecore;
using Sitecore.Shell.Applications.ContentEditor;
using Sitecore.Web.UI.HtmlControls;
using Sitecore.Web.UI.Sheer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SitecoreCookbook.Fields
{
    public class DateTimeWithTimeZone : Control, IContentField
    {
        // For selecting date and time
        private DateTimePicker _picker;

        // For selecting Time zone 
        private Combobox _combo;

        // Seperator between Date time and Time Zone
        private char _fieldSeparator = '|';

        // Handling menu button messages
        public override void HandleMessage(Message message)
        {
            base.HandleMessage(message);
            if (message["id"] != this.ID)
                return;
            switch (message.Name)
            {
                case "datetimezone:today":
                    SetValue(System.DateTime.Now.ToString("yyyyMMddTHHmmss"));
                    break;
                case "datetimezone:clear":
                    SetValue("");
                    break;
            }
        }

        public string GetValue()
        {
            if (this._picker != null)
                return _picker.Value + _fieldSeparator + _combo.Value;
            return "";
        }

        public void SetValue(string value)
        {
            if (_picker == null || _combo == null)
                return;
            string[] dateTime = value.Split(new char[] { _fieldSeparator }, System.StringSplitOptions.None);

            if (dateTime.Length > 0)
                _picker.Value = DateUtil.IsoDateToServerTimeIsoDate(dateTime[0]);
            
            if (dateTime.Length == 2)
            { 
                _combo.Value = dateTime[1];
                foreach (var listitem in _combo.Items)
                {
                    if (listitem.Value == _combo.Value)
                        listitem.Selected = true;
                }
            }
        }

        private void FillTimeZones()
        {
            var timeZones = TimeZoneInfo.GetSystemTimeZones();
            foreach (var timeZone in timeZones)
            {
                ListItem item = new ListItem();
                item.Value = timeZone.Id;
                item.Header = timeZone.DisplayName;
                _combo.Controls.Add(item);
            }
        }

        // Create controls and add to the UI
        protected override void OnInit(EventArgs e)
        {
            _picker = new DateTimePicker();
            _picker.ID = this.ID + "_picker";
            _picker.Changed += ((p0, p1) => this.SetModified());
            this.Controls.Add(_picker);

            _combo = new Combobox();
            _combo.ID = this.ID + "_combo";
            _combo.OnChange+= ((p0, p1) => this.SetModified());
            this.Controls.Add(_combo);
            FillTimeZones();

            base.OnInit(e);
        }

        protected void SetModified()
        {
            Sitecore.Context.ClientPage.Modified = true;
        }

        protected virtual string GetCurrentDate()
        {
            return DateUtil.ToIsoDate(DateUtil.ToServerTime(System.DateTime.UtcNow).Date);
        }
    }
}
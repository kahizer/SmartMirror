using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMirrorWinUniv.Concreates
{
    public class CalendarStatus
    {
        #region Fields


        #endregion

        #region Constructor

        public CalendarStatus()
        {
            this.CalenderItems = new List<CalendarItem>();
        }

        #endregion

        #region Public Properties

        public List<CalendarItem> CalenderItems { get; set; }

        #endregion

        #region Public Properties

        public void AddRawCalendarItems(string jsonData, string type)
        {
            if (this.CalenderItems == null) this.CalenderItems = new List<CalendarItem>();

            dynamic jsonObject = Newtonsoft.Json.JsonConvert.DeserializeObject(jsonData);
            var items = jsonObject.items;

            foreach (dynamic item in items)
            {
                try
                {
                    dynamic startobject = item.start;
                    dynamic dt = startobject.date;
                    dynamic value = dt.Value;
                    string dtString = value.ToString();
                    DateTime dueDate = DateTime.Parse(dtString);
                    string title = item.summary;

                    var calendarItem = new CalendarItem { DueDate = dueDate, Title = title, EasyDueDate = this.GetEasyDueDate(dueDate), Type = type, IconPath = this.GetIconPath(type)};
                    this.CalenderItems.Add(calendarItem);
                }
                catch (Exception ex)
                {
                    var msg = ex.Message;
                }

            }

            this.CalenderItems.Sort((x, y) => DateTime.Compare(x.DueDate, y.DueDate));
        }

        #endregion

        #region Private Methods

        private string GetIconPath(string type)
        {
            if (type.Equals("personal")) return $"Resources/calendar-24.png";

            return $"Resources/public-calendar-24.png";
        }

        private string GetEasyDueDate(DateTime dueDate)
        {
            int days = dueDate.Subtract(DateTime.Now).Days;

            if (days > 360)
            {
                var years = days / 360;
                return $"in {years} years";
            }
            else if (days > 30)
            {
                var months = days / 30;

                return months == 1 ? $"in {months} month" : $"in {months} months";
            }
            else if(days > 1)
            {
                return $"in {days} days";
            }
            else if (days == 1)
            {
                return "Tomorrow";
            }
            else
            {
                return "Today";
            }
        }

        #endregion
    }

    public class CalendarItem
    {
        public string Title { get; set; }
        public DateTime DueDate { get; set; }
        public string EasyDueDate { get; set; }
        public string Type { get; set; }
        public string IconPath { get; set; }

    }

}

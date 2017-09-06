﻿using System;
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

        public CalendarStatus(string rawjson)
        {
            this.CalenderItems = new List<CalendarItem>();
            this.Deserialize(rawjson);
        }

        #endregion

        #region Public Properties

        public List<CalendarItem> CalenderItems { get; set; }

        #endregion

        #region Private Methods
        private void Deserialize(string jsonData)
        {
            dynamic jsonObject = Newtonsoft.Json.JsonConvert.DeserializeObject(jsonData);
            var currentDate = DateTime.Now;
            var items = jsonObject.items;

            foreach(dynamic item in items)
            {
                var dtObject = item.start.dateTime.Value;
                DateTime dueDate = (DateTime)dtObject;
                string title = item.summary;

                var calendarItem = new CalendarItem { DueDate = dueDate, Title = title, EasyDueDate = this.GetEasyDueDate(dueDate) };
                this.CalenderItems.Add(calendarItem);
            }
        }

        private string GetEasyDueDate(DateTime dueDate)
        {
            int days = dueDate.Subtract(DateTime.Now).Days;

            if (days > 360)
            {
                var years = days / 360;
                return $"{years} years";
            }
            else if (days > 30)
            {
                var months = days / 30;
                return $"{months} months";
            }
            else if(days > 0)
            {
                return $"{days} days";
            }
            else
            {
                return "today";
            }
        }

        #endregion

    }

    public class CalendarItem
    {
        public string Title { get; set; }
        public DateTime DueDate { get; set; }
        public string EasyDueDate { get; set; }

    }

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMirrorWinUniv.Concreates
{
    public class CurrentTime
    {
        #region
        #endregion

        #region

        public CurrentTime(DateTime datetime)
        {
            this.DayName = datetime.DayOfWeek.ToString();
            this.Month = this.GetMonthName(datetime.Month);
            this.DayNumber = datetime.Day;
            this.DayInfo = $"{this.DayName}, {this.Month} {this.DayNumber}";
            this.ShortTime = datetime.ToString("h:mm tt");
        }

        #endregion

        #region Public Properties
        public string DayInfo { get; set; }
        public string DayName { get; set; }
        public string Month { get; set; }
        public int DayNumber { get; set; }
        public string ShortTime { get; set; }

        #endregion

        #region Private Methods
        
        private string GetMonthName(int monthNumber)
        {
            switch (monthNumber)
            {
                case 1:
                    return "January";

                case 2:
                    return "February";

                case 3:
                    return "March";

                case 4:
                    return "April";

                case 5:
                    return "May";

                case 6:
                    return "June";

                case 7:
                    return "July";

                case 8:
                    return "August";

                case 9:
                    return "September";

                case 10:
                    return "October";

                case 11:
                    return "November";

                case 12:
                    return "December";

                default:
                    return "Unknown";
            }
        }
        
        #endregion
    }
}

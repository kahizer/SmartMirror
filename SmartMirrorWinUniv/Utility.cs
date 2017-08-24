using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMirrorWinUniv
{
    public class Utility
    {
        public static DateTime GetDatetimeFromUnix(long unixTimeStamp)
        {
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp);
            return dtDateTime.ToLocalTime();
        }

        //public static DateTime GetDatetimeFromUnix(DateTimeOffset date)
        //{
        //    //System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
        //    //dtDateTime = dtDateTime.AddSeconds(unixTimeStamp);
        //    //return dtDateTime.ToLocalTime();
        //}
    }
}

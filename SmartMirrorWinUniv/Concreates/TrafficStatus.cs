using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMirrorWinUniv.Concreates
{
    public class TrafficStatus
    {
        #region Fields
        #endregion

        #region Constructor

        public TrafficStatus(string jsonString)
        {
            this.DeSerializeJson(jsonString);
        }

        #endregion

        #region Public Methods

        public int Distance { get; set; }

        public int Duration { get; set; }

        public DateTime ArrivalTime { get; set; }

        public string TrafficInformation { get; set; }

        #endregion

        #region Private Methods

        #region

        private void DeSerializeJson(string jsonData)
        {
            dynamic jsonObject = Newtonsoft.Json.JsonConvert.DeserializeObject(jsonData);

            double rawDistance = (double)jsonObject.routes[0].legs[0].distance.value;
            var rawTime = (int)jsonObject.routes[0].legs[0].duration.value;

            this.Distance = (int)(rawDistance * 0.00062137);
            this.Duration = rawTime / 60;

            this.ArrivalTime = DateTime.Now.AddSeconds(rawTime);
         }

        #endregion

        #endregion
    }
}

﻿namespace SmartMirrorWinUniv.Concreates
{
    using DarkSky.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    public class WeatherStatus
    {
        #region Fields
        #endregion

        #region Constructor

        public WeatherStatus(DarkSkyResponse rawResponse)
        {
            this.Summary = rawResponse.Response.Currently.Summary;
            this.DetailedSummary = rawResponse.Response.Hourly.Summary;
            this.Temperature = (int)rawResponse.Response.Currently.Temperature;
            this.IconCode = WeatherDaySummary.GetIconCode(rawResponse.Response.Currently.Icon);
            this.WindSpeed = (int)rawResponse.Response.Currently.WindSpeed;
            this.PrecipitationPercentage = (int)(rawResponse.Response.Currently.PrecipProbability * 100);
            this.HummidityPercentage = (int)(rawResponse.Response.Currently.Humidity * 100);

            var today = rawResponse.Response.Daily.Data.FirstOrDefault();
            if (today != null)
            {
                this.SunriseTime = today.SunriseDateTime.Value.DateTime;//Utility.GetDatetimeFromUnix(today.SunriseDateTime);
                this.SunsetTime = today.SunsetDateTime.Value.DateTime; //Utility.GetDatetimeFromUnix(today.sunsetTime);
            }

            this.DailySummary = new List<WeatherDaySummary>();
            foreach (var dayItem in rawResponse.Response.Daily.Data)
            {
                this.DailySummary.Add(new WeatherDaySummary(dayItem));
            }
        }

        #endregion

        #region Public Properties

        public string Summary { get; set; }

        public string DetailedSummary { get; set; }

        public int Temperature { get; set; }

        public string IconCode { get; set; }

        public int WindSpeed { get; set; }

        public int PrecipitationPercentage { get; set; }

        public int HummidityPercentage { get; set; }

        public List<WeatherDaySummary> DailySummary { get; set; }

        public DateTime SunriseTime { get; set; }

        public DateTime SunsetTime { get; set; }

        #endregion
    }

    public class WeatherDaySummary
    {
        #region Fields
        #endregion

        #region Constructor

        public WeatherDaySummary(DataPoint day)
        {
            this.Datetime = day.DateTime.DateTime;//Utility.GetDatetimeFromUnix(day.DateTime);
            this.FullDayName = this.Datetime.DayOfWeek.ToString();
            this.ShortDayName = this.FullDayName.Substring(0, 3);
            this.LowTemp = (int)day.TemperatureMin;
            this.HightTemp = (int)day.TemperatureMax;
            this.IconCode = GetIconCode(day.Icon);
        }

        #endregion

        #region Public Properties
        public DateTime Datetime { get; set; }
        public string FullDayName { get; set; }
        public string ShortDayName { get; set; }
        public int LowTemp { get; set; }
        public int HightTemp { get; set; }
        public string IconCode { get; set; }

        #endregion

        #region

        public static string GetIconCode(Icon value)
        {
            
            switch (value)
            {
                case Icon.ClearDay:
                    return "\xf00d";

                case Icon.ClearNight:
                    return "\xf02e";

                case Icon.Rain:
                    return "\xf008";

                case Icon.Snow:
                    return "\xf065";

                case Icon.Sleet:
                    return "\xf0b2";

                case Icon.Wind:
                    return "\xf085";

                case Icon.Fog:
                    return "\xf003";

                case Icon.Cloudy:
                    return "\xf013";

                case Icon.PartlyCloudyDay:
                    return "\xf002";

                case Icon.PartlyCloudyNight:
                    return "\xf086";

                default:
                    return "";
            }
        }

        #endregion

    }
}

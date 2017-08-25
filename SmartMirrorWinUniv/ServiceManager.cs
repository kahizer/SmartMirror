using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMirrorWinUniv
{
    using SmartMirrorWinUniv.Concreates;
    using SmartMirrorWinUniv.Services;

    public class ServiceManager
    {
        #region Fields

        private QuoteServices quoteServices;

        private WeatherServices weatherServices;

        private NewsServices newsServices;

        private GoogleMapsServices googleMapsServices;

        private float lat = 37.621998f;
        private float logt = -122.073551f;

        #endregion

        #region Constructor

        public ServiceManager()
        {
            this.googleMapsServices = new GoogleMapsServices();
            this.newsServices = new NewsServices();
            this.weatherServices = new WeatherServices(lat, this.logt);
            this.quoteServices = new QuoteServices();
        }

        #endregion

        #region Public Properties

        public TrafficStatus GetTrafficInformation()
        {
            return this.googleMapsServices == null ? null : this.googleMapsServices.GetTrafficInformation();
        }

        public NewsModel GetNews()
        {
            return this.newsServices == null ? null : this.newsServices.GetNews();
        }

        public WeatherStatus GetWeather()
        {
            return this.weatherServices == null ? null : this.weatherServices.GetWeatherStatus();
        }

        public QuoteModel GetQuoteOfTheDay()
        {
            return this.quoteServices == null ? null : this.quoteServices.GetDailyQuote();
        }

        public object GetCalenderData()
        {
            // need to access to calender
            return null;
        }

        #endregion

        #region
        #endregion
    }
}

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

        private DateTimeServices dateTimeServices;

        private CalendarServices calendarServices;

        private float lat = 37.621998f;
        private float logt = -122.073551f;

        #endregion

        #region Constructor

        public ServiceManager()
        {
            this.calendarServices = new CalendarServices();
            //this.googleMapsServices = new GoogleMapsServices();
            //this.googleMapsServices.TrafficUpdateEvent += (sender, status) => { this.TrafficUpdateEvent?.Invoke(this, status); };

            //this.newsServices = new NewsServices();
            //this.newsServices.LatestNewsEvent += (sender, model) => { this.LatestNewsEvent?.Invoke(this, model); };

            //this.weatherServices = new WeatherServices(this.lat, this.logt);
            //this.weatherServices.WeatherUpdateEvent += (sender, status) => { this.WeatherUpdateEvent?.Invoke(this, status); };

            //this.quoteServices = new QuoteServices();
            //this.quoteServices.QuoteUpdateEvent += (sender, model) => { this.QuoteUpdateEvent?.Invoke(this, model); };

            //this.dateTimeServices = new DateTimeServices();
            //this.dateTimeServices.TimerUpdateEvent += (sender, time) => { this.TimerUpdateEvent?.Invoke(this, time); };
        }

        #endregion

        #region Public Events

        public event EventHandler<CurrentTime> TimerUpdateEvent;

        public event EventHandler<QuoteModel> QuoteUpdateEvent;

        public event EventHandler<WeatherStatus> WeatherUpdateEvent;

        public event EventHandler<TrafficStatus> TrafficUpdateEvent;

        public event EventHandler<NewsModel> LatestNewsEvent;

        #endregion        

        #region
        #endregion
    }
}

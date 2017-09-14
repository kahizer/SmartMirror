namespace SmartMirrorWinUniv
{
    using System;
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

        private MailServices mailServices;

        private GoogleServices googleServices;

        private float lat = 37.621998f;
        private float logt = -122.073551f;

        #endregion

        #region Constructor

        public ServiceManager()
        {
            this.googleServices = new GoogleServices();
            this.googleServices.LatestCalendarEvent += (sender, calendar) => { this.CalendarStatusEvent?.Invoke(this, calendar); };
            this.googleServices.LatestEmailsEvent += (sender, mail) => { this.LatestEmailsEvent?.Invoke(this, mail); };

            this.googleMapsServices = new GoogleMapsServices();
            this.googleMapsServices.TrafficUpdateEvent += (sender, traffic) => { this.TrafficUpdateEvent?.Invoke(this, traffic); };

            this.newsServices = new NewsServices();
            this.newsServices.LatestNewsEvent += (sender, newsmodel) => { this.LatestNewsEvent?.Invoke(this, newsmodel); };

            this.weatherServices = new WeatherServices(this.lat, this.logt);
            this.weatherServices.WeatherUpdateEvent += (sender, weather) => { this.WeatherUpdateEvent?.Invoke(this, weather); };

            this.quoteServices = new QuoteServices();
            this.quoteServices.QuoteUpdateEvent += (sender, quote) => { this.QuoteUpdateEvent?.Invoke(this, quote); };

            this.dateTimeServices = new DateTimeServices();
            this.dateTimeServices.TimerUpdateEvent += (sender, time) => { this.TimerUpdateEvent?.Invoke(this, time); };
        }

        #endregion

        #region Public Events

        public event EventHandler<CurrentTime> TimerUpdateEvent;

        public event EventHandler<QuoteModel> QuoteUpdateEvent;

        public event EventHandler<WeatherStatus> WeatherUpdateEvent;

        public event EventHandler<TrafficStatus> TrafficUpdateEvent;

        public event EventHandler<NewsModel> LatestNewsEvent;

        public event EventHandler<CalendarStatus> CalendarStatusEvent;

        public event EventHandler<EmailStatus> LatestEmailsEvent;

        #endregion

        #region

        #endregion
    }
}

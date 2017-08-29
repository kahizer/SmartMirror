namespace SmartMirrorWinUniv.Services
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using DarkSky.Models;
    using DarkSky.Services;

    using SmartMirrorWinUniv.Concreates;

    public class WeatherServices
    {
        #region Fields

        private const string ApiKey = "3e8946340d7ea39ba01d9d8890329528";
        private float latitude = 0f;
        private float longitude = 0f;
        private int elapseTime = 86400000; // dayly
        private Timer timer;

        #endregion

        #region Constructor

        public WeatherServices(float lat, float log)
        {
            this.latitude = lat;
            this.longitude = log;

            this.timer = new Timer(this.UpdateWeather, null, 0, Timeout.Infinite);
        }

        #endregion

        #region Public Properties

        #endregion

        #region Public Events

        public event EventHandler<WeatherStatus> WeatherUpdateEvent;

        #endregion

        #region Public Methods


        #endregion

        #region Private Methods
        //private async Task<DarkSkyResponse> GetWeather()
        //{
        //    var client = new DarkSkyService(ApiKey);
        //    var result = await client.GetForecast(this.latitude, this.longitude).ConfigureAwait(false);
        //    return result;
        //}

        private void UpdateWeather(object state)
        {
            var weather = this.GetWeatherStatus();
            this.WeatherUpdateEvent?.Invoke(this, weather);
            this.timer.Change(this.elapseTime, Timeout.Infinite);
        }

        private WeatherStatus GetWeatherStatus()
        {
            var result = this.GetWeather();
            var weatherStatus = new WeatherStatus(result);
            return weatherStatus;
        }

        private DarkSkyResponse GetWeather()
        {
            DarkSkyResponse result = null;
            var getWeatherTask = new TaskFactory().StartNew(
                () =>
                    {
                        var client = new DarkSkyService(ApiKey);
                        result = client.GetForecast(this.latitude, this.longitude).Result;
                    });

            getWeatherTask.Wait();
            return result;
        }

        #endregion
    }
}

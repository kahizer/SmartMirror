namespace SmartMirrorWinUniv.Services
{
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

        #endregion

        #region Constructor

        public WeatherServices(float lat, float log)
        {
            this.latitude = lat;
            this.longitude = log;

            //this.GetWeatherStatus();
        }

        #endregion

        #region Public Properties

        #endregion

        #region Public Methods
        public WeatherStatus GetWeatherStatus()
        {
            var result = this.GetWeather();
            var weatherStatus = new WeatherStatus(result);
            return weatherStatus;
        }

        #endregion

        #region Private Methods
        //private async Task<DarkSkyResponse> GetWeather()
        //{
        //    var client = new DarkSkyService(ApiKey);
        //    var result = await client.GetForecast(this.latitude, this.longitude).ConfigureAwait(false);
        //    return result;
        //}

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

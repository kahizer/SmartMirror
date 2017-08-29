namespace SmartMirrorWinUniv.Services
{
    using System;
    using System.IO;
    using System.Net;
    using System.Threading;

    using SmartMirrorWinUniv.Concreates;

    public class GoogleMapsServices
    {
        #region Fields

        private string ApiKey = "AIzaSyCDqfWmGurOEuo9ISm5YmuSJe4ZZDHXZ8A";

        private string origen = "1437 Macatera Street, Hayward, CA";

        private string destination = "2080 N Loop Rd, Alameda, CA 94502";

        private string destinationFormatted;

        private string origenFormatted;

        private Timer timer;

        private int elapseTime = 30000; //every 1 minutes

        #endregion

        #region Constructor

        public GoogleMapsServices()
        {
            this.origenFormatted = this.origen.Replace(" ", "+");
            this.destinationFormatted = this.destination.Replace(" ", "+");
            this.timer = new Timer(this.UpdateTraffic, null, 0, Timeout.Infinite);
        }

        //public GoogleMapsServices(string origen, string destination)
        //{
        //    this.origen = origen;
        //    this.destination = destination;
        //    this.origenFormatted = this.origen.Replace(" ", "+");
        //    this.destinationFormatted = this.destination.Replace(" ", "+");
        //    this.GetTripInformation();
        //}

        #endregion

        #region Public Properties

        #endregion

        #region Public Events

        public event EventHandler<TrafficStatus> TrafficUpdateEvent;

        #endregion

        #region Public Methods

       

        #endregion

        #region Private Methods

        private void UpdateTraffic(object state)
        {
            var traffic = this.GetTrafficInformation();
            this.TrafficUpdateEvent?.Invoke(this, traffic);
            this.timer.Change(this.elapseTime, Timeout.Infinite);
        }

        private TrafficStatus GetTrafficInformation()
        {
            var result = this.GetTripInformation();
            var trafficInfo = new TrafficStatus(result);
            return trafficInfo;
        }

        private string GetTripInformation()
        {
            string url =
                $"https://maps.googleapis.com/maps/api/directions/json?origin={this.origenFormatted}&destination={this.destinationFormatted}&key={this.ApiKey}";


            WebRequest request = WebRequest.Create(url);

            WebResponse response = request.GetResponseAsync().Result;

            Stream data = response.GetResponseStream();

            StreamReader reader = new StreamReader(data);

            // json-formatted string from maps api
            string responseFromServer = reader.ReadToEnd();

            response.Dispose();

            return responseFromServer;
        }

        #endregion
    }
}

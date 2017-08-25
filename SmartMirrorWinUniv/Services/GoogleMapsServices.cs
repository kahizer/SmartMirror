namespace SmartMirrorWinUniv.Services
{
    using System.IO;
    using System.Net;
    using SmartMirrorWinUniv.Concreates;

    public class GoogleMapsServices
    {
        #region Fields

        private string ApiKey = "AIzaSyCDqfWmGurOEuo9ISm5YmuSJe4ZZDHXZ8A";

        private string origen = "1437 Macatera Street, Hayward, CA";

        private string destination = "2080 N Loop Rd, Alameda, CA 94502";

        private string destinationFormatted;

        private string origenFormatted;

        #endregion

        #region Constructor

        public GoogleMapsServices()
        {
            this.origenFormatted = this.origen.Replace(" ", "+");
            this.destinationFormatted = this.destination.Replace(" ", "+");
            //this.GetTripInformation();
        }

        public GoogleMapsServices(string origen, string destination)
        {
            this.origen = origen;
            this.destination = destination;
            this.origenFormatted = this.origen.Replace(" ", "+");
            this.destinationFormatted = this.destination.Replace(" ", "+");
            this.GetTripInformation();
        }

        #endregion

        #region Public Properties

        #endregion

        #region Public Methods

        public TrafficStatus GetTrafficInformation()
        {
            var result = this.GetTripInformation();
            var trafficInfo = new TrafficStatus(result);
            return trafficInfo;
        } 

        #endregion

        #region Private Methods

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

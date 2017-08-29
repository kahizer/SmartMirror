namespace SmartMirrorWinUniv.Services
{
    using System;
    using System.Text;
    using System.IO;
    using System.Net;
    using System.Threading;
    using SmartMirrorWinUniv.Concreates;

    public class QuoteServices
    {
        #region Fields

        private const string uri = "http://api.forismatic.com/api/1.0/";

        private const string post_data = "method=getQuote&format=json&key=&lang=en";

        private Timer timer;

        private int elapseTime = 600000; //every 10 mins

        #endregion

        #region Constructor

        public QuoteServices()
        {
            this.timer = new Timer(this.GetLatestQuote, null, 0, Timeout.Infinite);
        }

        private void GetLatestQuote(object state)
        {
            var quote = this.GetDailyQuote();
            this.QuoteUpdateEvent?.Invoke(this, quote);
            this.timer.Change(this.elapseTime, Timeout.Infinite);
        }

        #endregion

        #region Public Properties

        #endregion

        #region Public Events

        public event EventHandler<QuoteModel> QuoteUpdateEvent;

        #endregion

        #region Public Methods



        #endregion

        #region Private Methods

        private QuoteModel GetDailyQuote()
        {
            var result = this.GetQuote();
            var quote = new QuoteModel(result);
            return quote;
        }

        private string GetQuote()
        {
            // create a request
            var request = (HttpWebRequest)WebRequest.Create(uri);
            request.Method = "POST";

            var postBytes = Encoding.ASCII.GetBytes(post_data);
            request.ContentType = "application/x-www-form-urlencoded";
            var requestStream = request.GetRequestStreamAsync().Result;

            requestStream.Write(postBytes, 0, postBytes.Length);
            requestStream.Dispose();

            var response = (HttpWebResponse)request.GetResponseAsync().Result;
            var data = response.GetResponseStream();
            var reader = new StreamReader(data);

            // json-formatted string from maps api
            string responseFromServer = reader.ReadToEnd();
            response.Dispose();

            return responseFromServer;
        }

        #endregion
    }
}

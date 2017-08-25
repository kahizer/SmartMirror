using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMirrorWinUniv.Services
{
    using System.IO;
    using System.Net;
    using SmartMirrorWinUniv.Concreates;

    public class NewsServices
    {
        #region Fields

        private const string apiKey = "353e4b56ebeb423e862f851f06d4c5e0";

        private const string uri = "https://newsapi.org/v1/articles?source=google-news&sortBy=top&apiKey=";

        #endregion

        #region Constructor

        public NewsServices()
        {
        }

        #endregion

        #region Public Methods

        public NewsModel GetNews()
        {
            var newsRaw = this.GetLatestNews();
            var news = new NewsModel(newsRaw);
            return news;
        }

        #endregion

        #region Private Method

        private string GetLatestNews()
         {
            var request = WebRequest.Create(uri + apiKey);
            var response = request.GetResponseAsync().Result;
            var dataStream = response.GetResponseStream();
            var reader = new StreamReader(dataStream);

            var result = reader.ReadToEnd();

            reader.Dispose();
            response.Dispose();

            return result;
        }

        #endregion
    }
}

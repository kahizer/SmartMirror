using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMirrorWinUniv.Concreates
{
    public class NewsModel
    {
        #region Fields

        private DateTime retrievedTime;

        private const string apiKey = "353e4b56ebeb423e862f851f06d4c5e0";

        private const string uri = "https://newsapi.org/v1/articles?source=google-news&sortBy=top&apiKey=";

        #endregion

        #region Constructor

        public NewsModel(string jsonData)
        {
            this.retrievedTime = DateTime.Now;
            this.LatestNews = new List<NewsItem>();
            this.DeSerializeJson(jsonData);
        }

        #endregion

        #region Public Fields

        public List<NewsItem> LatestNews { get; set; }


        #endregion

        #region Private Methods

        private void DeSerializeJson(string jsonData)
        {
            dynamic jsonObject = Newtonsoft.Json.JsonConvert.DeserializeObject(jsonData);
            var articles = jsonObject.articles;
            foreach (dynamic item in articles)
            {
                var author = item.author.ToString();
                var title = item.title.ToString();
                var description = item.description.ToString();
                var publishedAt = DateTime.Parse(item.publishedAt.ToString());

                var newsItem = new NewsItem()
                                   {
                                       Author = author,
                                       Title = title,
                                       Description = description,
                                       PublishDate = publishedAt
                                   };
                this.LatestNews.Add(newsItem);
            }
        }

        //private void GetLatestNews()
        //{
        //    var request = WebRequest.Create(uri + apiKey);
        //    var response = request.GetResponse();
        //    var dataStream = response.GetResponseStream();
        //    var reader = new StreamReader(dataStream);

        //    var result = reader.ReadToEnd();
        //    this.DeSerializeJson(result);
        //    reader.Close();
        //    response.Close();

        //    // after calling service it should return the object model
        //    // all services should return object model
        //    // service should be call from main or make a service manager that will manage all calls
        //    // need to implement timers different services should be called at different times some daiy minutes in interval time
        //    // refresh every certain time the code is called
        //}

        #endregion 
    }

    public class NewsItem
    {
        public string Author { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public DateTime PublishDate { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMirrorWinUniv.Concreates
{
    public class QuoteModel
    {
        #region Fields
        #endregion

        #region Constructior

        public QuoteModel(string jsonString)
        {
            this.DeSerializeJson(jsonString);
        }

        #endregion

        #region Public Properties

        public string Author { get; set; }

        public string Quote { get; set; }

        #endregion

        #region Private Methods

        private void DeSerializeJson(string jsonData)
        {
            dynamic jsonObject = Newtonsoft.Json.JsonConvert.DeserializeObject(jsonData);
            this.Quote = $"\"{jsonObject.quoteText}\"";
            this.Author = $"- {jsonObject.quoteAuthor}";
        }

        #endregion
    }
}

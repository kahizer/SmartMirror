using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMirrorWinUniv.Concreates
{
    public class EmailStatus
    {
        #region Constructor

        public EmailStatus()
        {
            this.RetrievedTimeStamp = DateTime.Now;
            this.EmailMessages = new List<EmailMessage>();
        }

        #endregion

        #region Public Properties

        public List<EmailMessage> EmailMessages { get; set; }

        public DateTime RetrievedTimeStamp { get; set; }

        #endregion
    }

    public class EmailMessage
    {
        public string Snippet { get; set; }
        public string From { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Subject { get; set; }
    }
}

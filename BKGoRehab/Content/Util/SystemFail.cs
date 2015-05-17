using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BKGoRehab.Content.Util
{
    public class SystemFail
    {
        public string Message { get; set; }

        public bool IsError { get; set; }


        public Exception Error { get; set; }
    }
}
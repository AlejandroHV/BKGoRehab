using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BKGoRehab.Content.Util
{
    public class ApiResponse
    {

        public string Message { get; set; }

        public dynamic Data { get; set; }

        public Exception Error { get; set; }




    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using TestSegmento.BL.API.Models;

namespace TestSegmento.BL.Utils
{
    public class JsonRequestResult
    {
        public JsonRequestResult(HttpStatusCode code, object obj)
        {
            StatusCode = code;
            Payload = obj;
        }
        public HttpStatusCode StatusCode { get; set; }
        public object Payload { get; set; }
    }
}
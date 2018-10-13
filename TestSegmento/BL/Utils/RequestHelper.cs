using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using TestSegmento.BL.API.Models;

namespace TestSegmento.BL.Utils
{
    public static class RequestHelper
    {
        public static JsonRequestResult GenerateErrorResult(HttpStatusCode code, string error)
        {
            return new JsonRequestResult(code, error);
        }

        public static JsonRequestResult GenerateSuccessResult(object data)
        {
            return new JsonRequestResult(HttpStatusCode.OK, data);
        }

        public static JsonRequestResult ProcessException(Exception ex)
        {
            if (ex is VKException)
            {
                return GenerateErrorResult(HttpStatusCode.BadRequest, ((VKException)ex).Message);
            }
            else return GenerateErrorResult(HttpStatusCode.BadRequest, ex.Message);
        }
    }
}
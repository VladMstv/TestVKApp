using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestSegmento.BL.API.Models
{
    public class VKException: ApplicationException
    {
        private static string _msg;
        public VKException() { }
        public VKException(string message) : base(message)
        {
            _msg = message;
        }
        public VKException(string message, Exception inner) : base(message, inner)
        {
            _msg = message;
        }

        public override string Message
        {
            get
            {
                return _msg;
            }

        }
    }
}
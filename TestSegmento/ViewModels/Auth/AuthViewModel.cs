using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestSegmento.ViewModels.Auth
{
    public class AuthViewModel
    {
        public AuthViewModel()
        {
            Errors = new string[] { };
        }
        public string[] Errors { get; set; }
    }
}
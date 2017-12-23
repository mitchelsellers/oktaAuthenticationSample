using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.SharedAuth
{
    public class AuthSettings
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string Authority { get; set; }
        public string CallbackPath { get; set; }
    }
}

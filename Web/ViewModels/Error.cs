﻿using Newtonsoft.Json;

namespace Web.ViewModels
{
    public class Error
    {
        [JsonProperty(PropertyName = "ErrorCode")]
        public string ErrorCode { get; set; }

        [JsonProperty(PropertyName = "ErrorDescription")]
        public string ErrorDescription { get; set; }

        public Error(string errorCode, string errorDescription)
        {
            ErrorCode = errorCode;
            ErrorDescription = errorDescription;
        }
    }
}

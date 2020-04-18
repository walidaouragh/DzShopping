﻿namespace DzShopping.API.Errors
{
    public class ApiException : ApiResponse
    {
        public ApiException(int statusCode, string message = null) : base(statusCode, message)
        {
        }

        public ApiException(int statusCode, string details, string message = null) : base(statusCode, message)
        {
            Details = details;
        }

        public string Details { get; set; }
    }
}

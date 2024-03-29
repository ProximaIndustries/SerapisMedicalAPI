﻿namespace SerapisMedicalAPI.Model
{
    public class BaseResponse<TObject> where TObject : class 
    {
        public bool status { get;  set; }
        public bool isSuccesful { get;  set; }
        public string message { get;  set; }
        public TObject data { get;  set; }
        public string StatusCode { get;  set; }
    }
}
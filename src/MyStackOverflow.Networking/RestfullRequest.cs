﻿using System;
using JetBrains.Annotations;

namespace MyStackOverflow.Networking
{
    public enum RequestMethod
    {
        Get = 0,
        Post = 1
    }

    public class RestfullRequest<T> where T : new()
    {
        private readonly TimeSpan _timeoutTimeSpan = TimeSpan.FromSeconds(40);
        private readonly string _baseUrl;
        private readonly ReactiveWebService _webService;

        public RestfullRequest([NotNull] string baseUrl, [NotNull] ReactiveWebService webService)
        {
            if (baseUrl == null) throw new ArgumentNullException("baseUrl");
            if (webService == null) throw new ArgumentNullException("webService");

            _baseUrl = baseUrl;
            _webService = webService;
        }

        public string Url
        {
            get { return _baseUrl + AdditionalUrl; }
        }

        protected string AdditionalUrl { get; set; }

        public RequestMethod Method { get; protected set; }

        protected string Body { get; set; }

        public IObservable<T> Execute()
        {
            switch (Method)
            {
                case RequestMethod.Get:
                    return _webService.Get<T>(Url, _timeoutTimeSpan);
                case RequestMethod.Post:
                    return _webService.Post<T>(Url, Body, _timeoutTimeSpan);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
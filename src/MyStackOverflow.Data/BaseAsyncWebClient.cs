﻿using System;
using System.Diagnostics;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using JetBrains.Annotations;
using MySackOverflow.Networking;
using MyStackOverflow.Data.Restful;

namespace MyStackOverflow.Data
{
    public abstract class BaseAsyncWebClient
    {
        private readonly IWebCache _cache;
        protected readonly RestfulCallFactory CallFactory = new RestfulCallFactory();

        protected BaseAsyncWebClient([NotNull] IWebCache cache)
        {
            if (cache == null) throw new ArgumentNullException("cache");
            _cache = cache;
        }

        protected IObservable<T> GetDataAsync<T>(RestfullRequest<T> request) where T : new()
        {
            if (!_cache.IsCached<T>(request.Url))
            {
                Debug.WriteLine("DataProvider::Getting data:" + request.Url);
                return Observable.Create<T>(
                    observer =>
                        Scheduler.Default.Schedule(() => ExecuteRequest(request, observer))
                    );
            }
            Debug.WriteLine("DataProvider::Getting cached data:" + request.Url);
            return Observable.Create<T>(
                observer =>
                    Scheduler.Default.Schedule(() =>
                    {
                        var item = _cache.Fetch<T>(request.Url);
                        observer.OnNext(item);
                        observer.OnCompleted();
                    })
                );
        }

        private void ExecuteRequest<T>(RestfullRequest<T> request, IObserver<T> observer) where T : new()
        {
            request.Execute()
                .Subscribe(result =>
                {
                    _cache.Put(result, request.Url);
                    observer.OnNext(result);
                },
                    observer.OnError,
                    observer.OnCompleted);
        }
    }
}
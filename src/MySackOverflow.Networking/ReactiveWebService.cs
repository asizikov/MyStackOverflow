using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Text;
using JetBrains.Annotations;
using Ionic.Zlib;
using Newtonsoft.Json;

namespace MySackOverflow.Networking
{
    public class ReactiveWebService
    {
        [NotNull] private readonly Deserializer _deserializer = new Deserializer();


        [NotNull]
        public IObservable<T> Get<T>(string url, TimeSpan timeoutTimeSpan) where T : new()
        {
            return Observable.Create<T>(
                observer =>
                    Scheduler.Default.Schedule(() =>
                    {
                        var fullUrl = url;
                        var webRequest = (HttpWebRequest) WebRequest.Create(fullUrl);
                        webRequest.Method = "GET";
                        webRequest.Headers[HttpRequestHeader.AcceptEncoding] = "gzip";
                        Observable.FromAsyncPattern<WebResponse>(webRequest.BeginGetResponse, webRequest.EndGetResponse)
                            ()
                            .Timeout(timeoutTimeSpan)
                            .Take(1)
                            .Subscribe(response => HandleResponce(response, observer),
                                ex =>
                                {
                                    HandleException(ex);
                                    observer.OnError(ex);
                                    observer.OnCompleted();
                                },
                                observer.OnCompleted);
                    }
                        ));
        }

        public IObservable<T> Post<T>(string url, string body, TimeSpan timeoutTimeSpan) where T : new()
        {
            return Observable.Create<T>(
                observer =>
                    Scheduler.Default.Schedule(() =>
                    {
                        var fullUrl = url;
                        var webRequest = (HttpWebRequest) WebRequest.Create(fullUrl);
                        webRequest.Method = "POST";
                        webRequest.ContentType = "application/json;charset=utf-8";

                        var fetchRequestStream = Observable.FromAsyncPattern<Stream>(webRequest.BeginGetRequestStream,
                            webRequest.EndGetRequestStream);
                        var fetchResponse = Observable.FromAsyncPattern<WebResponse>(webRequest.BeginGetResponse,
                            webRequest.EndGetResponse);

                        Func<Stream, IObservable<HttpWebResponse>> postDataAndFetchResponse = st =>
                        {
                            using (var writer = new StreamWriter(st))
                                writer.Write(body);
                            var temp = fetchResponse().Select(
                                rp => (HttpWebResponse) rp);
                            return temp;
                        };

                        Func<HttpWebResponse, IObservable<HttpWebResponse>> fetchResult = rp =>
                        {
                            if (rp.StatusCode == HttpStatusCode.OK)
                            {
                                return Observable.Return(rp);
                            }
                            var msg = "HttpStatusCode == " + rp.StatusCode.ToString();
                            var ex = new WebException(msg);
                            return Observable.Throw<HttpWebResponse>(ex);
                        };

                        var postResponse =
                            from st in fetchRequestStream()
                            from rp in postDataAndFetchResponse(st)
                            from s in fetchResult(rp)
                            select s;


                        postResponse
                            .Timeout(timeoutTimeSpan)
                            .Take(1)
                            .Subscribe(response => HandleResponce(response, observer),
                                ex =>
                                {
                                    HandleException(ex);
                                    observer.OnError(ex);
                                    observer.OnCompleted();
                                },
                                observer.OnCompleted);
                    }
                        ));
        }

        private void HandleResponce<T>(WebResponse response, IObserver<T> observer) where T : new()
        {
            string json;
            var zip = response.Headers[HttpRequestHeader.ContentEncoding].ToLower() == "gzip";

            using (var stream = response.GetResponseStream())
            {
                using (var reader = zip
                    ? new StreamReader(new GZipStream((stream), CompressionMode.Decompress))
                    : new StreamReader(stream, Encoding.UTF8))
                {
                    json = reader.ReadToEnd();
                }
            }
            Debug.WriteLine(json);
            var result = _deserializer.Deserialize<T>(json);
// ReSharper disable once CompareNonConstrainedGenericWithNull
            if (result != null)
            {
                observer.OnNext(result);
            }
            else
            {
                observer.OnError(new JsonSerializationException("Can't deserialize the responce : " + json));
            }
        }

        private static void HandleException(Exception ignored)
        {
            if (ignored is WebException)
            {
                var ve = ignored as WebException;
                if (ve.Response != null)
                {
                    Debug.WriteLine("ReactiveWebService::exception::uir: " + ve.Response.ResponseUri);
                }
            }
            Debug.WriteLine("ReactiveWebService::exception: " + ignored);
        }
    }
}
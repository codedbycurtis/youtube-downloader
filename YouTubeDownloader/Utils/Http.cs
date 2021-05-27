using System;
using System.Net;
using System.Net.Http;

namespace YouTubeDownloader.Utils
{
    internal static class Http
    {
        private static readonly Lazy<HttpClient> LazyHttpClient = new Lazy<HttpClient>(() =>
        {
            var httpClientHandler = new HttpClientHandler();
            httpClientHandler.CookieContainer.Add(new Cookie("CONSENT", "YES+cb", "/", ".youtube.com"));
            return new HttpClient(httpClientHandler);
        });

        internal static HttpClient Client => LazyHttpClient.Value;
    }
}

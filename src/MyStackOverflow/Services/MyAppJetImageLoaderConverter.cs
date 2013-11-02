using System.IO;
using System.IO.IsolatedStorage;
using JetImageLoader;
using JetImageLoader.Cache;
using JetImageLoader.Cache.Memory.CacheImpl;
using JetImageLoader.Cache.Storage.CacheFileNameGenerators;
using JetImageLoader.Cache.Storage.CacheImpl;
using JetImageLoader.Network;

namespace MyStackOverflow.Services
{
    public class MyAppJetImageLoaderConverter : BaseJetImageLoaderConverter
    {
        protected override JetImageLoaderConfig GetJetImageLoaderConfig()
        {
            return new JetImageLoaderConfig.Builder
            {
                IsLogEnabled     = true,
                CacheMode        = CacheMode.MemoryAndStorageCache,
                DownloaderImpl   = new HttpWebRequestDownloader(),
                MemoryCacheImpl  = new WeakMemoryCache<string, Stream>(),
                StorageCacheImpl = new LimitedStorageCache(
                    IsolatedStorageFile.GetUserStoreForApplication(), 
                    "\\image_cache", new SHA1CacheFileNameGenerator(), 1024 * 1024 * 10), 
            }.Build();
        }
    }
}

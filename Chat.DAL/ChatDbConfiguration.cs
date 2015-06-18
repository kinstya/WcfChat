namespace Chat.DAL
{
    using System.Data.Entity;
    using System.Data.Entity.Core.Common;

    using EFCache;

    public class ChatDbConfiguration : DbConfiguration
    {
        public static InMemoryCache Cache { set { _cacheInstance = value; } }

        private static InMemoryCache _cacheInstance;

        public ChatDbConfiguration()
        {
            var transactionHandler = new CacheTransactionHandler(ChatDbConfiguration._cacheInstance ?? new InMemoryCache());

            AddInterceptor(transactionHandler);

            var cachingPolicy = new CachingPolicy();

            Loaded +=
                (sender, args) => args.ReplaceService<DbProviderServices>(
                    (s, _) => new CachingProviderServices(s, transactionHandler,
                                                          cachingPolicy));
        }
    }
}
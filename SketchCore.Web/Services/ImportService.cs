using HtmlAgilityPack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Polly;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Polly.Retry;

namespace SketchCore.Web.Services
{
    public class ImportService : IImportService
    {
        private readonly CloudQueue _queue;
        private readonly Policy _policy;
        private readonly Scraper _scraper = new Scraper();
        private readonly ILogger<ImportService> _logger;

        public ImportService(IConfiguration configuration, ILogger<ImportService> logger)
        {
            _logger = logger;

            var storageAccount = CloudStorageAccount.Parse(configuration.GetConnectionString("Storage"));
            var queueClient = storageAccount.CreateCloudQueueClient();
            _queue = queueClient.GetQueueReference("deepimport");
            _policy = Policy
                .Handle<StorageException>(x => x.RequestInformation.ExtendedErrorInformation?.ErrorCode == "QueueNotFound")
                .RetryAsync((exception, retryCount) =>
                {
                    _logger.LogInformation($"Creating queue [{_queue.Name}]");
                    return _queue.CreateAsync();
                });
        }
        public async Task DeepImport(string pageUrl)
        {
            if(!Uri.IsWellFormedUriString(pageUrl, UriKind.Absolute))
            {
                _logger.LogError($"Invalid uri string: [{pageUrl}]");
                throw new InvalidOperationException($"Invalid uri string: [{pageUrl}]");
            }
            var rssFeeUrl = _scraper.GetRssFeedUrl(pageUrl);
            if(rssFeeUrl == null)
            {
                _logger.LogWarning($"RSS feed url not found: [{pageUrl}]");
                return;
            }
            await _policy.ExecuteAsync(() => _queue.AddMessageAsync(new CloudQueueMessage(rssFeeUrl)));
        }

        public class Scraper
        {
            private readonly HtmlWeb _web = new HtmlWeb
            {
                UserAgent = new ProductInfoHeaderValue("Mozilla", "5.0").ToString()
            };
            public string GetRssFeedUrl(string pageUrl)
            {
                var document = _web.Load(pageUrl);
                var navigator = document.CreateNavigator();
                var node = navigator.SelectSingleNode("//link[@rel='alternate'][@type='application/rss+xml']");
                if (node == null) return null;
                return Uri.UnescapeDataString(node.GetAttribute("href", "")).Replace("&amp;", "&");
            }
        }
    }
}

using System;
using System.Diagnostics;
using System.Linq;
using AutoMapper;
using Sketch.Core.Commands;
using Sketch.Core.Infrastructure;
using Sketch.Core.ReadModel;
using Sketch.StockPhotoImporter.Scraping;
using Sketch.StockPhotoImporter.Syndication;

namespace Sketch.StockPhotoImporter
{
    internal class Importer
    {
        readonly ICommandBus _commandBus;
        readonly IStockPhotoDao _dao;
        readonly IClock _clock;

        public Importer(ICommandBus commandBus, IStockPhotoDao dao, IClock clock )
        {
            _commandBus = commandBus;
            _dao = dao;
            _clock = clock;
        }

        public string Url { get; set; }

        public void Start()
        {
            Trace.TraceInformation("Starting import of: " + Url);
            var scraper = new Scraper(Url);
            var rssFeedUrl = scraper.GetRssFeedUrl();
            if (rssFeedUrl != null)
            {
                var feed = new Feed(rssFeedUrl);
                foreach (var item in feed.Where(x => x.HasContent))
                {
                    if (!_dao.Exists(uniqueId: item.Guid))
                    {
                        var command = Mapper.Map<ImportStockPhoto>(item);
                        command.StockPhotoId = Guid.NewGuid();
                        _commandBus.Send(command);
                    }
                }
            }
        }
    }
}
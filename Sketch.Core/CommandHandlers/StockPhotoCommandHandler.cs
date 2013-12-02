using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Sketch.Core.Commands;
using Sketch.Core.Domain;
using Sketch.Core.Infrastructure;
using System.Xml.Linq;

namespace Sketch.Core.CommandHandlers
{
    public class StockPhotoCommandHandler: ICommandHandler<ImportStockPhoto>
    {
        private readonly IEventStore _store;
        public StockPhotoCommandHandler(IEventStore store)
        {
            _store = store;
        }
        public async void Handle(ImportStockPhoto command)
        {
            var stockPhoto = new StockPhoto(command.StockPhotoId, command.UniqueId,
                                            command.PageUrl, command.Category,
                                            command.Rating, command.PublishedDate);

            var client = new HttpClient();
            var result = client
                .GetStringAsync("http://backend.deviantart.com/oembed?format=xml&url=" + Uri.EscapeDataString(command.PageUrl));

            Task.WaitAll(result);

            var root = XDocument.Parse(result.Result).Root;
            var ns = XNamespace.Get("http://www.deviantart.com/difi/");

            var oEmbed = new OEmbedInfo();
            oEmbed.Version = (string)root.Element(ns.GetName("version"));
            oEmbed.Type = (string)root.Element(ns.GetName("type"));
            oEmbed.Title = (string)root.Element(ns.GetName("title"));
            oEmbed.Url = (string)root.Element(ns.GetName("url"));
            oEmbed.AuthorName = (string)root.Element(ns.GetName("author_name"));
            oEmbed.AuthorUrl = (string)root.Element(ns.GetName("author_url"));
            oEmbed.ProviderName = (string)root.Element(ns.GetName("provider_name"));
            oEmbed.ProviderUrl = (string)root.Element(ns.GetName("provider_url"));
            oEmbed.ThumbnailUrl = (string)root.Element(ns.GetName("thumbnail_url"));
            oEmbed.ThumbnailWidth = (int)root.Element(ns.GetName("thumbnail_width"));
            oEmbed.ThumbnailHeight = (int)root.Element(ns.GetName("thumbnail_height"));
            oEmbed.Width = (int)root.Element(ns.GetName("width"));
            oEmbed.Height = (int)root.Element(ns.GetName("height"));

            stockPhoto.UpdateOEmbedInfo(oEmbed);

            _store.Save(stockPhoto, command.Id.ToString());
        }
    }
}

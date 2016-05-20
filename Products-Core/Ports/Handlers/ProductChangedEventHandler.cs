using System;
using System.IO;
using Grean.AtomEventStore;
using Microsoft.Practices.Unity;
using paramore.brighter.commandprocessor;
using paramore.brighter.commandprocessor.Logging;
using Products_Core.Adapters.Atom;
using Products_Core.Ports.Events;
using Product_Service;

namespace Products_Core.Ports.Handlers
{
    public class ProductChangedEventHandler : RequestHandler<ProductChangedEvent>
    {
        private readonly IObserver<ProductEntry> _observer;

        public ProductChangedEventHandler(IObserver<ProductEntry> observer, ILog logger) : base(logger)
        {
            _observer = observer;
        }

        [InjectionConstructor]
        public ProductChangedEventHandler(ILog logger) : base(logger)
        {
            var storage = new AtomEventsInFiles(new DirectoryInfo(Globals.StoragePath));
            var serializer = new DataContractContentSerializer(
                DataContractContentSerializer
                    .CreateTypeResolver(typeof (ProductEntry).Assembly)
                );

            _observer= new AtomEventObserver<ProductEntry>(
                Globals.EventStreamId,
                25,
                storage,
                serializer
                );
        }

        public override ProductChangedEvent Handle(ProductChangedEvent productChangedEvent)
        {
            _observer.OnNext(new ProductEntry(
                type: ProductEntryType.Updated,
                productId: productChangedEvent.ProductId,
                productDescription: productChangedEvent.ProductDescription, 
                productName: productChangedEvent.ProductName, 
                productPrice: productChangedEvent.ProductPrice));

            return base.Handle(productChangedEvent);
        }
    }
}
